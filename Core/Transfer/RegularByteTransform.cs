/**********************************************************************************************
 * 
 *    说明：一般的转换类
 *    
 *    常规的数据转换继承自基类，并不需要进行变换运算
 * 
 **********************************************************************************************/

namespace DKCommunicationNET. Core;

/// <summary>
/// 常规的字节转换类
/// </summary>
public class RegularByteTransform : ByteTransformBase
{

    #region Constructor

    /// <summary>
    /// 实例化一个默认的对象
    /// </summary>
    public RegularByteTransform ( ) : base ( )
    {

    }

    /// <summary>
    /// 使用指定的解析规则来初始化对象
    /// </summary>
    /// <param name="dataFormat">解析规则</param>
    public RegularByteTransform ( DataFormat dataFormat ) : base ( dataFormat )
    {

    }

    #endregion
}
