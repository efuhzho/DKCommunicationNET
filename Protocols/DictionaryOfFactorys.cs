using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Protocols. Hex81;
using DKCommunicationNET. Protocols. Hex5A;

namespace DKCommunicationNET. Protocols;

internal class DictionaryOfFactorys
{
    readonly Dictionary<Models , IDandickFactory> keyValuePairs = new ( );
    public DictionaryOfFactorys ( )
    {
        keyValuePairs[Models. Hex81] = new Hex81Factory ( );
        keyValuePairs[Models. Hex5A] = new Hex5AFactory ( );
        //TODO 增加协议；
    }

    internal IDandickFactory GetFactory ( Models model )
    {
        return keyValuePairs[model];
    }
}
