using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

/// <summary>
/// 直流表功能模块
/// </summary>
public class DCM :IModuleDCM
{

    /// <summary>
    /// 定义交流表模块对象
    /// </summary>
    private IPacketBuilder_ACM? _PacketBuilder;

    public void GetRangesOfDCM ( )
    {
        throw new NotImplementedException ( );
    }
}
