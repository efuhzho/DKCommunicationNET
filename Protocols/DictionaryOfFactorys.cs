using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Protocols. Hex81;
using DKCommunicationNET. Protocols. Hex5A;
using System. Reflection;
using DKCommunicationNET;

namespace DKCommunicationNET. Protocols;

internal class DictionaryOfFactorys
{
    readonly Dictionary<Models , IDandickFactory> keyValuePairs = new ( );
    public DictionaryOfFactorys ( )
    {
        //keyValuePairs[Models. Hex81] = new Hex81Factory ( );
        //keyValuePairs[Models. Hex5A] = new Hex5AFactory ( );
        ////TODO 增加协议；
        Assembly assembly = Assembly. GetExecutingAssembly ( );
        foreach ( var item in assembly. GetTypes ( ) )
        {
            if ( typeof ( IDandickFactory ). IsAssignableFrom ( item ) && !item. IsInterface )
            {
                ModelAttribute? ModelsAttribute = item. GetCustomAttribute<ModelAttribute> ( );
                if ( ModelsAttribute != null )
                {
                    keyValuePairs[ModelsAttribute. Model] = Activator. CreateInstance ( item ) as IDandickFactory;
                }
            }
        }
    }

    internal IDandickFactory GetFactory ( Models model )
    {
        return keyValuePairs[model];
    }
}
