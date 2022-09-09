using DKCommunicationNET.Core;

namespace DKCommunicationNET.Protocols.Hex5A.Encoders;

internal class Hex5AEncoder_ACS : IEncoder_ACS
{
    private readonly Hex5AEncodeHelper _encodeHelper;
    private readonly IByteTransform _transform;


    public Hex5AEncoder_ACS(ushort id, IByteTransform byteTransform)
    {
        _encodeHelper = new Hex5AEncodeHelper(id);
        _transform = byteTransform;
    }

    public OperateResult<byte[ ]> Packet_GetRanges()
    {
        byte[ ] data = new byte[1] { (byte)Type_Model.ACS };
        return _encodeHelper.EncodeHelper(Hex5AInformation.GetRanges_ACS, Hex5AInformation.GetRanges_ACS_Len, data);
    }

    public OperateResult<byte[ ]> Packet_Open()
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[1];
        args[0] = new SetStandardSourceArgs(Channels.UIabc, 0);
        return SetArgs_ACS(Type_SetStandardSource.Amplitude, args);
    }

    public OperateResult<byte[ ]> Packet_ReadData()
    {
        return _encodeHelper.EncodeHelper(Hex5AInformation.ReadData_ACS);
    }

    public OperateResult<byte[ ]> Packet_ReadData_Status()
    {
        //不具备此功能
        return new OperateResult<byte[ ]>(StringResources.Language.NotSupportedFunction);
    }

    public OperateResult<byte[ ]> Packet_SetAmplitude(float UA, float UB, float UC, float IA, float IB, float IC, float IPAOrUx, float IPBOrIX, float IPC)
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[8];
        args[0] = new SetStandardSourceArgs(Channels.Ua, UA);
        args[1] = new SetStandardSourceArgs(Channels.Ub, UB);
        args[2] = new SetStandardSourceArgs(Channels.Uc, UC);
        args[3] = new SetStandardSourceArgs(Channels.Ia, IA);
        args[4] = new SetStandardSourceArgs(Channels.Ib, IB);
        args[5] = new SetStandardSourceArgs(Channels.Ic, IC);
        args[6] = new SetStandardSourceArgs(Channels.Ux, IPAOrUx);
        args[7] = new SetStandardSourceArgs(Channels.Ix, IPBOrIX);
        return SetArgs_ACS(Type_SetStandardSource.Amplitude, args);
    }

    public OperateResult<byte[ ]> Packet_SetClosedLoop(CloseLoopMode closeLoopMode)
    {
        return SetModeAndRanges_ACS(Flag_SetType.SetCloseLoopMode, 0, 0, closeLoopMode, 0, 0, Array.Empty<byte>());
    }

    public OperateResult<byte[ ]> Packet_SetFrequency(float FreqOfAll, float Freq_C_Or_X = 0)
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[1];
        args[0] = new SetStandardSourceArgs(SetStandardSource_Channels_Freq.Freq, FreqOfAll);
        args[1] = new SetStandardSourceArgs(SetStandardSource_Channels_Freq.Freq_X, Freq_C_Or_X);
        return SetArgs_ACS(Type_SetStandardSource.Freqency, args);
    }

    public OperateResult<byte[ ]> Packet_SetHarmonicMode(HarmonicMode harmonicMode)
    {
        return SetModeAndRanges_ACS(Flag_SetType.SetHarmonicMode, 0, 0, 0, harmonicMode, 0, Array.Empty<byte>());
    }

    public OperateResult<byte[ ]> Packet_SetHarmonics(Enum channels, HarmonicArgs[ ] harmonics)
    {
        //参数组数
        byte count = (byte)harmonics.Length;
        if (count == 0)
        {
            return Packet_ClearHarmonics(channels);
        }
        //定义数据区数组
        byte[ ] data = new byte[2 + count * 9];
        data[0] =(byte)(Channels)channels;
        data[1] = count;
        //将谐波组分别转换成字节数组并复制到数据字节数组中
        for (int i = 0; i < count; i++)
        {
            HarmonicArgs.HarmonicToBytes(harmonics[i], _transform).CopyTo(data, 2 + i * 9);

        }
        return _encodeHelper.EncodeHelper(Hex5AInformation.SetHarmonics, (ushort)(11 + data.Length), data);
    }

    public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc , float PhaseIx = 0 )
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[6];
        args[0] = new SetStandardSourceArgs(Channels.Ua, PhaseUa);
        args[1] = new SetStandardSourceArgs(Channels.Ub, PhaseUb);
        args[2] = new SetStandardSourceArgs(Channels.Uc, PhaseUc);
        args[3] = new SetStandardSourceArgs(Channels.Ia, PhaseIa);
        args[4] = new SetStandardSourceArgs(Channels.Ib, PhaseIb);
        args[5] = new SetStandardSourceArgs(Channels.Ic, PhaseIc);
        return SetArgs_ACS(Type_SetStandardSource.Phase, args);
    }


    public OperateResult<byte[ ]> Packet_SetRanges(byte rangeIndexOfACU, byte rangeIndexOfACI)
    {
        _rangeIndex_ACU = rangeIndexOfACU;
        _rangeIndex_ACI = rangeIndexOfACI;
        byte[ ] ranges = new byte[8] { rangeIndexOfACU, rangeIndexOfACI, rangeIndexOfACU, rangeIndexOfACI, rangeIndexOfACU, rangeIndexOfACI, _rangeIndex_Ux, _rangeIndex_Ix };
        return SetModeAndRanges_ACS(Flag_SetType.SetRanges, 0, 0, 0, 0, 0, ranges);
    }

    public OperateResult<byte[ ]> Packet_SetRanges_X(byte rangeIndex_Ux, byte rangeIndex_Ix)
    {
        _rangeIndex_Ux = rangeIndex_Ux;
        _rangeIndex_Ix = rangeIndex_Ix;

        byte[ ] ranges = new byte[8] { _rangeIndex_ACU, _rangeIndex_ACI, _rangeIndex_ACU, _rangeIndex_ACI, _rangeIndex_ACU, _rangeIndex_ACI, rangeIndex_Ux, rangeIndex_Ix };
        return SetModeAndRanges_ACS(Flag_SetType.SetRanges, 0, 0, 0, 0, 0, ranges);
    }

    public OperateResult<byte[ ]> Packet_SetRanges_IP(byte rangeIndex_IP)
    {
        //不具备此功能
        return new OperateResult<byte[ ]>(StringResources.Language.NotSupportedFunction);
    }

    public OperateResult<byte[ ]> Packet_SetWattLessPower(Enum Channel_WattLessPower, float q)
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[1];
        args[0] = new SetStandardSourceArgs(Channel_WattLessPower, q);
        return SetArgs_ACS(Type_SetStandardSource.WattlessPower, args);
    }

    public OperateResult<byte[ ]> Packet_SetWattPower(Enum Channel_WattPower, float p)
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[1];
        args[0] = new SetStandardSourceArgs(Channel_WattPower, p);
        return SetArgs_ACS(Type_SetStandardSource.WattPower, args);
    }

    public OperateResult<byte[ ]> Packet_SetWireMode(WireMode wireMode)
    {
        return SetModeAndRanges_ACS(Flag_SetType.SetWireMode, 0, wireMode, 0, 0, 0, Array.Empty<byte>());
    }

    public OperateResult<byte[ ]> Packet_Stop()
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[1];
        args[0] = new SetStandardSourceArgs(Channels.UIabc, -1);
        return SetArgs_ACS(Type_SetStandardSource.Amplitude, args);
    }

    public OperateResult<byte[ ]> Packet_ClearHarmonics(Enum Channels)
    {
        byte[ ] data = new byte[2];
        data[0] = Convert.ToByte(Channels);
        data[1] = 0;
        return _encodeHelper.EncodeHelper(Hex5AInformation.SetHarmonics, Hex5AInformation.SetHarmonics_Clear_L, data);
    }


    #region 私有方法和字段

    private byte _rangeIndex_ACU;
    private byte _rangeIndex_ACI;

    private byte _rangeIndex_Ux;
    private byte _rangeIndex_Ix;

    /// <summary>
    /// 设置模式及档位
    /// </summary>
    /// <param name="flag_Type"></param>
    /// <param name="workMode"></param>
    /// <param name="wireMode"></param>
    /// <param name="closeLoopMode"></param>
    /// <param name="harmonicMode"></param>
    /// <param name="qP_Mode"></param>
    /// <param name="rangs"></param>
    /// <returns></returns>
    private OperateResult<byte[ ]> SetModeAndRanges_ACS(Flag_SetType flag_Type, ACSWorkingMode workMode, WireMode wireMode, CloseLoopMode closeLoopMode, HarmonicMode harmonicMode, QP_Mode qP_Mode, byte[ ] rangs)
    {
        byte[ ] data = new byte[14];
        data[0] = (byte)flag_Type;
        data[1] = (byte)workMode;
        data[2] = (byte)wireMode;
        data[3] = (byte)closeLoopMode;
        data[4] = (byte)harmonicMode;
        data[5] = (byte)qP_Mode;

        if (rangs.Length == 8)
        {
            rangs.CopyTo(data, 6);
        }
        return _encodeHelper.EncodeHelper(Hex5AInformation.SetSystemModeAndRanges, Hex5AInformation.SetSystemModeAndRanges_L, data);
    }

    /// <summary>
    /// 设置标准源参数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    private OperateResult<byte[ ]> SetArgs_ACS(Type_SetStandardSource type, SetStandardSourceArgs[ ] args)
    {
        //参数组数
        int count = args.Length;

        //数据区字节定义
        byte[ ] data = new byte[2 + 5 * count];

        //填充数据
        data[0] = (byte)type;
        data[1] = (byte)count;

        //参数组结构体转字节
        for (int i = 0; i < count; i++)
        {
            var result = SetStandardSourceArgs.SourceArgsToBytes(args[i], _transform);
            if (!result.IsSuccess || result.Content == null)
            {
                return result;
            }
            result.Content.CopyTo(data, index: 2 + i * 5);
        }

        //返回结果
        return _encodeHelper.EncodeHelper(Hex5AInformation.SetStandardSource, (ushort)(11 + data.Length), data);
    }
    #endregion
}
