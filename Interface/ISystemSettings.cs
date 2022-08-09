using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Interface;

public interface ISystemSettings
{
    ISystemMode SystemMode { get; set; }
    IDisplayPage PageDisplay { get; }
}



public interface ISystemMode
{
    public void SetSystemMode ( Enum Mode );    
    public void SetSystemMode ( string Mode );
    public List<string> GetSystemModes ( );
}

public interface IDisplayPage
{
    public void SetDisplayPage ( Enum Page );

    public void SetDisplayPage (string Page );
    public List<string> GetDisplayPages ( );

}


public class SystemMode : ISystemMode
{
    
    //public string[ ] GetSystemModes ( )
    //{
    //    //switch ( _protocolType )
    //    //{
    //    //    case Models. Hex81:
    //    //        return Hex81Information. GetSystemMode ( );
    //    //    case Models. Hex5A:
    //    //        return Hex5AA5Information. GetSystemMode ( );
    //    //    default:
    //    //        return Hex81Information. GetSystemMode ( );
    //    //}
    //    return Hex81Information. GetSystemMode ( );
    //}

    public void SetSystemMode ( Enum SystemMode )
    {
        throw new NotImplementedException ( );
    }



    public void SetSystemMode ( string SystemMode )
    {

    }

    List<string> ISystemMode.GetSystemModes ( )
    {
        throw new NotImplementedException ( );
    }
}

//public class SystemSettings : ModuleBase, ISystemSettings
//{
//    public SystemSettings ( Models model )
//    {
//        _protocolType = model;
//    }

//    public ISystemMode SystemMode => new SystemMode ( _protocolType );

//    public IDisplayPage PageDisplay => throw new NotImplementedException ( );
//}
