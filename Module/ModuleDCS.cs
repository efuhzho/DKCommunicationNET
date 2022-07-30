using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCS : IModuleDCS
    {
        public bool IsDCSModuleConnected => throw new NotImplementedException ( );

        public void SetDCSAmplitude ( )
        {
            throw new NotImplementedException ( );
        }

        public void StartDCS ( )
        {
            throw new NotImplementedException ( );
        }

        public void StopDCS ( )
        {
            throw new NotImplementedException ( );
        }
    }
}
