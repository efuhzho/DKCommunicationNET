
/**************************************************************************************************
 * 
 *  【交流源报文创建类】 版本：V 1.0.0   Author:  Fuhong Zhou   2022年9月8日 22点43分  
 *  
 *  用于生成Hex81协议的命令报文。
 *
 *************************************************************************************************/

using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81. Encoders;

internal class Hex81Encoder_ACS : IEncoder_ACS
{
    private readonly Hex81EncodeHelper _encodeHelper;
    private readonly IByteTransform _byteTransform;

    public Hex81Encoder_ACS(ushort id, IByteTransform byteTransform)
    {
        _encodeHelper = new(id);
        _byteTransform = byteTransform;
    }

    public OperateResult<byte[ ]> Packet_GetRanges()
    {
        return _encodeHelper.EncodeHelper(Hex81.GetRanges_ACS);
    }

    public OperateResult<byte[ ]> Packet_SetAmplitude(float UA, float UB, float UC, float IA, float IB, float IC, float IPA, float IPB, float IPC)
    {
        float[ ] data = new float[9];
        data[0] = UA;
        data[1] = UB;
        data[2] = UC;
        data[3] = IA;
        data[4] = IB;
        data[5] = IC;
        data[6] = IPA;
        data[7] = IPB;
        data[8] = IPC;
        byte[ ] buffer = _byteTransform.TransByte(data);
        return _encodeHelper.EncodeHelper(Hex81.SetAmplitude_ACS, Hex81.SetAmplitude_ACS_Length, buffer);
    }

    public OperateResult<byte[ ]> Packet_SetFrequency(float FreqOfAll, float FreqOfC = 0)
    {
        float[ ] data = new float[2];
        data[0] = FreqOfAll;
        data[1] = FreqOfC;
        byte[ ] buffer = _byteTransform.TransByte(data);
        return _encodeHelper.EncodeHelper(Hex81.SetFrequency, Hex81.SetFrequencyLength, buffer);
    }

    public OperateResult<byte[ ]> Packet_SetPhase(float PhaseUa, float PhaseUb, float PhaseUc, float PhaseIa, float PhaseIb, float PhaseIc,float PhaseIx=0)
    {
        float[ ] data = new float[6];
        data[0] = PhaseUa;
        data[1] = PhaseUb;
        data[2] = PhaseUc;
        data[3] = PhaseIa;
        data[4] = PhaseIb;
        data[5] = PhaseIc;
        byte[ ] buffer = _byteTransform.TransByte(data);
        return _encodeHelper.EncodeHelper(Hex81.SetPhase, Hex81.SetPhaseLength, buffer);
    }

    public OperateResult<byte[ ]> Packet_SetWireMode(WireMode wireMode)
    {
        byte[ ] data = new byte[1] { (byte)wireMode };
        return _encodeHelper.EncodeHelper(Hex81.SetWireMode, Hex81.SetWireModeLength, data);
    }

    public OperateResult<byte[ ]> Packet_SetClosedLoop(CloseLoopMode closeLoopMode)
    {
        _CloseLoopMode = closeLoopMode;
        return Packet_SetClosedLoopAndHarmonicMode( closeLoopMode , _HarmonicMode);
    }

    public OperateResult<byte[ ]> Packet_SetHarmonicMode(HarmonicMode harmonicMode)
    {
        _HarmonicMode = harmonicMode;
        return Packet_SetClosedLoopAndHarmonicMode(_CloseLoopMode, harmonicMode);
    }

    public OperateResult<byte[ ]> Packet_SetHarmonics(Enum channels, HarmonicArgs[ ] harmonics)
    {
        //要设置的谐波个数
        byte count = (byte)harmonics.Length;

        //如果谐波组为空，则指令为清空谐波
        if (count == 0)
        {
            return Packet_ClearHarmonics(channels);
        }

        //当协议报文长度超过256个时，禁止发送报文以避免下位机出错。【来源于Hex81协议要求】
        if (count > 27)
        {
            return new OperateResult<byte[ ]>( "您设置的谐波个数超过了27个，建议您分批发送" );
        }
        //数据字节数组
        byte[ ] data = new byte[2 + count * 9];
        data[0] = ( byte ) ( Channels ) channels;
        data[1] = count;

        //将谐波组分别转换成字节数组并复制到数据字节数组中
        for (int i = 0; i < count; i++)
        {
            HarmonicArgs.HarmonicToBytes(harmonics[i], _byteTransform).CopyTo(data, i * 9 + 2);
        }

        return _encodeHelper.EncodeHelper(Hex81.SetHarmonics, (ushort)(data.Length + 7), data);
    }

    public OperateResult<byte[ ]> Packet_SetWattPower(Enum channel, float p)
    {
        byte[ ] data = new byte[5];
        data[0] = Convert.ToByte(channel);
        _byteTransform.TransByte(p).CopyTo(data, 1);

        return _encodeHelper.EncodeHelper(Hex81.SetWattPower, Hex81.SetWattPowerLength, data);
    }

    public OperateResult<byte[ ]> Packet_SetWattLessPower(Enum channel, float q)
    {
        byte[ ] data = new byte[5];
        data[0] = Convert.ToByte(channel);
        _byteTransform.TransByte(q).CopyTo(data, 1);

        return _encodeHelper.EncodeHelper(Hex81.SetWattlessPower, Hex81.SetWattlessPowerLength, data);
    }

    public OperateResult<byte[ ]> Packet_ReadData()
    {
        return _encodeHelper.EncodeHelper(Hex81.ReadData_ACS);
    }

    public OperateResult<byte[ ]> Packet_ReadData_Status()
    {
        return _encodeHelper.EncodeHelper(Hex81.GetStatus_ACS);
    }

    public OperateResult<byte[ ]> Packet_Stop()
    {
        return _encodeHelper.EncodeHelper(Hex81.CloseACS);
    }

    public OperateResult<byte[ ]> Packet_Open()
    {
        return _encodeHelper.EncodeHelper(Hex81.OpenACS);
    }

    public OperateResult<byte[ ]> Packet_SetRanges(byte rangeIndexOfACU, byte rangeIndexOfACI)
    {
        _rangeIndex_U = rangeIndexOfACU;
        _rangeIndex_I = rangeIndexOfACI;
        return Packet_SetRanges_Helper(rangeIndexOfACU, rangeIndexOfACI, _rangeIndex_IP);
    }

    public OperateResult<byte[ ]> Packet_SetRanges_IP(byte rangeIndex_IP)
    {
        _rangeIndex_IP = rangeIndex_IP;
        return Packet_SetRanges_Helper(_rangeIndex_U, _rangeIndex_I, rangeIndex_IP);
    }

    public OperateResult<byte[ ]> Packet_SetRanges_X(byte rangeIndex_Ux, byte rangeIndex_Ix)
    {
        //不具备此功能
        return new OperateResult<byte[ ]>(StringResources.Language.NotSupportedFunction);
    }

    public OperateResult<byte[ ]> Packet_ClearHarmonics(Enum Channels)
    {
        byte[ ] dataClear = new byte[2] { Convert.ToByte(Channels), 0 };
        return _encodeHelper.EncodeHelper(Hex81.SetHarmonics, (ushort)(dataClear.Length + 7), dataClear);
    }

    #region 私有方法和字段

    private CloseLoopMode _CloseLoopMode = CloseLoopMode.CloseLoop;
    private HarmonicMode _HarmonicMode = HarmonicMode.ValidValuesConstant;
    /// <summary>
    /// 创建报文:设置谐波模式和闭环模式
    /// </summary>
    /// <param name="closeLoopMode"></param>
    /// <param name="harmonicMode"></param>
    /// <returns></returns>
    private OperateResult<byte[ ]> Packet_SetClosedLoopAndHarmonicMode(CloseLoopMode closeLoopMode, HarmonicMode harmonicMode)
    {
        byte[ ] data = new byte[2] { (byte)closeLoopMode, (byte)harmonicMode };
        return _encodeHelper.EncodeHelper(Hex81.SetClosedLoop, Hex81.SetClosedLoopLength, data);
    }

    private byte _rangeIndex_IP;
    private byte _rangeIndex_I;
    private byte _rangeIndex_U;
    /// <summary>
    /// 设置档位
    /// </summary>
    /// <param name="rangeIndexOfACU"></param>
    /// <param name="rangeIndexOfACI"></param>
    /// <param name="rangeIndexOfIP"></param>
    /// <returns></returns>
    private OperateResult<byte[ ]> Packet_SetRanges_Helper(byte rangeIndexOfACU, byte rangeIndexOfACI, byte rangeIndexOfIP)
    {
        try
        {
            byte[ ] data = new byte[9];
            for (int i = 0; i < 3; i++)
            {
                data[i] = rangeIndexOfACU;
                data[i + 3] = rangeIndexOfACI;
                data[i + 6] = rangeIndexOfIP;
            }
            return _encodeHelper.EncodeHelper(Hex81.SetRanges_ACS, Hex81.SetRanges_ACS_Length, data);
        }
        catch (Exception ex)
        {
            return new OperateResult<byte[ ]>( ex.Message);
        }
    }




    #endregion
}
