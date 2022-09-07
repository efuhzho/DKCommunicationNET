using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

/// <summary>
/// 直流源功能模块
/// </summary>
public class DCS : IModuleDCS
{
    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文的委托方法
    /// </summary>
    private readonly Func<byte[ ] , bool , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IEncoder_DCS? _encoder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder_DCS? _decoder;   

    internal DCS ( IEncoder_DCS encoder , IDecoder_DCS decoder, Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse  )
    {
        //编码器
        _encoder = encoder;

        //解码器
        _decoder = decoder;
       
        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;          
    }

    #region 《读档位

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        var result = CommandAction. Action ( _encoder. Packet_GetRanges ( ) , _methodOfCheckResponse );

        if ( !result. IsSuccess )
        {
            return result;
        }
        //解码
        var decodeResult = _decoder. DecodeGetRanges_DCS ( result.Content );

        //如果解码失败
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }

        return result;
    }

    #endregion 读档位》

    #region 《源打开

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCI ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_Open_DCI ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCR ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_Open_DCR ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCU ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_Open_DCU ( ) , _methodOfCheckResponse );
    }
    #endregion 源打开》

    #region 《源停止
    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCI ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_Stop_DCI ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCR ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_Stop_DCR ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCU ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_Stop_DCU ( ) , _methodOfCheckResponse );
    }
    #endregion 停止输出命令》    

    #region 《设置幅值

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCI ( float SData , byte? rangeIndex_DCI = null )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //如果没有设置档位，则保持当前档位
        if ( rangeIndex_DCI == null )
        {
            return CommandAction. Action ( _encoder. Packet_SetAmplitude_DCI ( SData , _decoder. RangeIndex_DCI ) , _methodOfCheckResponse );
        }

        //执行用户设置的档位
        return CommandAction. Action ( _encoder. Packet_SetAmplitude_DCI ( SData , ( byte ) rangeIndex_DCI ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCR ( float SData , byte? rangeIndex_DCR = null )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //如果没有设置档位，则保持当前档位
        if ( rangeIndex_DCR == null )
        {
            return CommandAction. Action ( _encoder. Packet_SetAmplitude_DCR ( SData , _decoder.RangeIndex_DCR ) , _methodOfCheckResponse );
        }

        //执行用户设置的档位
        return CommandAction. Action ( _encoder. Packet_SetAmplitude_DCR ( SData , ( byte ) rangeIndex_DCR ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCU ( float SData , byte? rangeIndex_DCU = null )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //如果没有设置档位，则保持当前档位
        if ( rangeIndex_DCU == null )
        {
            return CommandAction. Action ( _encoder. Packet_SetAmplitude_DCU ( SData , _decoder.RangeIndex_DCU ) , _methodOfCheckResponse );
        }

        //执行用户设置的档位
        return CommandAction. Action ( _encoder. Packet_SetAmplitude_DCU ( SData , ( byte ) rangeIndex_DCU ) , _methodOfCheckResponse );
    }
    #endregion 设置幅值》

    #region 《设置档位
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCI ( byte rangeIndex_DCI )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetRange_DCI ( rangeIndex_DCI ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCR ( byte rangeIndex_DCR )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetRange_DCR ( rangeIndex_DCR ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCU ( byte rangeIndex_DCU )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetRange_DCU ( rangeIndex_DCU ) , _methodOfCheckResponse );
    }
    #endregion 设置档位》

    #region 《读数据

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( char? Resistor = null )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        var result = CommandAction. Action ( _encoder. Packet_ReadData ( Resistor ) , _methodOfCheckResponse );

        if ( !result. IsSuccess )
        {
            return result;
        }
        //解码
        var decodeResult = _decoder. DecodeReadData_DCS ( result.Content );

        //如果解码失败
        if ( !decodeResult. IsSuccess )
        {
            result. Message = StringResources. Language. DecodeError;
        }

        return result;
    }
    #endregion 读数据》
}
