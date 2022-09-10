using System. Reflection. Emit;

namespace DKCommunicationNET. Protocols. HexAA;

/**************************************************************************************************
 *  创建日期：2022年9月10日 14:48  作者:Fuhong.Zhou Addr：NO.11 XishiRoad,Yuhuatai District,Nanjing.   
 *
 *  支持的协议版本：三相多功能标准表通信协议V2012 修订日期：2012年12月
 *  
 *  1个长度的定义为一个字，即2个字节；最小单位为一个字
 *  浮点型数据格式：DCBA;
 *  校验码为:1:N-1代数和;
 *  串口属性：BaudRate=115200,DataBits=8,StopBit=1,Parity=None;
 *  量纲均为国际标准单位：频率：Hz;时间：ms;角度：rad;电压：V;电流：A;有功功率：W;无功功率：Var
 *  
 *  Review Records:
 *  
 *************************************************************************************************/


/// <summary>
/// 三相多功能标准表通信协议V2012 修订日期：2012年12月
/// </summary>
internal class HexAA
{
    public static readonly HexAA Instance = new( );

    #region 《命令码定义
    /// <summary>
    /// 本协议的公共类型定义
    /// </summary>
    public const Models Model = Models.HexAA;
    /// <summary>
    /// 协议帧起始标志
    /// </summary>
    public const ushort FrameID = 0xAA55;
    /// <summary>
    /// 无参指令的长度（最小长度指令）
    /// </summary>
    public const ushort MinLength = 4;
    /// <summary>
    /// 联机命令，读取终端型号和版本号
    /// </summary>
    public const ushort HandShake = 0x004C;
    /// <summary>
    /// 设置系统参数
    /// </summary>
    public const ushort SystemSettings = 0x0044;
    public const ushort SystemSettingsLen = 12; //TODO 如果错误则改为18；
    /// <summary>
    /// 设置电能参数并启动电能校验
    /// </summary>
    public const ushort SetStartEPQ = 0x0045;

    /// <summary>
    /// 获取量程和系统功能状态
    /// </summary>
    public const ushort GetRanges = 0x0089;
    /// <summary>
    /// 读基本参数
    /// </summary>
    public const ushort ReadData = 0x0090;
    /// <summary>
    /// 读谐波参数
    /// </summary>
    public const ushort ReadHarmonics = 0x0091;
    /// <summary>
    /// 读三相谐波含有率
    /// </summary>
    public const ushort ReadHarmonicContains = 0x0092;
    /// <summary>
    /// 读波形数据
    /// </summary>
    public const ushort ReadWaveform = 0x0093;
    public const ushort ReadWaveformLen = 7;
    /// <summary>
    /// 读电能误差
    /// </summary>
    public const ushort ReadDataOfEPQ = 0x0094;
    #endregion 命令码定义》
    
    /// <summary>
    /// 重写的ToString方法，返回协议文件版本
    /// </summary>
    /// <returns></returns>
    public override string ToString ( )
    {
        return "三相多功能标准表通信协议V2012 修订日期：2012年12月";
    }
}

#region 《枚举类型

#endregion 枚举类型》
