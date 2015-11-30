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

using System.Collections.Generic;
using GininDev.Common.DataTools.Helpers.TemplateTool.Parser.AST;

#endregion

namespace GininDev.Common.DataTools.Helpers.TemplateTool.Parser
{
	public class TagParser
	{
		List<Element> elements;

		public TagParser(List<Element> elements)
		{
			this.elements = elements;
		}

		public List<Element> CreateHierarchy()
		{
			List<Element> result = new List<Element>();

			for (int index=0; index<elements.Count; index++)
			{
				Element elem = elements[index];

				if (elem is Text)
					result.Add(elem);
				else if (elem is Expression)
					result.Add(elem);
				else if (elem is Tag)
				{
					result.Add(CollectForTag((Tag)elem, ref index));
				}
				else if (elem is TagClose)
				{
					throw new ParseException("Close tag for " + ((TagClose)elem).Name + " doesn't have matching start tag.", elem.Line, elem.Col);
				}
				else
					throw new ParseException("Invalid element: " + elem.GetType().ToString(), elem.Line, elem.Col);
			}

			return result;
		}

		private Tag CollectForTag(Tag tag, ref int index)
		{
			if (tag.IsClosed) // if self-closing tag, do not collect inner elements
			{
				return tag;
			}

			if (string.Compare(tag.Name, "if", true) == 0)
			{
				tag = new TagIf(tag.Line, tag.Col, tag.AttributeValue("test"));
			}

			Tag collectTag = tag;

			for (index++; index < elements.Count; index++)
			{
				Element elem = elements[index];

				if (elem is Text)
					collectTag.InnerElements.Add(elem);
				else if (elem is Expression)
					collectTag.InnerElements.Add(elem);
				else if (elem is Tag)
				{
					Tag innerTag = (Tag)elem;
					if (string.Compare(innerTag.Name, "else", true) == 0)
					{
						if (collectTag is TagIf)
						{
							((TagIf)collectTag).FalseBranch = innerTag;
							collectTag = innerTag;							
						}
						else
							throw new ParseException("else tag has to be positioned inside of if or elseif tag", innerTag.Line, innerTag.Col);

					}
					else if (string.Compare(innerTag.Name, "elseif", true) == 0)
					{
						if (collectTag is TagIf)
						{
							Tag newTag = new TagIf(innerTag.Line, innerTag.Col, innerTag.AttributeValue("test"));
							((TagIf)collectTag).FalseBranch = newTag;
							collectTag = newTag;
						}
						else
							throw new ParseException("elseif tag is not positioned properly", innerTag.Line, innerTag.Col);
					}
					else
						collectTag.InnerElements.Add(CollectForTag(innerTag, ref index));
				}
				else if (elem is TagClose)
				{
					TagClose tagClose = (TagClose)elem;
					if (string.Compare(tag.Name, tagClose.Name, true) == 0)
						return tag;

					throw new ParseException("Close tag for " + tagClose.Name + " doesn't have matching start tag.", elem.Line, elem.Col);
				}
				else
					throw new ParseException("Invalid element: " + elem.GetType().ToString(), elem.Line, elem.Col);

			}

			throw new ParseException("Start tag: " + tag.Name + " does not have matching end tag.", tag.Line, tag.Col);

		}

	}
}
