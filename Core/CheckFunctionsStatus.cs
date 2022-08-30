namespace DKCommunicationNET. Core;

/// <summary>
/// 检查所选型号的功能状态类
/// </summary>
internal class CheckFunctionsStatus
{
    /// <summary>
    /// 检查设备的功能状态
    /// </summary>
    /// <param name="packetBuilder">报文生成器</param>
    /// <param name="isEnabled">设备是否安装模块或是否禁用功能</param>
    public static OperateResult<byte[ ]> CheckFunctionsState ( object? packetBuilder , bool isEnabled )
    {
        //如果报文生成器为空,说明协议不支持此功能
        if ( packetBuilder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //如果设备未激活功能模块
        else if ( !isEnabled )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotEnabledModule );
        }

        //返回成功的检查结果
        return OperateResult. CreateSuccessResult ( Array. Empty<byte> ( ) );
    }
}
