using System.ComponentModel;
using DKCommunicationNET.Core;

namespace DKCommunicationNET.Protocols.Hex5A.Encoders;

/// <summary>
/// 对时功能报文创建
/// </summary>
internal class Hex5AEncoder_PPS : IEncoder_PPS
{
    private readonly Hex5AEncodeHelper _encodeHelper;
    public Hex5AEncoder_PPS(ushort id)
    {
        _encodeHelper = new Hex5AEncodeHelper ( id);
    }

    public OperateResult<byte[ ]> CompareTime_Auto(Enum Type_CompareTime, short timeZones = 8)
    {
        return CompareTime((Type_CompareTime)Type_CompareTime, DateTime.Now, timeZones);
    }


    public OperateResult<byte[ ]> CompareTime_Manual(Enum Type_CompareTime, DateTime dateTime, short timeZones = 8)
    {
        return CompareTime((Type_CompareTime)Type_CompareTime, dateTime, timeZones);
    }

    public OperateResult<byte[ ]> ReadData_PPS()
    {
        return _encodeHelper.EncodeHelper(Hex5A.ReadData_PPS);
    }

    #region 私有方法

    private OperateResult<byte[ ]> CompareTime(Type_CompareTime type, DateTime dateTime, short timeZones = 8)
    {
        byte[ ] data = new byte[6];
        data[0] = (byte)type;
        BitConverter.GetBytes(dateTime.Ticks).CopyTo(data, 1);
        data[5] = (byte)timeZones;
        return _encodeHelper.EncodeHelper(Hex5A.CompareTime, Hex5A.CompareTime_L, data);
    }
    #endregion


}
