using System;
using System.Drawing;
using System.Windows.Forms;
using log4net.Appender;
using log4net.Core;
using log4net.Util;

namespace log4net
{
    public class RichTextBoxAppender : AppenderSkeleton
    {
        private RichTextBox richtextBox = null;
        private Form containerForm = null;
        private LevelMapping levelMapping = new LevelMapping();
        private int maxTextLength = 100000;

        public RichTextBoxAppender() : base()
        {
        }

        public RichTextBox RichTextBoxToAppendTo
        {
            get
            {
                return richtextBox;
            }
            set
            {
                if (!ReferenceEquals(value, richtextBox))
                {
                    if (containerForm != null)
                    {
                        containerForm.FormClosed -= new FormClosedEventHandler(containerForm_FormClosed);
                        containerForm.Shown -= new EventHandler(containerForm_Shown);
                        containerForm = null;
                    }

                    if (value != null)
                    {
                        value.ReadOnly = true;
                        value.HideSelection = false;

                        containerForm = value.FindForm();
                        containerForm.FormClosed += new FormClosedEventHandler(containerForm_FormClosed);
                        containerForm.Shown += new EventHandler(containerForm_Shown);
                    }

                    richtextBox = value;
                }
            }
        }

        public void AddMapping(LevelTextStyle mapping)
        {
            levelMapping.Add(mapping);
        }
        public int MaxBufferLength
        {
            get
            {
                return maxTextLength;
            }
            set
            {
                if (value > 0)
                {
                    maxTextLength = value;
                }
            }
        }

        public override void ActivateOptions()
        {
            base.ActivateOptions();
            levelMapping.ActivateOptions();
        }

        protected override void Append(LoggingEvent LoggingEvent)
        {
            if (richtextBox != null)
            {
                if (richtextBox.InvokeRequired)
                {
                    richtextBox.Invoke(new UpdateControlDelegate(UpdateControl), new object[]
                    {
                        LoggingEvent
                    });
                }
                else
                {
                    UpdateControl(LoggingEvent);
                }
            }
        }

        private delegate void UpdateControlDelegate(LoggingEvent loggingEvent);

        private void UpdateControl(LoggingEvent loggingEvent)
        {
            // There may be performance issues if the buffer gets too long
            // So periodically clear the buffer
            if (richtextBox.TextLength > maxTextLength)
            {
                richtextBox.Clear();
                richtextBox.AppendText(string.Format("(earlier messages cleared because log length exceeded maximum of {0})\n\r", maxTextLength));
            }

            // look for a style mapping
            LevelTextStyle selectedStyle = levelMapping.Lookup(loggingEvent.Level) as LevelTextStyle;
            if (selectedStyle != null)
            {
                // set the colors of the text about to be appended
                if (!selectedStyle.BackgroundColor.IsEmpty)
                {
                    richtextBox.SelectionBackColor = selectedStyle.BackgroundColor;
                }
                if (!selectedStyle.ForgroundColor.IsEmpty)
                {
                    richtextBox.SelectionColor = selectedStyle.ForgroundColor;
                }

                // alter selection font as much as necessary
                // missing settings are replaced by the font settings on the control
                if (selectedStyle.Font != null)
                {
                    // set Font Family, size and styles
                    richtextBox.SelectionFont = selectedStyle.Font;
                }
                else if (selectedStyle.PointSize > 0 && richtextBox.Font.SizeInPoints != selectedStyle.PointSize)
                {
                    // use control's font family, set size and styles
                    float size = selectedStyle.PointSize > 0.0f ? selectedStyle.PointSize : richtextBox.Font.SizeInPoints;
                    richtextBox.SelectionFont = new Font(richtextBox.Font.FontFamily.Name, size, selectedStyle.FontStyle);
                }
                else if (richtextBox.Font.Style != selectedStyle.FontStyle)
                {
                    // use control's font family and size, set styles
                    richtextBox.SelectionFont = new Font(richtextBox.Font, selectedStyle.FontStyle);
                }
            }
            richtextBox.AppendText(RenderLoggingEvent(loggingEvent));
        }

        private void containerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RichTextBoxToAppendTo = null;
        }

        private void containerForm_Shown(object sender, EventArgs e)
        {
            RichTextBoxToAppendTo.Clear();
        }

        protected override void OnClose()
        {
            base.OnClose();
            if (containerForm != null)
            {
                containerForm.FormClosed -= new FormClosedEventHandler(containerForm_FormClosed);
                containerForm.Shown -= new EventHandler(containerForm_Shown);
                containerForm = null;
            }
        }


        protected override bool RequiresLayout
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Set the RichTextBox and Appender using the default buffer size of 100000
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="appenderName"></param>
        /// <returns></returns>
        public static bool SetRichTextBox(RichTextBox richTextBox, string appenderName)
        {
            if (appenderName == null) return false;

            IAppender[] appenders = LogManager.GetRepository().GetAppenders();
            foreach (IAppender appender in appenders)
            {
                if (appender.Name == appenderName)
                {
                    if (appender is RichTextBoxAppender)
                    {
                        ((RichTextBoxAppender)appender).RichTextBoxToAppendTo = richTextBox;
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        /// <summary>
        /// Set the RichTextBox, Appender, and MaxBufferLength
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="appenderName"></param>
        /// <param name="maxBufferLength"></param>
        /// <returns></returns>
        public static bool SetRichTextBox(RichTextBox richTextBox, string appenderName, int maxBufferLength)
        {
            if (appenderName == null) return false;
            IAppender[] appenders = LogManager.GetRepository().GetAppenders();
            foreach (IAppender appender in appenders)
            {
                if (appender.Name == appenderName)
                {
                    if (appender is RichTextBoxAppender)
                    {
                        ((RichTextBoxAppender)appender).RichTextBoxToAppendTo = richTextBox;
                        ((RichTextBoxAppender)appender).MaxBufferLength = maxBufferLength;
                        return true;
                    }
                    break;
                }
            }
            return false;
        }
    }
}
