namespace DKCommunicationNET.BaseClass;

public class DKSerialBase //: SerialBase
{
    protected ProtocolTypes protocolype = ProtocolTypes.Hex81;
    public ProtocolTypes ProtocolType
    {
        get { return protocolype; }
    }

    private int id;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    private string sn = string.Empty;

    public string SN
    {
        get
        {
            return sn;
        }

        set
        {
            sn = value;
        }
    }

    public void HandShake()
    {

    }
}
