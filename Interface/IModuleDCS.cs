namespace DKCommunicationNET. Interface
{
    public interface IModuleDCS
    {
        public bool IsDCSModuleConnected { get; set; }
        public void SetDCSAmplitude ( );
        public void StartDCS ( );
        public void StopDCS ( );
    }
}
