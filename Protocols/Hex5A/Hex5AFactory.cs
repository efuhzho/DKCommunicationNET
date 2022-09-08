using DKCommunicationNET. Core;
using DKCommunicationNET. Module;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Protocols. Hex5A. Encoders;


namespace DKCommunicationNET. Protocols. Hex5A;

[Model ( Models. Hex5A )]
internal class Hex5AFactory : IProtocolFactory
{
    public OperateResult<IEncoder_ACM> GetEncoderOfACM ( ushort id )
    {
        return new OperateResult<IEncoder_ACM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IEncoder_ACS> GetEncoderOfACS ( ushort id,IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex5AEncoder_ACS ( id, byteTransform ) as IEncoder_ACS );
    }

    public OperateResult<IEncoder_DCM> GetEncoderOfDCM ( ushort id )
    {
        return new OperateResult<IEncoder_DCM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IEncoder_DCS> GetPacketBuilderOfDCS ( ushort id )
    {
        return new OperateResult<IEncoder_DCS> ( StringResources. Language. NotSupportedModule );

    }

    public OperateResult<IEncoder_IO> GetPacketBuilderOfIO ( ushort id )
    {
        return new OperateResult<IEncoder_IO> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IEncoder_EPQ> GetPacketBuilderOfPQ ( ushort id )
    {
        return new OperateResult<IEncoder_EPQ> ( StringResources. Language. NotSupportedModule );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex5ACRCChecker ( );
    }

    public IProtocolFunctions GetProtocolFunctions ( )
    {

        return new Hex5AFunctions ( );
    }

    public IDecoders GetDecoder ( IByteTransform byteTransform )
    {

        return new Hex5ADecoder ( byteTransform );
    }

    public OperateResult<IEncoder_DCS> GetEncoderOfDCS ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_DCS> ( StringResources. Language. NotSupportedModule );

    }

    public OperateResult<IEncoder_IO> GetEncoderOfIO ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_IO> ( StringResources. Language. NotSupportedModule );

    }

    public OperateResult<IEncoder_EPQ> GetEncoderOfEPQ ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_EPQ> ( StringResources. Language. NotSupportedModule );

    }

    public OperateResult<IEncoder_Calibrate> GetEncoderOfCalibrate ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_Calibrate> ( StringResources. Language. NotSupportedModule );
    }

    public IDecoder_ACS GetDecoder_ACS ( IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<IEncoder_Settings> GetEncoder_Settings ( ushort id )
    {
        throw new NotImplementedException ( );
    }

    public IDecoder_DCS GetDecoder_DCS ( IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }
}
