using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 协议解码器
/// </summary>
internal interface IDecoder
{
    /// <summary>
    /// 数据区起始索引值
    /// </summary>
    int Offset { get; }

    /// <summary>
    /// 设备型号
    /// </summary>
    public string? Model { get; }

    /// <summary>
    /// 固件版本号
    /// </summary>
    public string? Firmware { get; }

    /// <summary>
    /// 设备出厂编号
    /// </summary>
    public string? SN { get;  }

    /// <summary>
    /// 指示是否激活交流源功能
    /// </summary>
    public bool IsEnabled_ACS { get;  } 

    /// <summary>
    /// 指示是否激活交流表功能
    /// </summary>
    public bool IsEnabled_ACM { get;  }

    /// <summary>
    /// 指示是否激活直流源功能
    /// </summary>
    public bool IsEnabled_DCS { get;  }

    /// <summary>
    /// 指示是否激活开关量功能
    /// </summary>
    public bool IsEnabled_IO { get;  }

    /// <summary>
    /// 指示是否激活电能功能
    /// </summary>
    public bool IsEnabled_EPQ { get;  }

    /// <summary>
    /// 指示是否激活直流表功能
    /// </summary>
    public bool IsEnabled_DCM { get;  }

    /// <summary>
    /// 解析联机指令的回复报文
    /// </summary>
    /// <param name="result">联机指令操作结果</param>
    void DecodeHandShake ( OperateResult<byte[ ]> result );
}
