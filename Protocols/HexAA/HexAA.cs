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
 *  *更改报文长度定义为字节长度，注意协议内报文长度为字节长度除以2；
 *  
 *************************************************************************************************/


/// <summary>
/// 三相多功能标准表通信协议V2012 修订日期：2012年12月
/// </summary>
internal class HexAA
{
    public static readonly HexAA Instance = new ( );

    #region 《命令码定义
    /// <summary>
    /// 本协议的公共类型定义
    /// </summary>
    public const Models Model = Models. HexAA;
    /// <summary>
    /// 协议帧起始标志
    /// </summary>
    public const byte FrameID0 = 0xAA;
    /// <summary>
    /// 协议帧起始标志
    /// </summary>
    public const byte FrameID1 = 0x55;
    /// <summary>
    /// 无参指令的长度（最小长度指令）
    /// </summary>
    public const byte MinLength = 8;
    /// <summary>
    /// 数据区起始索引值
    /// </summary>
    public const byte DataStartIndex = 6;
    /// <summary>
    /// 联机命令，读取终端型号和版本号
    /// </summary>
    public const byte HandShake = 0x4C;
    /// <summary>
    /// 设置系统参数
    /// </summary>
    public const byte SetSystemArgs = 0x44;
    public const byte SetSystemArgsLen = 24;
    /// <summary>
    /// 设置电能参数并启动电能校验
    /// </summary>
    public const byte SetStartEPQ = 0x45;

    /// <summary>
    /// 获取量程和系统功能状态
    /// </summary>
    public const byte GetRanges = 0x89;
    /// <summary>
    /// 读基本参数
    /// </summary>
    public const byte ReadData = 0x90;
    /// <summary>
    /// 读谐波参数
    /// </summary>
    public const byte ReadHarmonics = 0x91;
    /// <summary>
    /// 读三相谐波含有率
    /// </summary>
    public const byte ReadHarmonicContains = 0x92;
    /// <summary>
    /// 读波形数据
    /// </summary>
    public const byte ReadWaveform = 0x93;
    public const byte ReadWaveformLen = 7;
    /// <summary>
    /// 读电能误差
    /// </summary>
    public const byte ReadDataOfEPQ = 0x94;
    #endregion 命令码定义》

    public static byte[ ] CRCcalculator ( byte[ ] shell )
    {
        byte[ ] crc = new byte[2];
        ushort value = 0;
        //从第三个字节开始计算和校验，忽略报文头
        for ( int i = 2 ; i < shell. Length * 2 - 2 ; i++ )
        {
            value += shell[i];
        }
        byte[ ] temp = BitConverter. GetBytes ( value );
        crc[0] = temp[1];
        crc[1] = temp[0];
        return crc;
    }
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
internal enum HarmonicChannels
{
    A,B,C
}
#endregion 枚举类型》
