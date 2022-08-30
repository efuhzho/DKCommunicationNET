using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

/// <summary>
/// 交流源输出功能
/// </summary>
public class ACS : IModuleACS
{

    #region 私有字段

    /// <summary>
    /// 设备ID
    /// </summary>
    private readonly ushort _id;

    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文的委托方法
    /// </summary>
    private readonly Func<byte[ ] , bool , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IPacketsBuilder_ACS? _packetsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder _decoder;

    /// <summary>
    /// 功能是否激活
    /// </summary>
    private bool _isEnabled;

    #endregion

    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <param name="protocolFactory">协议工厂对象</param>
    /// <param name="methodOfCheckResponse"></param>
    /// <param name="byteTransform"></param>
    /// <param name="isEnabled"></param>
    internal ACS ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform , bool isEnabled )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器
        _packetsBuilder = protocolFactory. GetPacketBuilderOfACS ( _id , byteTransform ). Content;

        //接收解码器
        _decoder = protocolFactory. GetDecoder ( byteTransform );

        _isEnabled = isEnabled;
    }

    #endregion

    #region 属性
    /// <inheritdoc/>
    public float RangeValue_ACU => _decoder. RangeValue_ACU;

    /// <inheritdoc/>
    public float RangeValue_ACI => _decoder. RangeValue_ACI;

    /// <inheritdoc/>
    public float RangeValue_IPr { get => _decoder. RangeValue_IPr; }

    /// <inheritdoc/>
    public float[ ]? Ranges_ACU { get => _decoder. Ranges_ACU; set => _decoder. Ranges_ACU = value; }

    /// <inheritdoc/>
    public float[ ]? Ranges_ACI { get => _decoder. Ranges_ACI; set => _decoder. Ranges_ACU = value; }

    /// <inheritdoc/>
    public float[ ]? Ranges_IPr { get => _decoder. Ranges_IPr; set => _decoder. Ranges_IPr = value; }

    /// <inheritdoc/>
    public WireMode WireMode { get => _decoder. WireMode; set => SetWireMode ( value ); }
    /// <inheritdoc/>
    public CloseLoopMode CloseLoopMode { get => _decoder. CloseLoopMode; set => SetClosedLoop ( value ); }
    /// <inheritdoc/>
    public HarmonicMode HarmonicMode { get => _decoder. HarmonicMode; set => SetHarmonicMode ( value ); }
    /// <inheritdoc/>
    public float Freq => _decoder. Freq;
    /// <inheritdoc/>
    public float Freq_C => _decoder. Freq_C;
    /// <inheritdoc/>
    public byte HarmonicCount => _decoder. HarmonicCount;
    /// <inheritdoc/>
    public Channels_Harmonic? HarmonicChannels => _decoder. HarmonicChannels;
    /// <inheritdoc/>
    public HarmonicArgs[ ]? Harmonics => _decoder. Harmonics;
    /// <inheritdoc/>
    public float UA => _decoder. UA;
    /// <inheritdoc/>
    public float UB => _decoder. UB;
    /// <inheritdoc/>
    public float UC => _decoder. UC;
    /// <inheritdoc/>
    public float IA => _decoder. IA;
    /// <inheritdoc/>
    public float IB => _decoder. IB;
    /// <inheritdoc/>
    public float IC => _decoder. IC;
    /// <inheritdoc/>
    public float IPA => _decoder. IPA;
    /// <inheritdoc/>
    public float IPB => _decoder. IPB;
    /// <inheritdoc/>
    public float IPC => _decoder. IPC;
    /// <inheritdoc/>
    public float FAI_UA => _decoder. FAI_UA;
    /// <inheritdoc/>
    public float FAI_UB => _decoder. FAI_UB;
    /// <inheritdoc/>
    public float FAI_UC => _decoder. FAI_UC;
    /// <inheritdoc/>
    public float FAI_IA => _decoder. FAI_IA;
    /// <inheritdoc/>
    public float FAI_IB => _decoder. FAI_IB;
    /// <inheritdoc/>
    public float FAI_IC => _decoder. FAI_IC;
    /// <inheritdoc/>
    public float PA => _decoder. PA;
    /// <inheritdoc/>
    public float PB => _decoder. PB;
    /// <inheritdoc/>
    public float PC => _decoder. PC;
    /// <inheritdoc/>
    public float P => _decoder. P;
    /// <inheritdoc/>
    public float QA => _decoder. QA;
    /// <inheritdoc/>
    public float QB => _decoder. QB;
    /// <inheritdoc/>
    public float QC => _decoder. QC;
    /// <inheritdoc/>
    public float Q => _decoder. Q;

    /// <inheritdoc/>
    public float SA => _decoder. SA;

    /// <inheritdoc/>
    public float SB => _decoder. SB;
    /// <inheritdoc/>
    public float SC => _decoder. SC;

    /// <inheritdoc/>
    public float S => _decoder. S;

    /// <inheritdoc/>
    public float PFA => _decoder. PFA;

    /// <inheritdoc/>
    public float PFB => _decoder. PFB;

    /// <inheritdoc/>
    public float PFC => _decoder. PFC;

    /// <inheritdoc/>
    public float PF => _decoder. PF;

    /// <inheritdoc/>
    public byte Flag_A => _decoder. Flag_A;

    /// <inheritdoc/>
    public byte Flag_B => _decoder. Flag_B;

    /// <inheritdoc/>
    public byte Flag_C => _decoder. Flag_C;

    /// <inheritdoc/>
    public byte RangesCount_ACU => _decoder. RangesCount_ACU;

    /// <inheritdoc/>
    public byte RangesCount_ACI => _decoder. RangesCount_ACI;

    /// <inheritdoc/>
    public byte RangeIndex_ACU => _decoder. RangeIndex_ACU;
    /// <inheritdoc/>
    public byte RangeIndex_ACI => _decoder. RangeIndex_ACI;
    /// <inheritdoc/>
    public byte RangeIndex_IPr => _decoder. RangeIndex_IPr;

    /// <inheritdoc/>
    public byte RangesCount_IPr => _decoder. RangesCount_IPr;

    /// <inheritdoc/>
    public byte OnlyAStartIndex_ACU => _decoder. OnlyAStartIndex_ACU;

    /// <inheritdoc/>
    public byte OnlyAStartIndex_ACI => _decoder. OnlyAStartIndex_ACI;

    /// <inheritdoc/>
    public byte OnlyAStartIndex_IPr => _decoder. OnlyAStartIndex_IPr;

    #endregion

    #region 方法
    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_Open ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_Stop ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        //执行获取档位命令
        var result = CommandAction. Action ( _packetsBuilder. Packet_GetRanges ( ) , _methodOfCheckResponse );

        if ( !result. IsSuccess )
        {
            return result;
        }

        //解码下位机的回复报文：此处无需判断命令执行结果，判断下放
        var decodeResult = _decoder. DecodeGetRanges_ACS ( result );

        //如果解码不成功
        if ( decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }
        return result;
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetRanges ( rangeIndexOfACU , rangeIndexOfACI , rangeIndexOfIP ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPA = 0 , float IPB = 0 , float IPC = 0 )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetAmplitude ( UA , UB , UC , IA , IB , IC , IPA , IPB , IPC ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( float U , float I , float IP = 0 )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( SetAmplitude ( U , U , U , I , I , I , IP , IP , IP ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetPhase ( float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetPhase ( 0 , PhaseUb , PhaseUc , PhaseIa , PhaseIb , PhaseIc ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetFrequency ( FreqOfAll , FreqOfC ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetWireMode ( WireMode WireMode )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetWireMode ( WireMode ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetClosedLoop ( CloseLoopMode ClosedLoopMode )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetClosedLoop ( ClosedLoopMode , HarmonicMode ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetHarmonicMode ( HarmonicMode HarmonicMode )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetClosedLoop ( CloseLoopMode , HarmonicMode ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetHarmonics ( Channels_Harmonic harmonicChannels , HarmonicArgs[ ]? harmonicArgs = null )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        byte channel = Convert. ToByte ( harmonicChannels );
        return CommandAction. Action ( _packetsBuilder. Packet_SetHarmonics ( channel , harmonicArgs ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        var result = CommandAction. Action ( _packetsBuilder. Packet_ReadData ( ) , _methodOfCheckResponse );
        if ( !result. IsSuccess )
        {
            return result;
        }
        var decodeResult = _decoder. DecodeReadData_ACS ( result );
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData_Status ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行报文发送并接收下位机回复报文
        var result = CommandAction. Action ( _packetsBuilder. Packet_ReadData_Status ( ) , _methodOfCheckResponse );

        //如果执行失败
        if ( !result. IsSuccess )
        {
            return result;
        }

        //执行成功则解码
        var decodeResult = _decoder. DecodeReadData_Status_ACS ( result );
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }

        //返回执行结果
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ClearHarmonics ( Channels_Harmonic harmonicChannels )
    {
        return SetHarmonics ( harmonicChannels );
    }

    #endregion

    //#region Private Methods

    ///// <summary>
    ///// 执行命令前的功能状态校验,处理空引用异常
    ///// </summary>
    ///// <returns></returns>
    //OperateResult<byte[ ]> ExtraBeforeAction ( )
    //{
    //    var result = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );

    //    if ( !result. IsSuccess || _packetsBuilder == null )
    //    {
    //        return result;
    //    }
    //    return OperateResult. CreateSuccessResult ( Array. Empty<byte> ( ) );
    //}
    //#endregion

}
