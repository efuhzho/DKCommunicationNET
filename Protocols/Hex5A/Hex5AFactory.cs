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
    public OperateResult<IPacketBuilder_ACM> GetPacketBuilderOfACM ( ushort id )
    {
        return OperateResult.CreateSuccessResult( new Hex5APacketBuilderOfACM ( ) as IPacketBuilder_ACM);
    }

    public OperateResult<IPacketsBuilder_ACS> GetPacketBuilderOfACS ( ushort id )
    {
        return OperateResult.CreateSuccessResult(new Hex5APacketBuilderOfACS ( id ) as IPacketsBuilder_ACS);
    }

    public OperateResult<IPacketBuilder_DCM> GetPacketBuilderOfDCM ( ushort id )
    {
        return new OperateResult<IPacketBuilder_DCM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilder_DCS> GetPacketBuilderOfDCS ( ushort id )
    {
        return new OperateResult<IPacketBuilder_DCS> ( StringResources. Language. NotSupportedModule );

    }

    public OperateResult<IPacketBuilder_IO> GetPacketBuilderOfIO ( ushort id )
    {
        return new OperateResult<IPacketBuilder_IO> ( StringResources. Language. NotSupportedModule );

    }

    public OperateResult<IPacketBuilder_PQ> GetPacketBuilderOfPQ ( ushort id )
    {
        return new OperateResult<IPacketBuilder_PQ>( StringResources. Language. NotSupportedModule );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex5ACRCChecker ( );
    }

public IProtocolFunctions GetProtocolFunctions ( )
    {
        return new Hex5AFunctions ( );
    }

    public IDecoder GetDecoder ( IByteTransform byteTransform )
    {
        return new Hex5ADecoder( byteTransform );
    }
}
