using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GininDev.Common.DataTools.DynLoader;
using ServicesInforCollector.Core.Base;
using ServicesInforCollector.Core.Entities;

namespace ServicesInforCollector.Core.Controller
{
    public class FactController : ControllerBase<Facts>
    {
        private static FactController _instance;

        private static List<Facts> _arFacts;

        private FactController()
        {
            InitFacts();
        }

        private static void InitFacts()
        {
            _arFacts = DataFactory.Instance.LoadObjects<Facts>("1=1");
        }

        public static FactController Instance
        {
            get { return _instance ?? (_instance = new FactController()); }
        }

        public void RefreshData()
        {
            InitFacts();
        }

        public List<Facts> GetFactses()
        {
            return _arFacts;
        }


    }
}
