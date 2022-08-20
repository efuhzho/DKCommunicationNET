using System. IO. Ports;
using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Module;

/// <summary>
/// 交流源输出功能
/// </summary>
public class ACS : IModuleACS
{

    #region 私有字段
    /// <summary>
    /// 定义协议工厂变量
    /// </summary>
    private IProtocolFactory _protocolFactory;

    /// <summary>
    /// 定义串口类变量
    /// </summary>
    private SerialPort _serialPort;

    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文的委托方法
    /// </summary>
    private Func<byte[ ] , OperateResult<byte[ ]>> _methodOfCheckResponse;
    #endregion

    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="protocolFactory">协议工厂对象</param>
    /// <param name="serialPort">串口对象</param>
    /// <param name="methodOfCheckResponse"></param>
    internal ACS ( IProtocolFactory protocolFactory , SerialPort serialPort , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse )
    {
        _protocolFactory = protocolFactory;
        _serialPort = serialPort;
        _methodOfCheckResponse = methodOfCheckResponse;
    }
    #endregion

    #region 属性
    /// <inheritdoc/>
    public float Range_ACU => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float Range_ACI => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float Range_IProtect => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float[ ] ACU_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float[ ] ACI_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float[ ] IProtect_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public Enum WireMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public Enum CloseLoopMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public Enum HarmonicMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float Freq { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float Freq_C { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public byte HarmonicCount { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public Enum HarmonicChannels { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public HarmonicArgs[ ] Harmonics { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float UA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float UB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float UC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IPA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IPB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IPC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_UA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_UB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_UC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_IA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_IB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_IC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float PA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float PB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float PC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float P { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float QA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float QB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float QC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float Q { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    /// <inheritdoc/>
    public float SA => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float SB => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float SC => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float S => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float PFA => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float PFB => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float PFC => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public float PF => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public byte Flag_A => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public byte Flag_B => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public byte Flag_C => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public byte RangesCount_ACU => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public byte RangesCount_ACI => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public int RangeIndex_ACU { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public int RangeIndex_ACI { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public int RangeIndex_IProtect { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    /// <inheritdoc/>
    public byte RangesCount_IProtect => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public byte URanges_Asingle => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public byte IRanges_Asingle => throw new NotImplementedException ( );

    /// <inheritdoc/>
    public byte IProtectRanges_Asingle => throw new NotImplementedException ( );
    #endregion       

    #region 方法
    public OperateResult<byte[ ]> OpenACS ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> CloseACS ( )
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
