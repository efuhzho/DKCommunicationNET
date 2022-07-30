namespace DKCommunicationNET. Interface
{
    public interface IModuleACU
    {
        public ProtocolTypes ProtocolType { get; set; }
        public bool IsModuleACUConnected { get; }
        void SetACU(float amplitude);
        public void START ( );
        public void STOP ( );
    }
}
