using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class CloneUtils
{

    /// <summary>
    /// 深拷贝,要复制的实例必须可序列化，包括实例引用的其它实例都必须在类定义时加[Serializable]特性。
    /// </summary>
    public static T DeepClone<T>(T RealObject)
    {
        using (Stream objectStream = new MemoryStream())
        {
            //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制     
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, RealObject);
            objectStream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(objectStream);
        }
    }
}
