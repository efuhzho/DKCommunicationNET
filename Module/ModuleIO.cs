using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module
{
    internal class ModuleIO : ModuleBase, IModuleIO
    {
        public ModuleIO ( ProtocolTypes protocolType )
        {
            _protocolType = protocolType;
        }
    }
}
