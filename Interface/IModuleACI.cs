using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKCommunicationNET.Interface
{
    public interface IModuleACI
    {
        public bool IsModuleACIConnected { get; }       

        void SetACI(float amplitude);
    }
}
