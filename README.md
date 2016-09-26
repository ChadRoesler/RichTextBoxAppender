# RichTextBox-Log4Net

Based off of the work found here:
http://osdir.com/ml/log.log4net.user/2008-04/msg00023.html

Use:

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
