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



#endregion

namespace GininDev.Common.DataTools.Helpers.TemplateTool.Parser
{
	public enum TokenKind
	{
		EOF,
		Comment,
		// common tokens
		ID,				// (alpha)+

		// text specific tokens
		TextData,

		// tag tokens
		TagStart,		// <ad: 
		TagEnd,			// > 
		TagEndClose,	// />
		TagClose,		// </ad:
		TagEquals,		// =


		// expression
		ExpStart,		// # at the beginning
		ExpEnd,			// # at the end
		LParen,			// (
		RParen,			// )
		Dot,			// .
		Comma,			// ,
		Integer,		// integer number

		// string tokens
		StringStart,	// "
		StringEnd,		// "
		StringText		// text within the string
	}

	public class Token
	{
		int line;
		int col;
		string data;
		TokenKind tokenKind;

		public Token(TokenKind kind, string data, int line, int col)
		{
			this.tokenKind = kind;
			this.line = line;
			this.col = col;
			this.data = data;
		}

		public int Col
		{
			get { return this.col; }
		}

		public string Data
		{
			get { return this.data; }
			set { this.data = value; }
		}

		public int Line
		{
			get { return this.line; }
		}

		public TokenKind TokenKind
		{
			get { return this.tokenKind; }
			set { this.tokenKind = value; }
		}
	}
}
