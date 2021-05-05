using System;
using System.Reflection;

namespace BlockBase.BBLinq.Exceptions
{
    public class NoDependencyFoundInEntitiesException : Exception
    {
        public static string GenerateErrorMessage(PropertyInfo[] properties, Type[] entities)
        {
            var propertyNames = string.Empty;
            foreach (var property in properties)
            {
                propertyNames += property.Name;
                if (property == properties[^1]) continue;
                if (property == properties[^2])
                {
                    propertyNames += " and ";
                }
                else
                {
                    propertyNames += ", ";
                }
            }
            var message = $"No dependency on {propertyNames} for {properties[0].ReflectedType?.Name} was found";
            foreach (var entityType in entities)
            {
                message += $"{entityType.Name}\n";
            }
            return message;
        }

        public NoDependencyFoundInEntitiesException(PropertyInfo[] properties, Type[] entities):base(GenerateErrorMessage(properties, entities))
        {

        }
    }
}
