namespace DKCommunicationNET. Protocols. Hex81. Encoders;

/// <summary>
/// <inheritdoc cref="IEncoder_DCM"/>
/// </summary>
internal class Hex81Encoder_DCM : IEncoder_DCM
{
    private readonly Hex81EncodeHelper _encoderHelper;

    public Hex81Encoder_DCM(ushort id)
    {
        _encoderHelper = new Hex81EncodeHelper(id);
    }

    public bool IsMultiChannel { get; }

    #region 《读档位列表

    public OperateResult<byte[ ]> Packet_GetRanges()
    {
        return _encoderHelper.EncodeHelper(Hex81Information.GetRanges_DCS);
    }

    #endregion 读档位列表》

    #region 《读数据

    public OperateResult<byte[ ]> Packet_ReadData()
    {
        return _encoderHelper.EncodeHelper(Hex81Information.ReadData_DCM);
    }

    #endregion 读数据》

    #region 《设置档位

    public OperateResult<byte[ ]> Packet_SetRange_DCMI(byte rangeIndex)
    {
        return Packet_SetRange(rangeIndex, MeasureType_DCM.DCM_CurrentRipple);
    }

    public OperateResult<byte[ ]> Packet_SetRange_DCMI_Ripple(byte rangeIndex)
    {
        return Packet_SetRange(rangeIndex, MeasureType_DCM.DCM_CurrentRipple);
    }

    public OperateResult<byte[ ]> Packet_SetRange_DCMU(byte rangeIndex)
    {
        return Packet_SetRange(rangeIndex, MeasureType_DCM.DCM_Voltage);
    }

    public OperateResult<byte[ ]> Packet_SetRange_DCMU_Ripple(byte rangeIndex)
    {
        return Packet_SetRange(rangeIndex, MeasureType_DCM.DCM_VoltageRipple);
    }

    #endregion 设置档位》

    #region 《私有原始方法

    /// <summary>
    /// 设置量程的原始方法
    /// </summary>
    /// <param name="rangeIndex"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private OperateResult<byte[ ]> Packet_SetRange(byte rangeIndex, MeasureType_DCM type)
    {
        byte[ ] data = new byte[ ] { rangeIndex, (byte)type };
        return _encoderHelper.EncodeHelper(Hex81Information.SetRange_DCM, Hex81Information.SetRange_DCM_Length, data);
    }

    #endregion 私有原始方法》
}
