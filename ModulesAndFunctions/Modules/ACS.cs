using System. IO. Ports;
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
    private readonly Func<byte[ ] , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IPacketsBuilder_ACS? _PacketsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder _decoder;

    #endregion

    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <param name="protocolFactory">协议工厂对象</param>
    /// <param name="methodOfCheckResponse"></param>
    /// <param name="byteTransform"></param>
    internal ACS ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器
        _PacketsBuilder = protocolFactory. GetPacketBuilderOfACS ( _id , byteTransform ). Content; //忽略空值，调用时会捕获解引用为null的异常

        //接收解码器
        _decoder = protocolFactory. GetDecoder ( byteTransform );
    }

    #endregion

    #region 属性
    /// <inheritdoc/>
    public float URange_CurrentValue { get; private set; }

    /// <inheritdoc/>
    public float IRange_CurrentValue { get; private set; }

    /// <inheritdoc/>
    public float IProtectRange_CurrentValue { get; private set; }

    /// <inheritdoc/>
    public float[ ]? URanges { get; set; }

    /// <inheritdoc/>
    public float[ ]? IRanges { get; set; }

    /// <inheritdoc/>
    public float[ ]? IProtectRanges { get; set; }

    /// <inheritdoc/>
    public Enum WireMode { get; set; }
    /// <inheritdoc/>
    public Enum CloseLoopMode { get; set; }
    /// <inheritdoc/>
    public Enum HarmonicMode { get; set; }
    /// <inheritdoc/>
    public float Freq { get; set; }
    /// <inheritdoc/>
    public float Freq_C { get; set; }
    /// <inheritdoc/>
    public byte HarmonicCount { get; set; }
    /// <inheritdoc/>
    public Enum HarmonicChannels { get; set; }
    /// <inheritdoc/>
    public HarmonicArgs[ ] Harmonics { get; set; }
    /// <inheritdoc/>
    public float UA { get; set; }
    /// <inheritdoc/>
    public float UB { get; set; }
    /// <inheritdoc/>
    public float UC { get; set; }
    /// <inheritdoc/>
    public float IA { get; set; }
    /// <inheritdoc/>
    public float IB { get; set; }
    /// <inheritdoc/>
    public float IC { get; set; }
    /// <inheritdoc/>
    public float IPA { get; set; }
    /// <inheritdoc/>
    public float IPB { get; set; }
    /// <inheritdoc/>
    public float IPC { get; set; }
    /// <inheritdoc/>
    public float FAI_UA { get; set; }
    /// <inheritdoc/>
    public float FAI_UB { get; set; }
    /// <inheritdoc/>
    public float FAI_UC { get; set; }
    /// <inheritdoc/>
    public float FAI_IA { get; set; }
    /// <inheritdoc/>
    public float FAI_IB { get; set; }
    /// <inheritdoc/>
    public float FAI_IC { get; set; }
    /// <inheritdoc/>
    public float PA { get; set; }
    /// <inheritdoc/>
    public float PB { get; set; }
    /// <inheritdoc/>
    public float PC { get; set; }
    /// <inheritdoc/>
    public float P { get; set; }
    /// <inheritdoc/>
    public float QA { get; set; }
    /// <inheritdoc/>
    public float QB { get; set; }
    /// <inheritdoc/>
    public float QC { get; set; }
    /// <inheritdoc/>
    public float Q { get; set; }

    /// <inheritdoc/>
    public float SA { get; private set; }

    /// <inheritdoc/>
    public float SB { get; private set; }

    /// <inheritdoc/>
    public float SC { get; private set; }

    /// <inheritdoc/>
    public float S { get; private set; }

    /// <inheritdoc/>
    public float PFA { get; private set; }

    /// <inheritdoc/>
    public float PFB { get; private set; }

    /// <inheritdoc/>
    public float PFC { get; }

    /// <inheritdoc/>
    public float PF { get; private set; }

    /// <inheritdoc/>
    public byte Flag_A { get; private set; }

    /// <inheritdoc/>
    public byte Flag_B { get; private set; }

    /// <inheritdoc/>
    public byte Flag_C { get; private set; }

    /// <inheritdoc/>
    public byte URanges_Count { get; private set; }

    /// <inheritdoc/>
    public byte IRanges_Count { get; private set; }

    /// <inheritdoc/>
    public int URange_CurrentIndex { get; set; }
    /// <inheritdoc/>
    public int IRange_CurrentIndex { get; set; }
    /// <inheritdoc/>
    public int IProtectRange_CurrentIndex { get; set; }

    /// <inheritdoc/>
    public byte IProtectRanges_Count { get; private set; }

    /// <inheritdoc/>
    public byte URangeStartIndex_Asingle { get; private set; }

    /// <inheritdoc/>
    public byte IRangeStartIndex_Asingle { get; private set; }

    /// <inheritdoc/>
    public byte IProtectStartIndex_Asingle { get; private set; }
    byte IProperties_ACS.URange_CurrentIndex { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    byte IProperties_ACS.IRange_CurrentIndex { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    byte IProperties_ACS.IProtectRange_CurrentIndex { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    #endregion

    #region 方法
    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open ( )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Open ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop ( )
    {
        throw new NotImplementedException ( );
        /// <inheritdoc/>
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        //执行获取档位命令
        var result = CommandAction. Action ( _PacketsBuilder. Packet_GetRanges ( ) , _methodOfCheckResponse );

        //解码下位机的回复报文：此处无需判断命令执行结果，判断下放
        var decodeResult = _decoder. DecodeGetRanges_ACS ( result );

        //如果解码成功，则更新数据
        if ( decodeResult. IsSuccess )
        {
            URanges_Count = _decoder. URanges_Count;
            URangeStartIndex_Asingle = _decoder. URangeStartIndex_Asingle;
            IRanges_Count = _decoder . IRanges_Count;
            IRangeStartIndex_Asingle = _decoder.IRangeStartIndex_Asingle ;
            IProtectRanges_Count = _decoder. IProtectRanges_Count;
            IProtectStartIndex_Asingle = _decoder. IProtectStartIndex_Asingle;
            URanges = _decoder.URanges;
            IRanges = _decoder. IRanges;
            IProtectRanges = _decoder. IProtectRanges;
        }
        return result;
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_SetRanges ( rangeIndexOfACU , rangeIndexOfACI , rangeIndexOfIP ) , _methodOfCheckResponse );        
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPA = 0 , float IPB = 0 , float IPC = 0 )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_SetAmplitude ( UA , UB , UC , IA , IB , IC , IPA , IPB , IPC ) , _methodOfCheckResponse );        
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( float U , float I , float IP = 0 )
    {
        return CommandAction. Action ( SetAmplitude ( U , U , U , I , I , I , IP , IP , IP ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_SetPhase ( PhaseUa , PhaseUb , PhaseUc , PhaseIa , PhaseIb , PhaseIc ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_SetFrequency ( FreqOfAll , FreqOfC ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetWireMode ( Enum WireMode )
    {
        throw new NotImplementedException ( );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetClosedLoop ( Enum ClosedLoopMode )
    {
        throw new NotImplementedException ( );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetHarmonicMode ( Enum HarmonicMode )
    {
        throw new NotImplementedException ( );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetHarmonics ( Enum harmonicChannels , HarmonicArgs[ ] harmonicArgs )
    {
        throw new NotImplementedException ( );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( )
    {
        var result=CommandAction.Action(_PacketsBuilder.Packet_ReadData(), _methodOfCheckResponse );
        var decodeResult=_decoder.DecodeReadData_ACS( result );
        if ( decodeResult.IsSuccess )
        {
            Freq = _decoder. Freq;
            URange_CurrentIndex=_decoder. URange_CurrentIndex;
        }
        return result;
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData_Status ( )
    {
        throw new NotImplementedException ( );
    }

    #endregion

}
