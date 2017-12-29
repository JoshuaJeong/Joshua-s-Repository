using System;
using System.Reflection;
using xave.com.generator.cus.StructureSetModel;

namespace xave.web.generator.helper.Logic
{
    public static class ReflectionExtensions
    {
        public static string GetValue<T>(this T obj, string param)
        {
            if (string.IsNullOrEmpty(param)) return null;
            PropertyInfo p = typeof(T).GetProperty(param);
            object pValue = p == null ? null : p.GetValue(obj, null);
            return pValue == null ? null : (pValue is string ? (string)pValue : pValue.ToString());
        }

        //public static string GetValue<T>(this T obj, PropertyInfo prop)
        //{
        //    var x = prop.GetValue(obj, null);
        //    return x != null ? x.ToString() : string.Empty;
        //}

        public static To GetValue<Ti, To>(this Ti data, PropertyInfo prop)
        {
            return (To)prop.GetValue(data, null);
        }

        public static string GetVariable<T>(this T obj, BodyStructure b)
        {
            return b != null ?
                        (b.ValueType == "STATIC" ? b.Value :
                                (b.ValueType == "OBJECT" ? obj.GetValue(b.Property) : b.Code)) :
                string.Empty;
        }

        //public static void SetValue<T>(this T obj, string prop, object value)
        //{
        //    if (value == null) return;
        //    PropertyInfo propertyInfo = typeof(T).GetProperty(prop);
        //    if (propertyInfo != null)
        //        propertyInfo.SetValue(obj, value, null);
        //}

        public static void SetValue<T>(this T obj, string prop, object value, Type type)
        {
            if (value == null) return;
            PropertyInfo propertyInfo = type.GetProperty(prop);
            if (propertyInfo != null)
                propertyInfo.SetValue(obj, value, null);
        }
    }
}
