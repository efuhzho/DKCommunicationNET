using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Language;

/// <summary>
/// 类库的语言基类，默认中文
/// </summary>
internal class DefaultLanguage
{
    #region 设备错误码信息
    public virtual string ErrorUa => "故障：Ua；";
    public virtual string ErrorUb => "故障：Ub；";
    public virtual string ErrorUc => "故障：Uc；";
    public virtual string ErrorU0 => "故障：U0；";
    public virtual string ErrorIa => "故障：Ia；";
    public virtual string ErrorIb => "故障：Ib；";
    public virtual string ErrorIc => "故障：Ic；";
    public virtual string ErrorI0 => "故障：I0；";
    public virtual string ErrorDC => "故障：DC；";
    #endregion 设备错误码信息

    #region 设备功能状态
    //TODO 删除名字
    public virtual string NotEnabledModule => "设备未安装此功能模块请联系周付宏，当前设备：";

    //TODO 删除名字
    public virtual string NotSupportedModule => "该型号不支持此功能请联系周付宏，当前型号：";

    #endregion 设备功能状态

    #region 一般错误信息
    public virtual string ConnectedFailed => "连接失败：";
    public virtual string ConnectedSuccess => "连接成功！";
    public virtual string UnknownError => "未知错误";
    public virtual string ErrorCode => "错误代码";
    public virtual string TextDescription => "文本描述";
    public virtual string ExceptionMessage => "错误信息：";
    public virtual string ExceptionSourse => "错误源：";
    public virtual string ExceptionType => "错误类型：";
    public virtual string ExceptionStackTrace => "错误堆栈：";
    public virtual string ExceptopnTargetSite => "错误方法：";
    public virtual string ExceprionCustomer => "用户自定义方法出错：";
    public virtual string SuccessText => "成功";
    public virtual string TwoParametersLengthIsNotSame => "两个参数的个数不一致";
    public virtual string NotSupportedDataType => "输入的类型不支持，请重新输入";
    public virtual string NotSupportedFunction => "当前的功能逻辑不支持";
    public virtual string DataLengthIsNotEnough => "接收的数据长度不足，应该值:{0},实际值:{1}";
    public virtual string ReceiveDataTimeout => "接收数据超时：";
    public virtual string ReceiveDataLengthTooShort => "接收的数据长度太短：";
    public virtual string MessageTip => "消息提示：";
    public virtual string Close => "关闭";
    public virtual string Time => "时间：";
    public virtual string SoftWare => "软件：";
    public virtual string BugSubmit => "Bug提交";
    public virtual string MailServerCenter => "邮件发送系统";
    public virtual string MailSendTail => "邮件服务系统自动发出，请勿回复！";
    public virtual string IpAddresError => "Ip地址输入异常，格式不正确";
    public virtual string Send => "发送";
    public virtual string Receive => "接收";
    #endregion 一般错误信息

    public virtual string CRCCheckFailed => "CRC校验检查失败：";
}
