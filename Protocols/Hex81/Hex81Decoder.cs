﻿using DKCommunicationNET. BasicFramework;
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

    public int Offset => Hex81Information. DataStartIndex;

    public string? Model { get; set; }

    public string? SN { get; set; }

    public string? Firmware { get; private set; }

    public string? ProtocolVer => string. Empty;

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
    #endregion 属性

    #region 【Decoders】

    public void DecodeHandShake ( OperateResult<byte[ ]> response )
    {
        if ( !response. IsSuccess || response. Content == null )
        {
            return;
        }

        //下位机回复的原始报文
        byte[ ] buffer = response. Content;

        //将缓存数据转换成List方便查找字符串的结束标志:0x00
        List<byte> bufferList = buffer. ToList ( );    //可忽略null异常

        //获取设备型号结束符的索引值
        int endIndex = bufferList. IndexOf ( 0x00 , Offset );

        //计算model字节长度，包含0x00结束符，5=报文头的字节数6再减去1
        int modelLength = endIndex - Offset + 1;
        //解析的设备型号
        Model = _byteTransform. TransString ( buffer , Offset , modelLength , Encoding. ASCII );

        //解析下位机版本号
        byte verA = buffer[modelLength + Offset];
        byte verB = buffer[modelLength + Offset + 1];
        //下位机版本号
        Firmware = $"V{verA}.{verB}";

        //解析设备编号
        int serialEndIndex = bufferList. IndexOf ( 0x00 , Offset + modelLength + 2 );
        int serialLength = serialEndIndex - 7 - modelLength;
        //设备编号字节长度，包含0x00结束符            
        SN = _byteTransform. TransString ( buffer , Offset + modelLength + 2 , serialLength , Encoding. ASCII );

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
    public OperateResult<byte , byte , byte , byte , byte , byte , float[ ] , float[ ] , float[ ]> DecodeGetRanges_ACS ( OperateResult<byte[ ]> response )
    {
        if ( !response. IsSuccess || response. Content == null )
        {
            return new OperateResult<byte , byte , byte , byte , byte , byte , float[ ] , float[ ] , float[ ]> ( response. Message );
        }

        //下位机回复的经验证的有效报文
        byte[ ] responseBytes = response. Content;

        //电压档位数量
        byte uRangesCount = responseBytes[6];

        //单相电压档位起始档位索引值
        byte uRanges_Asingle = responseBytes[7];

        //电流档位数量
        byte iRangesCount = responseBytes[8];

        //单相电流档位起始档位索引值
        byte iRanges_Asingle = responseBytes[9];

        //保护电流档位数量
        byte iProtectRangesCount = responseBytes[10];

        //单相保护电流档位起始档位索引值
        byte iProtectRanges_Asingle = responseBytes[11];

        //电压档位集合
        float[ ] uRanges;

        //电流档位集合
        float[ ] iRanges;

        //保护电流档位集合
        float[ ] iProtectRanges;

        uRanges = _byteTransform. TransSingle ( responseBytes , 12 , uRangesCount );

        iRanges = _byteTransform. TransSingle ( responseBytes , 12 + 4 * uRangesCount , iRangesCount );

        iProtectRanges = _byteTransform. TransSingle ( responseBytes , 12 + 4 * uRangesCount + 4 * iRangesCount , iProtectRangesCount );

        return OperateResult. CreateSuccessResult ( uRangesCount , uRanges_Asingle , iRangesCount , iRanges_Asingle , iProtectRangesCount , iProtectRanges_Asingle , uRanges , iRanges , iProtectRanges );
    }
    #endregion Decoders

}
