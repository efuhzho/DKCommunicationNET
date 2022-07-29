using DKCommunicationNET.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKCommunicationNET.Module
{
    internal class ModuleDCSource:IDCSource
    {
        private bool isDCSConneted = true;       

        public bool IsDCSConnected => isDCSConneted;
        public void StarDCVoltage()
        {
            Console.WriteLine("DCU=600V");

        }

        public void StopDCVoltage()
        {
            Console.WriteLine("DCU=zero");

        }
    }
}
