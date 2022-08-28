using System. IO. Ports;
using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;
using System. Security. AccessControl;
using System. Runtime. CompilerServices;

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
    public byte RangesCount_DCU { get; private set; }

    /// <inheritdoc/>
    public byte RangesCount_DCI { get; private set; }

    /// <inheritdoc/>
    public byte RangesCount_DCR { get; private set; }

    /// <inheritdoc/>
    public float DCR { get; private set; }

    /// <inheritdoc/>
    public float DCU { get; private set; }

    /// <inheritdoc/>
    public float DCI { get; private set; }

    /// <inheritdoc/>
    public float[ ]? Ranges_DCU { get; private set; }

    /// <inheritdoc/>
    public float[ ]? Ranges_DCI { get; private set; }

    /// <inheritdoc/>
    public float[ ]? Ranges_DCR { get; private set; }

    /// <inheritdoc/>
    public bool IsOpen_DCU { get; private set; }

    /// <inheritdoc/>
    public bool IsOpen_DCI { get; private set; }

    /// <inheritdoc/>
    public bool IsOpen_DCR { get; private set; }

    /// <inheritdoc/>
    public bool IsAutoRange_DCU { get => _PacketsBuilder. IsAutoRange_DCU; set => _PacketsBuilder. IsAutoRange_DCU = value; }

    /// <inheritdoc/>
    public bool IsAutoRange_DCI { get => _PacketsBuilder. IsAutoRange_DCI; set => _PacketsBuilder. IsAutoRange_DCI = value; }

    /// <inheritdoc/>
    public bool IsAutoRange_DCR { get => _PacketsBuilder. IsAutoRange_DCR; set => _PacketsBuilder. IsAutoRange_DCR = value; }

    /// <inheritdoc/>
    public byte RangeIndex_DCI { get; set; }

    /// <inheritdoc/>
    public byte RangeIndex_DCR { get; set; }

    /// <inheritdoc/>
    public byte RangeIndex_DCU { get; set; }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        //执行命令并获取回复报文
        var result = CommandAction. Action ( _PacketsBuilder. Packet_GetRanges ( ) , _methodOfCheckResponse );

        //解码：连看都不看拿到的命令执行结果，直接甩给解码器
        var decodeResult = _decoder. DecodeGetRanges_DCS ( result );

        //如果解码成功就刷新数据
        if ( decodeResult. IsSuccess )
        {
            RangesCount_DCU = _decoder. RangesCount_DCU;
            RangesCount_DCI = _decoder. RangesCount_DCI;
            RangesCount_DCR = _decoder. RangesCount_DCR;

            Ranges_DCU = _decoder. Ranges_DCU;
            Ranges_DCI = _decoder. Ranges_DCI;
            Ranges_DCR = _decoder. Ranges_DCR;
        }

        //管他妈的解码成不成功，直接交出命令执行结果
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCI ( )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Open_DCI ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCR ( )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Open_DCR ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCU ( )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Open_DCU ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( char? Resistor = null )
    {
        var result = CommandAction. Action ( _PacketsBuilder. Packet_ReadData ( Resistor ) , _methodOfCheckResponse );

        //解码
        var decodeResult = _decoder. DecodeReadData_DCS ( result );

        //如果解码成功
        if ( decodeResult. IsSuccess )
        {
            DCU = _decoder. DCU;
            DCI = _decoder. DCI;
            DCR = _decoder. DCR;

            IsOpen_DCU = _decoder. IsOpen_DCU;
            IsOpen_DCI = _decoder. IsOpen_DCI;
            IsOpen_DCR = _decoder. IsOpen_DCR;

            RangeIndex_DCU = _decoder. RangeIndex_DCU;
            RangeIndex_DCI = _decoder. RangeIndex_DCI;
            RangeIndex_DCR = _decoder. RangeIndex_DCR;
        }
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCI ( float SData , byte? rangeIndex_DCI = null )
    {
        if ( rangeIndex_DCI == null )
        {
            return CommandAction. Action ( _PacketsBuilder. Packet_SetAmplitude_DCI ( SData , RangeIndex_DCI ) , _methodOfCheckResponse );
        }
        return CommandAction. Action ( _PacketsBuilder. Packet_SetAmplitude_DCI ( SData , ( byte ) rangeIndex_DCI ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCR ( float SData , byte? rangeIndex_DCR = null )
    {
        if ( rangeIndex_DCR == null )
        {
            return CommandAction. Action ( _PacketsBuilder. Packet_SetAmplitude_DCR ( SData , RangeIndex_DCR ) , _methodOfCheckResponse );
        }
        return CommandAction. Action ( _PacketsBuilder. Packet_SetAmplitude_DCR ( SData , ( byte ) rangeIndex_DCR ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCU ( float SData , byte? rangeIndex_DCU = null )
    {
        if ( rangeIndex_DCU == null )
        {
            return CommandAction. Action ( _PacketsBuilder. Packet_SetAmplitude_DCU ( SData , RangeIndex_DCU ) , _methodOfCheckResponse );
        }
        return CommandAction. Action ( _PacketsBuilder. Packet_SetAmplitude_DCU ( SData , ( byte ) rangeIndex_DCU ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCI ( byte rangeIndex_DCI )
    {
        throw new NotImplementedException ( );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCR ( byte rangeIndex_DCR )
    {
        throw new NotImplementedException ( );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCU ( byte rangeIndex_DCU )
    {
        throw new NotImplementedException ( );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCI ( )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Stop_DCI ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCR ( )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Stop_DCR ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCU ( )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_Stop_DCU ( ) , _methodOfCheckResponse );
    }
}
