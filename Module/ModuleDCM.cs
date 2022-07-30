using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCM : ModuleBase, IModuleDCM
    {
        public ModuleDCM ( ProtocolTypes protocolType )
        {
            _protocolType = protocolType;
        }
    }
}
