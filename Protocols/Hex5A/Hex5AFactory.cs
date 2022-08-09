using DKCommunicationNET. Module;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A;

[Model(Models.Hex5A)]
internal class Hex5AFactory : IDandickFactory
{
    public OperateResult<IModuleACS> GetModuleACS ( )
    {
        return OperateResult. CreateSuccessResult ( new ModuleACS (  ) as IModuleACS );
    }

    public OperateResult<IModuleDCM> GetModuleDCM ( )
    {
        
        return  OperateResult.CreateSuccessResult(new ModuleDCM(Models.Hex81) as IModuleDCM);
    }

    public OperateResult<IModuleDCS> GetModuleDCS ( )
    {
        return  OperateResult.CreateSuccessResult(new ModuleDCS(Models.Hex81) as IModuleDCS);

    }

    public OperateResult<IModuleIO> GetModuleIO ( )
    {
        return  OperateResult.CreateSuccessResult(new ModuleIO(Models.Hex81) as IModuleIO);

    }

    public OperateResult<IModulePQ> GetModulePQ ( )
    {
        return  OperateResult.CreateSuccessResult(new ModulePQ(Models.Hex81) as IModulePQ);

    }
}
