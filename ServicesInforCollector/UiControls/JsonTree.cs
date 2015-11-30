using System;
using System.Windows.Forms;
using GininDev.Common.DataTools.Helpers;
using Newtonsoft.Json.Linq;

namespace ServicesInforCollector.UiControls
{
    public partial class JsonTree : UserControl
    {
        public JsonTree()
        {
            InitializeComponent();
        }

        private JToken _dbi = null;
        private bool bInitz = false;

        public object DataBoundItem
        {
            set
            {
                _dbi = (JToken) value;

                if (_dbi != null)
                {
                    tvJson.Nodes.Clear();
                    recurTree(_dbi, tvJson);
                    if (!bInitz)
                    {
                        tvJson.NodeMouseClick += TvJsonOnNodeMouseClick;
                        bInitz = true;
                    }
                }
            }
        }

        private void TvJsonOnNodeMouseClick(object sender, TreeNodeMouseClickEventArgs treeNodeMouseClickEventArgs)
        {
            TreeNode objTn = treeNodeMouseClickEventArgs.Node;
            jsonNodeDetail1.DataBoundItem = objTn.Tag;
        }

        public void recurTree(JToken objTree, object oParent)
        {
            var aCapt = objTree.Path;
            aCapt = string.IsNullOrEmpty(aCapt) ? "Root" : StringHelper.GetJsonShortPath(aCapt);

            var oCloset = oParent;
            if (!(objTree is JProperty))
            {
                if (objTree is JValue)
                {
                    var aTv = ((JValue) objTree).Value;
                    if (aTv != null)
                        aCapt += string.Format(": {0}", aTv.ToString());
                }

                var aNode = new TreeNode(aCapt);
                aNode.Tag = objTree;
                if (oParent is TreeView)
                {
                    var objRoot = (TreeView) oParent;
                    objRoot.Nodes.Add(aNode);
                }
                else if (oParent is TreeNode)
                {
                    var oPare = (TreeNode) oParent;
                    oPare.Nodes.Add(aNode);
                }
                oCloset = aNode;
            }

            foreach (JToken azz in objTree.Children())
            {
                if (azz is JObject)
                {
                    var yy = (JObject) azz;
                    recurTree(yy, oCloset);
                }
                else if (azz is JProperty)
                {
                    var pp = (JProperty) azz;
                    recurTree(pp, oCloset);
                }
                else if (azz is JValue)
                {
                    var zzyy = (JValue) azz;
                    recurTree(zzyy, oCloset);
                }

            }
        }

    }
}