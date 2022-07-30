namespace DKCommunicationNET. Interface
{
    public interface IModuleDCS
    {
        public bool IsDCSModuleConnected { get; }
        public void SetDCSAmplitude ( );
        public void StartDCS ( );
        public void StopDCS ( );
    }
}
