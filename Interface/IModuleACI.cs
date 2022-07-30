namespace DKCommunicationNET. Interface
{
    public interface IModuleACI
    {
        public ProtocolTypes ProtocolType { get; set; }

        public bool IsModuleACIConnected { get; }       

        void SetACI(float amplitude);
        public void START ( );
        public void STOP ( );
    }
}
