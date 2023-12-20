using System.Reflection;

internal static class GenerateEntityClassHelpers
{
    public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
    {
        try
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }
        catch (Exception)
        {

            return default(T);
        }

    }
    public static T TryGetMaxAttributeFromPropertyInfo<T>(this PropertyInfo property) where T : Attribute
    {
        try
        {
            var attrType = typeof(T);
            // var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }
        catch (Exception)
        {

            return null;
        }

    }

}