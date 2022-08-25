using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Channels;
using System. Threading. Tasks;
using DKCommunicationNET. Protocols. Hex81;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 报文创建助手
/// </summary>
internal interface IPacketBuilderHelper
{
    //TODO 添加注释
    OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort commandLength , ushort id );
    OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort commandLength , byte[ ] data , ushort id );
    public OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort id );
}

internal interface IPacketBuilder_Settings
{
    /// <summary>
    /// 设置系统模式
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> SetSystemMode ( byte systemMode );

    /// <summary>
    /// 设置显示页面
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> SetDisplayPage ( byte displayPage );
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

    /// <summary>
    /// 【Hex81】创建报文：设置闭环模式
    /// </summary>
    /// <param name="closeLoopMode"></param>
    /// <param name="harmonicMode"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetClosedLoop ( byte closeLoopMode , byte harmonicMode );

    /// <summary>
    /// 【报文长度不可超过256】创建报文：设置谐波输出参数；如果谐波组为null,则指令为清空谐波
    /// </summary>
    /// <param name="channels">要设置的谐波通道</param>
    /// <param name="harmonics">谐波组,如果谐波组为null,则指令为清空谐波</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetHarmonics ( byte channels , HarmonicArgs[ ]? harmonics = null );

    /// <summary>
    /// 创建报文：设置有功功率
    /// </summary>
    /// <param name="channel">要设置的有功功率通道</param>
    /// <param name="p">要设置的有功功率值</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetWattPower ( byte channel , float p );

    /// <summary>
    ///  创建报文：设置无功功率
    /// </summary>
    /// <param name="channel">要设置的无功功率通道</param>
    /// <param name="q">要设置的无功功率值</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetWattLessPower ( byte channel , float q );

    /// <summary>
    /// 创建报文：读取交流标准表测量值/标准源输出值
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_ReadData_ACS ( );

    /// <summary>
    /// 读取输出状态：Flag=0表示输出稳定，Flag=1表示输出未稳定。：读标准源输出状态
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_GetReadDataStatus_ACS ( );
}
internal interface IPacketBuilder_ACM
{
  

}

internal interface IPacketBuilder_DCS
{
}

/// <summary>
/// 直流表模块报文创建类
/// </summary>
internal interface IPacketBuilder_DCM
{
    /// <summary>
    /// 设置直流表量程和测量类型
    /// </summary>
    /// <param name="rangeIndex">量程索引值</param>
    /// <param name="type">测量类型：0-直流电压；1-直流电流；2-纹波电压；3-纹波电流。</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetRange_DCM ( byte rangeIndex , byte type );

    /// <summary>
    /// 读取直流表数据
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> ReadData_DCM ( );
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