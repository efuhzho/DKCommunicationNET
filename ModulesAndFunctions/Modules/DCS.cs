﻿using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

/// <summary>
/// 直流源功能模块
/// </summary>
public class DCS : IModuleDCS, IReadProperties_DCS
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
    private readonly IPacketBuilder_DCS? _packetsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder _decoder;
    readonly bool _isEnabled;

    #endregion
    internal DCS ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform , bool isEnabled )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器对象
        _packetsBuilder = protocolFactory. GetPacketBuilderOfDCS ( _id , byteTransform ). Content; //忽略空值，调用时会捕获解引用为null的异常

        //接收解码器对象
        _decoder = protocolFactory. GetDecoder ( byteTransform );

        //模块功能是否激活
        _isEnabled = isEnabled;
    }

    /// <inheritdoc/>
    public byte RangesCount_DCU => _decoder. RangesCount_DCU;

    /// <inheritdoc/>
    public byte RangesCount_DCI => _decoder. RangesCount_DCI;

    /// <inheritdoc/>
    public byte RangesCount_DCR => _decoder. RangesCount_DCR;

    /// <inheritdoc/>
    public float DCR => _decoder. DCR;

    /// <inheritdoc/>
    public float DCU => _decoder. DCU;
    /// <inheritdoc/>
    public float DCI => _decoder. DCI;

    /// <inheritdoc/>
    public float[ ]? Ranges_DCU { get => _decoder. Ranges_DCU; set => _decoder. Ranges_DCU = value; }


    /// <inheritdoc/>
    public float[ ]? Ranges_DCI { get => _decoder. Ranges_DCI; set => _decoder. Ranges_DCI = value; }

    /// <inheritdoc/>
    public float[ ]? Ranges_DCR { get => _decoder. Ranges_DCR; set => _decoder. Ranges_DCR = value; }

    /// <inheritdoc/>
    public bool IsOpen_DCU => _decoder. IsOpen_DCU;

    /// <inheritdoc/>
    public bool IsOpen_DCI => _decoder. IsOpen_DCI;

    /// <inheritdoc/>
    public bool IsOpen_DCR => _decoder. IsOpen_DCR;

    /// <inheritdoc/>
    public bool IsAutoRange_DCU
    {
        get
        {
            if ( _packetsBuilder == null )
            {
                return false;
            }
            return _packetsBuilder. IsAutoRange_DCU;
        }
        set
        {
            if ( _packetsBuilder != null )
            {
                _packetsBuilder. IsAutoRange_DCU = value;
            }
        }
    }

    /// <inheritdoc/>
    public bool IsAutoRange_DCI
    {
        get
        {
            if ( _packetsBuilder == null )
            {
                return false;
            }
            return _packetsBuilder. IsAutoRange_DCI;
        }
        set
        {
            if ( _packetsBuilder != null )
            {
                _packetsBuilder. IsAutoRange_DCI = value;
            }
        }
    }

    /// <inheritdoc/>
    public bool IsAutoRange_DCR
    {
        get
        {
            if ( _packetsBuilder == null )
            {
                return false;
            }
            return _packetsBuilder. IsAutoRange_DCR;
        }
        set
        {
            if ( _packetsBuilder != null )
            {
                _packetsBuilder. IsAutoRange_DCR = value;
            }
        }
    }

    /// <inheritdoc/>
    public byte RangeIndex_DCI => _decoder. RangeIndex_DCI;

    /// <inheritdoc/>
    public byte RangeIndex_DCR => _decoder. RangeIndex_DCR;

    /// <inheritdoc/>
    public byte RangeIndex_DCU => _decoder. RangeIndex_DCU;

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        var result = CommandAction. Action ( _packetsBuilder. Packet_GetRanges ( ) , _methodOfCheckResponse );

        if ( !result. IsSuccess )
        {
            return result;
        }
        //解码
        var decodeResult = _decoder. DecodeGetRanges_DCS ( result );

        //如果解码失败
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }

        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCI ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_Open_DCI ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCR ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_Open_DCR ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open_DCU ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_Open_DCU ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( char? Resistor = null )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        var result = CommandAction. Action ( _packetsBuilder. Packet_ReadData ( Resistor ) , _methodOfCheckResponse );

        if ( !result. IsSuccess )
        {
            return result;
        }
        //解码
        var decodeResult = _decoder. DecodeReadData_DCS ( result );

        //如果解码失败
        if ( !decodeResult. IsSuccess )
        {
            result. Message = StringResources. Language. DecodeError;
        }

        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCI ( float SData , byte? rangeIndex_DCI = null )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //如果没有设置档位，则保持当前档位
        if ( rangeIndex_DCI == null )
        {
            return CommandAction. Action ( _packetsBuilder. Packet_SetAmplitude_DCI ( SData , RangeIndex_DCI ) , _methodOfCheckResponse );
        }

        //执行用户设置的档位
        return CommandAction. Action ( _packetsBuilder. Packet_SetAmplitude_DCI ( SData , ( byte ) rangeIndex_DCI ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCR ( float SData , byte? rangeIndex_DCR = null )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //如果没有设置档位，则保持当前档位
        if ( rangeIndex_DCR == null )
        {
            return CommandAction. Action ( _packetsBuilder. Packet_SetAmplitude_DCR ( SData , RangeIndex_DCR ) , _methodOfCheckResponse );
        }

        //执行用户设置的档位
        return CommandAction. Action ( _packetsBuilder. Packet_SetAmplitude_DCR ( SData , ( byte ) rangeIndex_DCR ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude_DCU ( float SData , byte? rangeIndex_DCU = null )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //如果没有设置档位，则保持当前档位
        if ( rangeIndex_DCU == null )
        {
            return CommandAction. Action ( _packetsBuilder. Packet_SetAmplitude_DCU ( SData , RangeIndex_DCU ) , _methodOfCheckResponse );
        }

        //执行用户设置的档位
        return CommandAction. Action ( _packetsBuilder. Packet_SetAmplitude_DCU ( SData , ( byte ) rangeIndex_DCU ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCI ( byte rangeIndex_DCI )
    {//执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetRange_DCI ( rangeIndex_DCI ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCR ( byte rangeIndex_DCR )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetRange_DCR ( rangeIndex_DCR ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRange_DCU ( byte rangeIndex_DCU )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetRange_DCU ( rangeIndex_DCU ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCI ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_Stop_DCI ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCR ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_Stop_DCR ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop_DCU ( )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_Stop_DCU ( ) , _methodOfCheckResponse );
    }

    #region Private Methods


    #endregion
}
