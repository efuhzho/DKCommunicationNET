using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Protocols. Hex81;

namespace DKCommunicationNET. Module;

/// <summary>
/// 交流源输出功能
/// </summary>
public class ACS : IModuleACS
{   
    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IEncoder_ACS? _encoder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder_ACS? _decoder;   

    /// <summary>
    /// 执行命令的模板方法,必须先设置CanExcute属性
    /// </summary>
    internal CommandAction CommandAction;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="encoder"></param>
    /// <param name="decoder"></param>
    /// <param name="methodOfCheckResponse"></param>
    internal ACS ( IEncoder_ACS? encoder , IDecoder_ACS? decoder , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse )
    {
        //初始化报文创建器
        _encoder = encoder;

        //接收解码器
        _decoder = decoder;       

        CommandAction = new CommandAction (   methodOfCheckResponse );
    }
    #region 《属性
    #region 《档位数量
    /// <inheritdoc/>
    public byte RangesCount_ACU => ( byte ) ( _decoder == null ? 0 : _decoder. RangesCount_ACU );
    /// <inheritdoc/>
    public byte RangesCount_ACI => ( byte ) ( _decoder == null ? 0 : _decoder. RangesCount_ACI );
    /// <inheritdoc/>
    public byte RangesCount_IPr => ( byte ) ( _decoder == null ? 0 : _decoder. RangesCount_IPr );

    #endregion 档位数量》

    #region 《当前档位索引值
    /// <inheritdoc/>
    public byte RangeIndex_Ua => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_Ua );
    /// <inheritdoc/>
    public byte RangeIndex_Ia => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_Ia );
    /// <inheritdoc/>
    public byte RangeIndex_IPa => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_IPa );
    /// <inheritdoc/>
    public byte RangeIndex_Ub => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_Ub );
    /// <inheritdoc/>
    public byte RangeIndex_Uc => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_Uc );
    /// <inheritdoc/>
    public byte RangeIndex_Ux => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_Ux );
    /// <inheritdoc/>
    public byte RangeIndex_Ib => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_Ib );
    /// <inheritdoc/>
    public byte RangeIndex_Ic => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_Ic );
    /// <inheritdoc/>
    public byte RangeIndex_Ix => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_Ix );
    /// <inheritdoc/>
    public byte RangeIndex_IPb => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_IPb );
    /// <inheritdoc/>
    public byte RangeIndex_IPc => ( byte ) ( _decoder == null ? 0 : _decoder. RangeIndex_IPc );

    #endregion 当前档位索引值》

    #region 《当前档位值
    /// <inheritdoc/>
    public float RangeValue_ACU => _decoder == null ? 0 : _decoder. RangeValue_ACU;

    /// <inheritdoc/>
    public float RangeValue_ACI => _decoder == null ? 0 : _decoder. RangeValue_ACI;

    /// <inheritdoc/>
    public float RangeValue_IPr => _decoder == null ? 0 : _decoder. RangeValue_IPr;

    #endregion 当前档位值》

    #region 《单相档位起始索引值：如果值为0，说明设备是单相输出

    /// <inheritdoc/>
    public byte OnlyAStartIndex_ACU => ( byte ) ( _decoder == null ? 0 : _decoder. OnlyAStartIndex_ACU );

    /// <inheritdoc/>
    public byte OnlyAStartIndex_ACI => ( byte ) ( _decoder == null ? 0 : _decoder. OnlyAStartIndex_ACI );

    /// <inheritdoc/>
    public byte OnlyAStartIndex_IPr => ( byte ) ( _decoder == null ? 0 : _decoder. OnlyAStartIndex_IPr );

    #endregion 单相档位起始索引值》

    #region 《档位列表

    /// <inheritdoc/>
    public float[ ]? Ranges_ACU => _decoder?.Ranges_ACU;
    /// <inheritdoc/>
    public float[ ]? Ranges_ACI => _decoder?.Ranges_ACI;
    /// <inheritdoc/>
    public float[ ]? Ranges_IPr => _decoder?.Ranges_IPr;

    #endregion 档位列表》

    #region 《枚举直设
    /// <inheritdoc/>
    public WireMode WireMode { get => _decoder == null ? 0 : _decoder. WireMode; set => SetWireMode ( value ); }
    /// <inheritdoc/>
    public CloseLoopMode CloseLoopMode { get => _decoder == null ? 0 : _decoder. CloseLoopMode; set => SetClosedLoop ( value ); }
    /// <inheritdoc/>
    public HarmonicMode HarmonicMode { get => _decoder == null ? 0 : _decoder. HarmonicMode; set => SetHarmonicMode ( value ); }
    /// <inheritdoc/>
    public QP_Mode QP_Mode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    #endregion 枚举直设》

    #region 《谐波数据

    /// <inheritdoc/>
    public byte HarmonicCount => ( byte ) ( _decoder == null ? 0 : _decoder. HarmonicCount );

    /// <inheritdoc/>
    public Enum? HarmonicChannels => _decoder?.HarmonicChannels;

    /// <inheritdoc/>
    public HarmonicArgs[ ]? Harmonics => _decoder?.Harmonics;

    #endregion 谐波数据》

    #region 《频率值
    /// <inheritdoc/>
    public float Freq => _decoder == null ? 0 : _decoder. Freq;
    /// <inheritdoc/>
    public float Freq_C => _decoder == null ? 0 : _decoder. Freq_C;
    /// <inheritdoc/>
    public float Freq_X => _decoder == null ? 0 : _decoder. Freq_X;
    /// <inheritdoc/>
    public string? FrequencySync => _decoder?.FrequencySync;
    #endregion 频率值》

    #region 《电压幅值

    /// <inheritdoc/>
    public float UA =>  _decoder == null ? 0 : _decoder. UA;
    /// <inheritdoc/>
    public float UB =>  _decoder == null ? 0 : _decoder. UB;
    /// <inheritdoc/>
    public float UC => _decoder == null ? 0 : _decoder. UC;
    /// <inheritdoc/>
    public float UX => _decoder == null ? 0 : _decoder. UX;

    #endregion 电压幅值》

    #region 《电流幅值
    /// <inheritdoc/>
    public float IA => _decoder == null ? 0 : _decoder. IA;
    /// <inheritdoc/>
    public float IB => _decoder == null ? 0 : _decoder. IB;
    /// <inheritdoc/>
    public float IC => _decoder == null ? 0 : _decoder. IC;
    /// <inheritdoc/>
    public float IX => _decoder == null ? 0 : _decoder. IX;
    #endregion 电流幅值》

    #region 《保护电流幅值
    /// <inheritdoc/>
    public float IPA => _decoder == null ? 0 : _decoder. IPA;
    /// <inheritdoc/>
    public float IPB => _decoder == null ? 0 : _decoder. IPB;
    /// <inheritdoc/>
    public float IPC => _decoder == null ? 0 : _decoder. IPC;
    #endregion 保护电流幅值

    #region 《相位
    /// <inheritdoc/>
    public float FAI_UA => _decoder == null ? 0 : _decoder. FAI_UA;
    /// <inheritdoc/>
    public float FAI_UB => _decoder == null ? 0 : _decoder. FAI_UB;
    /// <inheritdoc/>
    public float FAI_UC => _decoder == null ? 0 : _decoder. FAI_UC;
    /// <inheritdoc/>
    public float FAI_UX => _decoder == null ? 0 : _decoder. FAI_UX;
    /// <inheritdoc/>
    public float FAI_IA => _decoder == null ? 0 : _decoder. FAI_IA;
    /// <inheritdoc/>
    public float FAI_IB => _decoder == null ? 0 : _decoder. FAI_IB;
    /// <inheritdoc/>
    public float FAI_IC => _decoder == null ? 0 : _decoder. FAI_IC;
    /// <inheritdoc/>
    public float FAI_IX => _decoder == null ? 0 : _decoder. FAI_IX;
    #endregion 相位》

    #region 《有功功率
    /// <inheritdoc/>
    public float PA => _decoder == null ? 0 : _decoder. PA;
    /// <inheritdoc/>
    public float PB => _decoder == null ? 0 : _decoder. PB;
    /// <inheritdoc/>
    public float PC => _decoder == null ? 0 : _decoder. PC;
    /// <inheritdoc/>
    public float PX => _decoder == null ? 0 : _decoder. PX;
    /// <inheritdoc/>
    public float P => _decoder == null ? 0 : _decoder. P;
    #endregion 有功功率》

    #region 《无功功率
    /// <inheritdoc/>
    public float QA => _decoder == null ? 0 : _decoder. QA;
    /// <inheritdoc/>
    public float QB => _decoder == null ? 0 : _decoder. QB;
    /// <inheritdoc/>
    public float QC => _decoder == null ? 0 : _decoder. QC;
    /// <inheritdoc/>
    public float QX => _decoder == null ? 0 : _decoder. QX;
    /// <inheritdoc/>
    public float Q => _decoder == null ? 0 : _decoder. Q;
    #endregion 无功功率》

    #region 《视在功率
    /// <inheritdoc/>
    public float SA => _decoder == null ? 0 : _decoder. SA;
    /// <inheritdoc/>
    public float SB => _decoder == null ? 0 : _decoder. SB;
    /// <inheritdoc/>
    public float SC => _decoder == null ? 0 : _decoder. SC;
    /// <inheritdoc/>
    public float SX => _decoder == null ? 0 : _decoder. SX;
    /// <inheritdoc/>
    public float S => _decoder == null ? 0 : _decoder. S;
    #endregion 视在功率》

    #region 《功率因数
    /// <inheritdoc/>
    public float PFA => _decoder == null ? 0 : _decoder. PFA;
    /// <inheritdoc/>
    public float PFB => _decoder == null ? 0 : _decoder. PFB;
    /// <inheritdoc/>
    public float PFC => _decoder == null ? 0 : _decoder. PFC;
    /// <inheritdoc/>
    public float PFX => _decoder == null ? 0 : _decoder. PFX;
    /// <inheritdoc/>
    public float PF => _decoder == null ? 0 : _decoder. PF;
    #endregion 功率因数》

    #region 《输出稳定状态
    /// <inheritdoc/>
    public string? Status_Ua => _decoder?.Status_Ua;
    /// <inheritdoc/>
    public string? Status_Ub => _decoder?.Status_Ub;
    /// <inheritdoc/>
    public string? Status_Uc => _decoder?.Status_Uc;
    /// <inheritdoc/>
    public string? Status_Ux => _decoder?.Status_Ux;
    /// <inheritdoc/>
    public string? Status_Ia => _decoder?.Status_Ia;
    /// <inheritdoc/>
    public string? Status_Ib => _decoder?.Status_Ib;
    /// <inheritdoc/>
    public string? Status_Ic => _decoder?.Status_Ic;
    /// <inheritdoc/>
    public string? Status_Ix => _decoder?.Status_Ix;
    #endregion 输出稳定状态》

    #region 《其他属性
    /// <inheritdoc/>
    public byte? OutputtingChannelsNum => _decoder?.OutputtingChannelsNum;
    /// <inheritdoc/>
    public string? ACSWorkingMode => _decoder?. ACSWorkingMode;   
    /// <inheritdoc/>
    public byte? OutputChannelsNum =>_decoder?.OutputChannelsNum;
    #endregion 其他属性》
    #endregion 属性》

    #region 《方法
    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction.Action( _encoder. Packet_Open ( )  );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_Stop ( )  );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        var result = CommandAction. Action ( _encoder. Packet_GetRanges ( )  );

        //如果命令执行失败
        if ( !result. IsSuccess || result. Content == null )
        {
            return result;
        }

        //解码下位机的回复报文
        var decodeResult = _decoder. DecodeGetRanges_ACS ( result. Content );

        //如果解码不成功
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }
        return result;
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetRanges ( rangeIndexOfACU , rangeIndexOfACI )  );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float UX_IPA = 0 , float IX_IPB = 0 , float IPC = 0 )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetAmplitude ( UA , UB , UC , IA , IB , IC , UX_IPA , IX_IPB , IPC )  );  
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( float Uabc , float Iabc , float IPabc = 0 )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( SetAmplitude ( Uabc , Uabc , Uabc , Iabc , Iabc , Iabc , IPabc , IPabc , IPabc )  );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetPhase ( float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc ,float PhaseIx=0)
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetPhase ( 0 , PhaseUb , PhaseUc , PhaseIa , PhaseIb , PhaseIc ,PhaseIx)  );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetFrequency ( FreqOfAll , FreqOfC )  );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetWireMode ( WireMode WireMode )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetWireMode ( WireMode )  );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetClosedLoop ( CloseLoopMode ClosedLoopMode )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetClosedLoop ( ClosedLoopMode )  );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetHarmonicMode ( HarmonicMode HarmonicMode )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetHarmonicMode ( HarmonicMode )  );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetHarmonics ( Enum harmonicChannels , HarmonicArgs[ ]? harmonicArgs = null )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //如果参数为空则清空谐波
        if ( harmonicArgs == null )
        {
            return ClearHarmonics ( harmonicChannels );
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetHarmonics ( harmonicChannels , harmonicArgs )  );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        var result = CommandAction. Action ( _encoder. Packet_ReadData ( )  );
        if ( !result. IsSuccess || result. Content == null )
        {
            return result;
        }
        var decodeResult = _decoder. DecodeReadData_ACS ( result. Content );
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }
        return result;
        //TODO encoder添加设备型号属性，判断属性是否执行ReadData_Status方法
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData_Status ( )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        var result = CommandAction. Action ( _encoder. Packet_ReadData_Status ( )  );

        //如果执行失败
        if ( !result. IsSuccess || result. Content == null )
        {
            return result;
        }

        //执行成功则解码
        var decodeResult = _decoder. DecodeReadData_Status_ACS ( result. Content );
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }

        //返回执行结果
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ClearHarmonics ( Enum harmonicChannels )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_ClearHarmonics ( harmonicChannels )  );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRanges_IP ( byte rangeIndex_IP )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetRanges_IP ( rangeIndex_IP )  );
    }

    /// <summary>
    /// 设置X相档位
    /// </summary>
    /// <param name="rangeIndex_Ux"></param>
    /// <param name="rangeIndex_Ix"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRanges_X ( byte rangeIndex_Ux , byte rangeIndex_Ix )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetRanges_X ( rangeIndex_Ux , rangeIndex_Ix )  );
    }
    #endregion 方法》
}
