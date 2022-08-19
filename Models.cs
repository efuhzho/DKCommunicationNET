using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Interface;

namespace DKCommunicationNET;

/// <summary>
/// 设备型号列表，关联协议类型
/// </summary>
public enum Models
{
    Hex81 = 0B_0000_0001,
    Hex5A = 0B_0000_0010,
    DK34B1 = Hex81,
    DK34B2 = Hex81,
    DK34F1 = Hex81,
    DK34B3 = Hex5A,
    DKPTS1 = Hex5A,
    DKPTS = Hex5A,
}



