using DKCommunicationNET. Core;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

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
    private readonly Func<byte[ ] , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IPacketBuilder_EPQ? _PacketsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder _decoder;

    #endregion

    internal EPQ ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器对象
        _PacketsBuilder = protocolFactory. GetPacketBuilderOfEPQ ( _id , byteTransform ). Content; //忽略空值，调用时会捕获解引用为null的异常

        //接收解码器对象
        _decoder = protocolFactory. GetDecoder ( byteTransform );
    }

    /// <inheritdoc/>
    public float Const_PM { get => _PacketsBuilder. Const_PM; set => _PacketsBuilder. Const_PM = value; }

    /// <inheritdoc/>
    public float Const_QM { get => _PacketsBuilder. Const_QM; set => _PacketsBuilder. Const_QM = value; }

    /// <inheritdoc/>
    public float Const_PS { get => _PacketsBuilder. Const_PS; set => _PacketsBuilder. Const_PS = value; }

    /// <inheritdoc/>
    public float Const_QS { get => _PacketsBuilder. Const_QS; set => _PacketsBuilder. Const_QS = value; }

    /// <inheritdoc/>
    public uint DIV { get => _PacketsBuilder. DIV; set => _PacketsBuilder. DIV = value; }

    /// <inheritdoc/>
    public uint Rounds { get => _PacketsBuilder. Rounds; set => _PacketsBuilder. Rounds = value; }

    /// <inheritdoc/>
    public uint Rounds_Current => _decoder. Rounds_Current;

    /// <inheritdoc/>
    public uint Counts_Current => _decoder. Counts_Current;

    /// <inheritdoc/>
    public float EValue_P => _decoder. EValue_P;

    public float EValue_Q => _decoder. EValue_Q;

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( Channels_EPQ Channels = Channels_EPQ. Channel1 )
    {
        var result = CommandAction. Action ( _PacketsBuilder. Packet_ReadData ( Channels ) , _methodOfCheckResponse );

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
        return CommandAction. Action ( _PacketsBuilder. Packet_SetConst_PS ( Const_PS ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetConst_QS ( float Const_QS )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_SetConst_QS ( Const_QS ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> StartTest_P ( float Const_PM , uint Rounds = 10 , uint DIV = 1 )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_StartTest_P ( Const_PM , Rounds , DIV ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> StartTest_Q ( float Const_QM , uint Rounds = 10 , uint DIV = 1 )
    {
        return CommandAction. Action ( _PacketsBuilder. Packet_StartTest_Q ( Const_QM , Rounds , DIV ) , _methodOfCheckResponse );
    }
}