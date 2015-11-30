namespace ServicesInforCollector.UiControls
{
    partial class JsonTree
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.jsonNodeDetail1 = new ServicesInforCollector.UiControls.JsonNodeDetail();
            this.tvJson = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // jsonNodeDetail1
            // 
            this.jsonNodeDetail1.Dock = System.Windows.Forms.DockStyle.Right;
            this.jsonNodeDetail1.Location = new System.Drawing.Point(283, 0);
            this.jsonNodeDetail1.Name = "jsonNodeDetail1";
            this.jsonNodeDetail1.Size = new System.Drawing.Size(185, 301);
            this.jsonNodeDetail1.TabIndex = 0;
            // 
            // tvJson
            // 
            this.tvJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvJson.Location = new System.Drawing.Point(0, 0);
            this.tvJson.Name = "tvJson";
            this.tvJson.Size = new System.Drawing.Size(283, 301);
            this.tvJson.TabIndex = 2;
            // 
            // JsonTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvJson);
            this.Controls.Add(this.jsonNodeDetail1);
            this.Name = "JsonTree";
            this.Size = new System.Drawing.Size(468, 301);
            this.ResumeLayout(false);

        }

        #endregion

        private JsonNodeDetail jsonNodeDetail1;
        private System.Windows.Forms.TreeView tvJson;


    }
}
