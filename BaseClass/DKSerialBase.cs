namespace DKCommunicationNET.BaseClass;

public class DKSerialBase //: SerialBase
{
    protected ProtocolTypes _protocolType ;
    public ProtocolTypes ProtocolType
    {
        get { return _protocolType; }
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

    public virtual void HandShake()
    {

    }
}
