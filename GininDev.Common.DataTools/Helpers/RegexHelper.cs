using System.Text.RegularExpressions;

namespace GininDev.Common.DataTools.Helpers
{
    public class RegexHelper
    {
        public static RegexOptions Regopts = RegexOptions.Multiline
                                             | RegexOptions.RightToLeft
                                             | RegexOptions.CultureInvariant
                                             | RegexOptions.Compiled;

        public static Regex RegxXml =
            new Regex(
                "(<!--.*?-->)|(\\b(?<=(<|</|<\\?)\\s*]?)(:|_|-|\\.|[a-z]|[A-Z]|[0-9])+\\b)",
                Regopts
                );
    }
}