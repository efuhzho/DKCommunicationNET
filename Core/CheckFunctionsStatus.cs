using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Core;

/// <summary>
/// 检查所选型号的功能状态类
/// </summary>
internal class CheckFunctionsStatus
{     
    /// <summary>
    /// 检查设备的功能状态
    /// </summary>
    /// <param name="isSupported">协议是否支持</param>
    /// <param name="isEnabled">设备是否安装模块或是否禁用功能</param>
    /// <exception cref="Exception"></exception>
    public static void CheckFunctionsState ( bool isSupported , bool isEnabled )
    {
        if ( !isSupported )
        {
            throw new Exception ( StringResources. Language. NotSupportedModule );
        }
        else if ( !isEnabled )
        {
            throw new Exception ( StringResources. Language. NotEnabledModule );
        }
    }
}
