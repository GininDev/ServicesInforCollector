﻿/*****************************************************
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

namespace GininDev.Common.DataTools.Helpers.TemplateTool.Parser.AST
{
	public class Element
	{
		int line;
		int col;

		public Element(int line, int col)
		{
			this.line = line;
			this.col = col;
		}

		public int Col
		{
			get { return this.col; }
		}

		public int Line
		{
			get { return this.line; }
		}
	}
}
