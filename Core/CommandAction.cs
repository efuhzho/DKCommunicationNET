namespace DKCommunicationNET. Core;

internal class CommandAction
{
    /// <summary>
    /// 模板方法：将传入的报文发送给串口，并使用传入的校验器校验下位机 的回复报文，并返回下位机回复的报文
    /// </summary>
    /// <param name="methodOfGetPacket">获取报文的方法</param>
    /// <param name="methodOfCheckResponse">发送报文并校验下位机回复的报文有效性的委托方法【串口交互核心】</param>
    /// <param name="awaitData"></param>
    /// <returns></returns>
    public static OperateResult<byte[ ]> Action ( OperateResult<byte[ ]> methodOfGetPacket , Func<byte[ ] , bool,OperateResult<byte[ ]>> methodOfCheckResponse ,bool awaitData=true)
    {
        try
        {
            //报文获取失败则直接上抛失败的结果
            if ( !methodOfGetPacket. IsSuccess || methodOfGetPacket. Content == null )
            {
                return methodOfGetPacket;
            }

            //报文获取成功则：调用串口核心交互流程的方法：发送、接收、校验
            var CheckRes = methodOfCheckResponse. Invoke ( methodOfGetPacket. Content ,awaitData);

            //无论结果成功与否均返回结果，调用者需要判断结果是否成功。
            return CheckRes;
        }
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule + ex );
        }
    }
}
