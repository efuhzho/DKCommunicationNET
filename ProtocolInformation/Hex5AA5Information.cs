using DKCommunicationNET.BaseClass;
using DKCommunicationNET.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKCommunicationNET.ProtocolInformation;

internal class Hex5AA5Information 
{
    public enum SystemMode
    {

       A55,B55,C,
    }

    public static string [ ] GetSystemMode ( )
    {
        string [ ] result = new string [ 3 ];
        result [ 0 ] = SystemMode. A55. ToString ( );
        result [ 1 ] = SystemMode. B55. ToString ( );
        result [ 2 ] = SystemMode. C. ToString ( );

        return result;
    }
}
