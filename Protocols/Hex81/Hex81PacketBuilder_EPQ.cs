using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// 电能模块报文创建类
/// </summary>
internal class Hex81PacketBuilder_EPQ : IPacketBuilder_EPQ
{
    /// <summary>
    /// 设备ID
    /// </summary>
    readonly ushort _id;

    private readonly IByteTransform _transform;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <param name="byteTransform"></param>
    public Hex81PacketBuilder_EPQ ( ushort id ,IByteTransform byteTransform )
    {
        _id = id;
        _transform = byteTransform;
    }

    public OperateResult<byte[ ]> Packet_ReadData ( )
    {
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_EPQ , _id );
    }

    public OperateResult<byte[ ]> Packet_SetElectricity ( byte electricityType , float meterPConst , float meterQConst , float sourcePConst , float sourceQConst , uint meterDIV , uint meterRounds  )
    {
        byte[ ] data = new byte[25];
        data[0] = electricityType;
        _transform. TransByte ( meterPConst ). CopyTo ( data , 1 );
        _transform. TransByte ( meterQConst ). CopyTo ( data , 5 );
        _transform. TransByte ( sourcePConst ). CopyTo ( data , 9 );
        _transform. TransByte ( sourceQConst ). CopyTo ( data , 13 );
        BitConverter. GetBytes ( meterDIV ). CopyTo ( data , 17 );
        BitConverter. GetBytes ( meterRounds ). CopyTo ( data , 21 );

        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetElectricity , Hex81Information. SetElectricity_Length , _id );
    }
}
