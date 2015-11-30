using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GininDev.Common.DataTools.Helpers;
using NUnit.Framework;

namespace GininDev.Common.DataTools.TestFixture
{
    [TestFixture]
    public class TemplateHelperTest
    {
        [Test]
        public void Test01()
        {
            object obj = null;
            string sTplFile = "#obj.Number#";
            var mngr = TemplateHelper.FromString(sTplFile);
            mngr.SetValue("STP", obj);
            var writer = new System.IO.StringWriter();
            mngr.Process(writer);
            var sMsgContent = writer.ToString();
        }
    }
}
