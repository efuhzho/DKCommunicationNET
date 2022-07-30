namespace DKCommunicationNET. Interface
{
    public interface IModuleDCI
    {
        public ProtocolTypes ProtocolType { get; set; }

        public bool IsModuleDCIConnected { get; set; }
        public void SetDCI ( );
        public void START ( );
        public void STOP ( );       
    }
}
