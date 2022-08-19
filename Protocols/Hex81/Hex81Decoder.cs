using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;
using System. Text;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议解码器
/// </summary>
internal class Hex81Decoder : IDecoder
{
    private readonly IByteTransform _byteTransform;
    public Hex81Decoder ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    #region 【属性】

    public int Offset => 6;
    public string? Model { get; private set; }
    public string? Firmware { get; private set; }
    public string? SN { get; private set; }
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
    #endregion 属性

    #region 【Decoders】

    /// <summary>
    /// 联机协议解析器
    /// </summary>
    /// <param name="result">下位机回复结果</param>
    public void DecodeHandShake ( OperateResult<byte[ ]> result )
    {
        if ( !result. IsSuccess || result. Content == null )
        {
            return;
        }

        //下位机回复的原始报文
        byte[ ] buffer = result. Content;

        //将缓存数据转换成List方便查找字符串的结束标志:0x00
        List<byte> bufferList = buffer. ToList ( );    //可忽略null异常

        //获取设备型号结束符的索引值
        int endIndex = bufferList. IndexOf ( 0x00 , Offset );

        //计算model字节长度，包含0x00结束符
        int modelLength = endIndex - 5;
        //解析的设备型号
        Model = _byteTransform. TransString ( buffer , 6 , modelLength , Encoding. ASCII );

        //解析下位机版本号
        byte verA = buffer[modelLength + 6];
        byte verB = buffer[modelLength + 7];
        //下位机版本号
        Firmware = $"V{verA}.{verB}";

        //解析设备编号
        int serialEndIndex = bufferList. IndexOf ( 0x00 , 8 + modelLength );
        int serialLength = serialEndIndex - 7 - modelLength;
        //设备编号字节长度，包含0x00结束符            
        SN = _byteTransform. TransString ( buffer , 8 + modelLength , serialLength , Encoding. ASCII );

        //基本功能激活状态
        byte FuncB = buffer[^3];
        bool[ ] funcB = SoftBasic. ByteToBoolArray ( FuncB );
        IsEnabled_ACS = funcB[0];
        IsEnabled_ACM = funcB[1];
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
    }

    #endregion Decoders

}
