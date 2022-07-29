using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKCommunicationNET.Interface;

namespace DKCommunicationNET.Module;

internal class ModuleACSource : IACSource
{
    private bool isACSConneted = true;
    public bool IsACSConnected => isACSConneted;

    public void StarACVoltage(float amplitude)
    {
        Console.WriteLine($"ACU={amplitude}");
    }

    public void StopACVoltage()
    {
        Console.WriteLine("ACU=zero");
        
    }
}
