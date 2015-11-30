using System;
using System.Windows.Forms;
using GininDev.Common.DataTools.CustTypes;
using GininDev.Common.DataTools.Entities;

namespace GininDev.Common.DataTools.Helpers
{
    /// <summary>
    /// 服务太多时用多线程操作
    /// </summary>
    public class WorkThread
    {
        private readonly WmiServiceObj _objBiz;


        public WorkThread(WmiServiceObj dsvc)
        {
            _objBiz = dsvc;

        }

        public void ThreadPoolCallBack(Object oThreadContext)
        {
            WakeUp();
        }

        public void WakeUp()
        {
            ShellHelper.SwitchServiceStatusTo(_objBiz);
        }
    }
}