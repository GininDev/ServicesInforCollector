using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;

namespace ServicesInforCollector.DataProviders.TestFixture
{
    [TestFixture]
    public class SqliteHelperTest
    {
        private string _connString = null;
        [SetUp]
        public void InitEnv()
        {
            _connString = @"Data Source=Datas\Devices\SzConfig.db;Version=3;";
        }

        [Test]
        public void Test01()
        {
            var sSql = "Select * from Dicts where 1=1 and IsDeleted = '0'";

            var aHelper = new SqLiteHelper();
            var dsr = aHelper.ExecuteDataSet(_connString, sSql, CommandType.Text);

            object x = "";
            Console.WriteLine(dsr.Tables.Count);

        }
    }
}
