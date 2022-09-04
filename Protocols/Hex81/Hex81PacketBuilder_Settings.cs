using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// 系统设置报文创建类
/// </summary>
internal class Hex81PacketBuilder_Settings : IPacketBuilder_Settings
{
    /// <summary>
    /// 设备ID
    /// </summary>
    private ushort _id;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    public Hex81PacketBuilder_Settings (ushort id )
    {
        _id = id;
    }

    /// <summary>
    /// 创建报文：设置显示页面
    /// </summary>
    /// <param name="displayPage">显示页面</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetDisplayPage (byte displayPage )
    {
        byte[] data = new byte[ displayPage ];
        return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetDisplayPage , Hex81Information. SetDisplayPage_Length ,data,_id);
    }

    /// <summary>
    /// 创建报文：设置系统模式
    /// </summary>
    /// <param name="systemMdoe">系统模式</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetSystemMode (byte systemMdoe )
    {
        byte[ ] data = new byte[systemMdoe];
        return Hex81PacketBuilderHelper.Instance.PacketShellBuilder(Hex81Information.SetSystemMode,Hex81Information.SetSystemMode_Length ,data,_id);
    }
}
