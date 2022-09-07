using System.Security.AccessControl;
using System.Text;
using DKCommunicationNET.BasicFramework;
using DKCommunicationNET.Core;

namespace DKCommunicationNET.Protocols.Hex81.Decoders;

/// <summary>
/// Hex81协议解码器
/// </summary>
public class Hex81Decoder : IDecoders
{
    private readonly IByteTransform _byteTransform;
    public Hex81Decoder(IByteTransform byteTransform)
    {
        _byteTransform = byteTransform;
    }

    #region 【属性】

    public int Offset => Hex81Information.DataStartIndex;

    #region 属性>>>设备信息

    public string? Model { get; set; }

    public string? SN { get; set; }

    public string? Firmware { get; private set; }

    public string? ProtocolVer => string.Empty;

    #endregion 属性>>>设备信息

    #region 属性>>>功能状态

    public bool IsEnabled_ACS { get; private set; }

    public bool IsEnabled_ACM { get; private set; }

    public bool IsEnabled_DCS { get; private set; }

    public bool IsEnabled_IO { get; private set; }

    public bool IsEnabled_EPQ { get; private set; }

    public bool IsEnabled_DCM { get; private set; }

    public bool IsEnabled_DualFreqs { get; private set; }

    public bool IsEnabled_IProtect { get; private set; }

    public bool IsEnabled_PST { get; private set; }

    public bool IsEnabled_YX { get; private set; }

    public bool IsEnabled_HF { get; private set; }

    public bool IsEnabled_PWM { get; private set; }

    public bool IsEnabled_ACM_Cap { get; private set; }

    public bool IsEnabled_DCS_AUX { get; private set; }

    public bool IsEnabled_DCM_RIP { get; private set; }

    public bool IsEnabled_PPS { get; private set; }

    #endregion 属性>>>功能状态

    #region 属性>>>交流源/表

    public byte RangesCount_ACU { get; private set; }

    public byte RangesCount_ACI { get; private set; }

    public byte RangeIndex_ACU { get; set; }

    public float RangeValue_ACU { get; private set; }

    public float RangeValue_ACI { get; private set; }

    public float RangeValue_IPr { get; private set; }

    public byte RangesCount_IPr { get; private set; }

    public byte OnlyAStartIndex_ACU { get; private set; }

    public byte OnlyAStartIndex_ACI { get; private set; }

    public byte OnlyAStartIndex_IPr { get; private set; }

    public float[ ]? Ranges_ACU { get; set; }
    public float[ ]? Ranges_ACI { get; set; }
    public float[ ]? Ranges_IPr { get; set; }
    public WireMode WireMode { get; set; } = WireMode.WireMode_3P4L;
    public CloseLoopMode CloseLoopMode { get; set; } = CloseLoopMode.CloseLoop;
    public HarmonicMode HarmonicMode { get; set; } = HarmonicMode.ValidValuesConstant;
    public float Freq { get; set; }
    public float Freq_C { get; set; }
    public byte HarmonicCount { get; set; }
    public Enum? HarmonicChannels { get; set; }
    public HarmonicArgs[ ]? Harmonics { get; set; }
    public float UA { get; set; }
    public float UB { get; set; }
    public float UC { get; set; }
    public float IA { get; set; }
    public float IB { get; set; }
    public float IC { get; set; }
    public float IPA { get; set; }
    public float IPB { get; set; }
    public float IPC { get; set; }
    public float FAI_UA { get; set; }
    public float FAI_UB { get; set; }
    public float FAI_UC { get; set; }
    public float FAI_IA { get; set; }
    public float FAI_IB { get; set; }
    public float FAI_IC { get; set; }
    public float PA { get; set; }
    public float PB { get; set; }
    public float PC { get; set; }
    public float P { get; set; }
    public float QA { get; set; }
    public float QB { get; set; }
    public float QC { get; set; }
    public float Q { get; set; }

    public float SA { get; private set; }

    public float SB { get; private set; }

    public float SC { get; private set; }

    public float S { get; private set; }

    public float PFA { get; private set; }

    public float PFB { get; private set; }

    public float PFC { get; private set; }

    public float PF { get; private set; }

    public byte Flag_A { get; private set; }

    public byte Flag_B { get; private set; }

    public byte Flag_C { get; private set; }
    public byte RangeIndex_ACI { get; set; }
    public byte RangeIndex_IPr { get; set; }

    #endregion 属性>>>交流源/表

    #region 属性>>>直流源
    public byte RangesCount_DCU { get; private set; }

    public byte RangesCount_DCI { get; private set; }

    public byte RangesCount_DCR { get; private set; }

    public float DCU { get; private set; }

    public float DCI { get; private set; }

    public float DCR { get; private set; }


    public byte RangeIndex_DCU { get; set; }
    public byte RangeIndex_DCI { get; set; }
    public byte RangeIndex_DCR { get; set; }

    public float[ ]? Ranges_DCU { get; set; }

    public float[ ]? Ranges_DCI { get; set; }

    public float[ ]? Ranges_DCR { get; set; }

    public bool IsOpen_DCU { get; private set; }

    public bool IsOpen_DCI { get; private set; }

    public bool IsOpen_DCR { get; private set; }

    #endregion 属性>>>直流源

    #region 属性>>>直流表    

    /// <summary>
    /// 直流表电压档位集合
    /// </summary>
    public float[ ]? Ranges_DCMU { get; set; }

    /// <summary>
    /// 直流表电流档位集合
    /// </summary>
    public float[ ]? Ranges_DCMI { get; set; }

    /// <summary>
    /// 直流纹波电压表档位集合
    /// </summary>
    public float[ ]? Ranges_DCMU_Ripple { get; set; }

    /// <summary>
    /// 直流纹波电流表的档位集合
    /// </summary>
    public float[ ]? Ranges_DCMI_Ripple { get; set; }

    /// <summary>
    /// 直流表电压测量值
    /// </summary>
    public float DCMU { get; private set; }

    /// <summary>
    /// 直流表电流测量值
    /// </summary>
    public float DCMI { get; private set; }

    /// <summary>
    /// 直流纹波电压测量值
    /// </summary>
    public float DCMU_Ripple { get; private set; }

    /// <summary>
    /// 直流纹波电流测量值
    /// </summary>
    public float DCMI_Ripple { get; private set; }

    /// <summary>
    /// 直流表电压量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMU { get; private set; }

    /// <summary>
    /// 直流表电流量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMI { get; private set; }

    /// <summary>
    /// 直流纹波电压量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMU_Ripple { get; private set; }

    /// <summary>
    /// 直流纹波电流量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMI_Ripple { get; private set; }

    /// <summary>
    /// 直流表电压量程数量
    /// </summary>
    public byte RangesCount_DCMU { get; private set; }

    /// <summary>
    /// 直流表电流量程数量
    /// </summary>
    public byte RangesCount_DCMI { get; private set; }

    /// <summary>
    /// 直流纹波电压量程数量
    /// </summary>
    public byte RangesCount_DCMU_Ripple { get; private set; }

    /// <summary>
    /// 直流纹波电流量程数量
    /// </summary>
    public byte RangesCount_DCMI_Ripple { get; private set; }

    public uint Rounds_Current { get; private set; }

    public uint Counts_Current { get; private set; }

    public float EValue_P { get; private set; }

    public float EValue_Q { get; private set; }

    #endregion 属性>>>直流表


    #endregion 属性

    #region 【Decoders】

    public void DecodeHandShake(OperateResult<byte[ ]> response)
    {
        //判断回复结果
        if (!response.IsSuccess || response.Content == null)
        {
            return;
        }

        //下位机回复的原始报文
        byte[ ] buffer = response.Content;

        //将缓存数据转换成List方便查找字符串的结束标志:0x00
        List<byte> bufferList = buffer.ToList();    //可忽略null异常

        //获取设备型号结束符的索引值
        int endIndex = bufferList.IndexOf(0x00, Offset);

        //计算model字节长度，包含0x00结束符，5=报文头的字节数6再减去1
        int modelLength = endIndex - Offset + 1;
        //解析的设备型号
        Model = _byteTransform.TransString(buffer, Offset, modelLength, Encoding.ASCII);

        //解析下位机版本号
        byte verA = buffer[modelLength + Offset];
        byte verB = buffer[modelLength + Offset + 1];
        //下位机版本号
        Firmware = $"V{verA}.{verB}";

        //解析设备编号
        int serialEndIndex = bufferList.IndexOf(0x00, Offset + modelLength + 2);
        int serialLength = serialEndIndex - 7 - modelLength;
        //设备编号字节长度，包含0x00结束符            
        SN = _byteTransform.TransString(buffer, Offset + modelLength + 2, serialLength, Encoding.ASCII);

        //基本功能激活状态
        byte FuncB = buffer[^3];
        bool[ ] funcB = SoftBasic.ByteToBoolArray(FuncB);
        IsEnabled_ACS = funcB[0];
        IsEnabled_ACM = funcB[1];
        IsEnabled_DCS = funcB[2];
        IsEnabled_DCM = funcB[3];
        IsEnabled_EPQ = funcB[4];

        //特殊功能激活状态
        byte FuncS = buffer[^2];
        bool[ ] funcS = SoftBasic.ByteToBoolArray(FuncS);
        IsEnabled_DualFreqs = funcS[0];
        IsEnabled_IProtect = funcS[1];
        IsEnabled_PST = funcS[2];
        IsEnabled_YX = funcS[3];
        IsEnabled_HF = funcS[4];
        IsEnabled_PWM = funcS[5];
    }

    #region Decoders>>>交流源/表解码器

    /// <summary>
    /// 【解码】读取交流源档位信息
    /// </summary>
    /// <param name="response"></param>
    /// <returns>
    /// <list type="bullet">  
    ///     <item>T1:电压档位数量</item>
    ///     <item>T2:单相电压档位起始档位索引值</item>
    ///     <item>T3:电流档位数量</item>
    ///     <item>T4:单相电流档位起始档位索引值</item>
    ///     <item>T5:保护电流档位数量</item>
    ///     <item>T6:单相保护电流档位起始档位索引值</item>
    ///     <item>T7:电压档位集合</item>
    ///     <item>T8:电流档位集合</item>
    ///     <item>T9:保护电流档位集合</item>
    /// </list>
    /// </returns>
    public OperateResult DecodeGetRanges_ACS(OperateResult<byte[ ]> response)
    {
        //判断回复结果
        if (response.IsSuccess && response.Content != null)
        {
            //下位机回复的经验证的有效报文
            byte[ ] responseBytes = response.Content;

            //电压档位数量
            RangesCount_ACU = responseBytes[Offset];

            //单相电压档位起始档位索引值
            OnlyAStartIndex_ACU = responseBytes[7];

            //电流档位数量
            RangesCount_ACI = responseBytes[8];

            //单相电流档位起始档位索引值
            OnlyAStartIndex_ACI = responseBytes[9];

            //保护电流档位数量
            RangesCount_IPr = responseBytes[10];

            //单相保护电流档位起始档位索引值
            RangeIndex_IPr = responseBytes[11];

            //电压档位集合
            Ranges_ACU = _byteTransform.TransSingle(responseBytes, 12, RangesCount_ACU);

            //电流档位集合
            Ranges_ACI = _byteTransform.TransSingle(responseBytes, 12 + 4 * RangesCount_ACU, RangesCount_ACI);

            //保护电流档位集合
            Ranges_IPr = _byteTransform.TransSingle(responseBytes, 12 + 4 * RangesCount_ACU + 4 * RangesCount_ACI, RangesCount_IPr);
            return OperateResult.CreateSuccessResult();
        }
        return new OperateResult(response.Message);
    }

    public OperateResult DecodeReadData_ACS(OperateResult<byte[ ]> responsResult)
    {
        //判断回复结果
        if (!responsResult.IsSuccess || responsResult.Content == null)
        {
            return new OperateResult(responsResult.Message);
        }
        //提取原始报文
        byte[ ] responseBytes = responsResult.Content;

        Freq = _byteTransform.TransSingle(responseBytes, Offset);
        RangeIndex_ACU = responseBytes[Offset + 4];  //取UA的档位索引
        RangeIndex_ACI = responseBytes[Offset + 7];  //取IA的档位索引
        UA = _byteTransform.TransSingle(responseBytes, 16);
        UB = _byteTransform.TransSingle(responseBytes, 20);
        UC = _byteTransform.TransSingle(responseBytes, 24);
        IA = _byteTransform.TransSingle(responseBytes, 28);
        IB = _byteTransform.TransSingle(responseBytes, 32);
        IC = _byteTransform.TransSingle(responseBytes, 36);
        FAI_UA = _byteTransform.TransSingle(responseBytes, 40);
        FAI_UB = _byteTransform.TransSingle(responseBytes, 44);
        FAI_UC = _byteTransform.TransSingle(responseBytes, 48);
        FAI_IA = _byteTransform.TransSingle(responseBytes, 52);
        FAI_IB = _byteTransform.TransSingle(responseBytes, 56);
        FAI_IC = _byteTransform.TransSingle(responseBytes, 60);
        PA = _byteTransform.TransSingle(responseBytes, 64);
        PB = _byteTransform.TransSingle(responseBytes, 68);
        PC = _byteTransform.TransSingle(responseBytes, 72);
        P = _byteTransform.TransSingle(responseBytes, 76);
        QA = _byteTransform.TransSingle(responseBytes, 80);
        QB = _byteTransform.TransSingle(responseBytes, 84);
        QC = _byteTransform.TransSingle(responseBytes, 88);
        Q = _byteTransform.TransSingle(responseBytes, 92);
        SA = _byteTransform.TransSingle(responseBytes, 96);
        SB = _byteTransform.TransSingle(responseBytes, 100);
        SC = _byteTransform.TransSingle(responseBytes, 104);
        S = _byteTransform.TransSingle(responseBytes, 108);
        PFA = _byteTransform.TransSingle(responseBytes, 112);
        PFB = _byteTransform.TransSingle(responseBytes, 116);
        PFC = _byteTransform.TransSingle(responseBytes, 120);
        PF = _byteTransform.TransSingle(responseBytes, 124);
        WireMode = (WireMode)responseBytes[128];
        CloseLoopMode = (CloseLoopMode)responseBytes[129];
        HarmonicMode = (HarmonicMode)responseBytes[130];
        return OperateResult.CreateSuccessResult();
    }

    public OperateResult DecodeReadData_Status_ACS(OperateResult<byte[ ]> responsResult)
    {
        //判断回复结果
        if (!responsResult.IsSuccess || responsResult.Content == null)
        {
            return new OperateResult(responsResult.Message);
        }
        //提取原始报文
        byte[ ] response = responsResult.Content;

        Flag_A = response[6];
        Flag_B = response[7];
        Flag_C = response[8];
        Freq = _byteTransform.TransSingle(response, 9);
        Freq_C = _byteTransform.TransSingle(response, 17);
        IPA = _byteTransform.TransSingle(response, 21);
        IPB = _byteTransform.TransSingle(response, 25);
        IPC = _byteTransform.TransSingle(response, 29);
        RangeValue_ACU = _byteTransform.TransSingle(response, 33);
        RangeValue_ACI = _byteTransform.TransSingle(response, 37);
        RangeValue_IPr = _byteTransform.TransSingle(response, 41);

        return OperateResult.CreateSuccessResult();
    }

    #endregion Decoders>>>交流源/表解码器

    #region Decoders>>>直流源解码器

    public OperateResult DecodeReadData_DCS(OperateResult<byte[ ]> responsResult)
    {
        //判断回复结果
        if (!responsResult.IsSuccess || responsResult.Content == null)
        {
            return new OperateResult(responsResult.Message);
        }
        //提取原始报文
        byte[ ] response = responsResult.Content;


        OutputType_DCS outputType_DCS = (OutputType_DCS)response[11];

        switch (outputType_DCS)
        {
            case OutputType_DCS.DCS_Type_U:
                RangeIndex_DCU = response[6];
                DCU = _byteTransform.TransSingle(response, 7);
                IsOpen_DCU = _byteTransform.TransBool(response, 12);
                break;
            case OutputType_DCS.DCS_Type_I:
                RangeIndex_DCI = response[6];
                DCI = _byteTransform.TransSingle(response, 7);
                IsOpen_DCI = _byteTransform.TransBool(response, 12);
                break;
            case OutputType_DCS.DCS_Type_R:
                RangeIndex_DCR = response[6];
                DCR = _byteTransform.TransSingle(response, 7);
                IsOpen_DCR = _byteTransform.TransBool(response, 12);
                break;
            default:
                return new OperateResult(StringResources.GetLineNum(), StringResources.Language.DecodeError + StringResources.GetCurSourceFileName());
        }

        return OperateResult.CreateSuccessResult();
    }

    public OperateResult DecodeGetRanges_DCS(OperateResult<byte[ ]> responsResult)
    {
        //判断回复结果
        if (!responsResult.IsSuccess || responsResult.Content == null)
        {
            return new OperateResult(responsResult.Message);
        }

        //提取原始报文
        byte[ ] response = responsResult.Content;
        RangesCount_DCU = response[6];
        RangesCount_DCI = response[7];
        Ranges_DCU = _byteTransform.TransSingle(response, 8, RangesCount_DCU);
        Ranges_DCI = _byteTransform.TransSingle(response, 8 + RangesCount_DCU * 4, RangesCount_DCI);

        return OperateResult.CreateSuccessResult();
    }

    #endregion Decoders>>>直流源解码器

    #region Decoders>>>直流表解码器

    public OperateResult DecodeGetRanges_DCM(OperateResult<byte[ ]> responsResult)
    {
        //判断回复结果
        if (!responsResult.IsSuccess || responsResult.Content == null)
        {
            return new OperateResult(responsResult.Message);
        }

        //提取原始报文
        byte[ ] response = responsResult.Content;

        RangesCount_DCMU = response[8];
        RangesCount_DCMI = response[9];

        Ranges_DCMU = _byteTransform.TransSingle(response, 10, RangesCount_DCMU);
        Ranges_DCMI = _byteTransform.TransSingle(response, 10 + 4 * RangesCount_DCMU, RangesCount_DCMI);
        //返回解析成功结果
        return OperateResult.CreateSuccessResult();
    }

    public OperateResult DecodeReadData_DCM(OperateResult<byte[ ]> responsResult)
    {
        //判断回复结果
        if (!responsResult.IsSuccess || responsResult.Content == null)
        {
            return new OperateResult(responsResult.Message);
        }

        //提取原始报文
        byte[ ] response = responsResult.Content;

        //解析测量类型
        MeasureType_DCM measureType_DCM = (MeasureType_DCM)response[11];

        //根据测量类型解析数据
        switch (measureType_DCM)
        {
            case MeasureType_DCM.DCM_Voltage:
                RangeIndex_DCMU = response[6];
                DCMU = _byteTransform.TransSingle(response, 7);
                break;
            case MeasureType_DCM.DCM_Current:
                RangeIndex_DCMI = response[6];
                DCMI = _byteTransform.TransSingle(response, 7);
                break;
            case MeasureType_DCM.DCM_VoltageRipple:
                RangeIndex_DCMU_Ripple = response[6];
                DCMU_Ripple = _byteTransform.TransSingle(response, 7);
                break;
            case MeasureType_DCM.DCM_CurrentRipple:
                RangeIndex_DCMI_Ripple = response[6];
                DCMI_Ripple = _byteTransform.TransSingle(response, 7);
                break;
            default:
                return new OperateResult("（Hex81）MeasureType_DCM:回复数据解码失败:没能找到匹配的MeasureType_DCM枚举项");
        }
        //返回解析成功结果
        return OperateResult.CreateSuccessResult();
    }

    public OperateResult DecodeReadData_EPQ(OperateResult<byte[ ]> responsResult)
    {
        //判断回复结果
        if (!responsResult.IsSuccess || responsResult.Content == null)
        {
            return new OperateResult(responsResult.Message);
        }

        //提取原始报文
        byte[ ] response = responsResult.Content;

        DecodeEPQ_Flag decodeEPQ_Flag = (DecodeEPQ_Flag)response[6];

        switch (decodeEPQ_Flag)
        {
            case DecodeEPQ_Flag.Invalid:
                Rounds_Current = _byteTransform.TransUInt32(response, 11);
                break;
            case DecodeEPQ_Flag.EValue_P:
                EValue_P = _byteTransform.TransSingle(response, 7);
                Rounds_Current = _byteTransform.TransUInt32(response, 11);
                break;
            case DecodeEPQ_Flag.EValue_Q:
                EValue_Q = _byteTransform.TransSingle(response, 7);
                Rounds_Current = _byteTransform.TransUInt32(response, 11);
                break;
            default:
                return new OperateResult("（Hex81）DecodeReadData_EPQ:直流源回复数据解码失败：没能找到匹配的DecodeEPQ_Flag枚举项");
        }
        //返回解析成功结果
        return OperateResult.CreateSuccessResult();
    }

    #endregion

    #endregion Decoders

}
