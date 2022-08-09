using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

internal class Hex81Factory : IDandickFactory
{
    public OperateResult<IModuleACS> GetModuleACS ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<IModuleDCM> GetModuleDCM ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<IModuleDCS> GetModuleDCS ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<IModuleIO> GetModuleIO ( )
    {
        return new OperateResult<IModuleIO> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IModulePQ> GetModulePQ ( )
    {
        throw new NotImplementedException ( );
    }
}
