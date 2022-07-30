namespace DKCommunicationNET. Interface
{
    public interface IModuleACS
    {
        public bool IsACSModuleConnected { get; set; }
        public void SetACSAmplitude ( float amplitude );
        public void StartACS ( );
        public void StopACS ( );
    }
}
