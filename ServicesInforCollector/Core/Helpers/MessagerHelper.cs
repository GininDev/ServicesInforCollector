using System.Windows.Forms;

namespace ServicesInforCollector.Core.Helpers
{
    public class MessagerHelper
    {
        public static DialogResult Info(string sFormat, bool bNeedConfirm)
        {
            var objDr = DialogResult.None;
            if (bNeedConfirm)
            {
                objDr = MessageBox.Show(sFormat, "Need Confirm", MessageBoxButtons.YesNo);
            }
            else
            {
                MessageBox.Show(sFormat);
            }

            return objDr;
        }

        internal static void Info(string p)
        {
            Info(p, false);
        }
    }
}