using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. ProtocolInformation;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Interface;

public interface ISystemSettings
{
    ISystemMode SystemMode { get; }
    IDisplayPage PageDisplay { get; }
}



public interface ISystemMode
{
    internal void SetSystemMode ( Enum Mode );    
    public void SetSystemMode ( string Mode );
    public List<string> GetSystemModes ( );

}

public interface IDisplayPage
{
    internal void SetDisplayPage ( Enum Page );

    public void SetDisplayPage (string Page );
    public List<string> GetDisplayPages ( );

}


//public class SystemMode :ModuleBase , ISystemMode
//{
//    public SystemMode ( Models model)
//    {
//        _protocolType = model;
//    }
//    public string [ ] GetSystemModes ( )
//    {
//        switch ( _protocolType )
//        {
//            case Models. Hex81:
//                return Hex81. GetSystemMode ( );
//            case Models. Hex5AA5:
//                return Hex5AA5.GetSystemMode ( );   
//            default:
//                return Hex81. GetSystemMode ( );               
//        }
//    }

//    public void SetSystemMode ( Enum SystemMode )
//    {
//        throw new NotImplementedException ( );
//    }



//    public void SetSystemMode ( string SystemMode )
//    {

//    }
//}

//public class SystemSettings:ModuleBase,ISystemSettings
//{
//    public SystemSettings ( Models model)
//    {
//        _protocolType= model;
//    }

//    public ISystemMode SystemMode =>new SystemMode(_protocolType);

//    //    public IDisplayPage PageDisplay => throw new NotImplementedException ( );
//}
