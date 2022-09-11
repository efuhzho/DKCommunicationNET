using System. Drawing. Drawing2D;

namespace DKCommunicationNET. Core;

internal class CommandAction
{   
    /// <summary>
    /// 使能标志
    /// </summary>
    private bool _isEnabled;
    /// <summary>
    /// 执行报文发送和接收的委托方法
    /// </summary>
    private Func<byte[ ] , bool , OperateResult<byte[ ]>> _methodOfCheckResponse;
    public CommandAction (bool isEnabled, Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse )
    {      
        _isEnabled = isEnabled;
        _methodOfCheckResponse = methodOfCheckResponse;
    }
    /// <summary>
    /// 模板方法：将传入的报文发送给串口，并使用传入的校验器校验下位机 的回复报文，并返回下位机回复的报文
    /// </summary>
    /// <param name="methodOfGetPacket">获取报文的方法</param>
    /// <param name="awaitData">是否需要等待下位机回复该指令，当下位机不回复指令的时候需要设为false</param>
    /// <returns></returns>
    public  OperateResult<byte[ ]> Action ( OperateResult<byte[ ]> methodOfGetPacket , bool awaitData=true)
    {
        try
        {           
            //协议支持但是本设备未安装/激活该功能，则返回失败的结果
            if ( _isEnabled==false )
            {
                return new OperateResult<byte[ ]>(StringResources.Language.NotEnabledModule);
            }
            //如果报文获取失败，则直接上抛失败的结果
            if (!methodOfGetPacket. IsSuccess || methodOfGetPacket. Content == null )
            {
                return methodOfGetPacket;
            }

            //报文获取成功则：调用串口核心交互流程的方法：发送、接收、校验
            var CheckRes = _methodOfCheckResponse. Invoke ( methodOfGetPacket. Content ,awaitData);

            //无论结果成功与否均返回结果，调用者需要判断结果是否成功。
            return CheckRes;
        }
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( "执行命令出错了。" + ex );
        }
    }
}
