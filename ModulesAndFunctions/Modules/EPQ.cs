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
    /// 定义编码器
    /// </summary>
    private readonly IEncoder_EPQ? _encoder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder_EPQ? _decoder;

    internal CommandAction CommandAction;
    #endregion

    internal EPQ ( IEncoder_EPQ? encoder , IDecoder_EPQ? decoder , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse )
    {
        //编码器
        _encoder = encoder;

        //解码器
        _decoder = decoder;

        CommandAction = new CommandAction ( methodOfCheckResponse );
    }

    #region 《编码器属性：设置属性，可读写

    /// <inheritdoc/>
    public float Const_PM
    {
        get
        {
            if ( _encoder != null )
            {
                return _encoder. Const_PM;
            }
            return 3600_000;
        }
        set
        {
            if ( _encoder != null )
            {
                _encoder. Const_PM = value;
            }
        }
    }

    /// <inheritdoc/>
    public float Const_QM
    {
        get
        {
            if ( _encoder == null )
            {
                return 3600_000;
            }
            return _encoder. Const_QM;
        }
        set
        {
            if ( _encoder != null )
            {
                _encoder. Const_QM = value;
            }
        }
    }

    /// <inheritdoc/>
    public float Const_PS
    {
        get
        {
            if ( _encoder == null )
            {
                return 3600_000;
            }
            return _encoder. Const_PS;
        }

        set
        {
            if ( _encoder != null )
            {
                _encoder. Const_PS = value;
            }
        }
    }

    /// <inheritdoc/>
    public float Const_QS
    {
        get
        {
            if ( _encoder == null )
            {
                return 3600_000;
            }
            return _encoder. Const_QS;
        }

        set
        {
            if ( _encoder != null ) _encoder. Const_QS = value;
        }
    }

    /// <inheritdoc/>
    public uint DIV
    {
        get
        {
            if ( _encoder == null )
            {
                return 1;
            }
            return _encoder. DIV;
        }

        set
        {
            if ( _encoder != null ) _encoder. DIV = value;
        }
    }

    /// <inheritdoc/>
    public uint Rounds
    {
        get
        {
            if ( _encoder == null )
            {
                return 10;
            }
            return _encoder. Rounds;
        }

        set
        {
            if ( _encoder != null ) _encoder. Rounds = value;
        }
    }

    #endregion 编码器属性：设置属性，可读写》

    #region 《解码器属性：读取属性，只读
    /// <inheritdoc/>
    public uint Rounds_Current
    {
        get
        {
            if ( _decoder!=null )
            {
                return _decoder. Rounds_Current;
            }
            return 0;
        }
    }

    /// <inheritdoc/>
    public uint Counts_Current
    {
        get
        {
            if ( _decoder != null )
            {
                return _decoder. Counts_Current;
            }
            return 0;
        }
    }

    /// <inheritdoc/>
    public float EValue_P
    {
        get
        {
            if ( _decoder != null )
            {
                return _decoder. EValue_P;
            }
            return 0;
        }
    }

    /// <inheritdoc/>
    public float EValue_Q {
        get
        {
            if ( _decoder != null )
            {
                return _decoder. EValue_Q;
            }
            return 0;
        }
    }

    #endregion 解码器属性：读取属性，只读》

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( Channels_ReadEPQ Channels = Channels_ReadEPQ. Channel1 )
    {
        //检查协议是否支持
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行命令并获取回复报文
        var result = CommandAction. Action ( _encoder. Packet_ReadData ( Channels )  );

        if ( !result. IsSuccess || result. Content == null  )
        {
            return result;
        }

        var decodeResult = _decoder. DecodeReadData_EPQ ( result. Content );
        if (! decodeResult. IsSuccess )
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
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetConst_PS ( Const_PS )  );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetConst_QS ( float Const_QS )
    {
        //执行命令前的功能状态检查
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_SetConst_QS ( Const_QS )  );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> StartTest_P ( float Const_PM , uint Rounds = 10 , uint DIV = 1 )
    {
        //执行命令前的功能状态检查
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_StartTest_P ( Const_PM , Rounds , DIV )  );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> StartTest_Q ( float Const_QM , uint Rounds = 10 , uint DIV = 1 )
    {
        //执行命令前的功能状态检查
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }

        //执行命令并获取回复报文
        return CommandAction. Action ( _encoder. Packet_StartTest_Q ( Const_QM , Rounds , DIV )  );
    }
}