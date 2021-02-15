using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace BlockBase.BBLinq.Validators
{
    /// <summary>
    /// Performs validation actions on the model
    /// </summary>
    public static class ModelValidator
    {
        /// <summary>
        /// validates the model
        /// </summary>
        public static void Validate(Type[] model)
        {
            ValidateDuplicates(model);
            foreach (var entity in model)
            {
                EntityValidator.Validate(entity);
            }
        }

        /// <summary>
        /// Checks if there's a repeated entity
        /// </summary>
        public static void ValidateDuplicates(Type[] model)
        {
            (string, string)[] entityNames;

            {
                var tableNames = new List<(string,string)>();
                foreach (var entity in model)
                {
                    tableNames.Add((entity.Name, entity.GetTableName()));
                }
                entityNames = tableNames.ToArray();
            }
            
            var duplicates = entityNames.FindDuplicates((s1, s2)=>s1.Item2 == s2.Item2);
            if (!duplicates.IsNullOrEmpty())
            {
                throw new DuplicatedTablesOnModelException(entityNames);
            }
        }
    }
}
