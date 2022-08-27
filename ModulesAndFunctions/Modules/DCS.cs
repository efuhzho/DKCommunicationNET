using System. IO. Ports;
using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Module;

/// <summary>
/// 直流源功能模块
/// </summary>
public class DCS :  IModuleDCS
{
    internal DCS ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse, IByteTransform byteTransform )
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void GetRangesOfDCS ( )
    {
        throw new NotImplementedException ( );
    }

    public void SetDCSAmplitude ( )
    {
        throw new NotImplementedException ( );
    }

    public void StartDCS ( )
    {

    }

    public void StopDCS ( )
    {
        throw new NotImplementedException ( );
    }
}
