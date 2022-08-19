using DKCommunicationNET. Protocols. Hex81;
using System. Collections. Generic;

namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 【接口】交流源模块接口
/// </summary>
public interface IModuleACS
{
    /// <summary>
    /// 交流源打开命令
    /// </summary>    
    /// <returns>
    ///     <list  type="table">
    ///     <listheader>
    ///         <term>返回对象属性</term>
    ///         <description>描述</description>
    ///     </listheader>     
    ///     <item>
    ///         <term>IsSuccess</term>
    ///         <description>成功标志：指示操作是否成功；【重要】调用系统方法后必须判断此标志。</description>
    ///     </item>      
    ///     <item>
    ///         <term>ErrorCode</term>
    ///         <description>错误代码：IsSuccess为true时忽略。</description>
    ///     </item>     
    ///     <item>
    ///          <term>Message</term>
    ///          <description>错误信息：IsSuccess为true时忽略。</description>
    ///     </item>
    ///      <item>
    ///          <term>Content</term>
    ///          <description>下位机回复的原始报文（或字节数组），当怀疑系统解码错误时，可用于诊断和临时自行解码，IsSuccess为false时忽略。</description>
    ///     </item>
    ///      </list>    
    /// </returns>
    /// <exception cref="NotImplementedException" ></exception>
    public OperateResult<byte[ ]> OpenACS ( );

    /// <summary>
    /// 交流源关闭命令
    /// </summary>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> CloseACS ( );

    /// <summary>
    /// 读取交流源档位
    /// </summary>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> GetRangesOfACS ( );

    /// <summary>
    /// 设置交流源档位
    /// </summary>
    /// <param name="rangeIndexOfACU"></param>
    /// <param name="rangeIndexOfACI"></param>
    /// <param name="rangeIndexOfIP"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetRangesOfACS ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 );

    /// <summary>
    /// 设置交流源幅度
    /// </summary>
    /// <param name="amplitude"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetAmplitudeOfACS ( float amplitude );

    /// <summary>
    /// 设置相位
    /// </summary>
    /// <param name="PhaseUa"></param>
    /// <param name="PhaseUb"></param>
    /// <param name="PhaseUc"></param>
    /// <param name="PhaseIa"></param>
    /// <param name="PhaseIb"></param>
    /// <param name="PhaseIc"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );

    /// <summary>
    /// 设置频率
    /// </summary>
    /// <param name="FreqOfAll"></param>
    /// <param name="FreqOfC"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 );

    /// <summary>
    /// 设置接线模式
    /// </summary>
    /// <param name="WireMode"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetWireMode ( Enum WireMode );

    public OperateResult<byte[ ]> SetClosedLoop ( Enum ClosedLoopMode );

    /// <summary>
    /// 设置谐波模式
    /// </summary>
    /// <param name="HarmonicMode">枚举类型参数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetHarmonicMode(Enum HarmonicMode );

    /// <summary>
    /// 设置谐波参数
    /// </summary>
    /// <param name="harmonicChannels"></param>
    /// <param name="harmonicArgs"></param>
    /// <returns></returns>
    OperateResult<byte[ ]> WriteHarmonics ( Enum harmonicChannels ,HarmonicArgs[] harmonicArgs );
}

