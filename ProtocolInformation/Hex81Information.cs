using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. ProtocolInformation;

internal class Hex81Information 
{
    public const Models Model = Models. Hex81;

    public const byte FramID = 0x81;

    #region 系统
    /// <summary>
    /// 系统应答命令
    /// </summary>
    public const byte OK = 0x4B;
    public const ushort OKLength = 8;

    /// <summary>
    /// 联机命令，读取终端型号和版本号
    /// </summary>
    public const byte HandShake = 0x4C;
    public const ushort HandShakeCommandLength = 7;

    /// <summary>
    /// 设置系统模式
    /// </summary>
    public const byte SetSystemMode = 0x44;
    public const ushort SetSystemModeCommandLength = 8;

    /// <summary>
    /// 发送故障代码，带枚举数据
    /// </summary>
    public const byte ErrorCode = 0x52;

    /// <summary>
    /// 设置当前终端显示界面
    /// </summary>
    public const byte SetDisplayPage = 0x4A;
    public const ushort SetDisplayPageCommandLength = 8;
    #endregion

    public Enum ModeTTTTTEST
    {
        get;
        set;
    } = Models. Hex81;



    public enum SystemMode
    {

        a,
        b,
        c
    }

    public static string [ ] GetSystemMode ( )
    {
        string [ ] result = new string [ 3 ];
        result [ 0 ] = SystemMode. a. ToString ( );
        result [ 1 ] = SystemMode. b. ToString ( );
        result [ 2 ] = SystemMode. c. ToString ( );

        return result;
    }
}
