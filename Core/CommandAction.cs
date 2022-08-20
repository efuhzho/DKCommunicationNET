using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Core;

internal class CommandAction
{
    /// <summary>
    /// 模板方法：1.获取报文，2.发送报文给串口并检查下位机回复报文的有效性
    /// </summary>
    /// <param name="methodOfGetPacket">获取指定报文的方法</param>
    /// <param name="methodOfCheckResponse">发送并接收并且检查回复报文有效性的方法</param>
    /// <returns>是否成功的操作结果</returns>
    public static OperateResult<byte[ ]> Action ( Func< OperateResult<byte[ ]>> methodOfGetPacket, Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse)
    {
        //获取命令报文
        OperateResult<byte[ ]> packetRes = methodOfGetPacket. Invoke ( );

        //报文获取失败则直接上抛失败的结果
        if (!packetRes.IsSuccess )
        {
            return packetRes;
        }

        //报文获取成功则：调用串口核心交互流程的方法：发送、接收、校验
        OperateResult<byte[ ]> CheckRes = methodOfCheckResponse. Invoke ( packetRes. Content );

        //无论结果成功与否均上抛结果，调用者需要判断结果是否成功。
        return CheckRes;
    }
}
