using System. Text;
using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81. Decoders;

/// <summary>
/// 联机设置解码器
/// </summary>
internal class Hex81Decoder_Settings : IDecoder_Settings
{
    private readonly IByteTransform _byteTransform;
    internal Hex81Decoder_Settings ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    public OperateResult DecodeHandShake ( byte[ ] buffer )
    {
        try
        {
            //将缓存数据转换成List方便查找字符串的结束标志:0x00
            List<byte> bufferList = buffer. ToList ( );

            //获取设备型号结束符的索引值
            int endIndex = bufferList. IndexOf ( 0x00 , Hex81. DataStartIndex );

            //计算model字节长度，包含0x00结束符
            int modelLength = endIndex - Hex81. DataStartIndex + 1;
            //解析的设备型号
            Model = _byteTransform. TransString ( buffer , Hex81. DataStartIndex , modelLength , Encoding. ASCII );

            //解析下位机版本号
            byte verA = buffer[modelLength + Hex81. DataStartIndex];
            byte verB = buffer[modelLength + Hex81. DataStartIndex + 1];
            //下位机版本号
            Firmware = $"V{verA}.{verB}";

            //解析设备编号
            int serialEndIndex = bufferList. IndexOf ( 0x00 , Hex81. DataStartIndex + modelLength + 2 );
            int serialLength = serialEndIndex - 7 - modelLength;
            //设备编号字节长度，包含0x00结束符            
            SN = _byteTransform. TransString ( buffer , Hex81. DataStartIndex + modelLength + 2 , serialLength , Encoding. ASCII );

            //基本功能激活状态
            byte FuncB = buffer[^3];
            bool[ ] funcB = SoftBasic. ByteToBoolArray ( FuncB );
            IsEnabled_ACS = funcB[0];
            //IsEnabled_ACM = funcB[1]; //暂时禁用ACM，功能使用ACS.
            IsEnabled_DCS = funcB[2];
            IsEnabled_DCM = funcB[3];
            IsEnabled_EPQ = funcB[4];

            //特殊功能激活状态
            byte FuncS = buffer[^2];
            bool[ ] funcS = SoftBasic. ByteToBoolArray ( FuncS );
            IsEnabled_DualFreqs = funcS[0];
            IsEnabled_IProtect = funcS[1];
            IsEnabled_PST = funcS[2];
            IsEnabled_YX = funcS[3];
            IsEnabled_HF = funcS[4];
            IsEnabled_PWM = funcS[5];

            return OperateResult. CreateSuccessResult ( );
        }
        catch ( Exception ex )
        {
            return new OperateResult ( "HandShake数据解析失败。" + ex. Message );
        }

    }

    #region 《设备基本信息
    /// <summary>
    /// 设备型号
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// 设备出厂编号
    /// </summary>
    public string? SN { get; set; }

    /// <summary>
    /// 固件版本号
    /// </summary>
    public string? Firmware { get; private set; }

    /// <summary>
    /// 协议版本号
    /// </summary>
    public string? ProtocolVer { get; set; }
    #endregion 设备基本信息》

    #region 《基本功能 FuncB
    /// <summary>
    /// 指示交流源功能是否激活
    /// </summary>
    public bool IsEnabled_ACS { get; private set; }

    /// <summary>
    /// 指示交流表功能是否激活
    /// </summary>
    public bool IsEnabled_ACM { get; private set; }

    /// <summary>
    /// 指示标准表钳表功能是否激活
    /// </summary>
    public bool IsEnabled_ACM_Cap { get; private set; }

    /// <summary>
    /// 指示直流源功能是否激活
    /// </summary>
    public bool IsEnabled_DCS { get; private set; }

    /// <summary>
    /// 辅助直流源是否激活
    /// </summary>
    public bool IsEnabled_DCS_AUX { get; private set; }

    /// <summary>
    /// 指示直流表功能是否激活
    /// </summary>
    public bool IsEnabled_DCM { get; private set; }

    /// <summary>
    /// 指示直流纹波表是否激活
    /// </summary>
    public bool IsEnabled_DCM_RIP { get; private set; }


    /// <summary>
    /// 指示开关量功能是否激活
    /// </summary>
    public bool IsEnabled_IO { get; private set; }

    /// <summary>
    /// 指示电能校验功能是否激活
    /// </summary>
    public bool IsEnabled_EPQ { get; private set; }
    #endregion 基本功能 FuncB》

    #region 《特殊功能 FuncS 
    /// <summary>
    /// 指示双频输出功能是否激活
    /// </summary>
    public bool IsEnabled_DualFreqs { get; private set; }

    /// <summary>
    /// 指示保护电流功能是否激活
    /// </summary>
    public bool IsEnabled_IProtect { get; private set; }

    /// <summary>
    /// 指示闪变输出功能是否激活
    /// </summary>
    public bool IsEnabled_PST { get; private set; }

    /// <summary>
    /// 指示遥信功能是否激活
    /// </summary>
    public bool IsEnabled_YX { get; private set; }

    /// <summary>
    /// 指示高频输出功能是否激活
    /// </summary>
    public bool IsEnabled_HF { get; private set; }

    /// <summary>
    /// 指示电机控制功能是否激活
    /// </summary>
    public bool IsEnabled_PWM { get; private set; }

    /// <summary>
    /// 指示对时功能是否激活
    /// </summary>
    public bool IsEnabled_PPS { get; private set; }

    /// <summary>
    /// 指示校准功能是否激活
    /// </summary>
    public bool IsEnabled_Calibrate => true;
    #endregion 特殊功能 FuncS》
}
