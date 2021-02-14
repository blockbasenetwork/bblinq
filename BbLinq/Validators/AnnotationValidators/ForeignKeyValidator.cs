using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
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
        /// <param name="type">the model type</param>
        /// <param name="types">a list of types to compare</param>
        public static void Validate(Type type, List<Type> types)
        {
            var foreignKeyProperties = type.GetForeignKeyProperties();
            if(!foreignKeyProperties.Equals(default))
            {
                foreach (var foreignKeyProperty in foreignKeyProperties)
                {
                    var foreignKey = foreignKeyProperty.GetForeignKey();
                    ValidateForeignKeyState(type, foreignKeyProperty, foreignKey);
                    ValidateForeignKeyExistence(type, foreignKeyProperty, types);
                }
            }
        }

        /// <summary>
        /// Checks if a foreign key state is valid
        /// </summary>
        /// <param name="type">the model type</param>
        /// <param name="foreignKeyProperty">the property that has a foreign key</param>
        /// <param name="foreignKey">the attribute that has the foreign key data</param>
        private static void ValidateForeignKeyState(MemberInfo type, MemberInfo foreignKeyProperty, ForeignKeyAttribute foreignKey)
        {
            var regex = new Regex("([^(A-Za-z_0-9)])");

            if(foreignKey.Parent == null && string.IsNullOrEmpty(foreignKey.ParentName))
            {
                throw new InvalidParentTypeException(type.Name, foreignKeyProperty.Name);
            }

            if (regex.IsMatch(foreignKey.ParentName))
            {
                throw new InvalidParentTypeException(type.Name, foreignKey.ParentName);
            }
        }

        /// <summary>
        /// Checks if a foreign key state is valid
        /// </summary>
        /// <param name="type">the model type</param>
        /// <param name="property">the property that has a foreign key</param>
        /// <param name="types">a list of types that could have the parent class</param>
        private static void ValidateForeignKeyExistence(MemberInfo type, PropertyInfo property, IEnumerable<Type> types)
        {
            var foreignKey = property.GetForeignKey();
            foreach (var temporaryType in types)
            {
                if(temporaryType.Name == foreignKey.ParentName || temporaryType == foreignKey.Parent || temporaryType.GetTableName() == foreignKey.ParentName)
                {
                    return;
                }
            }
            if(foreignKey.Parent != null)
            {
                throw new InvalidParentTypeException(type.Name, foreignKey.Parent.Name);
            }
            else
            {
                throw new InvalidParentTypeException(type.Name, foreignKey.ParentName);
            }
        }


    }
}
