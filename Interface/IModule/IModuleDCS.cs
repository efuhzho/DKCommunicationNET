﻿namespace DKCommunicationNET. Interface. IModule
{
    public interface IModuleDCS
    {
        public void GetRangesOfDCS ( );
        public void SetDCSAmplitude ( );
        public void StartDCS ( );
        public void StopDCS ( );
    }
}