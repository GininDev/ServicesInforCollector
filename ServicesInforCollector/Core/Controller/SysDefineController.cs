using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GininDev.Common.DataTools.DynLoader;
using ServicesInforCollector.Core.Base;
using ServicesInforCollector.Core.Entities;

namespace ServicesInforCollector.Core.Controller
{
    public class SysDefineController : ControllerBase<SysDefines>
    {
        private static SysDefineController _instance;

        private static List<SysDefines> _arFacts;

        private SysDefineController()
        {
            InitFacts();
        }

        private static void InitFacts()
        {
            _arFacts = DataFactory.Instance.LoadObjects<SysDefines>("1=1");
        }

        public static SysDefineController Instance
        {
            get { return _instance ?? (_instance = new SysDefineController()); }
        }

        public void RefreshData()
        {
            InitFacts();
        }

        public List<SysDefines> GetFactses()
        {
            return _arFacts;
        }


    }
}
