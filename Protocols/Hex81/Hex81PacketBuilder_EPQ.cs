using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols. Hex5A;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// 电能模块报文创建类
/// </summary>
internal class Hex81PacketBuilder_EPQ : IPacketBuilder_EPQ
{
    /// <summary>
    /// 设备ID
    /// </summary>
    private readonly Hex5APacketBuilderHelper _PBHelper;


    private readonly IByteTransform _transform;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <param name="byteTransform"></param>
    public Hex81PacketBuilder_EPQ ( ushort id , IByteTransform byteTransform )
    {
        _PBHelper = new Hex5APacketBuilderHelper ( id );

        _transform = byteTransform;
    }

    #region 属性
    /// <summary>
    /// 表有功常数
    /// </summary>
    public float Const_PM { get; set; } = 3600000;

    /// <summary>
    /// 表无功常数
    /// </summary>
    public float Const_QM { get; set; } = 3600000;

    /// <summary>
    /// 源有功常数
    /// </summary>
    public float Const_PS { get; set; } = 3600000;

    /// <summary>
    /// 源无功常数
    /// </summary>
    public float Const_QS { get; set; } = 3600000;

    /// <summary>
    /// （表）分频系数
    /// </summary>
    public uint DIV { get; set; } = 1;

    /// <summary>
    /// （表）设置的校验圈数
    /// </summary>
    public uint Rounds { get; set; } = 10;
    #endregion

    public OperateResult<byte[ ]> Packet_ReadData ( Channels_EPQ Channels = Channels_EPQ. Channel1 )
    {
        return _PBHelper. PacketShellBuilder ( Hex81Information. ReadData_EPQ );
    }

    public OperateResult<byte[ ]> Packet_SetConst_PS ( float Const_PS )
    {
        return Packet_SetElectricity ( ElectricityType. P , Const_PM , Const_QM , Const_PS , Const_QS , DIV , Rounds );
    }

    public OperateResult<byte[ ]> Packet_SetConst_QS ( float Const_QS )
    {
        return Packet_SetElectricity ( ElectricityType. Q , Const_PM , Const_QM , Const_PS , Const_QS , DIV , Rounds );
    }

    public OperateResult<byte[ ]> Packet_StartTest_P ( float Const_PM , uint Rounds = 10 , uint DIV = 1 )
    {
        return Packet_SetElectricity ( ElectricityType. P , Const_PM , Const_QM , Const_PS , Const_QS , DIV , Rounds );
    }

    public OperateResult<byte[ ]> Packet_StartTest_Q ( float Const_QM , uint Rounds = 10 , uint DIV = 1 )
    {
        return Packet_SetElectricity ( ElectricityType. Q , Const_PM , Const_QM , Const_PS , Const_QS , DIV , Rounds );
    }

    private OperateResult<byte[ ]> Packet_SetElectricity ( ElectricityType electricityType , float Const_PM , float Const_QM , float Const_PS , float Const_QS , uint DIV , uint Rounds )
    {
        byte[ ] data = new byte[25];
        data[0] = ( byte ) electricityType;
        _transform. TransByte ( Const_PM ). CopyTo ( data , 1 );
        _transform. TransByte ( Const_QM ). CopyTo ( data , 5 );
        _transform. TransByte ( Const_PS ). CopyTo ( data , 9 );
        _transform. TransByte ( Const_QS ). CopyTo ( data , 13 );
        BitConverter. GetBytes ( DIV ). CopyTo ( data , 17 );
        BitConverter. GetBytes ( Rounds ). CopyTo ( data , 21 );

        return _PBHelper. PacketShellBuilder ( Hex81Information. SetElectricity , Hex81Information. SetElectricity_Length ,data);
    }
}
