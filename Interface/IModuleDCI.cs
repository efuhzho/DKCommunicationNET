using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKCommunicationNET.Interface
{
    public interface IModuleDCI
    {
        public bool IsModuleDCIConnected { get; set; }
        public void SetDCI ( );
    }
}
