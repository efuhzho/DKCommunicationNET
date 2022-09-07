using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Protocols. Hex81;

namespace DKCommunicationNET. Module;

/// <summary>
/// 交流源输出功能
/// </summary>
public class ACS : IModuleACS
{
    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文的委托方法
    /// </summary>
    private readonly Func<byte[ ] , bool , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IEncoder_ACS? _encoder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder_ACS? _decoder;
    
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="packetsBuilder_ACS"></param>
    /// <param name="decoder"></param>
    /// <param name="methodOfCheckResponse"></param>
    internal ACS (IEncoder_ACS  packetsBuilder_ACS , IDecoder_ACS decoder, Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse )
    {

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器
        _encoder =packetsBuilder_ACS;

        //接收解码器
        _decoder = decoder ;
    }

    #region 《方法
    /// <inheritdoc/>
    public OperateResult<byte[ ]> Open ( )
    {
        if ( _encoder==null )
        {
            return  new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_Open ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> Stop ( )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_Stop ( ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> GetRanges ( )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        var result = CommandAction. Action ( _encoder. Packet_GetRanges ( ) , _methodOfCheckResponse );

        //如果命令执行失败
        if ( !result. IsSuccess )
        {
            return result;
        }
               
        //解码下位机的回复报文：此处无需判断命令执行结果，判断下放
        var decodeResult = _decoder. DecodeGetRanges_ACS ( result.Content );

        //如果解码不成功
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;          
        }
        return result;
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetRanges ( rangeIndexOfACU , rangeIndexOfACI ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPA = 0 , float IPB = 0 , float IPC = 0 )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetAmplitude ( UA , UB , UC , IA , IB , IC , IPA , IPB , IPC ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitude ( float U , float I , float IP = 0 )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( SetAmplitude ( U , U , U , I , I , I , IP , IP , IP ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetPhase ( float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetPhase ( 0 , PhaseUb , PhaseUc , PhaseIa , PhaseIb , PhaseIc ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetFrequency ( FreqOfAll , FreqOfC ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetWireMode ( WireMode WireMode )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetWireMode ( WireMode ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetClosedLoop ( CloseLoopMode ClosedLoopMode )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetClosedLoop ( ClosedLoopMode ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetHarmonicMode ( HarmonicMode HarmonicMode )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetHarmonicMode ( HarmonicMode ) , _methodOfCheckResponse );
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetHarmonics ( Enum harmonicChannels , HarmonicArgs[ ]? harmonicArgs = null )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetHarmonics ( harmonicChannels , harmonicArgs ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData ( )
    {
        if ( _encoder == null||_decoder==null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        var result = CommandAction. Action ( _encoder. Packet_ReadData ( ) , _methodOfCheckResponse );
        if ( !result. IsSuccess )
        {
            return result;
        }
        var decodeResult = _decoder. DecodeReadData_ACS ( result.Content );
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ReadData_Status ( )
    {
        if ( _encoder == null || _decoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        var result = CommandAction. Action ( _encoder. Packet_ReadData_Status ( ) , _methodOfCheckResponse );

        //如果执行失败
        if ( !result. IsSuccess )
        {
            return result;
        }

        //执行成功则解码
        var decodeResult = _decoder. DecodeReadData_Status_ACS ( result.Content );
        if ( !decodeResult. IsSuccess )
        {
            result. IsSuccess = false;
            result. Message = StringResources. Language. DecodeError;
        }

        //返回执行结果
        return result;
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> ClearHarmonics ( Enum harmonicChannels )
    {
        if ( _encoder == null  )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_ClearHarmonics ( harmonicChannels ) , _methodOfCheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetRanges_IP ( byte rangeIndex_IP )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetRanges_IP ( rangeIndex_IP ) , _methodOfCheckResponse );
    }

    /// <summary>
    /// 设置X相档位
    /// </summary>
    /// <param name="rangeIndex_Ux"></param>
    /// <param name="rangeIndex_Ix"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRanges_X ( byte rangeIndex_Ux , byte rangeIndex_Ix )
    {
        if ( _encoder == null )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedModule );
        }
        //执行报文发送并接收下位机回复报文
        return CommandAction. Action ( _encoder. Packet_SetRanges_X ( rangeIndex_Ux , rangeIndex_Ix ) , _methodOfCheckResponse );
    }
    #endregion 方法》



}
