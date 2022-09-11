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

    readonly CommandAction CommandAction;

    internal DCM ( IEncoder_DCM? encoder , IDecoder_DCM? decoder , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse , bool isEnabled )
    {
        //初始化报文创建器
        _encoder = encoder;

        //接收解码器
        _decoder = decoder;

        CommandAction = new CommandAction ( isEnabled , methodOfCheckResponse );
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
