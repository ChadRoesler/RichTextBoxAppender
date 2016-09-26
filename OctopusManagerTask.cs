using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using System.Reflection;
using OctopusManager.Logging;

namespace OctopusManager
{
    public partial class frmOctopusManagerTask : Form
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public frmOctopusManagerTask()
        {
            InitializeComponent();
            RichTextBoxAppender.SetRichTextBox(rtbLogging, "RichTextBoxAppender");
            Log.Info("Info Check");
            Log.Debug("Debug Check");
            Log.Warn("Warn Check");
            Log.Error("Error Check");
            Log.Fatal("Fatal Check");
        }
    }
}
