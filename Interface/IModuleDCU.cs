namespace DKCommunicationNET. Interface
{
    public interface IModuleDCU
    {
        public ProtocolTypes ProtocolType { get; set; }

        public bool IsModuleDCUConnected { get;  }
        void SetDCU();
        public void START ( );
        public void STOP ( );
      
    }
}
