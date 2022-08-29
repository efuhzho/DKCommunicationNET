using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 【表源一体】交流表模块功能
/// </summary>
public interface IModuleACM:IProperties_ACS
{
    /// <summary>
    /// 创建报文：读取交流标准表测量值/标准源输出值
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> ReadData ( );

    /// <summary>
    /// 创建报文：读取输出状态：Flag=0表示输出稳定，Flag=1表示输出未稳定。：读标准源输出状态
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> ReadData_Status ( );
}
