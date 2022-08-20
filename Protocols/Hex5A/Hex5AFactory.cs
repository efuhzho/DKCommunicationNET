using DKCommunicationNET. Core;
using DKCommunicationNET. Module;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A;

[Model ( Models. Hex5A )]
internal class Hex5AFactory : IProtocolFactory
{
    public OperateResult<IPacketBuilderOfACM> GetPacketsOfACM ( ushort id )
    {
        return OperateResult.CreateSuccessResult( new Hex5APacketBuilderOfACM ( ) as IPacketBuilderOfACM);
    }

    public OperateResult<IPacketsBuilderOfACS> GetPacketsOfACS ( ushort id )
    {
        return OperateResult.CreateSuccessResult(new Hex5APacketBuilderOfACS ( id ) as IPacketsBuilderOfACS);
    }

    public OperateResult<IPacketBuilderOfDCM> GetPacketsOfDCM ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfDCM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilderOfDCS> GetPacketsOfDCS ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfDCS> ( StringResources. Language. NotSupportedModule );

    }

    public OperateResult<IPacketBuilderOfIO> GetPacketsOfIO ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfIO> ( StringResources. Language. NotSupportedModule );

    }

    public OperateResult<IPacketBuilderOfPQ> GetPacketsOfPQ ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfPQ>( StringResources. Language. NotSupportedModule );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex5ACRCChecker ( );
    }

public IProtocolFunctions GetProtocolFunctionsState ( )
    {
        return new Hex5AFunctions ( );
    }

    public IDecoder GetDecoder ( IByteTransform byteTransform )
    {
        return new Hex5ADecoder( byteTransform );
    }
}
