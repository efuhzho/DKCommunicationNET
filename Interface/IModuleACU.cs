namespace DKCommunicationNET. Interface
{
    public interface IModuleACU
    {
        public bool IsModuleACUConnected { get; }
        void SetACU(float amplitude);      
    }
}
