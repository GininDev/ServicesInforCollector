using System;
using System.Windows.Forms;
using GininDev.Common.DataTools.Entities;

namespace ServicesInforCollector.UiControls
{
    public partial class WmiSumInfo : UserControl
    {
        public WmiSumInfo()
        {
            InitializeComponent();

            tbName.DoubleClick += txtBox_DoubleClick;
            tbDispName.DoubleClick += txtBox_DoubleClick;
            tbPath.DoubleClick += txtBox_DoubleClick;
            tbDesc.DoubleClick += txtBox_DoubleClick;

            tbName.KeyUp += txtBox_KeyUp;
            tbDispName.KeyUp += txtBox_KeyUp;
            tbPath.KeyUp += txtBox_KeyUp;
            tbDesc.KeyUp += txtBox_KeyUp;
        }

        private void txtBox_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Control && keyEventArgs.KeyValue == 65)
            {
                TextBox tb = (TextBox)sender;
                tb.SelectAll();
            }
        }

        private void txtBox_DoubleClick(object sender, EventArgs eventArgs)
        {
            TextBox tb = (TextBox) sender;
            var aVal = tb.Text;
            if (!string.IsNullOrEmpty(aVal))
            {
                Clipboard.SetText(aVal);
            }
        }

        public object DataBoundItem
        {
            set
            {
                var curDataBoundItem = (WmiServiceObj) value;
                if (curDataBoundItem != null)
                {
                    tbName.Text = curDataBoundItem.Name;
                    tbDispName.Text = curDataBoundItem.DispName;
                    tbPath.Text = curDataBoundItem.Path;
                    tbDesc.Text = curDataBoundItem.Desc;
                }
                else
                {
                    tbName.Text = string.Empty;
                    tbDispName.Text = string.Empty;
                    tbPath.Text = string.Empty;
                    tbDesc.Text = string.Empty;
                }
            }
        }
    }
}