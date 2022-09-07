using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

/// <summary>
/// 电能功能模块
/// </summary>
public class EPQ : IModuleEPQ
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
    private readonly IEncoder_EPQ? _packetsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoders _decoder;

    /// <summary>
    /// 模块功能是否激活
    /// </summary>
    bool _isEnabled;

    #endregion

    internal EPQ ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform , bool isEnabled )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器对象
        _packetsBuilder = protocolFactory. GetPacketBuilderOfEPQ ( _id , byteTransform ). Content; //忽略空值，调用时会捕获解引用为null的异常

        //接收解码器对象
        _decoder = protocolFactory. GetDecoder ( byteTransform );
        _isEnabled = isEnabled;
    }

    /// <inheritdoc/>
    public float Const_PM
    {
        get
        {
            if ( _packetsBuilder != null )
            {
                return _packetsBuilder. Const_PM;
            }
            return 3600_000;
        }
        set
        {
            if ( _packetsBuilder != null )
            {
                _packetsBuilder. Const_PM = value;
            }
        }
    }

    /// <inheritdoc/>
    public float Const_QM
    {
        get
        {
            if ( _packetsBuilder == null )
            {
                return 3600_000;
            }
            return _packetsBuilder. Const_QM;
        }
        set
        {
            if ( _packetsBuilder != null )
            {
                _packetsBuilder. Const_QM = value;
            }
        }
    }

    /// <inheritdoc/>
    public float Const_PS
    {
        get
        {
            if ( _packetsBuilder == null )
            {
                return 3600_000;
            }
            return _packetsBuilder. Const_PS;
        }

        set
        {
            if ( _packetsBuilder != null )
            {
                _packetsBuilder. Const_PS = value;
            }
        }
    }

    /// <inheritdoc/>
    public float Const_QS
    {
        get
        {
            if ( _packetsBuilder == null )
            {
                return 3600_000;
            }
            return _packetsBuilder. Const_QS;
        }

        set
        {
            if ( _packetsBuilder != null ) _packetsBuilder. Const_QS = value;
        }
    }

    /// <inheritdoc/>
    public uint DIV
    {
        get
        {
            if ( _packetsBuilder == null )
            {
                return 1;
            }
            return _packetsBuilder. DIV;
        }

        set
        {
            if ( _packetsBuilder != null ) _packetsBuilder. DIV = value;
        }
    }

    /// <inheritdoc/>
    public uint Rounds
    {
        get
        {
            if ( _packetsBuilder == null )
            {
                return 10;
            }
            return _packetsBuilder. Rounds;
        }

        set
        {
            if ( _packetsBuilder != null ) _packetsBuilder. Rounds = value;
        }
    }

    /// <inheritdoc/>
    public uint Rounds_Current => _decoder. Rounds_Current;

    /// <inheritdoc/>
    public uint Counts_Current => _decoder. Counts_Current;

    /// <inheritdoc/>
    public float EValue_P => _decoder. EValue_P;
    
    /// <inheritdoc/>
    public float EValue_Q => _decoder. EValue_Q;

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( Channels_EPQ Channels = Channels_EPQ. Channel1 )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        var result = CommandAction. Action ( _packetsBuilder. Packet_ReadData ( Channels ) , _methodOfCheckResponse );

        if ( result. IsSuccess == false )
        {
            return result;
        }

        var decodeResult = _decoder. DecodeReadData_EPQ ( result );
        if ( decodeResult. IsSuccess == false )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }

        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetConst_PS ( float Const_PS )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetConst_PS ( Const_PS ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetConst_QS ( float Const_QS )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_SetConst_QS ( Const_QS ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> StartTest_P ( float Const_PM , uint Rounds = 10 , uint DIV = 1 )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_StartTest_P ( Const_PM , Rounds , DIV ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> StartTest_Q ( float Const_QM , uint Rounds = 10 , uint DIV = 1 )
    {
        //执行命令前的功能状态检查
        var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isEnabled );
        if ( !checkResult. IsSuccess || _packetsBuilder == null )
        {
            return checkResult;
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _packetsBuilder. Packet_StartTest_Q ( Const_QM , Rounds , DIV ) , _methodOfCheckResponse );
    }
}