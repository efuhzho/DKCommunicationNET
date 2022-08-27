using System. IO. Ports;
using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;
using System. Security. AccessControl;

namespace DKCommunicationNET. Module;

/// <summary>
/// 直流源功能模块
/// </summary>
public class DCS : IModuleDCS, IProperties_DCS
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
    private readonly IPacketBuilder_DCS? _PacketsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder _decoder;

    #endregion
    internal DCS ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器对象
        _PacketsBuilder = protocolFactory. GetPacketBuilderOfDCS ( _id , byteTransform ). Content; //忽略空值，调用时会捕获解引用为null的异常

        //接收解码器对象
        _decoder = protocolFactory. GetDecoder ( byteTransform );
    }

    /// <inheritdoc/>
    public byte URanges_Count_DCS { get; private set; }

    /// <inheritdoc/>
    public byte IRanges_Count_DCS { get; private set; }

    /// <inheritdoc/>
    public float U_CurrentValue_DCS { get; private set; }

    /// <inheritdoc/>
    public float I_CurrentValue_DCS { get; private set; }

    /// <inheritdoc/>
    public byte Index_CurrentRange_DCS { get; set; }

    /// <inheritdoc/>
    public float[ ]? URanges_DCS { get; private set; }

    /// <inheritdoc/>
    public float[ ]? IRanges_DCS { get; private set; }

    /// <inheritdoc/>
    public Enum? OutPutType_DCS { get; set; }

    /// <inheritdoc/>
    public bool U_IsOpen_DCS { get; private set; }

    /// <inheritdoc/>
    public bool I_IsOpen_DCS { get; private set; }

    /// <inheritdoc/>
    public float R_CurrentValue_DCS { get; private set; }

    /// <inheritdoc/>
    public bool R_IsOpen_DCS { get; private set; }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        var result = CommandAction. Action ( _PacketsBuilder. Packet_GetRanges ( ) , _methodOfCheckResponse );
        var decodeResult = _decoder. DecodeGetRanges_DCS ( result );
        if ( decodeResult. IsSuccess )
        {
            URanges_Count_DCS = _decoder. URanges_Count_DCS;
            IRanges_Count_DCS = _decoder. IRanges_Count_DCS;
            URanges_DCS = _decoder. URanges_DCS;
            IRanges_DCS = _decoder. IRanges_DCS;
        }
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open ( )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Open ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( Enum? dCSourceType = null )
    {
        var result = CommandAction. Action ( _PacketsBuilder. Packet_ReadData ( Convert. ToByte ( dCSourceType ) ) , _methodOfCheckResponse );
        var decodeResult = _decoder. DecodeReadData_DCS ( result );
        if ( decodeResult. IsSuccess )
        {
            Index_CurrentRange_DCS = _decoder. Index_CurrentRange_DCS;
            OutPutType_DCS = _decoder. OutPutType_DCS;
            U_CurrentValue_DCS = _decoder. U_CurrentValue_DCS;
            U_IsOpen_DCS = _decoder. U_IsOpen_DCS;
            I_CurrentValue_DCS = _decoder. I_CurrentValue_DCS;
            I_IsOpen_DCS = _decoder. I_IsOpen_DCS;
            R_CurrentValue_DCS = _decoder. R_CurrentValue_DCS;
            R_IsOpen_DCS = _decoder. R_IsOpen_DCS;
        }
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( byte rangeIndex , float SData , Enum dCSourceType )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_SetAmplitude ( rangeIndex , SData , Convert. ToByte ( dCSourceType ) ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange ( byte rangeIndex , Enum dCSourceType )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_SetRange ( rangeIndex , Convert. ToByte ( dCSourceType ) ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_AutoMode ( Enum dCSourceType )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_SetRange_Auto ( Convert. ToByte ( dCSourceType ) ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop ( Enum? type = null )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Stop ( ) , _methodOfCheckResponse );
    }
}
