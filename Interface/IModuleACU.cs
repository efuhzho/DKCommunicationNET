using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKCommunicationNET.Interface
{
    public interface IModuleACU
    {
        public bool IsModuleACUConnected { get; }
        void SetACU(float amplitude);      
    }
}
