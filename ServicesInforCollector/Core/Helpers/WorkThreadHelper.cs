using System;
using System.Windows.Forms;
using ServicesInforCollector.Core.Components;
using ServicesInforCollector.Core.Entities;

namespace ServicesInforCollector.Core.Helpers
{
    public class WorkThread
    {
        private readonly SortableList<WmiServiceObj> _arBiz;
        private readonly WmiServiceObj _objBiz;


        public WorkThread(DataGridViewCell dsvc, SortableList<WmiServiceObj> arBiz)
        {
            object obj = dsvc.OwningRow.DataBoundItem;
            _objBiz = (WmiServiceObj) obj;

            _arBiz = arBiz;
        }

        public void ThreadPoolCallBack(Object oThreadContext)
        {
            WakeUp();
        }

        public void WakeUp()
        {
            ShellHelper.SwitchServiceStatusTo(_objBiz, _arBiz);
        }
    }
}