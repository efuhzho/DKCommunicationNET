using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Module;

namespace DKCommunicationNET
{
    internal interface IDandickFactory
    {
        OperateResult<IModuleACS> GetModuleACS ( );
        OperateResult<IModuleDCM> GetModuleDCM ( );
        OperateResult<IModuleDCS> GetModuleDCS ( );
        OperateResult<IModuleIO> GetModuleIO ( );
        OperateResult<IModulePQ> GetModulePQ ( );
    }
}
