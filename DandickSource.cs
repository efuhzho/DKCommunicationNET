using DKCommunicationNET.Interface;
using DKCommunicationNET.Module;
using DKCommunicationNET.Protocol;
using DKCommunicationNET.BaseClass;

namespace DKCommunicationNET;

public class DandickSource : DKSerialBase, ISource
{
    /// <summary>
    /// 
    /// </summary>
    public IACSource ACSource { get; set; }
    public IDCSource DCSource { get; set; }

    public DandickSource(Models model)
    {
        protocolype = (ProtocolTypes)model;
    }
}
