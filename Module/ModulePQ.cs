using DKCommunicationNET. Interface;


namespace DKCommunicationNET. Module
{
    internal class ModulePQ :ModuleBase, IModulePQ
    {
        public ModulePQ ( ProtocolTypes protocolType )
        {
            _protocolType=protocolType;
        }
    }
}
