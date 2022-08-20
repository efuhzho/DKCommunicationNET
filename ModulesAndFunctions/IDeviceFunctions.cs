using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. ModulesAndFunctions
{
    internal interface IDeviceFunctions
    {
        /// <summary>
        /// 设备型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 设备出厂编号
        /// </summary>
        public string? SN { get; set; }

        /// <summary>
        /// 固件版本号
        /// </summary>
        public string? Firmware { get; }

        /// <summary>
        /// 协议版本号
        /// </summary>
        public string? ProtocolVer { get; }

        #region FuncB
        /// <summary>
        /// 指示交流源功能是否激活
        /// </summary>
        public bool IsEnabled_ACS { get; }

        /// <summary>
        /// 指示交流表功能是否激活
        /// </summary>
        public bool IsEnabled_ACM { get; }

        /// <summary>
        /// 指示标准表钳表功能是否激活
        /// </summary>
        public bool IsEnabled_ACM_Cap { get; }

        /// <summary>
        /// 指示直流源功能是否激活
        /// </summary>
        public bool IsEnabled_DCS { get; }

        /// <summary>
        /// 辅助直流源是否激活
        /// </summary>
        public bool IsEnabled_DCS_AUX { get; }

        /// <summary>
        /// 指示直流表功能是否激活
        /// </summary>
        public bool IsEnabled_DCM { get; }

        /// <summary>
        /// 指示直流纹波表是否激活
        /// </summary>
        public bool IsEnabled_DCM_RIP { get; }
   

        /// <summary>
        /// 指示开关量功能是否激活
        /// </summary>
        public bool IsEnabled_IO { get; }

        /// <summary>
        /// 指示电能校验功能是否激活
        /// </summary>
        public bool IsEnabled_EPQ { get; }
        #endregion FuncB

        #region FuncS 
        /// <summary>
        /// 指示双频输出功能是否激活
        /// </summary>
        public bool IsEnabled_DualFreqs { get; }

        /// <summary>
        /// 指示保护电流功能是否激活
        /// </summary>
        public bool IsEnabled_IProtect { get; }

        /// <summary>
        /// 指示闪变输出功能是否激活
        /// </summary>
        public bool IsEnabled_PST { get; }

        /// <summary>
        /// 指示遥信功能是否激活
        /// </summary>
        public bool IsEnabled_YX { get; }

        /// <summary>
        /// 指示高频输出功能是否激活
        /// </summary>
        public bool IsEnabled_HF { get; }

        /// <summary>
        /// 指示电机控制功能是否激活
        /// </summary>
        public bool IsEnabled_PWM { get; }

        /// <summary>
        /// 指示对时功能是否激活
        /// </summary>
        public bool IsEnabled_PPS { get; }

        #endregion FuncS
    }
}
