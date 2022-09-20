using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex5A. Decoders;

internal class Hex5ADecoder_Settings : IDecoder_Settings
{
    private readonly IByteTransform _byteTransform;

    public Hex5ADecoder_Settings ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    #region 《方法
   public OperateResult DecodeHandShake ( byte[ ] buffer )
    {
        try
        {
            //将缓存数据转换成List方便查找字符串的结束标志:0x00
            List<byte> bufferList = buffer. ToList ( );    //可忽略null异常

            //获取设备型号结束符的索引值
            int endIndex = bufferList. IndexOf ( 0x00 , Hex5A. DataStartIndex );

            //计算model字节长度，包含0x00结束符,7=报文头的字节数8再减去1
            int modelLength = endIndex - Hex5A. DataStartIndex + 1;
            //解析的设备型号
            Model = _byteTransform. TransString ( buffer , Hex5A. DataStartIndex , modelLength , Encoding. ASCII );

            //解析下位机版本号
            byte verA = buffer[modelLength + Hex5A. DataStartIndex];
            byte verB = buffer[modelLength + Hex5A. DataStartIndex + 1];
            byte verC = buffer[modelLength + Hex5A. DataStartIndex + 2];
            //下位机版本号
            Firmware = $"V{verA}.{verB}.{verC}";

            //解析设备编号
            int serialEndIndex = bufferList. IndexOf ( 0x00 , Hex5A. DataStartIndex + modelLength + 3 );
            int serialLength = serialEndIndex - 7 - modelLength;
            //设备编号字节长度，包含0x00结束符            
            SN = _byteTransform. TransString ( buffer , Hex5A. DataStartIndex + modelLength + 3 , serialLength , Encoding. ASCII );

            //交流功能激活状态
            byte FuncB = buffer[^8];
            bool[ ] funcB = SoftBasic. ByteToBoolArray ( FuncB );
            IsEnabled_ACS = funcB[0];
            IsEnabled_ACM = funcB[1];
            IsEnabled_ACM_Cap = funcB[2];

            //特殊交流功能激活状态
            byte FuncS = buffer[^7];
            bool[ ] funcS = SoftBasic. ByteToBoolArray ( FuncS );
            IsEnabled_DualFreqs = funcS[0];
            IsEnabled_IO = funcS[3];
            IsEnabled_PPS = funcS[6];

            //直流功能
            byte FuncD = buffer[^6];
            bool[ ] funcD = SoftBasic. ByteToBoolArray ( FuncD );
            IsEnabled_DCS = funcD[0];
            IsEnabled_DCM = funcD[1];
            IsEnabled_DCM_RIP = funcD[2];
            IsEnabled_DCS_AUX = funcD[3];

            //协议版本号A
            byte PT_VerA = buffer[^5];

            //协议版本号B
            byte PT_VerB = buffer[^4];

            //通讯协议版本号
            ProtocolVer = $"V{PT_VerA}.{PT_VerB}";
            return OperateResult. CreateSuccessResult ( );
        }
        catch ( Exception ex )
        {
            return new OperateResult ( "HandShake数据解析失败。" + ex. Message );
        }

    }
    #endregion 方法》

    #region 《属性
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
    public bool IsEnabled_Calibrate { get; }

    #endregion 特殊功能 FuncS》
    #endregion 属性》
}
