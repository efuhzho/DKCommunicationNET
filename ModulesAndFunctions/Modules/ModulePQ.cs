using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Module
{
    internal class ModulePQ :ModuleBase, IModulePQ
    {
        public ModulePQ ( Models protocolType )
        {
            _protocolType=protocolType;
        }
    }
}
