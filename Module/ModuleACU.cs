﻿using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module;

internal class ModuleACU: IModuleACU
{
    private bool isModuleACUConnected = true;
    public bool IsModuleACUConnected => isModuleACUConnected;
    public void SetACU ( float amplitude )
    {
        Console. WriteLine ( $"ModuleACU={amplitude}" );
    }

    public void STOP ( )
    {
        Console. WriteLine ( "ModuleACU=zero" );

    }

    public void StopACVoltage ( )
    {
        throw new NotImplementedException ( );
    }
}