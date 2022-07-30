namespace DKCommunicationNET. Interface
{
    public interface IModuleDCU
    {
        public bool IsModuleDCUConnected { get;  }
        void SetDCU();
    }
}
