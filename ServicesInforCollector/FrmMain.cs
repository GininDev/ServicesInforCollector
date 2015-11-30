using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using GininDev.Common.DataTools.CustTypes;
using GininDev.Common.DataTools.DynLoader;
using GininDev.Common.DataTools.Entities;
using GininDev.Common.DataTools.Helpers;
using GininDev.Common.DataTools.Helpers.ConfigTool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicesInforCollector.Core.Controller;
using ServicesInforCollector.Core.Events;
using ServicesInforCollector.UiControls;

namespace ServicesInforCollector
{
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 数据存储类型名
        /// </summary>
        private const string DbType = "System.Data.SQLite";
        /// <summary>
        /// 数据库文件配置名
        /// </summary>
        private const string DbConfigKey = "DbName";


        private static SortableList<WmiServiceObj> _arBiz;


        /// <summary>
        ///     系统进程名
        /// </summary>
        private List<string> _arSyses;

        public FrmMain()
        {
            InitializeComponent();
        }

        public event GridDataChangedHandler GridDataChangeded;

        protected virtual void OnGridDataChangeded(GridDataChangedHandlerArgs e)
        {
            GridDataChangeded(this, e); //Raise the event
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ClearBind();

            List<string> arFilter = GetFilters(null);

            string sExclude = tbExclude.Text;
            string[] arExclude = null;
            if (!string.IsNullOrEmpty(sExclude))
                arExclude = sExclude.Split('|');

            string sSql = "Select * From Win32_Service";
            sSql += ConvertFilter2Str(arFilter);

            var qry = new SelectQuery(sSql);
            var searcher = new ManagementObjectSearcher(qry);
            ManagementObjectCollection serviceGetter = searcher.Get();

            //主业务对象
            _arBiz = new SortableList<WmiServiceObj>();

            //显示行序号
            int[] iOrder = {0};
            foreach (
                WmiServiceObj objWmi in
                    serviceGetter.Cast<ManagementBaseObject>()
                        .Select(mobj => new WmiServiceObj(mobj, iOrder[0], arExclude))
                        .Where(objWmi => !objWmi.BSkipReadConfig))
            {
                _arBiz.Add(objWmi);
                iOrder[0] += 1;
            }

            BindBiz(true);
        }

        private void SetGridViewData(DataGridView dgv, object objData)
        {
            bool bChanged = objData != dgv.DataSource;

            if (!bChanged) return;

            dgv.DataSource = objData;
            OnGridDataChangeded(dgv, new GridDataChangedHandlerArgs(1));
        }

        /// <summary>
        ///     重新绑定数据到gridview或者textbox
        /// </summary>
        /// <param name="bRebind"></param>
        private void BindBiz(bool bRebind)
        {
            this.wmiSumInfo1.Size = new Size(tcMain.SelectedIndex == 0? 180 : 2, 345);
            
            switch (tcMain.SelectedIndex)
            {
                case 0:
                    if (dataGridView1.DataSource == null || bRebind)
                    {
                        if (_arBiz != null) SetGridViewData(dataGridView1, _arBiz.ToList());
                        foreach (DataGridViewColumn acol in dataGridView1.Columns)
                        {
                            acol.SortMode = DataGridViewColumnSortMode.Automatic;
                        }
                    }
                    
                    break;
                case 2:
                    string sTreeContent = StringHelper.SeriaString(_arBiz);
                    var objTree = (JToken)JsonConvert.DeserializeObject(sTreeContent);
                    jsonTree2.DataBoundItem = objTree;

                    break;
                default:
                    if (_arBiz == null || _arBiz.Count == 0)
                    {
                        MessagerHelper.Info("No Data!");
                    }
                    else
                    {
                        rtbOut.Clear();
                        string sers = StringHelper.SeriaString(_arBiz);
                        rtbOut.AppendText(sers);
                    }
                    break;
            }
        }

        private void BindBiz()
        {
            BindBiz(false);
        }


        /// <summary>
        ///     转换过滤器到sql查询参数
        /// </summary>
        /// <param name="arFilter"></param>
        /// <returns></returns>
        private static string ConvertFilter2Str(ICollection<string> arFilter)
        {
            string sFilter = string.Empty;
            if (arFilter == null || arFilter.Count <= 0) return sFilter;

            if (arFilter.Count > 0)
            {
                sFilter = string.Format(" where {0}", string.Join(" and ", arFilter.ToArray()));
            }
            return sFilter;
        }


        /// <summary>
        ///     获取过滤器
        /// </summary>
        /// <param name="arFilter"></param>
        /// <returns></returns>
        private List<string> GetFilters(List<string> arFilter)
        {
            if (!string.IsNullOrEmpty(tbName.Text))
            {
                if (arFilter == null) arFilter = new List<string>();

                string sName = tbName.Text;
                MatchCollection mis = StringHelper.RgName.Matches(sName);
                if (mis.Count > 0)
                {
                    var arNames = new List<string>();
                    for (int i = 0; i < mis.Count; i++)
                    {
                        string sCmd = mis[i].Result("${aName}");
                        arNames.Add(string.Format("Name like '%{0}%'", sCmd));
                    }
                    string tName = string.Join(" OR ", arNames.ToArray());
                    if (mis.Count > 1)
                        tName = string.Format("({0})", tName);
                    arFilter.Add(tName);
                }
                else
                {
                    arFilter.Add(string.Format("Name like '%{0}%'", tbName.Text));
                }
            }

            if (!string.IsNullOrEmpty(cbState.Text))
            {
                if (arFilter == null) arFilter = new List<string>();
                arFilter.Add(string.Format("State = '{0}'", cbState.Text));
            }

            if (!string.IsNullOrEmpty(cbStartMode.Text))
            {
                if (arFilter == null) arFilter = new List<string>();
                arFilter.Add(string.Format("StartMode = '{0}'", cbStartMode.Text));
            }

            if (string.IsNullOrEmpty(tbDesc.Text)) return arFilter;

            if (arFilter == null) arFilter = new List<string>();
            arFilter.Add(string.Format("Description like '%{0}%'", tbDesc.Text));

            return arFilter;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearBind();
        }

        /// <summary>
        ///     清除界面绑定
        /// </summary>
        private void ClearBind()
        {
            rtbOut.Clear();
            SetGridViewData(dataGridView1, null);
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBiz();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            GetAndBindSettings();

            GridDataChangeded += OnGridDataChangeded;
        }

        private void OnGridDataChangeded(object sender, GridDataChangedHandlerArgs gridDataChangedHandlerArgs)
        {
            var dgv = (DataGridView) sender;
            if (dgv == null || dgv.Rows.Count <= 0) return;

            var objRHStyle = new DataGridViewCellStyle();
            objRHStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersDefaultCellStyle = objRHStyle;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                DataGridViewRow aitem = dgv.Rows[i];
                object obj = aitem.DataBoundItem;
                var objz = (WmiServiceObj) obj;

                //aitem.HeaderCell.Value = objz.OrderNo.ToString();

                if (_arSyses.Contains(objz.ExeName))
                {
                    for (int j = 0; j < dgv.ColumnCount; j++)
                    {
                        DataGridViewCellStyle clStyle = aitem.Cells[j].Style;
                        clStyle.ForeColor = Color.Red;
                    }
                }

                if (objz.StartMode.ToLower() == "disabled")
                {
                    for (int j = 0; j < dgv.ColumnCount; j++)
                    {
                        DataGridViewCellStyle clStyle = aitem.Cells[j].Style;
                        clStyle.BackColor = Color.GhostWhite;
                    }
                }

                if (objz.State.ToLower() == "stopped")
                {
                    for (int j = 0; j < dgv.ColumnCount; j++)
                    {
                        DataGridViewCellStyle clStyle = aitem.Cells[j].Style;
                        clStyle.BackColor = Color.DarkGray;
                        clStyle.ForeColor = Color.Blue;
                    }
                }
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Do not automatically paint the focus rectangle.
            e.PaintParts &= ~DataGridViewPaintParts.Focus;

            // Determine whether the cell should be painted
            // with the custom selection background.
            if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                // Calculate the bounds of the row.
                Rectangle rowBounds = new Rectangle(
                    this.dataGridView1.RowHeadersWidth, e.RowBounds.Top,
                    this.dataGridView1.Columns.GetColumnsWidth(
                        DataGridViewElementStates.Visible) -
                    this.dataGridView1.HorizontalScrollingOffset + 1,
                    e.RowBounds.Height);

                // Paint the custom selection background.
                using (Brush backbrush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(rowBounds,
                        this.dataGridView1.DefaultCellStyle.SelectionBackColor,
                        e.InheritedRowStyle.ForeColor,
                        System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(backbrush, rowBounds);
                }
            }
        }

        /// <summary>
        ///     获取环境变量并绑定
        /// </summary>
        private void GetAndBindSettings()
        {
            string sDefaultName = ConfigHelper.ConfigReader("tbName");
            tbName.Text = sDefaultName;

            string sExclude = ConfigHelper.ConfigReader("tbExclude");
            tbExclude.Text = sExclude;


            string sProcesses = ConfigHelper.ConfigReader("sysProcess");
            if (!string.IsNullOrEmpty(sProcesses))
                _arSyses = new List<string>(sProcesses.Split('|'));

            /* 不完善  暂不用
            this.HelpButton = true;
            this.MaximizeBox = !this.HelpButton;
            this.MinimizeBox = !this.HelpButton;
            */
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridViewSelectedRowCollection arSelected = dataGridView1.SelectedRows;

            if (e.ClickedItem == null) return;
            if (e.ClickedItem.Tag == null) return;

            string itemNumber = e.ClickedItem.Tag.ToString();

            List<string> arOpts = null;

            switch (itemNumber)
            {
                case "0": //add filter
                    List<string> arNew;
                    if (AddSkip(arSelected, out arNew))
                    {
                        DialogResult objDr =
                            MessagerHelper.Info(
                                string.Format("new filter:\n'{0}' \nadded, requery or not?.",
                                    string.Join(",", arNew.ToArray())), true);
                        if (objDr == DialogResult.Yes)
                        {
                            btnSearch_Click(null, null);
                        }
                    }
                    else
                    {
                        MessagerHelper.Info("No New Filter Added");
                    }

                    break;
                case "1": // service stop
                    ShellStopHelper(arSelected, out arOpts);

                    if (arOpts != null)
                    {
                        DialogResult objDr =
                            MessagerHelper.Info(
                                string.Format("service stoped:\n{0} refresh?", string.Join(",", arOpts.ToArray())),
                                true);
                        if (objDr == DialogResult.Yes)
                        {
                            btnSearch_Click(null, null);
                        }
                    }
                    break;
                case "2": //service start
                    foreach (DataGridViewRow dsvc in arSelected)
                    {
                        object obj = dsvc.DataBoundItem;
                        var objz = (WmiServiceObj) obj;
                        bool aRet = ShellHelper.SwitchServiceStatusTo(objz, ServiceControllerStatus.Running);

                        if (arOpts == null)
                            arOpts = new List<string>();
                        arOpts.Add(string.Format("{0}->{1}\n", objz.Name, aRet ? "succ" : "fail"));
                    }
                    if (arOpts != null)
                    {
                        DialogResult objDr =
                            MessagerHelper.Info(
                                string.Format("service started:\n{0} refresh?", string.Join(",", arOpts.ToArray())),
                                true);
                        if (objDr == DialogResult.Yes)
                        {
                            btnSearch_Click(btnSearch, new MouseEventArgs(MouseButtons.Left, 1, 36, 12, 0));
                        }
                    }
                    break;
                case "3": //explorer to
                    if (arSelected.Count > 0)
                    {
                        object abi = arSelected[0].DataBoundItem;
                        var objz = (WmiServiceObj) abi;
                        ShellHelper.ExplorerPath(objz);
                    }

                    break;
                case "5": //delete service
                    if (MessagerHelper.Info("Are You Sure ? Deleted!", true) == DialogResult.Yes)
                    {
                        foreach (
                            WmiServiceObj objz in
                                (from DataGridViewRow dsvc in arSelected select dsvc.DataBoundItem into obj select obj)
                                    .Cast<WmiServiceObj>())
                        {
                            string sRel;
                            bool aRet = ShellHelper.DeleteService(objz, out sRel);

                            if (arOpts == null)
                                arOpts = new List<string>();
                            arOpts.Add(string.Format("{0}->{1}\n", objz.Name, aRet ? "succ" : "fail"));
                        }
                        if (arOpts != null)
                        {
                            DialogResult objDr =
                                MessagerHelper.Info(
                                    string.Format("service deleted:\n{0} refresh?", string.Join(",", arOpts.ToArray())),
                                    true);
                            if (objDr == DialogResult.Yes)
                            {
                                btnSearch_Click(btnSearch, new MouseEventArgs(MouseButtons.Left, 1, 36, 12, 0));
                            }
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="arSelected"></param>
        /// <param name="arOpts"></param>
        private static void ShellStopHelper(DataGridViewSelectedRowCollection arSelected, out List<string> arOpts)
        {
            arOpts = null;
            int iWorkCount = arSelected.Count;
            if (iWorkCount < 5)
            {
                foreach (DataGridViewRow dsvc in arSelected)
                {
                    object obj = dsvc.DataBoundItem;
                    var objz = (WmiServiceObj) obj;
                    bool aRet = ShellHelper.SwitchServiceStatusTo(objz, ServiceControllerStatus.Stopped);

                    if (arOpts == null)
                        arOpts = new List<string>();
                    arOpts.Add(string.Format("{0}->{1}\n", objz.Name, aRet ? "succ" : "fail"));
                }
            }
            else
            {
                //
                const int thrCou = 4; //并发数
                double xt = Math.Ceiling(iWorkCount/(float) thrCou); //周期数

                var device = new WorkThread[thrCou];
                bool bSetMaxThread = ThreadPool.SetMaxThreads(thrCou, thrCou*2);
                if (!bSetMaxThread)
                {
                    Console.WriteLine("Setting max threads of the threadpool failed!");
                }
                for (int i = 0; i < xt; i++)
                {
                    for (int j = 0; j < thrCou; j++)
                    {
                        int iPoint = i*thrCou + j;
                        if (iPoint >= iWorkCount)
                            break;

                        if (arSelected[iPoint].Cells.Count > 1)
                        {
                            var obj = arSelected[iPoint];
                            var objTmp = (WmiServiceObj) obj.DataBoundItem;
                            device[j] = new WorkThread(objTmp);
                            ThreadPool.QueueUserWorkItem(device[j].ThreadPoolCallBack);
                        }
                    }
                }
                Console.WriteLine("Pls Wait for a moment......Current Time:{0}", DateTime.Now.ToLongTimeString());
                //
            }
        }


        private bool AddSkip(IEnumerable arSelected, out List<string> arNew)
        {
            if (arSelected == null) throw new ArgumentNullException("arSelected");
            List<string> arSkips = null;

            string sExclude = tbExclude.Text;
            string[] arExclude = null;
            if (!string.IsNullOrEmpty(sExclude))
                arExclude = sExclude.Split('|');

            arNew = null;

            foreach (DataGridViewRow dsvc in arSelected)
            {
                var objTemp = (WmiServiceObj) dsvc.DataBoundItem;

                if (objTemp == null) continue;

                string a = objTemp.Path;

                bool bSkip = false;
                DirectoryInfo di;
                string sShort = FileHelper.GetShortNameAndSetDirInfo(a, out di);
                if (arExclude != null)
                {
                    if (arExclude.Any(ex => ex == sShort))
                    {
                        bSkip = true;
                    }
                }
                if (bSkip || string.IsNullOrEmpty(sShort)) continue;

                if (arSkips == null)
                    arSkips = new List<string>();

                if (arSkips.Contains(sShort)) continue;

                arSkips.Add(sShort);
                if (arNew == null)
                {
                    arNew = new List<string>();
                }
                arNew.Add(sShort);
            }

            string sOld = tbExclude.Text;

            string[] arOld = sOld.Split('|');

            foreach (string s in arOld)
            {
                if (arSkips == null)
                    arSkips = new List<string>();
                if (arSkips.Contains(s)) continue;

                arSkips.Add(s);
            }
            if (arSkips != null)
                arSkips.Sort();
            //
            if (arSkips == null)
                return false;

            tbExclude.Text = string.Join("|", arSkips.ToArray());

            return arNew != null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string curPath = Environment.CurrentDirectory;
            string dataPath = string.Format("{0}{1}Data", curPath, Path.DirectorySeparatorChar);

            string sName = string.Format("{0}_info_{1}.json", Environment.MachineName,
                (DateTime.Now).ToString("yyyyMMddHHmmss"));

            string sFullName = string.Format("{0}{1}{2}", dataPath, Path.DirectorySeparatorChar, sName);

            var sOutText = string.Empty;
            if (_arBiz == null)
            {
                MessagerHelper.Info("Content is null, Do nothing");
                return;
            }
            else
            {
                string sers = StringHelper.SeriaString(_arBiz);
                if (string.IsNullOrEmpty(sers))
                {
                    MessagerHelper.Info("Content is null, Do nothing");
                    return;
                }
                else
                {
                    FileHelper.WriteDataFile(sFullName, sers);

                    DialogResult objDr =
                        MessagerHelper.Info(
                            string.Format("results saved to file：{0}\nview or not?", sFullName), true);
                    if (objDr == DialogResult.Yes)
                    {
                        ShellHelper.NotepadFile(sFullName);
                    }
                }
            }
        }


        private void btnDirectoryWatcher_Click(object sender, EventArgs e)
        {
            Cursor cursPre = Cursor;
            string sDir = tbDirecotry.Text;

            var di = new DirectoryInfo(sDir);
            if (!Directory.Exists(sDir))
            {
                MessagerHelper.Info(string.Format("Directory {0} not exists", sDir));

                return;
            }

            Cursor = Cursors.WaitCursor;
            var arResu = new Dictionary<string, Dictionary<string, object>>();
            DirectoryInfo[] arX = di.GetDirectories();
            foreach (DirectoryInfo adi in arX)
            {
                Dictionary<string, object> objT = ConfigHelper.GetDirConfigs(adi);
                arResu.Add(adi.Name, objT);
            }
            StringWriter sw = FileHelper.JsonWriter(arResu);
            tbDirectoryOut.Text = sw.ToString();

            Cursor = cursPre;
        }


        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            ClearBind();

            string sBase = Environment.CurrentDirectory;
            string sOfdBase = string.Format("{0}{1}Data", sBase, Path.DirectorySeparatorChar);
            ofdJson.InitialDirectory = sOfdBase;
            DialogResult dr = ofdJson.ShowDialog();
            if (dr != DialogResult.OK) return;

            string sFull = ofdJson.FileName;
            string s = FileHelper.ReadDataFile(sFull);
            rtbOut.Text = s;

            try
            {
                JArray jsonRoot = JArray.Parse(s);
                IList<JToken> arObjs = jsonRoot.Children().ToList();
                _arBiz = null; //new global data
                foreach (JToken aWmiObj in arObjs)
                {
                    var aServiceObj = JsonConvert.DeserializeObject<WmiServiceObj>(aWmiObj.ToString());
                    if (_arBiz == null) _arBiz = new SortableList<WmiServiceObj>();
                    _arBiz.Add(aServiceObj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File DeserializeObject Error for:\n" + ex.Message);
            }


            BindBiz();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

            SortOrder x = column.HeaderCell.SortGlyphDirection;
            SortOrder newSortDirection = x == SortOrder.None
                ? SortOrder.Ascending
                : (x == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
            SortableList<WmiServiceObj> data = _arBiz;

            SetGridViewData(dataGridView1, data.SortByProperty(column.DataPropertyName, newSortDirection));

            dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = newSortDirection;
        }

        private void btnFolderView_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDirecotry.Text))
            {
                fbdPros.SelectedPath = tbDirecotry.Text;
            }
            DialogResult objRe = fbdPros.ShowDialog();
            if (objRe == DialogResult.OK)
            {
                tbDirecotry.Text = fbdPros.SelectedPath;
            }
        }

        private void tbDirectoryOut_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox) sender).SelectAll();
            }
        }

        private void rtbOut_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((RichTextBox) sender).SelectAll();
            }
        }

        private void startModeToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridViewSelectedRowCollection arSelected = dataGridView1.SelectedRows;

            if (e.ClickedItem == null) return;
            if (e.ClickedItem.Tag == null) return;

            string itemNumber = e.ClickedItem.Tag.ToString();

            List<string> arOpts = null;

            switch (itemNumber)
            {
                case "41":
                    foreach (DataGridViewRow dsvc in arSelected)
                    {
                        object obj = dsvc.DataBoundItem;
                        var objz = (WmiServiceObj) obj;
                        bool aRet = ShellHelper.ChangeServiceStartMode(objz, "41");

                        if (arOpts == null)
                            arOpts = new List<string>();
                        arOpts.Add(string.Format("{0}->{1}\n", objz.Name, aRet ? "succ" : "fail"));
                    }
                    if (arOpts != null)
                    {
                        //btnSearch_Click(null, null);
                        DialogResult objDr =
                            MessagerHelper.Info(
                                string.Format("startmode change2manual:\n{0} refresh?",
                                    string.Join(",", arOpts.ToArray())),
                                true);
                        if (objDr == DialogResult.Yes)
                        {
                            btnSearch_Click(btnSearch, new MouseEventArgs(MouseButtons.Left, 1, 36, 12, 0));
                        }
                    }
                    break;
                case "42":
                    foreach (DataGridViewRow dsvc in arSelected)
                    {
                        object obj = dsvc.DataBoundItem;
                        var objz = (WmiServiceObj) obj;
                        bool aRet = ShellHelper.ChangeServiceStartMode(objz, "42");

                        if (arOpts == null)
                            arOpts = new List<string>();
                        arOpts.Add(string.Format("{0}->{1}\n", objz.Name, aRet ? "succ" : "fail"));
                    }
                    if (arOpts != null)
                    {
                        //btnSearch_Click(null, null);
                        DialogResult objDr =
                            MessagerHelper.Info(
                                string.Format("startmode change2auto:\n{0} refresh?", string.Join(",", arOpts.ToArray())),
                                true);
                        if (objDr == DialogResult.Yes)
                        {
                            btnSearch_Click(btnSearch, new MouseEventArgs(MouseButtons.Left, 1, 36, 12, 0));
                        }
                    }
                    break;
                case "43":
                    foreach (DataGridViewRow dsvc in arSelected)
                    {
                        object obj = dsvc.DataBoundItem;
                        var objz = (WmiServiceObj) obj;
                        bool aRet = ShellHelper.ChangeServiceStartMode(objz, "43");

                        if (arOpts == null)
                            arOpts = new List<string>();
                        arOpts.Add(string.Format("{0}->{1}\n", objz.Name, aRet ? "succ" : "fail"));
                    }
                    if (arOpts != null)
                    {
                        //btnSearch_Click(null, null);
                        DialogResult objDr =
                            MessagerHelper.Info(
                                string.Format("startmode change2disable:\n{0} refresh?",
                                    string.Join(",", arOpts.ToArray())),
                                true);
                        if (objDr == DialogResult.Yes)
                        {
                            btnSearch_Click(btnSearch, new MouseEventArgs(MouseButtons.Left, 1, 36, 12, 0));
                        }
                    }
                    break;
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure clear all filter?", "Confirm", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                tbExclude.Text = string.Empty;
            }
        }

        private void tbName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                var objEa = new MouseEventArgs(MouseButtons.Left, 1, 39, 12, 1);
                btnSearch_Click(btnSearch, objEa);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection arSels = dataGridView1.SelectedRows;
            if (arSels != null && arSels.Count > 0)
            {
                DataGridViewRow lastSel = arSels[arSels.Count - 1];
                var objX = (WmiServiceObj) lastSel.DataBoundItem;
                if (objX != null)
                    wmiSumInfo1.DataBoundItem = objX;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 1 && !string.IsNullOrEmpty(tbDirectoryOut.Text))
            {
                string sJonsText = tbDirectoryOut.Text;
                var objTree = (JToken) JsonConvert.DeserializeObject(sJonsText);
                jsonTree1.DataBoundItem = objTree;

                string x = string.Empty;
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            var obj = DictController.Instance.GetPara("CompName");
            var arFacts = FactController.Instance.GetFactses();
            var arBizs = BizController.Instance.GetFactses();
            var arSys = SysDefineController.Instance.GetFactses();



            string sResult;

            Cursor cursPre = Cursor;

            /*
            Cursor = Cursors.WaitCursor;
            string sDir = tbDirecotry.Text;

            var di = new DirectoryInfo(sDir);
            if (!Directory.Exists(sDir))
            {
                MessagerHelper.Info(string.Format("Directory {0} not exists", sDir));
                return;
            }

            var arResu = new Dictionary<string, Dictionary<string, object>>();
            DirectoryInfo[] arX = di.GetDirectories();
            foreach (DirectoryInfo adi in arX)
            {
                //Dictionary<string, object> objT = ConfigHelper.GetDirConfigs(adi);
                //arResu.Add(adi.Name, objT);
            }
            //StringWriter sw = FileHelper.JsonWriter(arResu);
            //tbDirectoryOut.Text = sw.ToString();

            Cursor = cursPre;
             */
        }

        private static bool OpenConnection(IDbConnection cnn)
        {
            var sDbFile = ConfigManager.GetConfig(ConfigTypes.AppConfig).NormalGet(DbConfigKey);

            if (!File.Exists(sDbFile))
                return true;

            if (cnn == null)
                return true;

            cnn.ConnectionString = string.Format("Data Source={0}", sDbFile);
            cnn.Open();

            return cnn.State != ConnectionState.Open;
        }
    }
}