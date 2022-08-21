using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 报文创建助手
/// </summary>
internal interface IPacketBuilderHelper
{
    OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort commandLength , ushort id );
    OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort commandLength , byte[ ] data , ushort id );
}

internal interface IPacketsBuilder_ACS
{
    /// <summary>
    /// 创建【获取交流源档位信息】的报文
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_GetRanges ( );

    /// <summary>
    /// 创建报文：设置交流源档位
    /// </summary>
    /// <param name="rangeIndexOfACU">电压档位索引值</param>
    /// <param name="rangeIndexOfACI">电流档位索引值</param>
    /// <param name="rangeIndexOfIP">保护电流档位索引值</param>  
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 );

    /// <summary>
    /// 创建【设置交流源幅度】的报文
    /// </summary>
    /// <param name="UA">要设定的输出值：UA</param>
    /// <param name="UB">要设定的输出值：UB</param>
    /// <param name="UC">要设定的输出值：UC</param>
    /// <param name="IA">要设定的输出值：IA</param>
    /// <param name="IB">要设定的输出值：IB</param>
    /// <param name="IC">要设定的输出值：IC</param>
    /// <param name="IPA">要设定的输出值：IPA</param>
    /// <param name="IPB">要设定的输出值：IPB</param>
    /// <param name="IPC">要设定的输出值：IPC</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPA = 0 , float IPB = 0 , float IPC = 0 );

    /// <summary>
    /// 创建【打开交流源】的报文
    /// </summary>
    /// <returns></returns>
    internal OperateResult<byte[ ]> Packet_Open ( );

    /// <summary>
    /// 创建【关闭交流源】的报文
    /// </summary>
    /// <returns></returns>
    internal OperateResult<byte[ ]> Packet_Close ( );

    /// <summary>
    /// 创建报文：设置相位
    /// </summary>
    /// <param name="PhaseUa"></param>
    /// <param name="PhaseUb"></param>
    /// <param name="PhaseUc"></param>
    /// <param name="PhaseIa"></param>
    /// <param name="PhaseIb"></param>
    /// <param name="PhaseIc"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );

    /// <summary>
    /// 创建报文：设置频率
    /// </summary>
    /// <param name="FreqOfAll">输出频率</param>
    /// <param name="FreqOfC">【支持双频时有效】C相频率</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetFrequency ( float FreqOfAll , float FreqOfC = 0 );

    /// <summary>
    /// 创建报文：设置接线方式
    /// </summary>
    /// <param name="wireMode">接线方式</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetWireMode ( byte wireMode );
}
internal interface IPacketBuilder_ACM
{
}

internal interface IPacketBuilder_DCS
{
}

internal interface IPacketBuilder_DCM
{
}

internal interface IPacketBuilder_IO
{
}

internal interface IPacketBuilder_PQ
{
}
internal interface IHandShake
{

}