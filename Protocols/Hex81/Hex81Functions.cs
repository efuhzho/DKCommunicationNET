using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// 基于Hex81协议的设备所支持的功能状态
/// </summary>
internal class Hex81Functions : IProtocolFunctions
{
    /// <summary>
    /// 本类设备的协议类型：Hex81
    /// </summary>
    public Models Model => Models. Hex81;

    /// <summary>
    /// 基于本协议的设备：是否支持【交流源模块】：是
    /// </summary>
    public bool IsACSModuleSupported => false;

    /// <summary>
    /// 基于本协议的设备：是否支持【直流源模块】：是
    /// </summary>
    public bool IsDCSModuleSupported => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【直流表模块】：是
    /// </summary>
    public bool IsDCMModuleSupported => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【开关量模块】：否
    /// </summary>
    public bool IsIOModuleSupported { get; }

    /// <summary>
    /// 基于本协议的设备：是否支持【电能模块】：是
    /// </summary>
    public bool IsPQModuleSupported => true;




    #region MyRegion


    //public FunctionsHex81 ( )
    //{
    //    ModuleACS = new ModuleACS ( Model );
    //}
    //public IModuleACS ModuleACS { get; set; }
    //public IModuleDCS ModuleDCS { get; set; }
    //public IModuleDCM ModuleDCM { get; set; }
    //public IModulePQ ModulePQ { get; set; }
    //public IModuleIO ModuleIO { get; set; }

    //private IModuleACS _ModuleACS;
    //private IModuleDCS _ModuleDCS;
    //private IModuleDCM _ModuleDCM;
    //private IModulePQ _ModulePQ;

    //public float Ua { get; set; }
    //public float Ub { get; set; }
    //public float Uc { get; set; }
    //public float Ia { get; set; }
    //public float Ib { get; set; }
    //public float Ic { get; set; }
    //public float IPa { get; set; }
    //public float IPb { get; set; }
    //public float IPc { get; set; }
    //public float DCU { get; set; }
    //public float DCI { get; set; }

    ////多余的
    //public float Ux { get  ; set ; }
    //public float Ix { get  ; set ; }

    //public void Hex81dosomething ( )
    //{
    //    _ModuleACS  = new ModuleACS ( Model );
    //    _ModuleACS. SetAmplitude ( 100.005f );
    //}

    #endregion
}
