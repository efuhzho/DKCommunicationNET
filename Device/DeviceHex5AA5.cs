using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Device;

public class DeviceHex5AA5 : IDevice
{
    public bool IsACSModuleSupported => true;
    public bool IsDCSModuleSupported => true;
    public bool IsDCMModuleSupported => true;
    public bool IsIOModuleSupported => true;
    public bool IsPQModuleSupported => true;

    public ProtocolTypes ProtocolType => ProtocolTypes.Hex5AA5;
}
