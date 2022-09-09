using DKCommunicationNET. Core;

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
    DK_34B1 = Hex81,

    /// <summary>
    /// 【设备型号】<inheritdoc cref="Hex81" />
    /// </summary>
    DK_34B2 = Hex81,

    /// <summary>
    /// 【设备型号】<inheritdoc cref = "Hex81" />
    /// </summary>
    DK_34F1 = Hex81,

    /// <summary>
    /// 【设备型号】<inheritdoc cref = "Hex5A" />
    /// </summary>
    DK_34B3 = Hex5A,

    /// <summary>
    /// 【设备型号】<inheritdoc cref = "Hex5A" />
    /// </summary>
    DK_PTS1 = Hex5A,

    /// <summary>
    /// 【设备型号】<inheritdoc cref = "Hex5A" />
    /// </summary>
    DK_PTS = Hex5A,

    //TODO 完善设备型号
}

#region CloseLoop 闭环控制定义、谐波模式
/// <summary>
/// 接线方式枚举
/// </summary>
public enum WireMode : byte
{
    /// <summary>
    /// 三相四线制
    /// </summary>
    三相四线制 = 00,

    /// <summary>
    /// 三相三线制
    /// </summary>
    三相三线制 = 01,
}

/// <summary>
/// 闭环控制定义
/// </summary>
public enum CloseLoopMode : byte
{
    /// <summary>
    /// 闭环
    /// </summary>
    闭环 = 0,
    /// <summary>
    /// 开环
    /// </summary>
    开环 = 1
}

/// <summary>
/// 谐波模式
/// </summary>
public enum HarmonicMode : byte
{
    /// <summary>
    /// 以真有效值的百分比输入谐波
    /// </summary>
    真有效值 = 0,

    /// <summary>
    /// 以基波值的百分比输入谐波
    /// </summary>
    基波值 = 1
}

/// <summary>
/// 输出通道：Ua;Ub;Uc:Ux;Ia;Ib;Ic;Ix;IPa;IPb;IPc
/// </summary>
[Flags]
public enum Channels:byte
{
    /// <summary>
    /// A相电压通道
    /// </summary>
    Ua = 0b_0000_0001,
    /// <summary>
    /// A相电流通道
    /// </summary>
    Ia = 0b_0000_0010,
    /// <summary>
    /// B相电压通道
    /// </summary>
    Ub = 0b_0000_0100,
    /// <summary>
    /// B相电流通道
    /// </summary>
    Ib = 0b_0000_1000,
    /// <summary>
    /// C相电压通道
    /// </summary>
    Uc = 0b_0001_0000,
    /// <summary>
    /// C相电流通道
    /// </summary>
    Ic = 0b_0010_0000,
    /// <summary>
    /// X相电压通道
    /// </summary>
    Ux = 0b_0100_0000,
    /// <summary>
    /// X相电流通道
    /// </summary>
    Ix = 0b_1000_0000,
    /// <summary>
    /// 所有相电压:[不含X相]
    /// </summary>
    Uabc = Ua | Ub | Uc,
    /// <summary>
    /// 所有相电流:[不含X相]
    /// </summary>
    Iabc = Ia | Ib | Ic,
    /// <summary>
    /// 所有电压电流通道:[不含X相]
    /// </summary>
    UIabc = Uabc | Iabc,
    /// <summary>
    /// 所有电压电流通道:[含X相]
    /// </summary>
    UIabcx = UIabc | Ux | Ix
}

/// <summary>
/// 电能误差查询通道
/// </summary>
[Flags]
public enum Channels_ReadEPQ
{
    /// <summary>
    /// 通道1
    /// </summary>
    Channel1 = 0b_0000_0000_0000_0001,
    /// <summary>
    /// 通道2
    /// </summary>
    Channel2 = 0b_0000_0000_0000_0010,
    /// <summary>
    /// 通道3
    /// </summary>
    Channel3 = 0b_0000_0000_0000_0100,
    /// <summary>
    /// 通道4
    /// </summary>
    Channel4 = 0b_0000_0000_0000_1000,

    /// <summary>
    /// 通道5
    /// </summary>
    Channel5 = 0b_0000_0000_0001_0000,

    /// <summary>
    /// 通道6
    /// </summary>
    Channel6 = 0b_0000_0000_0010_0000,

    /// <summary>
    /// 通道7
    /// </summary>
    Channel7 = 0b_0000_0000_0100_0000,

    /// <summary>
    /// 通道8
    /// </summary>
    Channel8 = 0b_0000_0000_1000_0000,

    /// <summary>
    /// 通道9
    /// </summary>
    Channel9 = 0b_0000_0001_0000_0000,

    /// <summary>
    /// 通道10
    /// </summary>
    Channel10 = 0b_0000_0010_0000_0000,

    /// <summary>
    /// 通道11
    /// </summary>
    Channel11 = 0b_0000_0100_0000_0000,

    /// <summary>
    /// 通道12
    /// </summary>
    Channel12 = 0b_0000_1000_0000_0000,

    /// <summary>
    /// 通道13
    /// </summary>
    Channel13 = 0b_0001_0000_0000_0000,

    /// <summary>
    /// 通道14
    /// </summary>
    Channel14 = 0b_0010_0000_0000_0000,

    /// <summary>
    /// 通道15
    /// </summary>
    Channel15 = 0b_0100_0000_0000_0000,

    /// <summary>
    /// 通道16
    /// </summary>
    Channel16 = 0b_1000_0000_0000_0000,

    /// <summary>
    /// 所有通道
    /// </summary>
    Channel_All = 0xFF
}

/// <summary>
/// 无功计算方法
/// </summary>
public enum QP_Mode : byte
{
    /// <summary>
    /// 滤波法
    /// </summary>
    滤波法 = 0,
    /// <summary>
    /// 三角法
    /// </summary>
    三角法 = 1,
    /// <summary>
    /// 时延法
    /// </summary>
    时延法 = 2
}

/// <summary>
/// 频率标志
/// </summary>
internal enum FrequencySync : byte
{
    /// <summary>
    /// 
    /// </summary>
    同频 = 0,
    /// <summary>
    /// 
    /// </summary>
    异频 = 1
}
#endregion


#region Structs
/// <summary>
/// 设置谐波参数:9个字节长度
/// </summary>
public struct HarmonicArgs
{
    /// <summary>
    /// 支持的最大谐波次数
    /// </summary>
    internal readonly byte harmonicTimesMax = 100;
    /// <summary>
    /// 支持的最大谐波幅度
    /// </summary>
    internal readonly float amplitudeMax = 1F;
    /// <summary>
    /// 谐波参数构造函数
    /// </summary>    
    public HarmonicArgs ( byte HarmonicTimes , float Amplitude , float Angle )
    {
        _harmonicTimes = HarmonicTimes;
        _amplitude = Amplitude;
        _angle = Angle;
    }

    private byte _harmonicTimes = 0;
    /// <summary>
    /// 谐波次数:大于1小于100
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
    /// 谐波幅度：0--0.4（0% 到 40%）
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
    public static byte[ ] HarmonicToBytes ( HarmonicArgs harmonic , IByteTransform byteTransform )
    {
        byte[ ] bytes = new byte[9];
        bytes[0] = harmonic. HarmonicTimes;

        //将谐波幅度转换为字节数组并复制到目标
        byteTransform. TransByte ( harmonic. Amplitude ). CopyTo ( bytes , 1 );

        //将谐波相位转换为字节数组并复制到目标
        byteTransform. TransByte ( harmonic. Angle ). CopyTo ( bytes , 5 );

        return bytes;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString ( )
    {
        return $"谐波次数：{HarmonicTimes}；谐波幅度：{Amplitude}；谐波相位：{Angle}。支持的最大谐波次数：{harmonicTimesMax}；支持的最大谐波幅度：{amplitudeMax}";
    }
}


#endregion Structs



