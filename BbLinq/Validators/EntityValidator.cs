using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.Validators.AnnotationValidators;

namespace BlockBase.BBLinq.Validators
{
    /// <summary>
    /// Performs validation actions on an entity
    /// </summary>
    public static class EntityValidator
    {
        /// <summary>
        /// Validates the entity
        /// </summary>
        /// <param name="entity">an entity</param>
        public static void Validate(Type entity)
        {
            TableValidator.Validate(entity);

            var properties = entity.GetProperties();

            ValidateDuplicateColumns(entity, properties);
            foreach (var property in properties)
            {
                PropertyValidator.Validate(entity, property);
            }
        }


        /// <summary>
        /// Checks if there's a repeated entity
        /// </summary>
        public static void ValidateDuplicateColumns(Type type, PropertyInfo[] model)
        {
            (string, string)[] columnNames;

            {
                var tableNames = new List<(string, string)>();
                foreach (var property in model)
                {
                    tableNames.Add((property.Name, property.GetColumnName()));
                }
                columnNames = tableNames.ToArray();
            }

            var duplicates = columnNames.FindDuplicates((s1, s2) => s1.Item2 == s2.Item2);
            if (!duplicates.IsNullOrEmpty())
            {
                throw new DuplicatedColumnsOnTableException(type.Name, duplicates);
            }
        }
    }
}
