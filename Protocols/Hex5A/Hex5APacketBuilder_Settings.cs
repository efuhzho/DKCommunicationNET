using System;
using System. Collections. Generic;
using System. Linq;
using System. Security. Cryptography;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex5A;


internal class Hex5APacketBuilder_Settings:IPacketBuilder_Settings
{
    private readonly ushort _id;
    private readonly IByteTransform _byteTransform;
    public Hex5APacketBuilder_Settings ( ushort id , IByteTransform byteTransform )
    {
        _id = id;
        _byteTransform = byteTransform;
    }

    public OperateResult<byte[ ]> Packet_SetDeviceInfo ( char[ ] password , byte id , string sn )
    {
        byte[ ] data = new byte[31];

        //密码
        for ( int i = 0 ; i < 6 ; i++ )
        {
            data[i] = BitConverter. GetBytes ( password[i] )[0];
        }
        data[6] = id;

        //序列号
        var snBytes = _byteTransform. TransByte ( sn , System. Text. Encoding. ASCII );

        //序列号长度判断
        if ( snBytes. Length < 25 )
        {
            snBytes. CopyTo ( data , 7 );
        }
        else return new OperateResult<byte[ ]> ( "设备编号长度超限" );

        //返回结果
        return Hex5APacketBuilderHelper. Instance. PacketShellBuilder ( Hex5AInformation. SetDeviceInfo , Hex5AInformation. SetDeviceInfo_L , data , _id );
    }


    public OperateResult<byte[ ]> Packet_SetBaudRate ( ushort baudRate )
    {
        byte[ ] data = new byte[2];
        BitConverter. GetBytes ( baudRate ). CopyTo ( data , 0 );
        return Hex5APacketBuilderHelper. Instance. PacketShellBuilder ( Hex5AInformation. SetBaudRate , Hex5AInformation. SetBaudRate_L , data , _id );
    }

    public OperateResult<byte[ ]> Packet_SetSystemMode ( byte systemMode )
    {
        throw new NotImplementedException ( );

    }

    public OperateResult<byte[ ]> Packet_SetDisplayPage ( byte displayPage )
    {
        throw new NotImplementedException ( );
    }

   
}
