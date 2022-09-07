using DKCommunicationNET.Core;
using DKCommunicationNET.Protocols.Hex5A;
using DKCommunicationNET.Protocols.Hex5A.Encoders;

namespace DKCommunicationNET.Protocols.Hex81.Encoders;

/// <summary>
/// 创建校准报文类
/// </summary>
internal class Hex81Encoder_Calibrate : IEncoder_Calibrate
{
    private readonly Hex5AEncodeHelper _PBHelper;

    private readonly IByteTransform _transform;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <param name="byteTransform">数据转换规则</param>
    public Hex81Encoder_Calibrate(ushort id, IByteTransform byteTransform)
    {
        _PBHelper = new Hex5AEncodeHelper(id);

        _transform = byteTransform;
    }
    public OperateResult<byte[ ]> Packet_ClearData(CalibrateType calibrateType, byte uRangeIndex, byte iRangeIndex)
    {
        byte[ ] data = new byte[3] { (byte)calibrateType, uRangeIndex, iRangeIndex };
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_ClearData, Hex81Information.Calibrate_ClearDataLength, data);
    }

    public OperateResult<byte[ ]> Packet_DoAC(byte uRangeIndex, byte iRangeIndex, CalibrateLevel calibrateLevel, float mUA, float mUB, float mUC, float mIA, float mIB, float mIC)
    {
        byte[ ] data = new byte[27];
        data[0] = uRangeIndex;
        data[1] = iRangeIndex;
        data[2] = (byte)calibrateLevel;
        _transform.TransByte(mUA).CopyTo(data, 3);
        _transform.TransByte(mUB).CopyTo(data, 7);
        _transform.TransByte(mUC).CopyTo(data, 11);
        _transform.TransByte(mIA).CopyTo(data, 15);
        _transform.TransByte(mIB).CopyTo(data, 19);
        _transform.TransByte(mIC).CopyTo(data, 23);
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_DoAC, Hex81Information.Calibrate_DoACLength, data);
    }

    public OperateResult<byte[ ]> Packet_DoACMeter(byte uRangeIndex, byte iRangeIndex, CalibrateLevel calibrateLevel, float UA, float UB, float UC, float IA, float IB, float IC)
    {
        byte[ ] data = new byte[27];
        data[0] = uRangeIndex;
        data[1] = iRangeIndex;
        data[2] = (byte)calibrateLevel;
        _transform.TransByte(UA).CopyTo(data, 3);
        _transform.TransByte(UB).CopyTo(data, 7);
        _transform.TransByte(UC).CopyTo(data, 11);
        _transform.TransByte(IA).CopyTo(data, 15);
        _transform.TransByte(IB).CopyTo(data, 19);
        _transform.TransByte(IC).CopyTo(data, 23);
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_DoACMeter, Hex81Information.Calibrate_DoACMeterlength, data);
    }

    public OperateResult<byte[ ]> Packet_DoDC(Calibrate_DCSourceType dCSourceType, byte rangeIndex, CalibrateLevel calibrateLevel, float mDCAmplitude)
    {
        byte[ ] data = new byte[7];
        data[0] = (byte)dCSourceType;
        data[1] = rangeIndex;
        data[2] = (byte)calibrateLevel;
        _transform.TransByte(mDCAmplitude).CopyTo(data, 3);
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_DoDC, Hex81Information.Calibrate_DoDClength, data);
    }

    public OperateResult<byte[ ]> Packet_DoDCMeter(Calibrate_DCMeterType dCMeterType, byte rangeIndex, CalibrateLevel calibrateLevel, float sDCAmplitude)
    {
        byte[ ] data = new byte[7];
        data[0] = (byte)dCMeterType;
        data[1] = rangeIndex;
        data[2] = (byte)calibrateLevel;
        _transform.TransByte(sDCAmplitude).CopyTo(data, 3);
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_DoDCMeter, Hex81Information.Calibrate_DoDCMeterLength, data);
    }

    public OperateResult<byte[ ]> Packet_Save(byte uRangeIndex, byte iRangeIndex, CalibrateLevel calibrateLevel)
    {
        byte[ ] data = new byte[3] { uRangeIndex, iRangeIndex, (byte)calibrateLevel };
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_Save, Hex81Information.Calibrate_SaveLength, data);
    }

    public OperateResult<byte[ ]> Packet_SwitchACPoint(byte uRangeIndex, byte iRangeIndex, CalibrateLevel calibrateLevel, float sUA, float sUB, float sUC, float sIA, float sIB, float sIC)
    {
        byte[ ] data = new byte[27];
        data[0] = uRangeIndex;
        data[1] = iRangeIndex;
        data[2] = (byte)calibrateLevel;
        _transform.TransByte(sUA).CopyTo(data, 3);
        _transform.TransByte(sUB).CopyTo(data, 7);
        _transform.TransByte(sUC).CopyTo(data, 11);
        _transform.TransByte(sIA).CopyTo(data, 15);
        _transform.TransByte(sIB).CopyTo(data, 19);
        _transform.TransByte(sIC).CopyTo(data, 23);
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_SwitchACPoint, Hex81Information.Calibrate_SwitchACPointLength, data);
    }

    public OperateResult<byte[ ]> Packet_SwitchACRange(byte uRangeIndex, byte iRangeIndex)
    {
        byte[ ] data = new byte[2] { uRangeIndex, iRangeIndex };
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_SwitchACRange, Hex81Information.Calibrate_SwitchACRangeLength, data);
    }

    public OperateResult<byte[ ]> Packet_SwitchDCPoint(Calibrate_DCSourceType dCSourceType, byte rangeIndex, CalibrateLevel calibrateLevel, float sDCAmplitude)
    {
        byte[ ] data = new byte[7];
        data[0] = (byte)dCSourceType;
        data[1] = rangeIndex;
        data[2] = (byte)calibrateLevel;
        _transform.TransByte(sDCAmplitude).CopyTo(data, 3);
        return _PBHelper.EncodeHelper(Hex81Information.Calibrate_SwitchDCPoint, Hex81Information.Calibrate_SwitchDCPointLength, data);
    }
}
