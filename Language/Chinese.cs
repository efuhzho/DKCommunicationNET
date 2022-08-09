using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Language;

/// <summary>
/// 类库的语言基类，默认中文
/// </summary>
internal class Chinese
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

    #region CRC校验信息
    public virtual string CRCCheckFailed => "CRC校验检查失败：";
    #endregion CRC校验信息

    #region 日志相关信息
    public virtual string LogNetDebug => "调试";
    public virtual string LogNetInfo => "信息";
    public virtual string LogNetWarn => "警告";
    public virtual string LogNetError => "错误";
    public virtual string LogNetFatal => "致命";
    public virtual string LogNetAbandon => "放弃";
    public virtual string LogNetAll => "全部";
    #endregion 日志相关信息

    #region Client相关
    public virtual string ReConnectServerSuccess => "重连服务器成功";
    public virtual string ReConnectServerAfterTenSeconds => "在10秒后重新连接服务器";
    public virtual string KeyIsNotAllowedNull => "关键字不允许为空";
    public virtual string KeyIsExistAlready => "当前的关键字已经存在";
    public virtual string KeyIsNotExist => "当前订阅的关键字不存在";
    public virtual string ConnectingServer => "正在连接服务器...";
    public virtual string ConnectFailedAndWait => "连接断开，等待{0}秒后重新连接";
    public virtual string AttemptConnectServer => "正在尝试第{0}次连接服务器";
    public virtual string ConnectServerSuccess => "连接服务器成功";
    public virtual string GetClientIpaddressFailed => "客户端IP地址获取失败";
    public virtual string ConnectionIsNotAvailable => "当前的连接不可用";
    public virtual string DeviceCurrentIsLoginRepeat => "当前设备的id重复登录";
    public virtual string DeviceCurrentIsLoginForbidden => "当前设备的id禁止登录";
    public virtual string PasswordCheckFailed => "密码验证失败";
    public virtual string DataTransformError => "数据转换失败，源数据：";
    public virtual string RemoteClosedConnection => "远程关闭了连接";
    #endregion Client相关
}
