namespace DKCommunicationNET. Interface
{
    public interface IModuleACI
    {
        public bool IsModuleACIConnected { get; }       

        void SetACI(float amplitude);
    }
}
