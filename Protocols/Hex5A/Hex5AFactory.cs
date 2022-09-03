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
        return new OperateResult<IPacketBuilder_ACM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketsBuilder_ACS> GetPacketBuilderOfACS ( ushort id,IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex5APacketBuilder_ACS ( id, byteTransform ) as IPacketsBuilder_ACS );
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

    public OperateResult<IPacketBuilder_EPQ> GetPacketBuilderOfPQ ( ushort id )
    {
        return new OperateResult<IPacketBuilder_EPQ> ( StringResources. Language. NotSupportedModule );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex5ACRCChecker ( );
    }

    public IProtocolFunctions GetProtocolFunctions ( )
    {
        throw new NotImplementedException ( );
        // return new Hex5AFunctions ( );
    }

    public IDecoder GetDecoder ( IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
        // return new Hex5ADecoder( byteTransform );
    }

    public OperateResult<IPacketsBuilder_ACS> GetPacketBuilderOfACS ( ushort id , IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<IPacketBuilder_DCS> GetPacketBuilderOfDCS ( ushort id , IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<IPacketBuilder_IO> GetPacketBuilderOfIO ( ushort id , IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<IPacketBuilder_EPQ> GetPacketBuilderOfEPQ ( ushort id , IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<IPacketBuilder_Calibrate> GetPacketBuilderOfCalibrate ( ushort id , IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }
}
