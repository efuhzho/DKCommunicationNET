using DKCommunicationNET. BaseClass;
using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;
using DKCommunicationNET. Module;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. ModulesAndFunctions. Functions;
using DKCommunicationNET. ModulesAndFunctions. Modules;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET;

/// <summary>
/// 丹迪克标准表类
/// </summary>
public class DKStandardMeter : DandickSerialBase<RegularByteTransform>
{
    /// <summary>
    /// 【全局】协议工厂
    /// </summary>
    readonly IProtocolFactory protocolFactory;

    #region 《校验器
    /// <summary>
    /// CRC校验器
    /// </summary>
    readonly ICRCChecker _CRCChecker;
    #endregion 校验器》 

    #region 《编码器
    /// <summary>
    /// 系统设置编码器
    /// </summary>
    private readonly IEncoder_Settings encoder_Settings;
    /// <summary>
    /// 交流标准表编码器
    /// </summary>
    private readonly IEncoder_ACM? encoder_ACM;
    /// <summary>
    /// 直流标准表编码器
    /// </summary>
    private readonly IEncoder_DCM? encoder_DCM;
    /// <summary>
    /// 电能模块编码器
    /// </summary>
    private readonly IEncoder_EPQ? encoder_EPQ;
    #endregion 编码器》

    #region 《解码器  
    /// <summary>
    /// 系统设置解码器
    /// </summary>
    private readonly IDecoder_Settings decoder_Settings;
    /// <summary>
    /// 交流标准表解码器
    /// </summary>
    private readonly IDecoder_ACM? decoder_ACM;
    /// <summary>
    /// 直流标准表解码器
    /// </summary>
    private readonly IDecoder_DCM? decoder_DCM;
    /// <summary>
    /// 电能模块解码器
    /// </summary>
    private readonly IDecoder_EPQ? decoder_EPQ;
    #endregion 解码器》  

    #region 《功能模块
    /// <summary>
    /// 交流标准表模块
    /// </summary>
    public IModuleACM?  ACM { get;private set; }
    /// <summary>
    /// 直流标准表模块
    /// </summary>
    public IModuleDCM? DCM { get;private set; }
    /// <summary>
    /// 系统设置属功能
    /// </summary>
    ///   /// <summary>
    /// 电能模块
    /// </summary>
    public EPQ? EPQ { get; private set; }

    /// <summary>
    /// 系统设置功能
    /// </summary>
    public IFuncSettings Settings { get; private set; }
    #endregion 功能模块》

    /// <summary>
    /// 丹迪克标准表类
    /// </summary>
    /// <param name="model"></param>
    /// <param name="protName"></param>
    /// <param name="baudRate"></param>
    /// <param name="id"></param>
    public DKStandardMeter ( Models model , string protName , int baudRate = 115200 , ushort id = 0 )
    {
        //【依赖注入】
        {
            ID = id;
            protocolFactory = new DictionaryOfFactorys ( ). GetFactory ( model );
            SerialPortInni ( protName , baudRate );
        }

        //【校验器】实例化
        {
            _CRCChecker = protocolFactory. GetCRCChecker ( );
        }

        //【编码器】实例化
        {
            encoder_Settings = protocolFactory. GetEncoder_Settings ( ID , ByteTransform );
            encoder_ACM = protocolFactory. GetEncoderOfACM ( id ).Content;
            encoder_DCM = protocolFactory. GetEncoderOfDCM ( id ).Content;
            encoder_EPQ = protocolFactory. GetEncoderOfEPQ ( ID , ByteTransform ). Content;
        }

        //【解码器】实例化
        {
            decoder_Settings = protocolFactory. GetDecoder_Settings ( ByteTransform );
            decoder_ACM = protocolFactory. GetDecoder_ACM ( ByteTransform ).Content;
            decoder_DCM = protocolFactory. GetDecoder_DCM ( ByteTransform ).Content;
            decoder_EPQ = protocolFactory. GetDecoder_EPQ ( ByteTransform ). Content;
        }

        //【功能模块】实例化
        {
            Settings = new Settings ( encoder_Settings , decoder_Settings , CheckResponse );
        }
    }

    /// <summary>
    /// 在打开端口时的初始化方法
    /// </summary>
    /// <returns>是否初始化成功</returns>
    protected override OperateResult<byte[ ]> InitializationOnOpen ( )
    {
        return HandShake ( );
    }

    /// <summary>
    /// 联机命令；执行该命令实例化功能模块对象
    /// </summary>
    /// <returns></returns>
    public override OperateResult<byte[ ]> HandShake ( )
    {
        //使用接口显式调用HandShake方法；
        IFuncSettings settings = Settings;
        var result = settings. HandShake ( );

        //如果发送联机命令成功则实例化对象
        if ( result. IsSuccess && result. Content != null )
        {
            ACM = new ACM ( );          

            DCM = new DCM ( encoder_DCM , decoder_DCM , CheckResponse , Settings. IsEnabled_DCM );         

            EPQ = new EPQ ( encoder_EPQ , decoder_EPQ , CheckResponse , Settings. IsEnabled_EPQ );
        }
        //无论是否成功都返回联机命令执行结果
        return result;
    }

    #region 《Core Interative 核心交互
    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文。
    /// </summary>
    /// <param name="send">发送的报文</param>
    /// <param name="awaitData"></param>
    /// <returns></returns>
    private OperateResult<byte[ ]> CheckResponse ( byte[ ] send , bool awaitData = true )
    {
        // 发送报文并获取回复报文
        OperateResult<byte[ ]> response = ReadBase ( send , awaitData );

        //获取回复不成功
        if ( !response. IsSuccess || response. Content == null )
        {
            return response;
        }

        // 长度校验
        if ( response. Content. Length < 7 )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. ReceiveDataLengthTooShort );
        }

        // 检查CRC:CheckCRC包含报文头验证
        if ( !_CRCChecker. CheckCRC ( response. Content ) )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. CRCCheckFailed + SoftBasic. ByteToHexString ( response. Content , ' ' ) );
        }
        return response;
    }
    #endregion Core Interative 核心交互》
}
