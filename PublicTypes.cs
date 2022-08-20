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

    private byte _harmonicTimes;
    /// <summary>
    /// 谐波次数:2--32次
    /// </summary>
    public byte HarmonicTimes
    {
        get { return _harmonicTimes; }
        set
        {
            if ( value > 1 && value < 32 )    //谐波次数为2到31次
            {
                _harmonicTimes = value;
            }
            else
            {
                throw new Exception ( "谐波次数支持范围为2至31次。" );
            }
        }
    }

    private float _amplitude;
    /// <summary>
    /// 谐波幅度：0--0.4（0%--40%）
    /// </summary>
    public float Amplitude
    {
        get { return _amplitude; }
        set
        {
            if ( value >= 0 && value <= 0.4F )  //谐波幅度叠加不超过40%；
            {
                _amplitude = value;
            }
            else
            {
                throw new Exception ( "幅度支持范围为0至40%。" );
            }
        }
    }

    private float _angle;
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
}
#endregion Structs



