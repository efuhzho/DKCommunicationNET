namespace DKCommunicationNET. Core;

/// <summary>
/// 检查所选型号的功能状态类
/// </summary>
internal class CheckFunctionsStatus
{
    /// <summary>
    /// 检查设备的功能状态
    /// </summary>
    /// <param name="packetBuilder"></param>
    /// <param name="isEnabled">设备是否安装模块或是否禁用功能</param>
    /// <exception cref="Exception"></exception>
    public static OperateResult<byte[ ]> CheckFunctionsState ( object? packetBuilder , bool isEnabled )
    {
        if ( packetBuilder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        else if ( !isEnabled )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotEnabledModule );
        }
        return OperateResult. CreateSuccessResult ( Array. Empty<byte> ( ) );
    }
}
