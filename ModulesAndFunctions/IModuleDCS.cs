namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 直流源功能模块接口
/// </summary>
public interface IModuleDCS :ISetProperties_DCS,IReadProperties_DCS
{
    /// <summary>
    /// 获取直流源档位信息
    /// </summary>
    public OperateResult<byte[ ]> GetRanges ( );

    /// <summary>
    /// 设置直流源电压输出幅值和档位（可选），如果要设置自动档位请设置属性：IsAutoRange_DCU
    /// </summary>
    /// <param name="SData">要设置的幅值</param>
    /// <param name="rangeIndex_DCU">要设置的直流源电压档位索引值，如果要设置自动档位请设置属性：IsAutoRange_DCU</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetAmplitude_DCU ( float SData , byte? rangeIndex_DCU = null );

    /// <summary>
    /// 设置直流源电流输出幅值和档位（可选），如果要设置自动档位请设置属性：IsAutoRange_DCI
    /// </summary>
    /// <param name="SData">要设置的幅值</param>
    /// <param name="rangeIndex_DCI">要设置的直流源电流档位索引值，如果要设置自动档位请设置属性：IsAutoRange_DCI</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetAmplitude_DCI ( float SData , byte? rangeIndex_DCI = null );

    /// <summary>
    /// 设置直流电阻输出幅值和档位（可选），如果要设置自动档位请设置属性：IsAutoRange_DCR
    /// </summary>
    /// <param name="SData">要设置的幅值</param>
    /// <param name="rangeIndex_DCR">要设置的直流电阻档位索引值，如果要设置自动档位请设置属性：IsAutoRange_DCR</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetAmplitude_DCR ( float SData , byte? rangeIndex_DCR = null );

    /// <summary>
    /// 打开直流电压使能信号：此时输出为0；
    /// </summary>
    OperateResult<byte[ ]> Open_DCU ( );

    /// <summary>
    /// 打开直流电流使能信号：此时输出为0；
    /// </summary>
    OperateResult<byte[ ]> Open_DCI ( );

    /// <summary>
    /// 打开直流电阻使能信号：此时输出为0；
    /// </summary>
    OperateResult<byte[ ]> Open_DCR ( );

    /// <summary>
    /// 停止直流电压输出命令
    /// </summary>
    OperateResult<byte[ ]> Stop_DCU ( );

    /// <summary>
    /// 停止直流电流输出命令
    /// </summary>
    OperateResult<byte[ ]> Stop_DCI ( );

    /// <summary>
    /// 停止直流电阻输出命令
    /// </summary>
    OperateResult<byte[ ]> Stop_DCR ( );

    /// <summary>
    /// 读取直流源数据
    /// </summary>
    /// <param name="holding">读取保持，循环读取</param>
    /// <param name="Resistor">【可选参数：当支持直流电阻输出时有效】当需要读取直流输出电阻值时：请输入字符 'R'</param>
    /// <returns></returns>
    OperateResult<byte[ ]> ReadData (bool holding=false, char? Resistor = null );
      

    /// <summary>
    /// 设置直流源电压档位
    /// </summary>
    /// <param name="rangeIndex_DCU">要设置的直流电压档位索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetRange_DCU ( byte rangeIndex_DCU );

    /// <summary>
    /// 设置直流源电流档位
    /// </summary>
    /// <param name="rangeIndex_DCI">要设置的直流电流档位索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetRange_DCI ( byte rangeIndex_DCI );

    /// <summary>
    /// 设置直流电阻档位
    /// </summary>
    /// <param name="rangeIndex_DCR">要设置的直流电阻档位索引值</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetRange_DCR ( byte rangeIndex_DCR );
 
}

/// <summary>
/// 直流源设置属性
/// </summary>
public interface ISetProperties_DCS
{
    /// <summary>
    /// 直流电压档位是否为自动档
    /// </summary>
    public bool IsAutoRange_DCU { get; set; }

    /// <summary>
    /// 直流电流档位是否为自动档
    /// </summary>
    public bool IsAutoRange_DCI { get; set; }

    /// <summary>
    /// 直流电阻档位是否为自动档
    /// </summary>
    public bool IsAutoRange_DCR { get; set; }
}

/// <summary>
/// 直流源属性接口
/// </summary>
public interface IReadProperties_DCS
{
    #region 《档位数量

    /// <summary>
    /// 直流源电压档位个数
    /// </summary>
    byte RangesCount_DCU { get; }

    /// <summary>
    /// 直流源电流档位个数
    /// </summary>
    byte RangesCount_DCI { get; }

    /// <summary>
    /// 直流源电阻档位个数
    /// </summary>
    byte RangesCount_DCR { get; }

    #endregion 档位数量》

    #region 《幅值
    /// <summary>
    /// 当前直流源电压幅值
    /// </summary>
    float DCU { get; }

    /// <summary>
    /// 当前直流源电流幅值
    /// </summary>
    float DCI { get; }

    /// <summary>
    /// 当前直流电阻幅值
    /// </summary>
    float DCR { get; }

    #endregion 幅值》

    #region 《档位索引

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    byte RangeIndex_DCU { get; }

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    byte RangeIndex_DCI { get; }

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    byte RangeIndex_DCR { get; }

    #endregion 档位索引》

    #region 《档位列表

    /// <summary>
    /// 直流源电压档位列表
    /// </summary>
    float[ ]? Ranges_DCU { get;  }

    /// <summary>
    /// 直流源电流档位列表
    /// </summary>
    float[ ]? Ranges_DCI { get;  }

    /// <summary>
    /// 直流源电阻档位列表
    /// </summary>
    float[ ]? Ranges_DCR { get; }

    #endregion 档位列表》

    #region 《输出状态

    /// <summary>
    /// 当前直流电压输出状态：true=源打开；false=源关闭
    /// </summary>
    bool IsOpen_DCU { get; }

    /// <summary>
    /// 当前直流电流输出状态：true=源打开；false=源关闭
    /// </summary>
    bool IsOpen_DCI { get; }

    /// <summary>
    /// 当前直流电阻输出状态：true=源打开；false=源关闭
    /// </summary>
    bool IsOpen_DCR { get; }

    #endregion 输出状态》
}