using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols. Hex5A. Decoders;
using DKCommunicationNET. Protocols. Hex5A. Encoders;

/**************************************************************************************************
 * 
 *  【Hex5A协议工厂类】 版本：V 1.0.0   Author:  Fuhong Zhou   2022年9月9日 13点39分  
 *  
 *  生产本协议的组件
 *
 *************************************************************************************************/

namespace DKCommunicationNET. Protocols. Hex5A;

[Model ( Models. Hex5A )]
internal class Hex5AFactory : IProtocolFactory
{
    #region 《编码器
    public OperateResult<IEncoder_ACS> GetEncoderOfACS ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex5AEncoder_ACS ( id , byteTransform ) as IEncoder_ACS );
    }

    public OperateResult<IEncoder_PPS> GetEncoder_PPS ( ushort id )
    {
        return OperateResult. CreateSuccessResult ( new Hex5AEncoder_PPS ( id ) as IEncoder_PPS );
    }

    public OperateResult<IEncoder_Settings> GetEncoder_Settings ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex5AEncoder_Settings ( id , byteTransform ) as IEncoder_Settings );
    }
    #endregion 编码器》

    #region 《解码器
   public OperateResult<IDecoder_ACS> GetDecoder_ACS ( IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex5ADecoder_ACS ( byteTransform ) as IDecoder_ACS );
    }

    public OperateResult<IDecoder_Settings> GetDecoder_Settings ( IByteTransform byteTransform )
    {
       return OperateResult.CreateSuccessResult(new Hex5ADecoder_Settings(byteTransform) as IDecoder_Settings );
    }

    public OperateResult<IDecoder_PPS> GetDecoder_PPS (  )
    {
        return OperateResult. CreateSuccessResult ( new Hex5ADecoder_PPS (  ) as IDecoder_PPS );
    }
    #endregion 解码器》

    #region 《校验器
    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex5ACRCChecker ( );
    }
    #endregion 校验器》

    #region 《不具备的功能
    public OperateResult<IEncoder_ACM> GetEncoderOfACM ( ushort id )
    {
        //不具备此功能模块
        return new OperateResult<IEncoder_ACM> ( StringResources. Language. NotSupportedModule );
    }
    #endregion 不具备的功能》


    /****************************************************以下待实现******************************************************************************************************/



    public OperateResult<IEncoder_DCM> GetEncoderOfDCM ( ushort id )
    {
        return new OperateResult<IEncoder_DCM> ( StringResources. Language. NotSupportedModule );
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

    public OperateResult<IDecoder_DCM> GetDecoder_DCM ( IByteTransform byteTransform )
    {
        return new OperateResult<IDecoder_DCM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IDecoder_EPQ> GetDecoder_EPQ ( IByteTransform byteTransform )
    {
        return new OperateResult<IDecoder_EPQ> ( StringResources. Language. NotSupportedModule );
    }

 

    public OperateResult<IDecoder_ACM> GetDecoder_ACM ( IByteTransform byteTransform )
    {
        return new OperateResult<IDecoder_ACM> ( StringResources. Language. NotSupportedModule );
    }

  

    public OperateResult<IDecoder_IO> GetDecoder_IO ( IByteTransform byteTransform )
    {
        return new OperateResult<IDecoder_IO> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IDecoder_DCS> GetDecoder_DCS ( IByteTransform byteTransform )
    {
        return new OperateResult<IDecoder_DCS> ( StringResources. Language. NotSupportedModule );
    }
}
