/// <summary>
/// author:hongyaohua
/// date:2015.1.4
/// </summary>
using System;
using System.Collections;
using System.ComponentModel;

public class EnumUtils
{
    public static T GetEnum<T>(string str)
    {
        return GetEnum<T>(str, StringComparison.Ordinal);
    }

    public static T GetEnum<T>(string str, StringComparison comparisonType)
    {
        System.Type enumType = typeof(T);

		foreach (T current in Enum.GetValues(enumType))
		{
			if (GetString<T>(current).Equals(str, comparisonType))
			{
				return current;
			}
		}

        throw new ArgumentException(string.Format("EnumUtils.GetEnum() - {0} has no matching value in enum {1}", str, enumType));
    }

    public static string GetString<T>(T enumVal)
    {
        string name = enumVal.ToString();
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) enumVal.GetType().GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (customAttributes.Length > 0)
        {
            return customAttributes[0].Description;
        }
        return name;
    }

    public static T Parse<T>(string str)
    {
        try
        {
            return (T) Enum.Parse(typeof(T), str);
        }
        catch (Exception)
        {
            return default(T);
        }
    }

	public static int GetLen<T>()
	{
		System.Type enumType = typeof(T);
		return Enum.GetNames(enumType).Length;
	}
}

