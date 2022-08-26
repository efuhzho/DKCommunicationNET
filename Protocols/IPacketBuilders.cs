using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Channels;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols. Hex81;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 报文创建助手接口
/// </summary>
internal interface IPacketBuilderHelper
{
    //TODO 添加注释
    OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort commandLength , ushort id );
    OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort commandLength , byte[ ] data , ushort id );
    public OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort id );
}

/// <summary>
/// 系统设置报文创建类接口
/// </summary>
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

/// <summary>
/// 交流源报文创建类接口
/// </summary>
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
    public OperateResult<byte[ ]> Packet_Open ( );

    /// <summary>
    /// 创建【关闭交流源】的报文
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_Stop ( );

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
}

/// <summary>
/// 交流表报文创建类接口
/// </summary>
internal interface IPacketBuilder_ACM
{
    /// <summary>
    /// 创建报文：读取交流标准表测量值/标准源输出值
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_ReadData ( );

    /// <summary>
    /// 创建报文：读取输出状态：Flag=0表示输出稳定，Flag=1表示输出未稳定。：读标准源输出状态
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_GetReadDataStatus ( );
}

/// <summary>
/// 直流源报文创建类接口
/// </summary>
internal interface IPacketBuilder_DCS
{
    /// <summary>
    /// 关闭直流源
    /// </summary>
    /// <param name="type"><inheritdoc cref="Packet_SetRange(byte, byte)"/></param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Stop (byte? type=null );

    /// <summary>
    /// 打开直流源
    /// </summary>
    /// <param name="type"><inheritdoc cref="Packet_SetRange(byte, byte)"/></param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Open (byte? type=null );
    /// <summary>
    /// 创建报文：设置直流源档位：【indexOfRange=0xFF时为自动档位，支持自动换挡模式时有效】
    /// </summary>
    /// <param name="indexOfRange">档位索引值：indexOfRange=0xFF时为自动档位，支持自动换挡模式时有效</param>
    /// <param name="type">输出类型：‘U'=直流电压；’I‘=直流电流；’R‘=直流电阻</param>
    /// <returns></returns>
    OperateResult<byte[]> Packet_SetRange (byte indexOfRange,byte type );

    /// <summary>
    /// 创建报文：设置直流源幅度
    /// </summary>
    /// <param name="indexOfRange"><inheritdoc cref="Packet_SetRange(byte, byte)"/></param>
    /// <param name="amplitude">要设定的幅值</param>
    /// <param name="type"><inheritdoc cref="Packet_SetRange(byte, byte)"/></param>
    /// <param name="byteTransform">数据转换规则</param>
    /// <returns></returns>
    OperateResult<byte[]> Packet_SetAmplitude ( byte indexOfRange,float amplitude , byte type , IByteTransform byteTransform );

    /// <summary>
    /// 创建报文：读取直流源当前输出值
    /// </summary>
    /// <param name="type"><inheritdoc cref="Packet_SetRange(byte, byte)"/></param>
    /// <returns></returns>
    OperateResult<byte[]> Packet_ReadData ( byte? type =null);

    /// <summary>
    /// 创建报文：获取直流源档位信息
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_GetRanges (  );

}

/// <summary>
/// 直流表模块报文创建类接口
/// </summary>
internal interface IPacketBuilder_DCM
{
    /// <summary>
    /// 设置直流表量程和测量类型
    /// </summary>
    /// <param name="rangeIndex">量程索引值</param>
    /// <param name="type">测量类型：0-直流电压；1-直流电流；2-纹波电压；3-纹波电流。</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetRange ( byte rangeIndex , byte type );

    /// <summary>
    /// 读取直流表数据
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_ReadData ( );

    /// <summary>
    /// 获取直流表档位信息
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_GetRanges ( );
}

/// <summary>
/// 开关量模块报文创建类接口
/// </summary>
internal interface IPacketBuilder_IO
{
}

/// <summary>
/// 电能模块报文创建类接口
/// </summary>
internal interface IPacketBuilder_PQ
{
    /// <summary>
    /// 创建报文：读取电能校验误差
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_ReadData ( );

    /// <summary>
    /// 创建报文：设置电能校验参数并启动电能校验
    /// </summary>
    /// <param name="electricityType">电能类型：’‘=有功；’‘=无功</param>
    /// <param name="meterPConst">【表】有功脉冲常数</param>
    /// <param name="meterQConst">【表】无功脉冲常数</param>
    /// <param name="sourcePConst">有功脉冲常数</param>
    /// <param name="sourceQConst">无功脉冲常数</param>
    /// <param name="meterDIV">【表】分频系数</param>
    /// <param name="meterRounds">【表】校验圈数</param>
    /// <param name="byteTransform">数据转换规则</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetElectricity( byte electricityType , float meterPConst , float meterQConst , float sourcePConst , float sourceQConst , uint meterDIV , uint meterRounds , IByteTransform byteTransform );
}

