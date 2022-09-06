﻿using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Protocols. Hex81;

namespace DKCommunicationNET. ModulesAndFunctions. Functions
{
    /// <summary>
    /// 校准
    /// </summary>
    public class Calibrate : ICalibrate
    {      
        /// <summary>
        /// 发送报文，获取并校验下位机的回复报文的委托方法
        /// </summary>
        private readonly Func<byte[] , bool , OperateResult<byte[]>> _methodOfCheckResponse;

        /// <summary>
        /// 定义交流源模块对象
        /// </summary>
        private readonly IPacketBuilder_Calibrate? _packetsBuilder;

        /// <summary>
        /// 协议是否支持校准功能
        /// </summary>
        private readonly bool _isSupported ;

        internal Calibrate ( ushort id , IProtocolFactory protocolFactory , Func<byte[] , bool , OperateResult<byte[]>> methodOfCheckResponse , IByteTransform byteTransform ,bool isSupported )
        {
            //接收执行报文发送接收的委托方法        
            _methodOfCheckResponse = methodOfCheckResponse;

            //初始化报文创建器
            _packetsBuilder = protocolFactory. GetPacketBuilderOfCalibrate ( id , byteTransform ). Content;  

            _isSupported = isSupported;
        }

        /// <summary>
        /// 当前校准类型
        /// </summary>
        public CalibrateType CalibrateType { get; set; }

        /// <summary>
        /// 当前校准点
        /// </summary>
        public CalibrateLevel CalibrateLevel { get; set; }

        /// <summary>
        /// 【清空校准参数：交流】
        /// </summary>
        /// <param name="calibrateType">校准时的操作类型</param>
        /// <param name="uRangeIndex">电压档位索引值</param>
        /// <param name="iRangeIndex">电流档位索引值</param>
        /// <returns>下位机回复的原始报文，用于自主解析，通常可忽略</returns>
        public OperateResult<byte[]> Calibrate_ClearData ( CalibrateType calibrateType , byte uRangeIndex , byte iRangeIndex )            
        {
            if ( ( int ) calibrateType == 3 || ( int ) calibrateType == 4 )
            {
                return new OperateResult<byte[]> ( 8113 , "请使用直流源（表）数据清空方法:Calibrate_ClearDCU_Data或者Calibrate_ClearDCI_Data" );
            }
            //执行命令前的功能状态检查
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }

            //执行命令并获取回复报文
            OperateResult<byte[]> result =CommandAction.Action( _packetsBuilder.Packet_ClearData ( calibrateType , uRangeIndex , iRangeIndex ),_methodOfCheckResponse);
            if ( result. IsSuccess )
            {
                CalibrateType = calibrateType;
            }
            return result;
        }

        /// <summary>
        /// 【清空校准参数：直流电压】
        /// </summary>
        /// <param name="calibrateType">校准时的操作类型</param>
        /// <param name="uRangeIndex">电压档位索引值</param>
        /// <returns>下位机回复的原始报文，用于自主解析，通常可忽略</returns>
        public OperateResult<byte[]> Calibrate_ClearDCU_Data ( CalibrateType calibrateType , byte uRangeIndex )
        {
            //执行命令前的功能状态检查
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }

            //执行命令并获取回复报文
            OperateResult<byte[]> result =CommandAction.Action( _packetsBuilder.Packet_ClearData ( calibrateType , uRangeIndex , 100 ),_methodOfCheckResponse);    //100：保护无需清空的电压档位数据
            if ( result. IsSuccess )
            {
                CalibrateType = calibrateType;
            }
            return result;
        }

        /// <summary>
        /// 【清空校准参数：直流电流】
        /// </summary>
        /// <param name="calibrateType">校准时的操作类型</param>
        /// <param name="iRangeIndex">电流档位索引值</param>
        /// <returns>下位机回复的原始报文，用于自主解析，通常可忽略</returns>
        public OperateResult<byte[]> Calibrate_ClearDCI_Data ( CalibrateType calibrateType , byte iRangeIndex )
        {
            //执行命令前的功能状态检查
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }

            //执行命令并获取回复报文
            OperateResult<byte[]> response =CommandAction.Action( _packetsBuilder.Packet_ClearData ( calibrateType , 100 , iRangeIndex ),_methodOfCheckResponse);    //100：保护无需清空的电流档位数据
            if ( response. IsSuccess )
            {
                CalibrateType = calibrateType;
            }
            return response;
        }

        /// <summary>
        /// 【切换交流源（表）校准档位】
        /// </summary>
        /// <param name="uRangeIndex">电压档位索引值</param>
        /// <param name="iRangeIndex">电流档位索引值</param>
        /// <returns>下位机回复的原始报文，用于自主解析，通常可忽略</returns>
        public OperateResult<byte[]> Calibrate_SwitchACRange ( byte uRangeIndex , byte iRangeIndex )
        {
            //执行命令前的功能状态检查
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }

            //执行命令并获取回复报文
            OperateResult<byte[]> response =CommandAction.Action( _packetsBuilder.Packet_SwitchACRange  ( uRangeIndex , iRangeIndex ),_methodOfCheckResponse);

            return response;
        }

        /// <summary>
        /// 【切换交流源（表）校准点】
        /// </summary>
        /// <param name="uRangeIndex">电压档位索引值，校验用</param>
        /// <param name="iRangeIndex">电流档位索引值，校验用</param>
        /// <param name="calibrateLevel">当前校准点</param>
        /// <param name="sUA">校准点的标准值</param>
        /// <param name="sUB">校准点的标准值</param>
        /// <param name="sUC">校准点的标准值</param>
        /// <param name="sIA">校准点的标准值</param>
        /// <param name="sIB">校准点的标准值</param>
        /// <param name="sIC">校准点的标准值</param>
        /// <returns>下位机回复的原始报文，用于自主解析，通常可忽略</returns>
        public OperateResult<byte[]> Calibrate_SwitchACPoint ( byte uRangeIndex , byte iRangeIndex , CalibrateLevel calibrateLevel , float sUA , float sUB , float sUC , float sIA , float sIB , float sIC )
        {
            //执行命令前的功能状态检查
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }

            //执行命令并获取回复报文
            OperateResult<byte[]> response =CommandAction.Action( _packetsBuilder.Packet_SwitchACPoint ( uRangeIndex , iRangeIndex , calibrateLevel , sUA , sUB , sUC , sIA , sIB , sIC ),_methodOfCheckResponse);
            if ( response. IsSuccess )
            {
                CalibrateLevel = calibrateLevel;
            }
            return response;
        }

        /// <summary>
        /// 【设置相位校准点】
        /// </summary>
        /// <param name="uRangeIndex">电压档位索引值，校验用</param>
        /// <param name="iRangeIndex">电流档位索引值，校验用</param>
        /// <returns>下位机回复的原始报文，用于自主解析，通常可忽略</returns>
        public OperateResult<byte[]> Calibrate_SwitchACPoint_Phase ( byte uRangeIndex , byte iRangeIndex )
        {
            return Calibrate_SwitchACPoint ( uRangeIndex , iRangeIndex , ( CalibrateLevel ) 3 , 0 , 120f , 240f , 0 , 120f , 240f );
        }

        /// <summary>
        /// 【执行交流源校准】
        /// </summary>
        /// <param name="uRangeIndex">电压档位索引值，校验用</param>
        /// <param name="iRangeIndex">电流档位索引值，校验用</param>
        /// <param name="calibrateLevel"></param>
        /// <param name="mUA">当前所接的标准表的读数：必须在标准值±20%范围内，否则下位机不执行校准命令</param>
        /// <param name="mUB">当前所接的标准表的读数：必须在标准值±20%范围内，否则下位机不执行校准命令</param>
        /// <param name="mUC">当前所接的标准表的读数：必须在标准值±20%范围内，否则下位机不执行校准命令</param>
        /// <param name="mIA">当前所接的标准表的读数：必须在标准值±20%范围内，否则下位机不执行校准命令</param>
        /// <param name="mIB">当前所接的标准表的读数：必须在标准值±20%范围内，否则下位机不执行校准命令</param>
        /// <param name="mIC">当前所接的标准表的读数：必须在标准值±20%范围内，否则下位机不执行校准命令</param>
        /// <returns>下位机回复的原始报文，用于自主解析，通常可忽略</returns>
        public OperateResult<byte[]> Calibrate_DoAC ( byte uRangeIndex , byte iRangeIndex , CalibrateLevel calibrateLevel , float mUA , float mUB , float mUC , float mIA , float mIB , float mIC )
        {
            //执行命令前的功能状态检查
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }

            //执行命令并获取回复报文
            OperateResult<byte[]> response =CommandAction.Action( _packetsBuilder.Packet_DoAC ( uRangeIndex , iRangeIndex , calibrateLevel , mUA , mUB , mUC , mIA , mIB , mIC ),_methodOfCheckResponse);

            return response;
        }

        /// <summary>
        /// 【确认交流源校准，保存校准参数】
        /// </summary>
        /// <param name="uRangeIndex"></param>
        /// <param name="iRangeIndex">电流档位索引，用作核验，只对5A有效</param>
        /// <param name="calibrateLevel"></param>
        /// <returns></returns>
        public OperateResult<byte[]> Calibrate_Save ( byte uRangeIndex , byte iRangeIndex , CalibrateLevel calibrateLevel )
        {
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }
            OperateResult<byte[]> result = CommandAction. Action ( _packetsBuilder. Packet_Save ( uRangeIndex , iRangeIndex , calibrateLevel ) , _methodOfCheckResponse );

            return result;
        }

        /// <summary>
        /// 【交流标准表和钳形表校准】
        /// </summary>
        /// <param name="uRangeIndex"></param>
        /// <param name="iRangeIndex"></param>
        /// <param name="calibrateLevel"></param>
        /// <param name="UA"></param>
        /// <param name="UB"></param>
        /// <param name="UC"></param>
        /// <param name="IA"></param>
        /// <param name="IB"></param>
        /// <param name="IC"></param>
        /// <returns></returns>
        public OperateResult<byte[]> Calibrate_DoACMeter ( byte uRangeIndex , byte iRangeIndex , CalibrateLevel calibrateLevel , float UA , float UB , float UC , float IA , float IB , float IC )
        {
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }
            OperateResult<byte[]> response = CommandAction. Action ( _packetsBuilder. Packet_DoACMeter ( uRangeIndex , iRangeIndex , calibrateLevel , UA , UB , UC , IA , IB , IC ) , _methodOfCheckResponse );

            return response;
        }

        /// <summary>
        /// 【设置直流源校准点】
        /// </summary>
        /// <param name="dCSourceType"></param>
        /// <param name="rangeIndex"></param>
        /// <param name="calibrateLevel"></param>
        /// <param name="sDCAmplitude"></param>
        /// <returns></returns>
        public OperateResult<byte[]> Calibrate_SwitchDCPoint (
            Calibrate_DCSourceType dCSourceType ,
            byte rangeIndex ,
            CalibrateLevel calibrateLevel ,
            float sDCAmplitude )
        {
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }
            OperateResult<byte[]> result = CommandAction. Action ( _packetsBuilder. Packet_SwitchDCPoint ( dCSourceType , rangeIndex , calibrateLevel , sDCAmplitude ) , _methodOfCheckResponse );
            if ( result.IsSuccess )
            {
                CalibrateLevel = calibrateLevel;
            }

            return result;
        }

        /// <summary>
        /// 【执行直流源校准】
        /// </summary>
        /// <param name="dCSourceType"></param>
        /// <param name="rangeIndex"></param>
        /// <param name="calibrateLevel"></param>
        /// <param name="sDCAmplitude"></param>
        /// <returns></returns>
        public OperateResult<byte[]> Calibrate_DoDC (
          Calibrate_DCSourceType dCSourceType ,
          byte rangeIndex ,
          CalibrateLevel calibrateLevel ,
          float sDCAmplitude )
        {
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }
            OperateResult<byte[]> result = CommandAction. Action ( _packetsBuilder. Packet_DoDC ( dCSourceType , rangeIndex , calibrateLevel , sDCAmplitude ) , _methodOfCheckResponse );          

            return result;
        }

        /// <summary>
        /// 【直流表校准】
        /// </summary>
        /// <param name="dCSourceType"></param>
        /// <param name="rangeIndex"></param>
        /// <param name="calibrateLevel"></param>
        /// <param name="sDCAmplitude"></param>
        /// <returns></returns>
        public OperateResult<byte[]> Calibrate_DoDCMeter (
            Calibrate_DCMeterType dCSourceType ,
            byte rangeIndex ,
            CalibrateLevel calibrateLevel ,
            float sDCAmplitude )
        {
            var checkResult = CheckFunctionsStatus. CheckFunctionsState ( _packetsBuilder , _isSupported );
            if ( !checkResult. IsSuccess || _packetsBuilder == null )
            {
                return checkResult;
            }
            OperateResult<byte[]> result = CommandAction. Action ( _packetsBuilder. Packet_DoDCMeter ( dCSourceType , rangeIndex , calibrateLevel , sDCAmplitude ) , _methodOfCheckResponse );

            return result;
        }
    }
}