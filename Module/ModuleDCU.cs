using DKCommunicationNET.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKCommunicationNET.Module
{
    internal class ModuleDCU:IModuleDCU
    {
        private bool isModuleDCUConneted = true;       

        public bool IsModuleDCUConnected => isModuleDCUConneted;
        public void SetDCU()
        {
            Console.WriteLine("ModuleDCU=600V");

        }

        public void StopDCVoltage()
        {
            Console.WriteLine("ModuleDCU=zero");

        }
    }
}
