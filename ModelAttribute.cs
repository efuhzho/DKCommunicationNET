using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET;

/// <summary>
/// 协议工厂的特性标签；用户选择的设备型号关联协议类型
/// </summary>
[AttributeUsage ( AttributeTargets. All , Inherited = false , AllowMultiple = true )]
internal sealed class ModelAttribute : Attribute
{
    readonly Models model;

    // This is a positional argument
    public ModelAttribute ( Models model )
    {
        this. model = model;
    }

    public Models Model
    {
        get { return model; }
    }
}
