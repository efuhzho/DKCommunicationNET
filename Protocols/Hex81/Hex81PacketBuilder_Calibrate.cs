using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// 创建校准报文类
/// </summary>
internal class Hex81PacketBuilder_Calibrate : IPacketBuilder_Calibrate
{
    private readonly ushort _id;
    private readonly IByteTransform _transform;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <param name="byteTransform">数据转换规则</param>
    public Hex81PacketBuilder_Calibrate ( ushort id , IByteTransform byteTransform )
    {
        _id = id;
        _transform = byteTransform;
    }
    public OperateResult<byte[ ]> Packet_ClearData ( byte calibrateType , byte uRangeIndex , byte iRangeIndex )
    {
        byte[ ] data = new byte[3] { calibrateType , uRangeIndex , iRangeIndex };
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_ClearData , Hex81Information. Calibrate_ClearDataLength , data , _id );
    }

    public OperateResult<byte[ ]> Packet_DoAC ( byte uRangeIndex , byte iRangeIndex , byte calibrateLevel , float mUA , float mUB , float mUC , float mIA , float mIB , float mIC )
    {
        byte[ ] data = new byte[27];
        data[0] = uRangeIndex;
        data[1] = iRangeIndex;
        data[2] = calibrateLevel;
        _transform. TransByte ( mUA ). CopyTo ( data , 3 );
        _transform. TransByte ( mUB ). CopyTo ( data , 7 );
        _transform. TransByte ( mUC ). CopyTo ( data , 11 );
        _transform. TransByte ( mIA ). CopyTo ( data , 15 );
        _transform. TransByte ( mIB ). CopyTo ( data , 19 );
        _transform. TransByte ( mIC ). CopyTo ( data , 23 );
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_DoAC , Hex81Information. Calibrate_DoACLength , data , _id );
    }

    public OperateResult<byte[ ]> Packet_DoACMeter ( byte uRangeIndex , byte iRangeIndex , byte calibrateLevel , float UA , float UB , float UC , float IA , float IB , float IC )
    {
        byte[ ] data = new byte[27];
        data[0] = uRangeIndex;
        data[1] = iRangeIndex;
        data[2] = calibrateLevel;
        _transform. TransByte ( UA ). CopyTo ( data , 3 );
        _transform. TransByte ( UB ). CopyTo ( data , 7 );
        _transform. TransByte ( UC ). CopyTo ( data , 11 );
        _transform. TransByte ( IA ). CopyTo ( data , 15 );
        _transform. TransByte ( IB ). CopyTo ( data , 19 );
        _transform. TransByte ( IC ). CopyTo ( data , 23 );
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_DoACMeter , Hex81Information. Calibrate_DoACMeterlength , data , _id );
    }

    public OperateResult<byte[ ]> Packet_DoDC ( byte dCSourceType , byte rangeIndex , byte calibrateLevel , float mDCAmplitude )
    {
        byte[ ] data = new byte[7];
        data[0] = dCSourceType;
        data[1] = rangeIndex;
        data[2] = calibrateLevel;
        _transform. TransByte ( mDCAmplitude ). CopyTo ( data , 3 );
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_DoDC , Hex81Information. Calibrate_DoDClength , data , _id );
    }

    public OperateResult<byte[ ]> Packet_DoDCMeter ( byte dCSourceType , byte rangeIndex , byte calibrateLevel , float sDCAmplitude )
    {
        byte[ ] data = new byte[7];
        data[0] = dCSourceType;
        data[1] = rangeIndex;
        data[2] = calibrateLevel;
        _transform. TransByte ( sDCAmplitude ). CopyTo ( data , 3 );
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_DoDCMeter , Hex81Information. Calibrate_DoDCMeterLength , data , _id );
    }

    public OperateResult<byte[ ]> Packet_Save ( byte uRangeIndex , byte iRangeIndex , byte calibrateLevel )
    {
        byte[ ] data = new byte[3] { uRangeIndex , iRangeIndex , calibrateLevel };
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_Save , Hex81Information. Calibrate_SaveLength , data , _id );
    }

    public OperateResult<byte[ ]> Packet_SwitchACPoint ( byte uRangeIndex , byte iRangeIndex , byte calibrateLevel , float sUA , float sUB , float sUC , float sIA , float sIB , float sIC )
    {
        byte[ ] data = new byte[27];
        data[0] = uRangeIndex;
        data[1] = iRangeIndex;
        data[2] = calibrateLevel;
        _transform. TransByte ( sUA ). CopyTo ( data , 3 );
        _transform. TransByte ( sUB ). CopyTo ( data , 7 );
        _transform. TransByte ( sUC ). CopyTo ( data , 11 );
        _transform. TransByte ( sIA ). CopyTo ( data , 15 );
        _transform. TransByte ( sIB ). CopyTo ( data , 19 );
        _transform. TransByte ( sIC ). CopyTo ( data , 23 );
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_SwitchACPoint , Hex81Information. Calibrate_SwitchACPointLength , data , _id );
    }

    public OperateResult<byte[ ]> Packet_SwitchACRange ( byte uRangeIndex , byte iRangeIndex )
    {
        byte[ ] data = new byte[2] { uRangeIndex , iRangeIndex };
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_SwitchACRange , Hex81Information. Calibrate_SwitchACRangeLength , data , _id );
    }

    public OperateResult<byte[ ]> Packet_SwitchDCPoint ( byte dCSourceType , byte rangeIndex , byte calibrateLevel , float sDCAmplitude )
    {
        byte[ ] data = new byte[7];
        data[0] = dCSourceType;
        data[1] = rangeIndex;
        data[2] = calibrateLevel;
        _transform. TransByte ( sDCAmplitude ). CopyTo ( data , 3 );
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Calibrate_SwitchDCPoint , Hex81Information. Calibrate_SwitchDCPointLength , data , _id );
    }
}
