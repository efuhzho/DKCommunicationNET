using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module;

internal class ModuleACI : IModuleACI
{
  
    public bool IsModuleACIConnected => throw new NotImplementedException ( );

 

    public void SetACI ( float amplitude )
    {
        throw new NotImplementedException ( );
    }      

    public void STOP ( )
    {
        throw new NotImplementedException ( );
    }
}
