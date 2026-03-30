using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace RishUI
{
    /// <summary>
    /// Tells Rishenerator to use this default provider.
    /// The default value must be a static property of the RishValueType type you want to provide a default for. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DefaultAttribute : PreserveAttribute { }
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DefaultValueAttribute : PreserveAttribute
    {
        private object value;

        public DefaultValueAttribute(object value = null)
        {
            this.value = value;
        }
    }
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DefaultCodeAttribute : PreserveAttribute
    {
        private string code;
        public bool isStringLiteral;

        public DefaultCodeAttribute(string code, bool isStringLiteral = false)
        {
            this.code = code;
            this.isStringLiteral = isStringLiteral;
        }
    }
    
    public static class Defaults
    {
        private delegate T DefaultValueGetter<out T>() where T : struct;
        
        private static Dictionary<Type, MethodInfo> Methods { get; }
        private static Dictionary<Type, Delegate> Delegates { get; } = new();
        private static HashSet<Type> GenericTypes { get; }

        public static int Count => Methods.Count + GenericTypes.Count;

        static Defaults()
        {
            Methods = new Dictionary<Type, MethodInfo>(200);
            GenericTypes = new HashSet<Type>();
            foreach (var type in Rish.PlayerTypes)
            {
                if (!type.IsValueType)
                {
                    var baseType = type.BaseType;
                    if(baseType == null || !baseType.IsGenericType || type.BaseType?.GetGenericTypeDefinition() != typeof(RishElement<,>)) continue;
                }
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                foreach (var property in properties)
                {
                    if (!Attribute.IsDefined(property, typeof(DefaultAttribute))) continue;
                    
                    var propertyType = property.PropertyType;
                    
                    if (type.IsValueType)
                    {
                        if (propertyType != type) continue;
                    }
                    else
                    {
                        if(propertyType != type.BaseType.GenericTypeArguments[1]) continue;
                    }

                    if (propertyType.IsGenericType)
                    {
                        GenericTypes.Add(propertyType);
                    }
                    else
                    {
                        var getter = property.GetGetMethod(true);
                        if (Methods.ContainsKey(propertyType) && !type.IsValueType) continue;
                        Methods[propertyType] = getter;
                    }
                }
            }
        }

        public static T GetValue<T>() where T : struct
        {
            var type = typeof(T);
            if (type.IsGenericType)
            {
                if (!GenericTypes.Contains(type.GetGenericTypeDefinition()))
                {
                    return default;
                }

                var property = type.GetProperty("Default",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                return (T)property.GetValue(null);
            }

            if (Delegates.TryGetValue(type, out var getterDelegate))
            {
                var getter = (DefaultValueGetter<T>) getterDelegate;

                return getter?.Invoke() ?? default;
            }
            if (Methods.TryGetValue(type, out var getterMethod))
            {
                var getter = (DefaultValueGetter<T>) Delegate.CreateDelegate(typeof(DefaultValueGetter<T>), null, getterMethod);
                Delegates.Add(type, getter);
                
                return getter.Invoke();
            }

            return default;
        }
    }
}