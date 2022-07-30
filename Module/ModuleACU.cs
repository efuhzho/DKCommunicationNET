using DKCommunicationNET. Interface;


namespace DKCommunicationNET. Module;

internal class ModuleACU: IModuleACU
{
   
    private bool isModuleACUConnected = true;
    public bool IsModuleACUConnected => isModuleACUConnected;

    public ProtocolTypes ProtocolType { get; set; }

    public ModuleACU (Models model )
    {
        ProtocolType = (ProtocolTypes)model;
    }
    public ModuleACU ( ProtocolTypes protocolType )
    {
        ProtocolType = protocolType;
    }
    public void SetACU ( float amplitude )
    {
       
    }

    public void START ( )
    {
       
    }

    public void STOP ( )
    {
        
    }  
}
