using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GininDev.Common.DataTools.DynLoader;
using ServicesInforCollector.Core.Base;
using ServicesInforCollector.Core.Entities;

namespace ServicesInforCollector.Core.Controller
{
    public class DictController : ControllerBase<Dicts>
    {
        private static DictController _instance;

        private static Dictionary<string, string> _arDics;

        private DictController()
        {
            InitDicts();
        }

        private static void InitDicts()
        {
            var arP = DataFactory.Instance.LoadObjects<Dicts>("1=1");
            if (arP != null)
                foreach (var para in arP)
                {
                    if (_arDics == null) _arDics = new Dictionary<string, string>();
                    if (!_arDics.ContainsKey(para.dName))
                        _arDics.Add(para.dName, para.dValue);
                    else
                        _arDics[para.dName] = para.dValue;
                }
        }

        public static DictController Instance
        {
            get { return _instance ?? (_instance = new DictController()); }
        }

        public void RefreshData()
        {
            InitDicts();
        }

        public string GetPara(string sParName)
        {
            return GetPara(sParName, string.Empty);
        }

        public string GetPara(string sParName, string sDefault)
        {
            var sDf = sDefault;
            if (_arDics != null && _arDics.ContainsKey(sParName)) sDf = _arDics[sParName];
            return sDf;
        }
    }
}
