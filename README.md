# RichTextBoxAppender

Based off of the work found here:
http://osdir.com/ml/log.log4net.user/2008-04/msg00023.html

All credit goes to this post, just added some modifications I thought would be usefull.

#WindowsForm Example:
```C#
public frmForYourHealth()
{
    InitializeComponent();
    RichTextBoxAppender.SetRichTextBox(rtbLogging, "RichTextBoxAppender");
}
```

#ConfigFile Example:
```XML
<log4net>
  <appender name="RichTextBoxAppender" type="log4net.Appender.RichTextBoxAppender, RichTextBoxAppender">
    <mapping>
      <level value="WARN" />
      <foreColor value="OrangeRed" />
      <isItalic value="true" />
    </mapping>
    <mapping>
      <level value="INFO" />
      <foreColor value="ControlText" />
    </mapping>
    <mapping>
      <level value="DEBUG" />
      <foreColor value="DarkGreen" />
    </mapping>
    <mapping>
      <level value="FATAL" />
      <foreColor value="Red" />
      <isBold value="true" />
      <isItalic value="true" />
      <pointSize value="10" />
    </mapping>
    <mapping>
      <level value="ERROR" />
      <foreColor value="DarkRed" />
      <isBold value="true" />
      <pointSize value="10" />
    </mapping>
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%message%newline%exception" />
    </layout>
  </appender>
  <root>
    <level value="ALL" />
    <appender-ref ref="RichTextBoxAppender" />
  </root>    
</log4net>    
 ```
