using DKCommunicationNET.Core;

namespace DKCommunicationNET.Protocols.Hex5A.Encoders;


internal class Hex5AEncoder_Settings : IEncoder_Settings
{
    private readonly IByteTransform _byteTransform;
    private readonly Hex5AEncodeHelper _encodeHelper;
    public Hex5AEncoder_Settings(ushort id, IByteTransform byteTransform)
    {
        _byteTransform = byteTransform;
        _encodeHelper = new Hex5AEncodeHelper(id);
    }

    public OperateResult<byte[ ]> Packet_SetDeviceInfo(char[ ] password, byte id, string sn)
    {
        byte[ ] data = new byte[31];

        //密码
        for (int i = 0; i < 6; i++)
        {
            data[i] = BitConverter.GetBytes(password[i])[0];
        }
        data[6] = id;

        //序列号
        var snBytes = _byteTransform.TransByte(sn, System.Text.Encoding.ASCII);

        //序列号长度判断
        if (snBytes.Length < 25)
        {
            snBytes.CopyTo(data, 7);
        }
        else return new OperateResult<byte[ ]>("设备编号长度超限");

        //返回结果
        return _encodeHelper.EncodeHelper(Hex5AInformation.SetDeviceInfo, Hex5AInformation.SetDeviceInfo_L, data);
    }


    public OperateResult<byte[ ]> Packet_SetBaudRate(ushort baudRate)
    {
        byte[ ] data = new byte[2];
        BitConverter.GetBytes(baudRate).CopyTo(data, 0);
        return _encodeHelper.EncodeHelper(Hex5AInformation.SetBaudRate, Hex5AInformation.SetBaudRate_L, data);
    }

    public OperateResult<byte[ ]> Packet_SetSystemMode(byte systemMode)
    {
        //不具备此功能
        return new OperateResult<byte[ ]>(StringResources.Language.NotSupportedFunction);
    }

    public OperateResult<byte[ ]> Packet_SetDisplayPage(byte displayPage)
    {
        //不具备此功能
        return new OperateResult<byte[ ]>(StringResources.Language.NotSupportedFunction);
    }

    public OperateResult<byte[ ]> Packet_HandShake ( )
    {
        return _encodeHelper. EncodeHelper ( Hex5AInformation. HandShake );
    }
}
