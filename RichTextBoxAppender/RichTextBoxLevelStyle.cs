using System;
using System.Drawing;
using log4net.Util;

namespace log4net.Appender
{
    public class LevelTextStyle : LevelMappingEntry
    {
        private string textColorName = string.Empty;
        private string backColorName = string.Empty;
        private Color textForeGroundColor;
        private Color textBackGroundColor;
        private FontStyle fontStyle = FontStyle.Regular;
        private float pointSize = 0.0f;
        private bool bold = false;
        private bool italic = false;
        private string fontFamilyName = null;
        private Font font = null;


        public string ForeColor
        {
            get
            {
                return textColorName;
            }
            set
            {
                textColorName = value;
            }
        }

        public string BackColor
        {
            get
            {
                return backColorName;
            }
            set
            {
                backColorName = value;
            }
        }

        public string FontFamilyName
        {
            get
            {
                return fontFamilyName;
            }
            set
            {
                fontFamilyName = value;
            }
        }

        public bool IsBold
        {
            get
            {
                return bold;
            }
            set
            {
                bold = value;
            }
        }

        public bool IsItalic
        {
            get
            {
                return italic;
            }
            set
            {
                italic = value;
            }
        }

        public float PointSize
        {
            get
            {
                return pointSize;
            }
            set
            {
                pointSize = value;
            }
        }

        public override void ActivateOptions()
        {
            base.ActivateOptions();
            if (!string.IsNullOrWhiteSpace(textColorName))
            {
                textForeGroundColor = Color.FromName(textColorName);
            }
            if (!string.IsNullOrWhiteSpace(backColorName))
            {
                textBackGroundColor = Color.FromName(backColorName);
            }
            if (bold)
            {
                fontStyle |= FontStyle.Bold;
            }
            if (italic)
            {
                fontStyle |= FontStyle.Italic;
            }

            if (fontFamilyName != null)
            {
                float size = pointSize > 0.0f ? pointSize : 8.25f;
                try
                {
                    font = new Font(fontFamilyName, size, fontStyle);
                }
                catch (Exception)
                {
                    font = null;
                }
            }
        }

        internal Color ForgroundColor
        {
            get
            {
                return textForeGroundColor;
            }
        }

        internal Color BackgroundColor
        {
            get
            {
                return textBackGroundColor;
            }
        }

        internal FontStyle FontStyle
        {
            get
            {
                return fontStyle;
            }
        }

        internal Font Font
        {
            get
            {
                return font;
            }
        }
    }
}
