/*****************************************************
 * AderTemplates
 * (C) Andrew Deren 2004
 * http://www.adersoftware.com
 *
 *	This file is part of AderTemplate
 * AderTemplate is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * AderTemplate is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Foobar; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *****************************************************/

#region Using directives

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace GininDev.Common.DataTools.Helpers.TemplateTool.Parser
{
	public class TemplateLexer
	{
		enum LexMode
		{
			Text,
			Tag,
			Expression,
			String
		}

		const char EOF = (char)0;

		LexMode currentMode;
		Stack<LexMode> modes;

		int line;
		int column;
		int pos;	// position within data

		string data;

		int saveLine;
		int saveCol;
		int savePos;

		public TemplateLexer(TextReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader");

			data = reader.ReadToEnd();

			Reset();
		}

		public TemplateLexer(string data)
		{
			if (data == null)
				throw new ArgumentNullException("data");

			this.data = data;

			Reset();
		}

		private void EnterMode(LexMode mode)
		{
			modes.Push(currentMode);
			currentMode = mode;
		}

		private void LeaveMode()
		{
			currentMode = modes.Pop();
		}

		private void Reset()
		{
			modes = new Stack<LexMode>();
			currentMode = LexMode.Text;
			modes.Push(currentMode);

			line = 1;
			column = 1;
			pos = 0;
		}

		protected char LA(int count)
		{
			if (pos + count >= data.Length)
				return EOF;
			else
				return data[pos + count];
		}

		protected char Consume()
		{
			char ret = data[pos];
			pos++;
			column++;

			return ret;
		}

		protected char Consume(int count)
		{
			if (count <= 0)
				throw new ArgumentOutOfRangeException("count", "count has to be greater than 0");

			char ret = ' ';
			while (count > 0)
			{
				ret = Consume();
				count--;
			}
			return ret;
		}

		void NewLine()
		{
			line++;
			column = 1;
		}

		protected Token CreateToken(TokenKind kind, string value)
		{
			return new Token(kind, value, line, column);
		}

		protected Token CreateToken(TokenKind kind)
		{
			string tokenData = data.Substring(savePos, pos - savePos);
			if (kind == TokenKind.StringText)
				tokenData = tokenData.Replace("\"\"", "\""); // replace double "" with single "
			if (kind == TokenKind.StringText || kind == TokenKind.TextData)
				tokenData = tokenData.Replace("##", "#");	// replace ## with #

			return new Token(kind, tokenData, saveLine, saveCol);
		}

		/// <summary>
		/// reads all whitespace characters (does not include newline)
		/// </summary>
		/// <returns></returns>
		protected void ReadWhitespace()
		{
			while (true)
			{
				char ch = LA(0);
				switch (ch)
				{
					case ' ':
					case '\t':
						Consume();
						break;
					case '\n':
						Consume();
						NewLine();
						break;

					case '\r':
						Consume();
						if (LA(0) == '\n')
							Consume();
						NewLine();
						break;
					default:
						return;
				}
			}
		}

		/// <summary>
		/// save read point positions so that CreateToken can use those
		/// </summary>
		private void StartRead()
		{
			saveLine = line;
			saveCol = column;
			savePos = pos;
		}

		public Token Next()
		{
			switch (currentMode)
			{
				case LexMode.Text: return NextText();
				case LexMode.Expression: return NextExpression();
				case LexMode.Tag: return NextTag();
				case LexMode.String: return NextString();
				default: throw new ParseException("Encountered invalid lexer mode: " + currentMode.ToString(), line, column);
			}
		}

		private Token NextExpression()
		{
			StartRead();
			char ch = LA(0);
			switch (ch)
			{
				case EOF:
					return CreateToken(TokenKind.EOF);
				case ',':
					Consume();
					return CreateToken(TokenKind.Comma);
				case '.':
					Consume();
					return CreateToken(TokenKind.Dot);
				case '(':
					Consume();
					return CreateToken(TokenKind.LParen);
				case ')':
					Consume();
					return CreateToken(TokenKind.RParen);
				case '#':
					Consume();
					LeaveMode();
					return CreateToken(TokenKind.ExpEnd);
				case ' ':
				case '\t':
				case '\r':
				case '\n':
					ReadWhitespace();
					return NextExpression();

				case '"':
					Consume();
					EnterMode(LexMode.String);
					return CreateToken(TokenKind.StringStart);

				case '0': case '1': case '2':
				case '3': case '4': case '5':
				case '6': case '7': case '8':
				case '9':
					return ReadNumber();

				default:
					if (Char.IsLetter(ch) || ch == '_')
						return ReadId();
					else
						throw new ParseException("Invalid character in expression: " + ch, line, column);

			}
		}

		private Token NextTag()
		{
			StartRead();
			StartTagRead:
			char ch = LA(0);
			switch (ch)
			{
				case EOF:
					return CreateToken(TokenKind.EOF);
				case '=':
					Consume();
					return CreateToken(TokenKind.TagEquals);
				case '"':
					Consume();
					EnterMode(LexMode.String);
					return CreateToken(TokenKind.StringStart);
				case ' ':
				case '\t':
				case '\r':
				case '\n':
					ReadWhitespace();	// ignore whitespace
					StartRead();		// remark current position
					goto StartTagRead;	// start again
				case '>':
					Consume();
					LeaveMode();
					return CreateToken(TokenKind.TagEnd);
				case '/':
					if (LA(1) == '>')
					{
						Consume(2); // consume />
						LeaveMode();
						return CreateToken(TokenKind.TagEndClose);
					}
					break;
				default:
					if (Char.IsLetter(ch) || ch == '_')
						return ReadId();
					break;

			}
			throw new ParseException("Invalid character in tag: " + ch, line, column);
		}

		private Token NextString()
		{
			StartRead();
			StartStringRead:
			char ch = LA(0);
			switch (ch)
			{
				case EOF:
					return CreateToken(TokenKind.EOF);

				case '#':
					if (LA(1) == '#') // just escape
					{
						Consume(2);
						goto StartStringRead;
					}
					else if (savePos == pos)
					{
						Consume();
						EnterMode(LexMode.Expression);
						return CreateToken(TokenKind.ExpStart);
					}
					else
						break; // just break and we will return the text token

				case '\r':
				case '\n':
					ReadWhitespace();
					goto StartStringRead;
				case '"':
					if (LA(1) == '"')
					{
						// just escape
						Consume(2);
						goto StartStringRead;
					}
					else if (pos == savePos)
					{
						Consume();
						LeaveMode();
						return CreateToken(TokenKind.StringEnd);
					}
					else
						break; // just break so that text is returned
				default:
					Consume();
					goto StartStringRead;

			}

			return CreateToken(TokenKind.StringText);
		}

		private Token NextText()
		{
			StartRead();

			StartTextRead:
			switch (LA(0))
			{
				case EOF:
					if (savePos == pos)
						return CreateToken(TokenKind.EOF);
					else
						break;

				case '#':
					if (LA(1) == '#') // # was just escape
					{
						Consume(2); // consume both #
						goto StartTextRead;
					}
					else if (savePos == pos)
					{
						Consume();
						EnterMode(LexMode.Expression);
						return CreateToken(TokenKind.ExpStart);
					}
					else
						break; // even if we have exp, we break because we read some characters that need to be returned as text

				case '<':
					if (LA(1) == 'a' && LA(2) == 'd' && LA(3) == ':')
					{
						if (savePos == pos)
						{
							Consume(4); // consume <ad:
							EnterMode(LexMode.Tag);
							return CreateToken(TokenKind.TagStart);
						}
						else
							break;
					}
					else if (LA(1) == '/' && LA(2) == 'a' && LA(3) == 'd' && LA(4) == ':')
					{
						if (savePos == pos)
						{
							Consume(5); // consume </ad:
							EnterMode(LexMode.Tag);
							return CreateToken(TokenKind.TagClose);
						}
						else
							break;
					}
					Consume();
					goto StartTextRead;
				case '\n': 
				case '\r':
					ReadWhitespace();	// handle newlines specially so that line number count is kept
					goto StartTextRead;	

				default:
					Consume();
					goto StartTextRead;
			}

			return CreateToken(TokenKind.TextData);
		}

		/// <summary>
		/// reads word. Word contains any alpha character or _
		/// </summary>
		protected Token ReadId()
		{
			StartRead();

			Consume(); // consume first character of the word

			while (true)
			{
				char ch = LA(0);
				if (Char.IsLetterOrDigit(ch) || ch == '_')
					Consume();
				else
					break;
			}

			return CreateToken(TokenKind.ID);
		}

		protected Token ReadNumber()
		{
			StartRead();
			Consume(); // consume first digit

			while (true)
			{
				char ch = LA(0);
				if (Char.IsNumber(ch))
					Consume();
				else
					break;
			}

			return CreateToken(TokenKind.Integer);
		}

	}
}
