using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Validators.AnnotationValidators
{
    /// <summary>
    /// Performs a validation to a property that should be a foreign key
    /// </summary>
    public static class ForeignKeyValidator
    {
        /// <summary>
        /// Checks if a foreign key is valid
        /// </summary>
        public static void Validate(Type type, List<Type> types)
        {
            var foreignKeyProperties = type.GetForeignKeyProperties();
            if (foreignKeyProperties == null || foreignKeyProperties.Length == 0)
            {
                return;
            }
            foreach (var foreignKeyProperty in foreignKeyProperties)
            {
                var foreignKey = foreignKeyProperty.GetForeignKeys()[0];
                ValidateForeignKeyState(type, foreignKeyProperty, foreignKey);
                ValidateForeignKeyExistence(type, foreignKeyProperty, foreignKey, types);
                ValidateForeignKeyType(type, foreignKeyProperty, foreignKey, types);
            }
        }

        /// <summary>
        /// Checks if a foreign key state is valid
        /// </summary>
        private static void ValidateForeignKeyState(MemberInfo type, MemberInfo foreignKeyProperty, ForeignKeyAttribute foreignKey)
        {

            if(foreignKey.Parent == null)
            {
                throw new NoParentSetException(type.Name, foreignKeyProperty.Name);
            }
        }

        /// <summary>
        /// Checks if the parent key exists
        /// </summary>
        private static void ValidateForeignKeyExistence(MemberInfo type, PropertyInfo property, ForeignKeyAttribute foreignKey, IEnumerable<Type> types)
        {
            foreach (var parent in types)
            {
                if (parent == foreignKey.Parent)
                {
                    return;
                }
            }
            throw new InvalidParentTypeException(type.Name, property.Name, foreignKey.Parent.Name);
        }

        /// <summary>
        /// Checks if the parent primary key has the same type as the foreign key
        /// </summary>
        private static void ValidateForeignKeyType(MemberInfo type, PropertyInfo property, ForeignKeyAttribute foreignKey, IEnumerable<Type> types)
        {
            foreach (var parent in types)
            {
                if (parent == foreignKey.Parent)
                {
                    var primaryKey = parent.GetPrimaryKeyProperties();
                    if (primaryKey != null && primaryKey.Length > 0)
                    {
                        if (property.PropertyType == primaryKey[0])
                        {
                            return;
                        }
                    }
                }
            }
            throw new InvalidForeignKeyTypeException(type.Name, property.Name, foreignKey.Parent.Name, property.PropertyType.Name);
        }
    }
}
