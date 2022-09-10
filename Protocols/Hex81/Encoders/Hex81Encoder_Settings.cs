namespace DKCommunicationNET. Protocols. Hex81. Encoders;

/// <summary>
/// 系统设置报文创建类
/// </summary>
internal class Hex81Encoder_Settings : IEncoder_Settings
{
    private readonly Hex81EncodeHelper _encodeHelper;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    public Hex81Encoder_Settings ( ushort id )
    {
        _encodeHelper = new Hex81EncodeHelper ( id );
    }

    public OperateResult<byte[ ]> Packet_HandShake ( )
    {
        return _encodeHelper. EncodeHelper ( Hex81. HandShake );
    }

    /// <summary>
    /// 创建报文：设置显示页面
    /// </summary>
    /// <param name="displayPage">显示页面</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetDisplayPage ( Enum displayPage )
    {
        byte[ ] data = new byte[( byte ) ( DisplayPage ) displayPage];
        return _encodeHelper. EncodeHelper ( Hex81. SetDisplayPage , Hex81. SetDisplayPage_Length , data );
    }

    /// <summary>
    /// 创建报文：设置系统模式
    /// </summary>
    /// <param name="systemMdoe">系统模式</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetSystemMode ( Enum systemMdoe )
    {
        byte[ ] data = new byte[( byte ) ( SystemModes ) systemMdoe];
        return _encodeHelper. EncodeHelper ( Hex81. SetSystemMode , Hex81. SetSystemMode_Length , data );
    }

    /****************************************************************************************************************************************
     *  2022年9月8日   17点04分  
     *
     *  以下方法均为Hex81协议不支持的功能。
     *
     *  不可删除的接口虚拟实现。
     ***************************************************************************************************************************************/

    #region 《不支持的功能

    public OperateResult<byte[ ]> Packet_SetBaudRate ( ushort baudRate )
    {
        //不具备此功能
        return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedFunction );
    }

    public OperateResult<byte[ ]> Packet_SetDeviceInfo ( char[ ] password , byte id , string sn )
    {
        //不具备此功能
        return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedFunction );
    }
    #endregion 不支持的功能》
}
