using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// 基于Hex81协议的设备所支持的功能状态
/// </summary>
internal class Hex81Functions : IProtocolFunctions
{
    public bool IsSupported_ACS => true;
    
    public bool IsSupported_ACM { get; }
   
    public bool IsSupported_DCS => true;
   
    public bool IsSupported_DCM => true;
   
    public bool IsSupported_IO { get; }
    
    public bool IsSupported_EPQ => true;

    public bool IsSupported_ACM_Cap { get; }

    public bool IsSupported_DCS_AUX { get; }

    public bool IsSupported_DCM_RIP { get; }

    public bool IsSupported_DualFreqs { get; }

    public bool IsSupported_IProtect { get; }

    public bool IsSupported_PST { get; }

    public bool IsSupported_YX { get; }

    public bool IsSupported_HF { get; }

    public bool IsSupported_PWM { get; }

    public bool IsSupported_PPS { get; }

    public OperateResult<byte[ ]> GetPacketOfHandShake ( )
    {
        return OperateResult. CreateSuccessResult ( Hex81Information. HandShakePacket );
    }
    #region MyRegion


    //public FunctionsHex81 ( )
    //{
    //    ModuleACS = new ModuleACS ( Model );
    //}
    //public IModuleACS ModuleACS { get; set; }
    //public IModuleDCS ModuleDCS { get; set; }
    //public IModuleDCM ModuleDCM { get; set; }
    //public IModuleEPQ ModulePQ { get; set; }
    //public IModuleIO ModuleIO { get; set; }

    //private IModuleACS _ModuleACS;
    //private IModuleDCS _ModuleDCS;
    //private IModuleDCM _ModuleDCM;
    //private IModuleEPQ _ModulePQ;

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
