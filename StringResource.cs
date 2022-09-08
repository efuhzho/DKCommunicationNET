using DKCommunicationNET. Language;

namespace DKCommunicationNET;

/// <summary>
/// DKCommunication的字符串资源及多语言管理中心
/// </summary>
internal class StringResources
{
    #region Constractor

    static StringResources ( )
    {
        if ( System. Globalization. CultureInfo. CurrentCulture. ToString ( ). StartsWith ( "zh" ) )
        {
            SetLanguageChinese ( );
        }
        else
        {
            SeteLanguageEnglish ( );
        }
    }

    #endregion


    /// <summary>
    /// 获取或设置系统的语言选项 ->
    /// Gets or sets the language options for the system
    /// </summary>
    public static Chinese Language = new ( );

    /// <summary>
    /// 将语言设置为中文 ->
    /// Set the language to Chinese
    /// </summary>
    public static void SetLanguageChinese ( )
    {
        Language = new Chinese ( );
    }

    /// <summary>
    /// 将语言设置为英文 ->
    /// Set the language to English
    /// </summary>
    public static void SeteLanguageEnglish ( )
    {
        Language = new English ( );
    }
}

