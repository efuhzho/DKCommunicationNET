using System. Reflection;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 反射工厂类
/// </summary>
internal class DictionaryOfFactorys
{
    /// <summary>
    /// 创建字典：key：型号，value：协议工厂
    /// </summary>
    readonly Dictionary<Models , IProtocolFactory> keyValuePairs = new ( );

    /// <summary>
    /// 初始化字典的反射方法
    /// </summary>
    public DictionaryOfFactorys ( )
    {  
        //获取当前运行实例的程序集
        Assembly assembly = Assembly. GetExecutingAssembly ( );

        //将指定特性标志的工厂类添加进字典
        foreach ( var item in assembly. GetTypes ( ) )
        {
            if ( typeof ( IProtocolFactory ). IsAssignableFrom ( item ) && !item. IsInterface )
            {
                ModelAttribute? ModelsAttribute = item. GetCustomAttribute<ModelAttribute> ( );
                if ( ModelsAttribute != null )
                {
                    keyValuePairs[ModelsAttribute. Model] = Activator. CreateInstance ( item ) as IProtocolFactory;
                }
            }
        }
    }

    /// <summary>
    /// 根据型号返回协议工厂
    /// </summary>
    /// <param name="model">设备型号</param>
    /// <returns>协议工厂</returns>
    internal IProtocolFactory GetFactory ( Models model )
    {
        return keyValuePairs[model];
    }
}
