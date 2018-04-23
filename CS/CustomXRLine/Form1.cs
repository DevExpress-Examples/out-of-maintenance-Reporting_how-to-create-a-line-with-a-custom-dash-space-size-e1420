using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace CustomXRLine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XtraReport1 report = new XtraReport1();
            report.DesignerLoaded += new DevExpress.XtraReports.UserDesigner.DesignerLoadedEventHandler(report_DesignerLoaded);
            report.ShowDesigner();
        }

        void report_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            IToolboxService tb = e.DesignerHost.GetService(typeof(IToolboxService)) as IToolboxService;
            tb.AddToolboxItem(new ToolboxItem(typeof(CustomLine)));
        }
    }
}