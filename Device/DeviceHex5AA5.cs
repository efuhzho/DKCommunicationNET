using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Device;

public class DeviceHex5AA5 : IDevice
{
    public bool IsACSModuleSupported { get; }
    public bool IsDCSModuleSupported { get; }
    public bool IsDCMModuleSupported { get; }
    public bool IsIOModuleSupported { get; }
    public bool IsPQModuleSupported { get; }

    public ProtocolTypes ProtocolType => ProtocolTypes.Hex5AA5;
}
