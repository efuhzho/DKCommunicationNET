namespace DKCommunicationNET.Protocols.Hex81.Encoders;

/// <summary>
/// 系统设置报文创建类
/// </summary>
internal class Hex81Encoder_Settings : IEncoder_Settings
{
    private readonly Hex81EncodeHelper _PBHelper;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    public Hex81Encoder_Settings(ushort id)
    {
        _PBHelper = new Hex81EncodeHelper(id);

    }

    public OperateResult<byte[ ]> Packet_HandShake ( )
    {
        return OperateResult.CreateSuccessResult ( Hex81Information. HandShakePacket );
    }

    public OperateResult<byte[ ]> Packet_SetBaudRate(ushort baudRate)
    {
        //不具备此功能
        return new OperateResult<byte[ ]>(StringResources.Language.NotSupportedFunction);
    }

    public OperateResult<byte[ ]> Packet_SetDeviceInfo(char[ ] password, byte id, string sn)
    {
        //不具备此功能
        return new OperateResult<byte[ ]>(StringResources.Language.NotSupportedFunction);
    }

    /// <summary>
    /// 创建报文：设置显示页面
    /// </summary>
    /// <param name="displayPage">显示页面</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetDisplayPage(byte displayPage)
    {
        byte[ ] data = new byte[displayPage];
        return _PBHelper.EncodeHelper(Hex81Information.SetDisplayPage, Hex81Information.SetDisplayPage_Length, data);
    }

    /// <summary>
    /// 创建报文：设置系统模式
    /// </summary>
    /// <param name="systemMdoe">系统模式</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetSystemMode(byte systemMdoe)
    {
        byte[ ] data = new byte[systemMdoe];
        return _PBHelper.EncodeHelper(Hex81Information.SetSystemMode, Hex81Information.SetSystemMode_Length, data);
    }
}
