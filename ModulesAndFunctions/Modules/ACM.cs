using System. Net. Http. Headers;
using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. ModulesAndFunctions. Modules;

/// <summary>
/// 交流表模块
/// </summary>
public class ACM : IModuleACM
{
    internal CommandAction CommandAction;
    IEncoder_ACM? _encoder;
    IDecoder_ACM? _decoder;
    internal ACM ( IEncoder_ACM? encoder , IDecoder_ACM? decoder , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse )
    {
        //初始化报文创建器
        _encoder = encoder;

        //接收解码器
        _decoder = decoder;

        CommandAction = new CommandAction ( methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData (bool holding=false )
    {
        do
        {
            if ( _encoder is null || _decoder is null )
            {
                return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
            }
            var result = CommandAction. Action ( _encoder. Packet_ReadData ( ) );
            if ( !result. IsSuccess || result. Content is null )
            {
                return result;
            }
            //解码
            var deResult = _decoder. DecodeReadData ( result. Content );
            if ( !deResult. IsSuccess )
            {
                return OperateResult. CreateFailedResult<byte[ ]> ( deResult );
            }
            return result;
        } while ( holding );  
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRanges ( byte rangeIndex_U , byte rangeIndex_I )
    {
        if ( _encoder is null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        return CommandAction. Action ( _encoder. Packet_SetRanges ( rangeIndex_U , rangeIndex_I , rangeIndex_U , rangeIndex_I , rangeIndex_U , rangeIndex_I ) );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        if ( _encoder is null || _decoder is null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        var result = CommandAction. Action ( GetRanges ( ) );
        if ( !result. IsSuccess || result. Content is null )
        {
            return result;
        }
        var decodeResult = _decoder. DecodeReadData ( result. Content );
        if ( decodeResult. IsSuccess )
        {
            return result;
        }
        return OperateResult. CreateFailedResult<byte[ ]> ( decodeResult );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadHarmonics ( )
    {
        return new OperateResult<byte[ ]> ( "暂未实现的方法。" );
    }

    /// <inheritdoc/>
   public OperateResult<byte[ ]> SetRangeSwitchMode ( RangeSwitchMode rangeSwitchMode )
    {
        if ( _encoder is null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        return CommandAction. Action ( _encoder. Packet_SetRangeSwitchMode ( rangeSwitchMode ) );
    }

    /// <inheritdoc/>
  public  OperateResult<byte[ ]> SetWireMode ( WireMode wireMode )
    {
        if ( _encoder is null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        return CommandAction. Action ( _encoder. Packet_SetWireMode ( wireMode ) );
    }

    /// <inheritdoc/>
   public OperateResult<byte[ ]> SetCurrentInputChannel ( CurrentInputChannel currentInputChannel )
    {
        if ( _encoder is null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        return CommandAction. Action ( _encoder. Packet_SetCurrentInputChannel ( currentInputChannel ) );
    }
    #region 《档位列表
    /// <summary>
    /// 电压档位集合
    /// </summary>
    public float[ ]? Ranges_ACU => _decoder?.Ranges_ACU;

    /// <summary>
    /// 电流档位集合
    /// </summary>
    public float[ ]? Ranges_ACI => _decoder?.Ranges_ACI;
    #endregion 档位列表》

    #region 《档位数量
    /// <summary>
    /// 电压档位个数
    /// </summary>
    public byte RangesCount_ACU => _decoder?.RangesCount_ACU ?? 0;

    /// <summary>
    /// 电流档位个数
    /// </summary>
    public byte RangesCount_ACI => _decoder?.RangesCount_ACI ?? 0;

    #endregion 档位数量》

    #region 《当前档位索引
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ua => _decoder?.RangeIndex_Ua ?? 0;
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ub => _decoder?.RangeIndex_Ub ?? 0;
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Uc => _decoder?.RangeIndex_Uc ?? 0;

    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ia => _decoder?.RangeIndex_Ia ?? 0;
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ib => _decoder?.RangeIndex_Ib ?? 0;
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ic => _decoder?.RangeIndex_Ic ?? 0;
    #endregion 当前档位索引》

    #region 《当前档位值
    /// <summary>
    /// 当前交流电压档位值，单位V
    /// </summary>
    public float RangeValue_ACU => _decoder?.RangeValue_ACU ?? 0;

    /// <summary>
    /// 当前交流电流档位值，单位A
    /// </summary>
    public float RangeValue_ACI => _decoder?.RangeValue_ACI ?? 0;
    #endregion 当前档位值》 

    #region 《频率幅值
    /// <summary>
    /// 频率(支持双频输出时为AB相频率)
    /// </summary>
    public float Freq => _decoder?.Freq ?? 50f;
    #endregion 频率幅值》

    #region 《电压幅值
    /// <summary>
    /// A相电压数据
    /// </summary>
    public float UA => _decoder?.UA ?? 0;
    /// <summary>
    /// B相电压数据
    /// </summary>
    public float UB => _decoder?.UB ?? 0;
    /// <summary>
    /// C相电压数据
    /// </summary>
    public float UC => _decoder?.UC ?? 0;
    #endregion 电压幅值》

    #region 《电流幅值
    /// <summary>
    /// A相电流数据
    /// </summary>
    public float IA => _decoder?.IA ?? 0;
    /// <summary>
    /// B相电流数据
    /// </summary>
    public float IB => _decoder?.IB ?? 0;

    /// <summary>
    /// C相电流数据
    /// </summary>
    public float IC => _decoder?.IC ?? 0;
    #endregion 电流幅值》

    #region 《相位幅值
    /// <summary>
    /// A相电压相位数据
    /// </summary>
    public float FAI_UA => _decoder?.FAI_UA ?? 0;

    /// <summary>
    /// B相电压相位数据
    /// </summary>
    public float FAI_UB => _decoder?.FAI_UB ?? 0;

    /// <summary>
    /// C相电压相位数据
    /// </summary>
    public float FAI_UC => _decoder?.FAI_UC ?? 0;


    /// <summary>
    /// A相电流相位数据
    /// </summary>
    public float FAI_IA => _decoder?.FAI_IA ?? 0;

    /// <summary>
    /// B相电流相位数据
    /// </summary>
    public float FAI_IB => _decoder?.FAI_IB ?? 0;

    /// <summary>
    /// C相电流相位数据
    /// </summary>
    public float FAI_IC => _decoder?.FAI_IC ?? 0;
    #endregion 相位幅值》

    #region 《功率幅值
    /// <summary>
    /// A相有功功率数据
    /// </summary>
    public float PA => _decoder?.PA ?? 0;

    /// <summary>
    /// B相有功功率数据
    /// </summary>
    public float PB => _decoder?.PB ?? 0;

    /// <summary>
    /// C相有功功率数据
    /// </summary>
    public float PC => _decoder?.PC ?? 0;

    /// <summary>
    /// 总有功功率数据
    /// </summary>
    public float P => _decoder?.P ?? 0;

    /// <summary>
    /// A相无功功率数据
    /// </summary>
    public float QA => _decoder?.QA ?? 0;

    /// <summary>
    /// B相无功功率数据
    /// </summary>
    public float QB => _decoder?.QB ?? 0;

    /// <summary>
    /// C相无功功率数据
    /// </summary>
    public float QC => _decoder?.QC ?? 0;

    /// <summary>
    /// 总无功功率数据
    /// </summary>    
    public float Q => _decoder?.Q ?? 0;

    /// <summary>
    /// A相视在功率，单位：VA
    /// </summary>
    public float SA => _decoder?.SA ?? 0;

    /// <summary>
    /// B相视在功率，单位：VA
    /// </summary>
    public float SB => _decoder?.SB ?? 0;

    /// <summary>
    /// C相视在功率，单位：VA
    /// </summary>
    public float SC => _decoder?.SC ?? 0;

    /// <summary>
    /// 总实在功率
    /// </summary>
    public float S => _decoder?.S ?? 0;
    #endregion 功率幅值》

    #region 《功率因数
    /// <summary>
    /// A相功率因数
    /// </summary>
    public float PFA => _decoder?.PFA ?? 0;

    /// <summary>
    /// B相功率因数
    /// </summary>
    public float PFB => _decoder?.PFB ?? 0;

    /// <summary>
    /// C相功率因数
    /// </summary>
    public float PFC => _decoder?.PFC ?? 0;

    /// <summary>
    /// 总功率因数
    /// </summary>
    public float PF => _decoder?.PF ?? 0;
    #endregion 功率因数》

    #region 《谐波数据
    /// <summary>
    /// 当前输出的谐波个数
    /// </summary>
    public byte HarmonicCount => _decoder?.HarmonicCount ?? 0;

    /// <summary>
    /// 当前谐波输出通道
    /// </summary>
    public Channels? HarmonicChannels => _decoder?. HarmonicChannels;

    /// <summary>
    /// 当前谐波输出数据
    /// </summary>
    public HarmonicArgs[ ]? Harmonics => _decoder?. Harmonics;
    #endregion 谐波数据》

    #region 《枚举属性
    /// <summary>
    /// 当前接线模式
    /// </summary>
    public WireMode WireMode { get => WireMode; set =>SetWireMode ( value ); }
    /// <summary>
    /// 当前谐波模式
    /// </summary>
    public HarmonicMode HarmonicMode => _decoder?. HarmonicMode??HarmonicMode.ValidValues;//TODO 完善方法
    /// <summary>
    /// 无功计算方法
    /// </summary>
    public QP_Mode QP_Mode => _decoder?. QP_Mode??QP_Mode.三角法;//TODO 完善
    /// <summary>
    /// 档位切换模式
    /// </summary>
    public RangeSwitchMode RangeSwitchMode { get=> RangeSwitchMode; set=>SetRangeSwitchMode(value); }
    /// <summary>
    /// 大小电流输入通道切换
    /// </summary>
    public CurrentInputChannel CurrentInputChannel { get=>CurrentInputChannel; set=>SetCurrentInputChannel(value); }
    HarmonicMode IPropertiesACM.HarmonicMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    QP_Mode IPropertiesACM.QP_Mode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    #endregion 枚举属性》
}
