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
using GininDev.Common.DataTools.Helpers.TemplateTool.Parser;
using GininDev.Common.DataTools.Helpers.TemplateTool.Parser.AST;

#endregion

namespace GininDev.Common.DataTools.Helpers.TemplateTool
{
	public class Template
	{
		string name;
		List<Element> elements;
		Template parent;

		Dictionary<string, Template> templates;

		public Template(string name, List<Element> elements)
		{
			this.name = name;
			this.elements = elements;
			this.parent = null;

			InitTemplates();
		}

		public Template(string name, List<Element> elements, Template parent)
		{
			this.name = name;
			this.elements = elements;
			this.parent = parent;

			InitTemplates();
		}

		/// <summary>
		/// load template from file
		/// </summary>
		/// <param name="name">name of template</param>
		/// <param name="filename">file from which to load template</param>
		/// <returns></returns>
		public static Template FromFile(string name, string filename)
		{
			using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
			{
				string data = reader.ReadToEnd();
				return Template.FromString(name, data);
			}
		}

		/// <summary>
		/// load template from string
		/// </summary>
		/// <param name="name">name of template</param>
		/// <param name="data">string containg code for template</param>
		/// <returns></returns>
		public static Template FromString(string name, string data)
		{
			TemplateLexer lexer = new TemplateLexer(data);
			TemplateParser parser = new TemplateParser(lexer);
			List<Element> elems = parser.Parse();

			TagParser tagParser = new TagParser(elems);
			elems = tagParser.CreateHierarchy();

			return new Template(name, elems);
		}

		/// <summary>
		/// go thru all tags and see if they are template tags and add
		/// them to this.templates collection
		/// </summary>
		private void InitTemplates()
		{
			this.templates = new Dictionary<string, Template>(StringComparer.InvariantCultureIgnoreCase);

			foreach (Element elem in elements)
			{
				if (elem is Tag)
				{
					Tag tag = (Tag)elem;
					if (string.Compare(tag.Name, "template", true) == 0)
					{
						Expression ename = tag.AttributeValue("name");
						string tname;
						if (ename is StringLiteral)
							tname = ((StringLiteral)ename).Content;
						else
							tname = "?";

						Template template = new Template(tname, tag.InnerElements, this);
						templates[tname] = template;
					}
				}
			}
		}

		/// <summary>
		/// gets a list of elements for this template
		/// </summary>
		public List<Element> Elements
		{
			get { return this.elements; }
		}

		/// <summary>
		/// gets the name of this template
		/// </summary>
		public string Name
		{
			get { return this.name; }
		}

		/// <summary>
		/// returns true if this template has parent template
		/// </summary>
		public bool HasParent
		{
			get { return parent != null; }
		}

		/// <summary>
		/// gets parent template of this template
		/// </summary>
		/// <value></value>
		public Template Parent
		{
			get { return this.parent; }
		}

		/// <summary>
		/// finds template matching name. If this template does not
		/// contain template called name, and parent != null then
		/// FindTemplate is called on the parent
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual Template FindTemplate(string name)
		{
			if (templates.ContainsKey(name))
				return templates[name];
			else if (parent != null)
				return parent.FindTemplate(name);
			else
				return null;
		}

		/// <summary>
		/// gets dictionary of templates defined in this template
		/// </summary>
		public System.Collections.Generic.Dictionary<string, Template> Templates
		{
			get { return this.templates; }
		}
	}
}
