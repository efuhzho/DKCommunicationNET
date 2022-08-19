﻿using DKCommunicationNET. Core;
using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议工厂类
/// </summary>
[Model ( Models. Hex81 )]
internal class Hex81Factory<TByteTransform> : IProtocolFactory  where TByteTransform : IByteTransform
{
    private readonly TByteTransform _byteTransform;

    public Hex81Factory ( TByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }
    public OperateResult<IPacketBuilderOfACM> GetPacketsOfACM ( )
    {
        return new OperateResult<IPacketBuilderOfACM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketsBuilderOfACS> GetPacketsOfACS ( )
    {
        return OperateResult. CreateSuccessResult ( new Hex81PacketBuilderOfACS ( ) as IPacketsBuilderOfACS );

    }

    public OperateResult<IPacketBuilderOfDCM> GetPacketsOfDCM ( )
    {
        return new OperateResult<IPacketBuilderOfDCM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilderOfDCS> GetPacketsOfDCS ( )
    {
        return new OperateResult<IPacketBuilderOfDCS> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilderOfIO> GetPacketsOfIO ( )
    {
        return new OperateResult<IPacketBuilderOfIO> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilderOfPQ> GetPacketsOfPQ ( )
    {
        return new OperateResult<IPacketBuilderOfPQ> ( StringResources. Language. NotSupportedModule );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex81CRCChecker ( ) ;
    }

    public IProtocolFunctionsState GetProtocolFunctionsState ( )
    {
        return new Hex81FunctionsState ( );
    }

    public IDecoder GetDecoder ( )
    {
        return new Hex81Decoder<TByteTransform> ( _byteTransform );
    }
}
