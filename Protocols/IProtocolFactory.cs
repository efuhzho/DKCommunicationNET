using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET;

/// <summary>
/// 协议抽象工厂接口
/// </summary>
internal interface IProtocolFactory
{
    #region 《编码器
    /// <summary>
    /// 获取交流源报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_ACS> GetEncoderOfACS ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取交流表报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_ACM> GetEncoderOfACM ( ushort id );

    /// <summary>
    /// 获取直流源报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_DCS> GetEncoderOfDCS ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取直流表报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_DCM> GetEncoderOfDCM ( ushort id );

    /// <summary>
    /// 获取开关量报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_IO> GetEncoderOfIO ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取：电能报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_EPQ> GetEncoderOfEPQ ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取：对时报文创建类
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    OperateResult<IEncoder_PPS> GetEncoder_PPS(ushort id);

    /// <summary>
    /// 获取：校准报文创建类
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_Calibrate> GetEncoderOfCalibrate ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取：系统设置编码器
    /// </summary>
    /// <param name="id"></param>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    OperateResult<IEncoder_Settings> GetEncoder_Settings ( ushort id , IByteTransform byteTransform );
    #endregion 编码器》

    #region 《解码器
    /// <summary>
    /// 生产交流源解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>  
    OperateResult<IDecoder_ACS>  GetDecoder_ACS ( IByteTransform byteTransform );

    /// <summary>
    /// 生产直流源解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    OperateResult<IDecoder_DCS>  GetDecoder_DCS ( IByteTransform byteTransform );

    /// <summary>
    /// 直流表解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    OperateResult<IDecoder_DCM> GetDecoder_DCM( IByteTransform byteTransform );

    /// <summary>
    /// 电能模块解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    OperateResult<IDecoder_EPQ> GetDecoder_EPQ (  IByteTransform byteTransform );

    /// <summary>
    /// 系统设置解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    OperateResult<IDecoder_Settings> GetDecoder_Settings( IByteTransform byteTransform );

    /// <summary>
    /// 交流标准表表解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    OperateResult<IDecoder_ACM> GetDecoder_ACM ( IByteTransform byteTransform );

    /// <summary>
    /// 对时功能解码器
    /// </summary>   
    /// <returns></returns>
    OperateResult<IDecoder_PPS> GetDecoder_PPS (  );

    /// <summary>
    /// 获取：开关量解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    OperateResult<IDecoder_IO> GetDecoder_IO ( IByteTransform byteTransform );

    #endregion 解码器》

    #region 《校验器
    /// <summary>
    /// 获取CRC校验器对象
    /// </summary>
    /// <returns></returns>
    ICRCChecker GetCRCChecker ( );
   
    #endregion 校验器》

    //TODO 添加其他模块功能的报文创建器
}
