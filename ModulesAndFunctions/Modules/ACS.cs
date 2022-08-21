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
    /// 定义协议工厂变量
    /// </summary>
    private readonly IProtocolFactory _protocolFactory;

    /// <summary>
    /// 定义串口类变量
    /// </summary>
    private readonly SerialPort _serialPort;

    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文的委托方法
    /// </summary>
    private readonly Func<byte[ ] , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IPacketsBuilder_ACS? _PacketsBuilder;


    #endregion

    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <param name="protocolFactory">协议工厂对象</param>
    /// <param name="serialPort">串口对象</param>
    /// <param name="methodOfCheckResponse"></param>
    internal ACS ( ushort id , IProtocolFactory protocolFactory , SerialPort serialPort , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse )
    {
        _id = id;
        _protocolFactory = protocolFactory;
        _serialPort = serialPort;
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器
        _PacketsBuilder = _protocolFactory. GetPacketBuilderOfACS ( _id ). Content; //忽略空值，调用时会捕获解引用为null的异常

    }

    #endregion

    #region 属性
    /// <inheritdoc/>
    public float Range_ACU { get; }

    /// <inheritdoc/>
    public float Range_ACI { get; }

    /// <inheritdoc/>
    public float Range_IProtect { get; }

    /// <inheritdoc/>
    public float[ ] ACU_RangesList { get; set; }
    /// <inheritdoc/>
    public float[ ] ACI_RangesList { get; set; }
    /// <inheritdoc/>
    public float[ ] IProtect_RangesList { get; set; }
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
    public float SA { get; }

    /// <inheritdoc/>
    public float SB { get; }

    /// <inheritdoc/>
    public float SC { get; }

    /// <inheritdoc/>
    public float S { get; }

    /// <inheritdoc/>
    public float PFA { get; }

    /// <inheritdoc/>
    public float PFB { get; }

    /// <inheritdoc/>
    public float PFC { get; }

    /// <inheritdoc/>
    public float PF { get; }

    /// <inheritdoc/>
    public byte Flag_A { get; }

    /// <inheritdoc/>
    public byte Flag_B { get; }

    /// <inheritdoc/>
    public byte Flag_C { get; }

    /// <inheritdoc/>
    public byte RangesCount_ACU { get; }

    /// <inheritdoc/>
    public byte RangesCount_ACI { get; }

    /// <inheritdoc/>
    public int RangeIndex_ACU { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public int RangeIndex_ACI { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public int RangeIndex_IProtect { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    /// <inheritdoc/>
    public byte RangesCount_IProtect { get; }

    /// <inheritdoc/>
    public byte URanges_Asingle { get; }

    /// <inheritdoc/>
    public byte IRanges_Asingle { get; }

    /// <inheritdoc/>
    public byte IProtectRanges_Asingle { get; }

    #endregion       

    #region 方法

    /// <inheritdoc/>
     OperateResult<byte[ ]> IModuleACS.OpenACS ( )
    {
        return CommandAction. Action ( _PacketsBuilder. PacketOfOpen , _methodOfCheckResponse );
    }

     OperateResult<byte[ ]> IModuleACS.CloseACS ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> GetRangesOfACS ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetRangesOfACS ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetAmplitudeOfACS ( float amplitude )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetWireMode ( Enum WireMode )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetClosedLoop ( Enum ClosedLoopMode )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetHarmonicMode ( Enum HarmonicMode )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> WriteHarmonics ( Enum harmonicChannels , HarmonicArgs[ ] harmonicArgs )
    {
        throw new NotImplementedException ( );
    }

    #endregion

}
