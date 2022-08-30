using DKCommunicationNET. Core;
using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

/// <summary>
/// 直流表功能模块
/// </summary>
public class DCM :IModuleDCM
{
    /// <summary>
    /// 定义交流表模块对象
    /// </summary>
    private IPacketBuilder_DCM? _PacketBuilder;

    /// <summary>
    /// 设备ID
    /// </summary>
    private readonly ushort _id;

    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文的委托方法
    /// </summary>
    private readonly Func<byte[ ] , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder _decoder;


    internal DCM ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器对象
        _PacketBuilder = protocolFactory. GetPacketBuilderOfDCM ( _id  ). Content; //忽略空值，调用时会捕获解引用为null的异常

        //接收解码器对象
        _decoder = protocolFactory. GetDecoder ( byteTransform );
    }

    #region 属性>>>直流表

    /// <summary>
    /// 是否是多通道直流表
    /// </summary>
    public bool IsMultiChannel => _PacketBuilder.IsMultiChannel;

    /// <summary>
    /// 直流表电压档位集合
    /// </summary>
    public float[ ]? Ranges_DCMU { get=> _decoder. Ranges_DCMU; set=> _decoder. Ranges_DCMU = value; }
    

    /// <summary>
    /// 直流表电流档位集合
    /// </summary>
    public float[ ]? Ranges_DCMI { get => _decoder. Ranges_DCMI; set => _decoder. Ranges_DCMI = value; } 

    /// <summary>
    /// 直流纹波电压表档位集合
    /// </summary>
    public float[ ]? Ranges_DCMU_Ripple { get => _decoder. Ranges_DCMU_Ripple; set => _decoder. Ranges_DCMU_Ripple = value; } 

    /// <summary>
    /// 直流纹波电流表的档位集合
    /// </summary>
    public float[ ]? Ranges_DCMI_Ripple { get => _decoder. Ranges_DCMI_Ripple; set => _decoder. Ranges_DCMI_Ripple = value; }

    /// <summary>
    /// 直流表电压测量值
    /// </summary>
    public float DCMU => _decoder. DCMU;

    /// <summary>
    /// 直流表电流测量值
    /// </summary>
    public float DCMI => _decoder. DCMI;

    /// <summary>
    /// 直流纹波电压测量值
    /// </summary>
    public float DCMU_Ripple => _decoder. DCMU_Ripple;

    /// <summary>
    /// 直流纹波电流测量值
    /// </summary>
    public float DCMI_Ripple => _decoder. DCMI_Ripple;

    /// <summary>
    /// 直流表电压量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMU => _decoder. RangeIndex_DCMU;

    /// <summary>
    /// 直流表电流量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMI => _decoder. RangeIndex_DCMI;

    /// <summary>
    /// 直流纹波电压量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMU_Ripple => _decoder. RangeIndex_DCMU_Ripple;

    /// <summary>
    /// 直流纹波电流量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMI_Ripple => _decoder. RangeIndex_DCMI_Ripple;

    /// <summary>
    /// 直流表电压量程数量
    /// </summary>
    public byte RangesCount_DCMU => _decoder. RangesCount_DCMU;

    /// <summary>
    /// 直流表电流量程数量
    /// </summary>
    public byte RangesCount_DCMI => _decoder. RangesCount_DCMI;

    /// <summary>
    /// 直流纹波电压量程数量
    /// </summary>
    public byte RangesCount_DCMU_Ripple => _decoder. RangesCount_DCMU_Ripple;

    /// <summary>
    /// 直流纹波电流量程数量
    /// </summary>
    public byte RangesCount_DCMI_Ripple => _decoder. RangesCount_DCMI_Ripple;

    #endregion 属性>>>直流表

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        var result= CommandAction. Action ( _PacketBuilder. Packet_GetRanges ( ) , _methodOfCheckResponse );
        var decodeResult=_decoder.DecodeGetRanges_DCM ( result );
        if ( !decodeResult.IsSuccess )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. DecodeError );
        }
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( )
    {
        var result=CommandAction.Action(_PacketBuilder.Packet_ReadData(), _methodOfCheckResponse );
        var decodeResult=_decoder.DecodeReadData_DCM ( result );
        if ( !decodeResult. IsSuccess )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. DecodeError );
        }
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCMI ( byte rangeIndex_DCMI )
    {
        return CommandAction. Action ( _PacketBuilder. Packet_SetRange_DCMI (rangeIndex_DCMI ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCMU ( byte rangeIndex_DCMU )
    {
        return CommandAction. Action ( _PacketBuilder. Packet_SetRange_DCMI ( rangeIndex_DCMU ) , _methodOfCheckResponse );

    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCMI_Ripple ( byte rangeIndex_DCMU_Ripple )
    {
        return CommandAction. Action ( _PacketBuilder. Packet_SetRange_DCMI ( rangeIndex_DCMU_Ripple ) , _methodOfCheckResponse );

    } 

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCMU_Ripple ( byte rangeIndex_DCMI_Ripple )
    {
        return CommandAction. Action ( _PacketBuilder. Packet_SetRange_DCMI ( rangeIndex_DCMI_Ripple ) , _methodOfCheckResponse );

    }
}
