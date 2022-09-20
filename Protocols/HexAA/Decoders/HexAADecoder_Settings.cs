using System. Text;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. HexAA. Decoders;

internal class HexAADecoder_Settings : IDecoder_Settings
{
    private readonly IByteTransform _byteTransform;
    internal HexAADecoder_Settings ( IByteTransform byteTransform )
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
            int modelEndIndex = bufferList. IndexOf ( 0x00 , HexAA. DataStartIndex );

            //计算model字节长度，包含0x00结束符，5=报文头的字节数6再减去1
            int modelLength = modelEndIndex - HexAA. DataStartIndex + 1;
            //解析的设备型号
            Model = _byteTransform. TransString ( buffer , HexAA. DataStartIndex , modelLength , Encoding. ASCII );

            //解析下位机版本号:两个字节取后面的一个字节
            byte verA = buffer[modelLength + HexAA. DataStartIndex + 1];
            byte verB = buffer[modelLength + HexAA. DataStartIndex + 3];
            //下位机版本号
            Firmware = $"V{verA}.{verB}";

            //解析设备编号：4为版本号的四个字节
            int serialEndIndex = bufferList. IndexOf ( 0x00 , HexAA. DataStartIndex + modelLength + 4 );
            //4为版本号的四个字节
            int serialLength = serialEndIndex - modelEndIndex - 4;
            //设备编号字节长度，包含0x00结束符            
            SN = _byteTransform. TransString ( buffer , HexAA. DataStartIndex + modelLength + 4 , serialLength , Encoding. ASCII );
            return OperateResult. CreateSuccessResult ( );
        }
        catch ( Exception ex )
        {
            return new OperateResult ( "HandShake数据解析发生异常。" + ex. Message );
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
    public bool IsEnabled_ACM => true;

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
    public bool IsEnabled_DCM => false;

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
    public bool IsEnabled_Calibrate { get; }

    #endregion 特殊功能 FuncS》
}

