using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

/// <summary>
/// 直流表功能模块
/// </summary>
public class DCM : IModuleDCM
{
    /// <summary>
    /// 定义交流表模块对象
    /// </summary>
    private readonly IEncoder_DCM? _encoder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder_DCM? _decoder;

    internal CommandAction CommandAction;

    internal DCM ( IEncoder_DCM? encoder , IDecoder_DCM? decoder , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse  )
    {
        //初始化报文创建器
        _encoder = encoder;

        //接收解码器
        _decoder = decoder;

        CommandAction = new CommandAction ( methodOfCheckResponse );
    }

    #region 《属性

    /// <summary>
    /// 是否是多通道直流表
    /// </summary>
    public bool IsMultiChannel
    {
        get
        {
            if ( _encoder == null )
            {
                return false;
            }
            return _encoder. IsMultiChannel;
        }
    }
    /// <summary>
    /// 直流表电压档位集合
    /// </summary>
    public float[ ]? Ranges_DCMU => _decoder?. Ranges_DCMU;

    /// <summary>
    /// 直流表电流档位集合
    /// </summary>
    public float[ ]? Ranges_DCMI => _decoder?. Ranges_DCMI;

    /// <summary>
    /// 直流纹波电压表档位集合
    /// </summary>
    public float[ ]? Ranges_DCMU_Ripple => _decoder?. Ranges_DCMU_Ripple;

    /// <summary>
    /// 直流纹波电流表的档位集合
    /// </summary>
    public float[ ]? Ranges_DCMI_Ripple => _decoder?.Ranges_DCMI_Ripple;

    /// <summary>
    /// 直流表电压测量值
    /// </summary>
    public float DCMU => _decoder?. DCMU??0;

    /// <summary>
    /// 直流表电流测量值
    /// </summary>
    public float DCMI => _decoder?.DCMI ?? 0;

    /// <summary>
    /// 直流纹波电压测量值
    /// </summary>
    public float DCMU_Ripple => _decoder?. DCMU_Ripple ?? 0;

    /// <summary>
    /// 直流纹波电流测量值
    /// </summary>
    public float DCMI_Ripple => _decoder?. DCMI_Ripple ?? 0;

    /// <summary>
    /// 直流表电压量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMU => _decoder?.RangeIndex_DCMU ?? 0;

    /// <summary>
    /// 直流表电流量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMI =>_decoder?.RangeIndex_DCMI ?? 0;

    /// <summary>
    /// 直流纹波电压量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMU_Ripple => _decoder?. RangeIndex_DCMU_Ripple ?? 0;

    /// <summary>
    /// 直流纹波电流量程当前索引值
    /// </summary>
    public byte RangeIndex_DCMI_Ripple => _decoder?.RangeIndex_DCMI_Ripple ?? 0;

    /// <summary>
    /// 直流表电压量程数量
    /// </summary>
    public byte RangesCount_DCMU =>_decoder?.RangesCount_DCMU ?? 0;

    /// <summary>
    /// 直流表电流量程数量
    /// </summary>
    public byte RangesCount_DCMI =>_decoder?.RangesCount_DCMI ?? 0;

    /// <summary>
    /// 直流纹波电压量程数量
    /// </summary>
    public byte RangesCount_DCMU_Ripple => _decoder?.RangesCount_DCMI_Ripple ?? 0;

    /// <summary>
    /// 直流纹波电流量程数量
    /// </summary>
    public byte RangesCount_DCMI_Ripple => _decoder?.RangesCount_DCMI_Ripple ?? 0;
    #endregion 属性》

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        var result = CommandAction. Action ( _encoder. Packet_GetRanges ( ) );

        //如果命令执行失败
        if ( !result. IsSuccess || result. Content == null )
        {
            return result;
        }

        //解码
        var decodeResult = _decoder. DecodeGetRanges_DCM ( result. Content );

        //如果解码失败
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }

        //返回执行结果       
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( )
    {
        //执行命令前的功能状态检查
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        var result = CommandAction. Action ( _encoder. Packet_ReadData ( ) );

        //如果命令执行失败
        if ( !result. IsSuccess || result. Content == null )
        {
            return result;
        }

        //解码
        var decodeResult = _decoder. DecodeReadData_DCM ( result. Content );

        //解码失败
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCMI ( byte rangeIndex_DCMI )
    {
        //执行命令前的功能状态检查
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetRange_DCMI ( rangeIndex_DCMI ) );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCMU ( byte rangeIndex_DCMU )
    {
        //执行命令前的功能状态检查
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetRange_DCMI ( rangeIndex_DCMU ) );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCMI_Ripple ( byte rangeIndex_DCMU_Ripple )
    {
        //执行命令前的功能状态检查
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetRange_DCMI ( rangeIndex_DCMU_Ripple ) );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCMU_Ripple ( byte rangeIndex_DCMI_Ripple )
    {
        //执行命令前的功能状态检查
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetRange_DCMI ( rangeIndex_DCMI_Ripple ) );
    }
}
