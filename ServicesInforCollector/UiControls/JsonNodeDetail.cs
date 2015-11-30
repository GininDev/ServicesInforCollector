using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GininDev.Common.DataTools.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServicesInforCollector.UiControls
{
    public partial class JsonNodeDetail : UserControl
    {
        private JToken _dbi = null;

        public object DataBoundItem
        {
            set
            {
                var _cType = value.GetType().Name;

                gpNodeDetail.Controls.Clear();

                if (value is JValue)
                {
                    var objJv = (JValue) value;
                    if (objJv.Value != null)
                    {
                        JObject objSubJson = null;
                        try
                        {
                            objSubJson = (JObject) JsonConvert.DeserializeObject(objJv.Value.ToString());

                            Stack<Control> arControls = null;
                            DynReturBuildJControl(objSubJson, ref arControls);

                            if (arControls != null)
                            {
                                gpNodeDetail.Controls.Clear();
                                while (arControls.Count > 0)
                                {
                                    var aControl = (Control) arControls.Pop();
                                    if (aControl != null)
                                        gpNodeDetail.Controls.Add(aControl);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            TextBox tbValue;
                            var tbName = ShowNormalValue(out tbValue);

                            tbValue.Text = objJv.Value.ToString();
                            tbName.Text = objJv.Path;
                        }
                    }
                    _dbi = objJv;

                }
                else if (value is JObject)
                {
                    JObject x = (JObject) value;
                    if (x == null) return;

                    TextBox tbValue;
                    var tbName = ShowNormalValue(out tbValue);

                    tbValue.Text = JsonConvert.SerializeObject(x);
                    tbName.Text = x.Path;
                }
            }
        }

        private TextBox ShowNormalValue(out TextBox tbValue)
        {
            var lblName = new Label();
            lblName.Text = "Name";
            lblName.Dock = DockStyle.Top;
            lblName.TabIndex = 0;

            var tbName = new TextBox();
            tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tbName.Dock = DockStyle.Top;
            tbName.TabIndex = 1;

            var lblValue = new Label();
            lblValue.Text = "Value";
            lblValue.Dock = DockStyle.Top;
            lblValue.TabIndex = 2;

            tbValue = new TextBox();
            tbValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tbValue.Dock = DockStyle.Fill;
            tbValue.Multiline = true;
            tbValue.TabIndex = 3;

            gpNodeDetail.Controls.Add(tbValue);
            gpNodeDetail.Controls.Add(lblValue);
            gpNodeDetail.Controls.Add(tbName);
            gpNodeDetail.Controls.Add(lblName);

            return tbName;
        }

        private static void DynReturBuildJControl(JToken objSubJson, ref Stack<Control> arControls)
        {
            foreach (var aNode in objSubJson.Children())
            {
                if (aNode is JProperty || aNode is JObject)
                {
                    DynReturBuildJControl(aNode, ref arControls);
                }
                else if (aNode is JValue)
                {
                    JValue aJv = (JValue)aNode;

                    var tmpLbl = new Label();
                    tmpLbl.Text = aJv.Path;
                    tmpLbl.Dock = DockStyle.Top;
                    //tmpLbl.TabIndex = iIndex * 2 + 0;

                    var tmpTbx = new TextBox();
                    tmpTbx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    tmpTbx.Dock = DockStyle.Top;
                    //tmpTbx.TabIndex = iIndex * 2 + 1;
                    tmpTbx.Text = aJv.Value.ToString();

                    if (arControls == null)
                        arControls = new Stack<Control>();
                    arControls.Push(tmpLbl);
                    arControls.Push(tmpTbx);
                }
            }
        }

        /// <summary>
        /// 根据JToken 动态创建label and text
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        private Stack<object> DynBuildJLabel(JToken z)
        {
            Stack<object> arControls = null;
            var iIndex = 0;
            foreach (var aPro in z.Children())
            {
                if (aPro is JProperty)
                {
                    JProperty aJp = (JProperty) aPro;

                    var tmpLbl = new Label();
                    tmpLbl.Text = aPro.Path;
                    tmpLbl.Dock = DockStyle.Top;
                    tmpLbl.TabIndex = iIndex*2 + 0;

                    var tmpTbx = new TextBox();
                    tmpTbx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    tmpTbx.Dock = DockStyle.Top;
                    tmpTbx.TabIndex = iIndex*2 + 1;
                    tmpTbx.Text = aJp.Value.ToString();

                    if (arControls == null)
                        arControls = new Stack<object>();
                    arControls.Push(tmpLbl);
                    arControls.Push(tmpTbx);

                    iIndex += 1;
                }
            }

            return arControls;
        }

        public JsonNodeDetail()
        {
            InitializeComponent();
        }
    }
}
