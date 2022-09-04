using System. ComponentModel;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex5A;

/// <summary>
/// 对时功能报文创建
/// </summary>
internal class Hex5APacketBuilder_PPS : IPacketBuilder_PPS
{
    private readonly Hex5APacketBuilderHelper _PBHelper;
    public Hex5APacketBuilder_PPS ( ushort id )
    {
        _PBHelper = new Hex5APacketBuilderHelper ( id );
    }

    public OperateResult<byte[ ]> CompareTime_Auto ( Enum Type_CompareTime , short timeZones = 8 )
    {
        return CompareTime ( ( Type_CompareTime ) Type_CompareTime , DateTime. Now , timeZones );
    }


    public OperateResult<byte[ ]> CompareTime_Manual ( Enum Type_CompareTime , DateTime dateTime , short timeZones = 8 )
    {
        return CompareTime ( ( Type_CompareTime ) Type_CompareTime , dateTime , timeZones );
    }

    public OperateResult<byte[ ]> ReadData_PPS ( )
    {
        return _PBHelper. PacketShellBuilder ( Hex5AInformation. ReadData_PPS );
    }

    #region 私有方法

    private OperateResult<byte[ ]> CompareTime ( Type_CompareTime type , DateTime dateTime , short timeZones = 8 )
    {
        byte[ ] data = new byte[6];
        data[0] = ( byte ) type;
        BitConverter. GetBytes ( dateTime. Ticks ). CopyTo ( data , 1 );
        data[5] = ( byte ) timeZones;
        return _PBHelper. PacketShellBuilder ( Hex5AInformation. CompareTime , Hex5AInformation. CompareTime_L , data );
    }
    #endregion


}
