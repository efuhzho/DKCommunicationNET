using DKCommunicationNET. BaseClass;
using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;
using DKCommunicationNET. Module;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. ModulesAndFunctions. Functions;
using System. Security. Cryptography;
using DKCommunicationNET. ModulesAndFunctions. Modules;

namespace DKCommunicationNET;

/// <summary>
/// 丹迪克设备类:版本Ver2分组架构 2022年8月25日01点56分。第二次修改
/// </summary>
public class DKStandardSource : DandickSerialBase<RegularByteTransform>
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
    /// 交流源编码器
    /// </summary>
    private readonly IEncoder_ACS? encoder_ACS;
    /// <summary>
    /// 直流源编码器
    /// </summary>
    private readonly IEncoder_DCS? encoder_DCS;
    /// <summary>
    /// 直流表编码器
    /// </summary>
    private readonly IEncoder_DCM? encoder_DCM;
    /// <summary>
    /// 电能模块编码器
    /// </summary>
    private readonly IEncoder_EPQ? encoder_EPQ;
    /// <summary>
    /// 开关量模块编码器
    /// </summary>
    private readonly IEncoder_IO? encoder_IO;
    /// <summary>
    /// 对时功能编码器
    /// </summary>
    private readonly IEncoder_PPS? encoder_PPS;
    /// <summary>
    /// 系统设置编码器
    /// </summary>
    private readonly IEncoder_Settings? encoder_Settings;
    /// <summary>
    /// 校准功能编码器
    /// </summary>
    private readonly IEncoder_Calibrate? encoder_Calibrate;
    #endregion 编码器》

    #region 《解码器
    /// <summary>
    /// 交流源解码器
    /// </summary>
    private readonly IDecoder_ACS? decoder_ACS;
    /// <summary>
    /// 直流源解码器
    /// </summary>
    private readonly IDecoder_DCS? decoder_DCS;
    /// <summary>
    /// 直流表解码器
    /// </summary>
    private readonly IDecoder_DCM? decoder_DCM;
    /// <summary>
    /// 电能模块解码器
    /// </summary>
    private readonly IDecoder_EPQ? decoder_EPQ;
    /// <summary>
    /// 对时功能解码器
    /// </summary>
    private readonly IDecoder_PPS? decoder_PPS;
    /// <summary>
    /// 开关量模块解码器
    /// </summary>
    private readonly IDecoder_IO? decoder_IO;
    /// <summary>
    /// 系统设置解码器
    /// </summary>
    private readonly IDecoder_Settings? decoder_Settings;
    #endregion 解码器》    

    #region 《功能模块
    /// <summary>
    /// 交流源模块
    /// </summary>
    public ACS? ACS { get; }
    /// <summary>
    /// 直流源模块
    /// </summary>
    public DCS? DCS { get; }
    /// <summary>
    /// 直流表模块
    /// </summary>
    public DCM? DCM { get; }
    /// <summary>
    /// 开关量模块
    /// </summary>
    public IO? IO { get; }
    /// <summary>
    /// 电能模块
    /// </summary>
    public EPQ? EPQ { get; }
    /// <summary>
    /// 对时模块
    /// </summary>
    public PPS? PPS { get; }
    /// <summary>
    /// 【高级权限功能】[警告！错误使用此功能将可能导致严重的后果！]
    /// </summary>
    public Calibrater? Calibrate;
    /// <summary>
    /// 系统设置功能（包含HandShake）
    /// </summary>
    public Settings? Settings { get; }
    #endregion 功能模块》

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="model">枚举的设备型号（或协议类型），[找不到对应的设备型号？]：可按枚举中的协议类型实例化对象</param>
    /// <param name="id">设备ID,默认值为0，[可选参数]</param>
    public DKStandardSource ( Models model , ushort id = 0 )
    {
        //【全局变量】实例化
        {
            ID = id;
            protocolFactory = new DictionaryOfFactorys ( ). GetFactory ( model );
        }

        //【校验器】实例化
        {
            _CRCChecker = protocolFactory. GetCRCChecker ( );
        }

        //【编码器】实例化
        {
            encoder_ACS = protocolFactory. GetEncoderOfACS ( ID , ByteTransform ). Content;
            encoder_DCS = protocolFactory. GetEncoderOfDCS ( ID , ByteTransform ). Content;
            encoder_Settings = protocolFactory. GetEncoder_Settings ( ID , ByteTransform ). Content;
            encoder_DCM = protocolFactory. GetEncoderOfDCM ( ID ). Content;
            encoder_EPQ = protocolFactory. GetEncoderOfEPQ ( ID , ByteTransform ). Content;
            encoder_IO = protocolFactory. GetEncoderOfIO ( ID , ByteTransform ). Content;
            encoder_PPS = protocolFactory. GetEncoder_PPS ( ID ). Content;
            encoder_Calibrate = protocolFactory. GetEncoderOfCalibrate ( ID , ByteTransform ). Content;
        }

        //【解码器】实例化
        {
            decoder_ACS = protocolFactory. GetDecoder_ACS ( ByteTransform ). Content;
            decoder_DCS = protocolFactory. GetDecoder_DCS ( ByteTransform ). Content;
            decoder_Settings = protocolFactory. GetDecoder_Settings ( ByteTransform ). Content;
            decoder_DCM = protocolFactory. GetDecoder_DCM ( ByteTransform ). Content;
            decoder_EPQ = protocolFactory. GetDecoder_EPQ ( ByteTransform ). Content;
            decoder_PPS = protocolFactory. GetDecoder_PPS ( ByteTransform ). Content;
            decoder_IO = protocolFactory. GetDecoder_IO ( ByteTransform ). Content;
        }

        //【功能模块】实例化
        {
            if ( encoder_ACS != null && decoder_ACS != null )
            {
                ACS = new ACS ( encoder_ACS , decoder_ACS , CheckResponse );
            }

            if ( encoder_DCS != null && decoder_DCS != null )
            {
                DCS = new DCS ( encoder_DCS , decoder_DCS , CheckResponse );
            }

            if ( encoder_DCM!=null &&decoder_DCM!=null)
            {
                DCM = new DCM ( encoder_DCM , decoder_DCM , CheckResponse );
            }
            if ( encoder_IO!=null&&decoder_IO!=null )
            {
                IO=new IO() ;   //TODO 未实现
            }
            if ( encoder_EPQ != null && decoder_EPQ != null )
            {
                EPQ=new EPQ(encoder_EPQ , decoder_EPQ , CheckResponse );
            }
            if ( encoder_PPS!=null&&decoder_PPS!=null )
            {
                PPS = new PPS ( );  //TODO 未实现
            }

            if ( encoder_Calibrate != null )
            {
                Calibrate = new Calibrater ( encoder_Calibrate , CheckResponse );
            }

            if ( encoder_Settings != null && decoder_Settings != null )
            {
                Settings = new Settings ( encoder_Settings , decoder_Settings , CheckResponse );
            }
        }
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
            return new OperateResult<byte[ ]> ( StringResources. GetLineNum ( ) , StringResources. Language. ReceiveDataLengthTooShort );
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





