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
    public Hex81PacketBuilder_DCM ( ushort id)
    {
        _id=id;
    }

    public OperateResult<byte[ ]> Packet_GetRanges ( )
    {
        return Hex81PacketBuilderHelper.Instance.PacketShellBuilder(Hex81Information.GetRanges_DCS, _id);
    }

    public OperateResult<byte[ ]> Packet_ReadData ( )
    {
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_DCM , _id );
    }

    public OperateResult<byte[ ]> Packet_SetRange ( byte rangeIndex , byte type )
    {
        byte[ ] data = new byte[ ] { rangeIndex , type };
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetRange_DCM , Hex81Information. SetRange_DCM_Length , _id );
    }
}
