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
using GininDev.Common.DataTools.Helpers.TemplateTool.Parser.AST;

#endregion

namespace GininDev.Common.DataTools.Helpers.TemplateTool.Parser
{
	public class TemplateParser
	{
		TemplateLexer lexer;
		Token current;
		List<Element> elements;

		public TemplateParser(TemplateLexer lexer)
		{
			this.lexer = lexer;
			this.elements = new List<Element>();
		}

		Token Consume()
		{
			Token old = current;
			current = lexer.Next();
			return old;
		}

		Token Consume(TokenKind kind)
		{
			Token old = current;
			current = lexer.Next();

			if (old.TokenKind != kind)
				throw new ParseException("Unexpected token: " + current.TokenKind.ToString() + ". Was expecting: " + kind , current.Line, current.Col);

			return old;
		}

		Token Current
		{
			get { return current; }
		}

		public List<Element> Parse()
		{
			elements.Clear();

			Consume();

			while (true)
			{
				Element elem = ReadElement();
				if (elem == null)
					break;
				else
					elements.Add(elem);
			}
			return elements;
		}

		Element ReadElement()
		{
			switch (Current.TokenKind)
			{
				case TokenKind.EOF:
					return null;
				case TokenKind.TagStart:
					return ReadTag();
				case TokenKind.TagClose:
					return ReadCloseTag();
				case TokenKind.ExpStart:
					return ReadExpression();
				case TokenKind.TextData:
					Text text = new Text(Current.Line, Current.Col, Current.Data);
					Consume();
					return text;
				default:
					throw new ParseException("Invalid token: " + Current.TokenKind.ToString(), Current.Line, Current.Col);
			}
		}

		TagClose ReadCloseTag()
		{
			Consume(TokenKind.TagClose);
			Token idToken = Consume(TokenKind.ID);
			Consume(TokenKind.TagEnd);

			return new TagClose(idToken.Line, idToken.Col, idToken.Data);
		}

		Expression ReadExpression()
		{
			Consume(TokenKind.ExpStart);

			Expression exp = PrimaryExpression();

			Consume(TokenKind.ExpEnd);

			return exp;
		}

		Tag ReadTag()
		{
			Consume(TokenKind.TagStart);
			Token name = Consume(TokenKind.ID);
			Tag tag = new Tag(name.Line, name.Col, name.Data);

			while (true)
			{
				if (Current.TokenKind == TokenKind.ID)
					tag.Attributes.Add(ReadAttribute());
				else if (Current.TokenKind == TokenKind.TagEnd)
				{
					Consume();
					break;
				}
				else if (Current.TokenKind == TokenKind.TagEndClose)
				{
					Consume();
					tag.IsClosed = true;
					break;
				}
				else
					throw new ParseException("Invalid token in tag: " + Current.TokenKind, Current.Line, Current.Col);

			}

			return tag;

		}

		TagAttribute ReadAttribute()
		{
			Token name = Consume(TokenKind.ID);
			Consume(TokenKind.TagEquals);

			Expression exp = null;

			if (Current.TokenKind == TokenKind.StringStart)
				exp = ReadString();
			else
				throw new ParseException("Unexpected token: " + Current.TokenKind + ". Was expection '\"'", Current.Line, Current.Col);

			return new TagAttribute(name.Data, exp);
		}

		Expression ReadString()
		{
			Token start = Consume(TokenKind.StringStart);
			StringExpression exp = new StringExpression(start.Line, start.Col);

			while (true)
			{
				Token tok = Current;

				if (tok.TokenKind == TokenKind.StringEnd)
				{
					Consume();
					break;
				}
				else if (tok.TokenKind == TokenKind.EOF)
					throw new ParseException("Unexpected end of file", tok.Line, tok.Col);
				else if (tok.TokenKind == TokenKind.StringText)
				{
					Consume();
					exp.Add(new StringLiteral(tok.Line, tok.Col, tok.Data));
				}
				else if (tok.TokenKind == TokenKind.ExpStart)
					exp.Add(ReadExpression());
				else
					throw new ParseException("Unexpected token in string: " + tok.TokenKind, tok.Line, tok.Col);
			}

			if (exp.ExpCount == 1)
				return exp[0];
			else
				return exp;
		}

		Expression PrimaryExpression()
		{
			if (Current.TokenKind == TokenKind.StringStart)
				return ReadString();
			else if (Current.TokenKind == TokenKind.ID)
			{
				Token id = Consume();
				if (Current.TokenKind == TokenKind.LParen)
				{
					Consume();	// consume LParen
					Expression[] args = ReadArguments();
					Consume(TokenKind.RParen);

					return new FCall(id.Line, id.Col, id.Data, args);
				}
				else if (Current.TokenKind == TokenKind.Dot)
				{
					Consume();
					Token field = Consume(TokenKind.ID);
					return new FieldAccess(id.Line, id.Col, id.Data, field.Data);
				}
				else
					return new Name(id.Line, id.Col, id.Data);
			}
			else if (Current.TokenKind == TokenKind.Integer)
			{
				int value = Int32.Parse(Current.Data);
				IntLiteral intLiteral = new IntLiteral(Current.Line, Current.Col, value);
				Consume(); // consume int
				return intLiteral;
			}
			else
				throw new ParseException("Invalid token in expression: " + Current.TokenKind + ". Was expecting ID or string.", Current.Line, Current.Col);

		}

		private Expression[] ReadArguments()
		{
			List<Expression> exps = new List<Expression>();

			int index = 0;
			while (true)
			{
				if (Current.TokenKind == TokenKind.RParen)
					break;

				if (index > 0)
					Consume(TokenKind.Comma);

				exps.Add(PrimaryExpression());

				index++;
			}

			return exps.ToArray();
		}
	}
}
