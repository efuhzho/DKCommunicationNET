﻿using DKCommunicationNET. Core;

namespace DKCommunicationNET;

/// <summary>
/// 设备型号列表，关联协议类型
/// </summary>
public enum Models
{
    /// <summary>
    /// 协议类型为<see cref="Hex81"/>,需引用命名空间：DKCommunicationNET. Protocols. Hex81;
    /// </summary>
    Hex81 = 0x81,

    /// <summary>
    /// 协议类型为<see cref="Hex5A"/>,需引用命名空间：DKCommunicationNET. Protocols. Hex5A;
    /// </summary>
    Hex5A = 0x5A,

    /// <summary>
    /// 【设备型号】<inheritdoc cref="Hex81" />
    /// </summary>
    DK34B1 = Hex81,

    /// <summary>
    /// 【设备型号】<inheritdoc cref="Hex81" />
    /// </summary>
    DK34B2 = Hex81,

    /// <summary>
    /// 【设备型号】<inheritdoc cref = "Hex81" />
    /// </summary>
    DK34F1 = Hex81,

    /// <summary>
    /// 【设备型号】<inheritdoc cref = "Hex5A" />
    /// </summary>
    DK34B3 = Hex5A,

    /// <summary>
    /// 【设备型号】<inheritdoc cref = "Hex5A" />
    /// </summary>
    DKPTS1 = Hex5A,

    /// <summary>
    /// 【设备型号】<inheritdoc cref = "Hex5A" />
    /// </summary>
    DKPTS = Hex5A,

    //TODO 完善设备型号
}

#region Structs
/// <summary>
/// 设置谐波参数:9个字节长度
/// </summary>
public struct HarmonicArgs
{  
    /// <summary>
    /// 支持的最大谐波次数
    /// </summary>
    internal byte harmonicTimesMax = 100;
    /// <summary>
    /// 支持的最大谐波幅度
    /// </summary>
    internal float amplitudeMax = 1F;
    /// <summary>
    /// 谐波参数构造函数
    /// </summary>    
    /// <param name="harmonicTimesMax">支持的最大谐波次数</param>
    /// <param name="amplitudeMax">支持的最大谐波幅度</param>
    public HarmonicArgs ( byte harmonicTimesMax = 100 , float amplitudeMax = 1F )
    {      
        this. harmonicTimesMax = harmonicTimesMax;
        this. amplitudeMax = amplitudeMax;
    }

    private byte _harmonicTimes = 0;
    /// <summary>
    /// 谐波次数:2--32次
    /// </summary>
    public byte HarmonicTimes
    {
        get { return _harmonicTimes; }
        set
        {
            if ( value > 1 && value <= harmonicTimesMax )    //举例：谐波次数为2到31次
            {
                _harmonicTimes = value;
            }
            else
            {
                throw new Exception ( $"谐波次数支持范围为2至{harmonicTimesMax}次。" );
            }
        }
    }

    private float _amplitude = 0;
    /// <summary>
    /// 谐波幅度：0--0.4（0%--40%）
    /// </summary>
    public float Amplitude
    {
        get { return _amplitude; }
        set
        {
            if ( value >= 0 && value <= amplitudeMax )  //举例：谐波幅度叠加不超过40%；
            {
                _amplitude = value;
            }
            else
            {
                throw new Exception ( $"幅度支持范围为0至{amplitudeMax}。" );
            }
        }
    }

    private float _angle = 0;
    /// <summary>
    /// 谐波相位：0--359.99
    /// </summary>
    public float Angle
    {
        get { return _angle; }
        set
        {
            if ( value >= 0 && value <= 359.99F )
            {
                _angle = value;
            }
            else
            {
                throw new Exception ( "谐波相位支持范围为0.00°至359.99°。" );
            }
        }
    }

    /// <summary>
    /// 谐波数据转换成字节数组
    /// </summary>
    /// <param name="harmonic">谐波参数结构体</param>
    /// <param name="byteTransform">数据转换规则</param>
    /// <returns></returns>
    public  byte[ ] HarmonicToBytes ( HarmonicArgs harmonic ,IByteTransform byteTransform )
    {
        byte[ ] bytes = new byte[9];
        bytes[0] = harmonic. HarmonicTimes;

        //将谐波幅度转换为字节数组并复制到目标
        byteTransform. TransByte ( harmonic. Amplitude ). CopyTo ( bytes , 1 );

        //将谐波相位转换为字节数组并复制到目标
        byteTransform. TransByte ( harmonic. Angle ). CopyTo ( bytes , 5 );

        return bytes;
    }
}
#endregion Structs



