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

using System.Collections.Generic;

#endregion

namespace GininDev.Common.DataTools.Helpers.TemplateTool.Parser.AST
{


	public class Tag : Element
	{
		string name;
		List<TagAttribute> attribs;
		List<Element> innerElements;
		TagClose closeTag;
		bool isClosed;	// set to true if tag ends with />

		public Tag(int line, int col, string name)
			:base(line, col)
		{
			this.name = name;
			this.attribs = new List<TagAttribute>();
			this.innerElements = new List<Element>();
		}

		public List<TagAttribute> Attributes
		{
			get { return this.attribs; }
		}

		public Expression AttributeValue(string name)
		{
			foreach (TagAttribute attrib in attribs)
				if (string.Compare(attrib.Name, name, true) == 0)
					return attrib.Expression;

			return null;
		}

		public List<Element> InnerElements
		{
			get { return this.innerElements; }
		}

		public string Name
		{
			get { return this.name; }
		}

		public TagClose CloseTag
		{
			get { return this.closeTag; }
			set { this.closeTag = value; }
		}

		public bool IsClosed
		{
			get { return this.isClosed; }
			set { this.isClosed = value; }
		}


	}
}
