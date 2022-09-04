using System. Security. Cryptography;
using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols. Hex5A;
using DKCommunicationNET. Protocols. Hex81;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 报文创建助手接口
/// </summary>
internal interface IPacketBuilderHelper
{
    /// <summary>
    /// 有参数
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">完成命令长度</param>
    /// <param name="data">数据</param>
    /// <returns></returns>
    OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode , ushort commandLength , byte[ ] data );

    /// <summary>
    /// 无参数简化
    /// </summary>
    /// <param name="commandCode"><inheritdoc cref="PacketShellBuilder(byte, ushort, byte[])"/></param>
    /// <returns></returns>
    OperateResult<byte[ ]> PacketShellBuilder ( byte commandCode );
}

/// <summary>
/// 系统设置报文创建类接口
/// </summary>
internal interface IPacketBuilder_Settings
{
    /// <summary>
    /// 设置系统模式
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetSystemMode ( byte systemMode );

    /// <summary>
    /// 设置显示页面
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetDisplayPage ( byte displayPage );

    /// <summary>
    /// 【高级别权限】设备信息设置
    /// </summary>
    /// <param name="password">密码</param>
    /// <param name="id"></param>
    /// <param name="sn">设备序列号</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetDeviceInfo ( char[ ] password , byte id , string sn );

    /// <summary>
    /// 设置波特率；设备不保存 固件V5.26 以后版本支持，该命令设置后不应答，可在设置界面查询当前波特率
    /// </summary>
    /// <param name="baudRate">要设置的波特率</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetBaudRate ( ushort baudRate );
}

/// <summary>
/// 交流源报文创建类接口
/// </summary>
internal interface IPacketsBuilder_ACS
{
    /// <summary>
    /// 创建【获取交流源档位信息】的报文
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_GetRanges ( );

    /// <summary>
    /// 创建报文：[具备保护电流]设置交流源档位
    /// </summary>
    /// <param name="rangeIndexOfACU">电压档位索引值</param>
    /// <param name="rangeIndexOfACI">电流档位索引值</param>
    /// <param name="rangeIndexOfIP">保护电流档位索引值</param>  
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 );

    /// <summary>
    /// 创建报文：[具备X相]设置交流源档位
    /// </summary>
    /// <param name="rangeIndexOfACU"></param>
    /// <param name="rangeIndexOfACI"></param>
    /// <param name="rangeIndex_Ux"></param>
    /// <param name="rangeIndex_Ix"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndex_Ux =0, byte rangeIndex_Ix =0);

    /// <summary>
    /// 创建【设置交流源幅度】的报文
    /// </summary>
    /// <param name="UA">要设定的输出值：UA</param>
    /// <param name="UB">要设定的输出值：UB</param>
    /// <param name="UC">要设定的输出值：UC</param>
    /// <param name="IA">要设定的输出值：IA</param>
    /// <param name="IB">要设定的输出值：IB</param>
    /// <param name="IC">要设定的输出值：IC</param>
    /// <param name="IPAOrUx">保护电流或X相电压</param>
    /// <param name="IPBOrIX">要设定的输出值：保护电流或X相电流</param>
    /// <param name="IPC">要设定的输出值：IPC</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPAOrUx, float IPBOrIX  , float IPC );
      
    /// <summary>
    /// 创建【打开交流源】的报文
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_Open ( );

    /// <summary>
    /// 创建【关闭交流源】的报文
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_Stop ( );

    /// <summary>
    /// 创建报文：设置相位
    /// </summary>
    /// <param name="PhaseUa"></param>
    /// <param name="PhaseUb"></param>
    /// <param name="PhaseUc"></param>
    /// <param name="PhaseIa"></param>
    /// <param name="PhaseIb"></param>
    /// <param name="PhaseIc"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );

    /// <summary>
    /// 创建报文：设置频率
    /// </summary>
    /// <param name="FreqOfAll">输出频率</param>
    /// <param name="FreqOfC">【支持双频时有效】C相频率</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetFrequency ( float FreqOfAll , float FreqOfC = 0 );

    /// <summary>
    /// 创建报文：设置接线方式
    /// </summary>
    /// <param name="wireMode">接线方式</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetWireMode ( WireMode wireMode );

    /// <summary>
    /// 【Hex81】创建报文：设置闭环模式
    /// </summary>
    /// <param name="closeLoopMode"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetClosedLoop ( CloseLoopMode closeLoopMode );

    /// <summary>
    /// 设置谐波模式
    /// </summary>
    /// <param name="harmonicMode"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetHarmonicMode ( HarmonicMode harmonicMode );

    /// <summary>
    /// 【报文长度不可超过256】创建报文：设置谐波输出参数；如果谐波组为null,则指令为清空谐波
    /// </summary>
    /// <param name="channels">要设置的谐波通道</param>
    /// <param name="harmonics">谐波组,如果谐波组为null,则指令为清空谐波</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetHarmonics ( Channels channels , HarmonicArgs[ ]? harmonics = null );

    /// <summary>
    /// 创建报文：设置有功功率
    /// </summary>
    /// <param name="channel">要设置的有功功率通道</param>
    /// <param name="p">要设置的有功功率值</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetWattPower ( byte channel , float p );

    /// <summary>
    ///  创建报文：设置无功功率
    /// </summary>
    /// <param name="channel">要设置的无功功率通道</param>
    /// <param name="q">要设置的无功功率值</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_SetWattLessPower ( byte channel , float q );

    /// <summary>
    /// 创建报文：读取交流标准表测量值/标准源输出值
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_ReadData ( );

    /// <summary>
    /// 创建报文：读取输出状态：Flag=0表示输出稳定，Flag=1表示输出未稳定。：读标准源输出状态
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_ReadData_Status ( );
}

/// <summary>
/// 交流表报文创建类接口
/// </summary>
internal interface IPacketBuilder_ACM
{
    /// <summary>
    /// 创建报文：读取交流标准表测量值/标准源输出值
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_ReadData ( );

    /// <summary>
    /// 创建报文：读取输出状态：Flag=0表示输出稳定，Flag=1表示输出未稳定。：读标准源输出状态
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> Packet_ReadData_Status ( );
}

/// <summary>
/// 直流源报文创建类接口
/// </summary>
internal interface IPacketBuilder_DCS : ISetProperties_DCS
{
    /// <summary>
    /// 创建报文：停止直流电压输出
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Stop_DCU ( );

    /// <summary>
    /// 创建报文：停止直流电压流输出
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Stop_DCI ( );

    /// <summary>
    /// 创建报文：停止直流电阻输出
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Stop_DCR ( );

    /// <summary>
    /// 创建报文：打开直流电压输出
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Open_DCU ( );

    /// <summary>
    /// 创建报文：打开直流电流输出
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Open_DCI ( );

    /// <summary>
    /// 创建报文：打开直流电阻输出
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Open_DCR ( );

    /// <summary>
    /// 创建报文：设置直流电流幅度和档位
    /// </summary>
    /// <param name="indexOfRange">档位索引值</param>
    /// <param name="amplitude">要设定的幅值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetAmplitude_DCI ( float amplitude , byte indexOfRange );

    /// <summary>
    /// 创建报文：设置直流电压幅度和档位
    /// </summary>
    /// <param name="indexOfRange">档位索引值</param>
    /// <param name="amplitude">要设定的幅值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetAmplitude_DCU ( float amplitude , byte indexOfRange );

    /// <summary>
    /// 创建报文：设置直流电阻幅度和档位
    /// </summary>
    /// <param name="amplitude">要设定的幅值</param>
    /// <param name="indexOfRange">档位索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetAmplitude_DCR ( float amplitude , byte indexOfRange );

    /// <summary>
    /// 创建报文：读取直流源当前输出值
    /// </summary>
    /// <param name="Resistor">【可选参数：当支持直流电阻输出时有效】当需要读取直流输出电阻值时：请输入字符 'R'</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_ReadData ( char? Resistor = null );

    /// <summary>
    /// 创建报文：获取直流源档位信息
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_GetRanges ( );

    /// <summary>
    /// 创建报文：设置直流电流幅度和档位
    /// </summary>
    /// <param name="indexOfRange">档位索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetRange_DCI ( byte indexOfRange );

    /// <summary>
    /// 创建报文：设置直流电压幅度和档位
    /// </summary>
    /// <param name="indexOfRange">档位索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetRange_DCU ( byte indexOfRange );

    /// <summary>
    /// 创建报文：设置直流电阻幅度和档位
    /// </summary>
    /// <param name="indexOfRange">档位索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetRange_DCR ( byte indexOfRange );
}

/// <summary>
/// 直流表模块报文创建类接口
/// </summary>
internal interface IPacketBuilder_DCM : ISetProperties_DCM
{
    /// <summary>
    /// 设置直流表量程和测量类型
    /// </summary>
    /// <param name="rangeIndex">量程索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetRange_DCMU ( byte rangeIndex );
    OperateResult<byte[ ]> Packet_SetRange_DCMI ( byte rangeIndex );
    OperateResult<byte[ ]> Packet_SetRange_DCMU_Ripple ( byte rangeIndex );
    OperateResult<byte[ ]> Packet_SetRange_DCMI_Ripple ( byte rangeIndex );

    /// <summary>
    /// 读取直流表数据
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_ReadData ( );

    /// <summary>
    /// 获取直流表档位信息
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_GetRanges ( );
}

/// <summary>
/// 开关量模块报文创建类接口
/// </summary>
internal interface IPacketBuilder_IO
{
}

/// <summary>
/// 电能模块报文创建类接口
/// </summary>
internal interface IPacketBuilder_EPQ : ISetProperties_EPQ
{

    /// <summary>
    /// 创建报文：读取电能校验误差
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_ReadData ( Channels_EPQ Channels = Channels_EPQ. Channel1 );

    /// <summary>
    /// 创建报文：设置电能校验参数并启动有功电能校验
    /// </summary>
    /// <param name="Const_PM">表有功脉冲常数</param>
    /// <param name="Rounds">校验圈数</param>
    /// <param name="DIV">分频系数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_StartTest_P ( float Const_PM , uint Rounds = 10 , uint DIV = 1 );

    /// <summary>
    /// 创建报文：设置电能校验参数并启动无功电能校验
    /// </summary>
    /// <param name="Const_QM">表无功脉冲常数</param>
    /// <param name="Rounds">校验圈数</param>
    /// <param name="DIV">分频系数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_StartTest_Q ( float Const_QM , uint Rounds = 10 , uint DIV = 1 );

    /// <summary>
    /// 创建报文：设置有功输出脉冲常数
    /// </summary>
    /// <param name="Const_PS">源有功脉冲常数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetConst_PS ( float Const_PS );

    /// <summary>
    /// 创建报文：设置无功输出脉冲常数
    /// </summary>
    /// <param name="Const_QS">源无功脉冲常数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SetConst_QS ( float Const_QS );
}

/// <summary>
/// 校准功能报文创建类接口
/// </summary>
internal interface IPacketBuilder_Calibrate
{
    /// <summary>
    /// 清空校准参数，恢复出厂默认设置
    /// </summary>
    /// <param name="calibrateType">类型：0-标准源；1-标准表；2-钳形表；3-直流源，4-直流表（备用）。</param>
    /// <param name="uRangeIndex">要清除的电压档位索引值。该命令会清除该档位下的幅值和相位校准参数。</param>
    /// <param name="iRangeIndex">要清除的电流档位索引值。该命令会清除该档位下的幅值和相位校准参数。</param>
    /// <remarks>【注意】协议类型Hex81:当 Type=3 或 4 时，如果要清空电压档位参数，则设置电流档位为一个大于电流总档位数的值（建议设置为 100）；如果要清空电流档位参数，则设置电压档位为一个大于电压总档位数的值（建议设置为 100）。</remarks>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_ClearData ( CalibrateType calibrateType , byte uRangeIndex , byte iRangeIndex );

    /// <summary>
    /// 设置交流源和交流标准表校准档位
    /// </summary>
    /// <param name="uRangeIndex">要设置的电压档位索引值</param>
    /// <param name="iRangeIndex">要设置的电流档位索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SwitchACRange ( byte uRangeIndex , byte iRangeIndex );

    /// <summary>
    /// 切换交流源校准点
    /// </summary>
    /// <param name="uRangeIndex">当前电压档位索引值：【注意】校准命令中的档位索引值不做换档动作，只作为判断校验所用。</param>
    /// <param name="iRangeIndex">当前电流档位索引值：【注意】校准命令中的档位索引值不做换档动作，只作为判断校验所用。</param>
    /// <param name="calibrateLevel">当前的校准点，0-零点，1-20%，2-100%，3-相位校准。</param>
    /// <param name="sUA">设置的校准点标准值，float</param>
    /// <param name="sUB">设置的校准点标准值，float</param>
    /// <param name="sUC">设置的校准点标准值，float</param>
    /// <param name="sIA">设置的校准点标准值，float</param>
    /// <param name="sIB">设置的校准点标准值，float</param>
    /// <param name="sIC">设置的校准点标准值，float</param>
    /// <remarks>
    /// Level=0时为 0.0；level=1 时为 20%RG；level=2 时为 100%RG；以上为幅值校准。level = 3 时为相位校准，建议 SUa = SIa = 0；SUb = SIb = 120°；SUc = SIc = 240°
    /// </remarks>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SwitchACPoint ( byte uRangeIndex , byte iRangeIndex , CalibrateLevel calibrateLevel , float sUA , float sUB , float sUC , float sIA , float sIB , float sIC );

    /// <summary>
    /// 交流源校准命令
    /// </summary>
    /// <param name="uRangeIndex">当前电压档位索引值：【注意】校准命令中的档位索引值不做换档动作，只作为判断校验所用。</param>
    /// <param name="iRangeIndex">当前电流档位索引值：【注意】校准命令中的档位索引值不做换档动作，只作为判断校验所用。</param>
    /// <param name="calibrateLevel">当前的校准点，0-零点，1-20%，2-100%，3-相位校准。</param>
    /// <param name="mUA">当前所接的校准用标准表的读数，float</param>
    /// <param name="mUB">当前所接的校准用标准表的读数，float</param>
    /// <param name="mUC">当前所接的校准用标准表的读数，float</param>
    /// <param name="mIA">当前所接的校准用标准表的读数，float</param>
    /// <param name="mIB">当前所接的校准用标准表的读数，float</param>
    /// <param name="mIC">当前所接的校准用标准表的读数，float</param>
    /// <remarks>【注意】输入的标准表读数限制在校准点标准值（SXx）的±20%之内，如果大于±20%，不发送校准命令。</remarks>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_DoAC ( byte uRangeIndex , byte iRangeIndex , CalibrateLevel calibrateLevel , float mUA , float mUB , float mUC , float mIA , float mIB , float mIC );

    /// <summary>
    /// 确认交流源校准，保存校准参数
    /// <list type="number">
    /// <listheader>
    /// <description>交流标准源幅值和相位校准步骤:</description>
    /// </listheader>
    /// <item>
    /// <description>设置系统模式为 10（即标准源校准模式）(44H)；</description>
    /// </item>
    /// <item>
    /// <description>设置源档位参数(21H)；</description>
    /// </item>
    /// <item>
    /// <description>设置校准点 Level=1 (22H)；（注意：level=0 由终端自动进行校准）；</description>
    /// </item>
    /// <item>
    /// <description>当输出稳定后，根据校准用标准表的读数，发送幅值校准命令(23H)；</description>
    /// </item>
    /// <item>
    /// <description>当源输出幅值和标准表的幅值达到要求的精度后，发送保存校准参数命令(24H)；</description>
    /// </item>
    /// <item>
    /// <description>设置下一个校准点（level=2/3），重复（3）—（5）操作，完成一组档位的校准；</description>
    /// </item>
    /// <item>
    /// <description>校准下一组档位参数，重复（2）—（6）操作。</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="uRangeIndex">当前校准的电压档位</param>
    /// <param name="iRangeIndex">当前校准的电流档位</param>
    /// <param name="calibrateLevel">当前校准点</param>    /// 
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_Save ( byte uRangeIndex , byte iRangeIndex , CalibrateLevel calibrateLevel );

    /// <summary>
    /// 交流标准表和钳形表校准命令
    /// </summary>
    /// <param name="uRangeIndex">当前电压档位索引值：【注意】校准命令中的档位索引值不做换档动作，只作为判断校验所用。</param>
    /// <param name="iRangeIndex">当前电流档位索引值：【注意】校准命令中的档位索引值不做换档动作，只作为判断校验所用。</param>
    /// <param name="calibrateLevel">当前的校准点，0-零点，1-20%，2-100%，3-相位校准。</param>
    /// <param name="UA">当前所接的校准用标准源的读数，float</param>
    /// <param name="UB">当前所接的校准用标准源的读数，float</param>
    /// <param name="UC">当前所接的校准用标准源的读数，float</param>
    /// <param name="IA">当前所接的校准用标准源的读数，float</param>
    /// <param name="IB">当前所接的校准用标准源的读数，float</param>
    /// <param name="IC">当前所接的校准用标准源的读数，float</param>
    /// <remarks>
    ///     <list type="number">
    ///         <listheader>
    ///             <description>交流标准表和钳形表校准步骤:</description>
    ///         </listheader>
    ///         <item><description>设置系统模式为 11 或 12（即标准表或钳形表校准模式）(44H)</description></item>
    ///         <item><description>设置源档位参数(21H)</description></item>
    ///         <item><description>打开校准用标准源输出，设置校准用标准源的输出为 0 或 20%RG 或 100%RG</description></item>
    ///         <item><description>当标准表读数稳定后，根据校准用标准源的读数，发送交流标准表校准命令(25H)</description></item>
    ///         <item><description>校准下一组档位参数，重复（2）—（4）操作</description></item>          
    ///     </list>
    /// </remarks>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_DoACMeter ( byte uRangeIndex , byte iRangeIndex , CalibrateLevel calibrateLevel , float UA , float UB , float UC , float IA , float IB , float IC );

    /// <summary>
    /// 设置直流校准点
    /// </summary>
    /// <param name="dCSourceType">类型，’DCU’-电压，’I’-电流。</param>
    /// <param name="rangeIndex"></param>
    /// <param name="calibrateLevel"></param>
    /// <param name="sDCAmplitude">设置的校准点的标准值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_SwitchDCPoint ( Calibrate_DCSourceType dCSourceType , byte rangeIndex , CalibrateLevel calibrateLevel , float sDCAmplitude );

    /// <summary>
    /// 直流源校准
    /// </summary>
    /// <param name="dCSourceType">类型，’DCU’-电压，’I’-电流。</param>
    /// <param name="rangeIndex"></param>
    /// <param name="calibrateLevel"></param>
    /// <param name="mDCAmplitude">校准用的标准表读数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_DoDC ( Calibrate_DCSourceType dCSourceType , byte rangeIndex , CalibrateLevel calibrateLevel , float mDCAmplitude );

    /// <summary>
    /// 直流表校准
    /// </summary>
    /// <param name="dCSourceType">类型，’DCU’-电压，’I’-电流。</param>
    /// <param name="rangeIndex"></param>
    /// <param name="calibrateLevel"></param>
    /// <param name="sDCAmplitude">校准用的标准源输出幅值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> Packet_DoDCMeter ( Calibrate_DCMeterType dCSourceType , byte rangeIndex , CalibrateLevel calibrateLevel , float sDCAmplitude );
}

/// <summary>
/// 对时功能报文创建类接口
/// </summary>
internal interface IPacketBuilder_PPS
{
    /// <summary>
    /// 自动对时方式
    /// </summary>
    /// <param name="Type_CompareTime"></param>
    /// <param name="timeZones"></param>
    /// <returns></returns>
    OperateResult<byte[ ]> CompareTime_Auto ( Enum Type_CompareTime , short timeZones );

    /// <summary>
    /// 手动对时方式
    /// </summary>
    /// <param name="Type_CompareTime"></param>
    /// <param name="dateTime"></param>
    /// <param name="timeZones"></param>
    /// <returns></returns>
    OperateResult<byte[ ]> CompareTime_Manual ( Enum Type_CompareTime , DateTime dateTime , short timeZones = 8 );

    /// <summary>
    /// 查询对时模块状态数据
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> ReadData_PPS ( );


}

