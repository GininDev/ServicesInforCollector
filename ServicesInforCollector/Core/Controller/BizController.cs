using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GininDev.Common.DataTools.DynLoader;
using ServicesInforCollector.Core.Base;
using ServicesInforCollector.Core.Entities;

namespace ServicesInforCollector.Core.Controller
{
    public class BizController : ControllerBase<Bizs>
    {
        private static BizController _instance;

        private static List<Bizs> _arFacts;

        private BizController()
        {
            InitFacts();
        }

        private static void InitFacts()
        {
            _arFacts = DataFactory.Instance.LoadObjects<Bizs>("1=1");
        }

        public static BizController Instance
        {
            get { return _instance ?? (_instance = new BizController()); }
        }

        public void RefreshData()
        {
            InitFacts();
        }

        public List<Bizs> GetFactses()
        {
            return _arFacts;
        }


    }
}
