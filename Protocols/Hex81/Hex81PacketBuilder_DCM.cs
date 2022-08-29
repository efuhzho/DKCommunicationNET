using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// <inheritdoc cref="IPacketBuilder_DCM"/>
/// </summary>
internal class Hex81PacketBuilder_DCM : IPacketBuilder_DCM
{
    /// <summary>
    /// 设备ID
    /// </summary>
    private readonly ushort _id;
    public Hex81PacketBuilder_DCM ( ushort id )
    {
        _id = id;
    }

    public bool IsMultiChannel { get; }

    public OperateResult<byte[ ]> Packet_GetRanges ( )
    {
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetRanges_DCS , _id );
    }

    public OperateResult<byte[ ]> Packet_ReadData ( )
    {
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_DCM , _id );
    }

    public OperateResult<byte[ ]> Packet_SetRange_DCMI ( byte rangeIndex )
    {
        return Packet_SetRange ( rangeIndex , MeasureType_DCM. DCM_CurrentRipple );
    }

    public OperateResult<byte[ ]> Packet_SetRange_DCMI_Ripple ( byte rangeIndex )
    {
        return Packet_SetRange(rangeIndex,MeasureType_DCM.DCM_CurrentRipple);
    }

    public OperateResult<byte[ ]> Packet_SetRange_DCMU ( byte rangeIndex )
    {
        return Packet_SetRange ( rangeIndex , MeasureType_DCM. DCM_Voltage );
    }

    public OperateResult<byte[ ]> Packet_SetRange_DCMU_Ripple ( byte rangeIndex )
    {
        return Packet_SetRange(rangeIndex , MeasureType_DCM.DCM_VoltageRipple );
    }

    /// <summary>
    /// 设置量程的原始方法
    /// </summary>
    /// <param name="rangeIndex"></param>
    /// <param name="type"></param>
    /// <returns></returns>

    private OperateResult<byte[ ]> Packet_SetRange ( byte rangeIndex , MeasureType_DCM type )
    {
        byte[ ] data = new byte[ ] { rangeIndex , ( byte ) type };
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetRange_DCM , Hex81Information. SetRange_DCM_Length , _id );
    }
}
