using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Protocols. Hex5A;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// <inheritdoc cref="IPacketBuilder_DCM"/>
/// </summary>
internal class Hex81PacketBuilder_DCM : IPacketBuilder_DCM
{
    private readonly Hex5APacketBuilderHelper _PBHelper;

    public Hex81PacketBuilder_DCM ( ushort id )
    {
        _PBHelper = new Hex5APacketBuilderHelper ( id );

    }

    public bool IsMultiChannel { get; }

    public OperateResult<byte[ ]> Packet_GetRanges ( )
    {
        return _PBHelper. PacketShellBuilder ( Hex81Information. GetRanges_DCS );
    }

    public OperateResult<byte[ ]> Packet_ReadData ( )
    {
        return _PBHelper. PacketShellBuilder ( Hex81Information. ReadData_DCM );
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
        return _PBHelper. PacketShellBuilder ( Hex81Information. SetRange_DCM , Hex81Information. SetRange_DCM_Length ,data );
    }
}
