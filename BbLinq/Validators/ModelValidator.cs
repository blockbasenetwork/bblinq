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
        /// <param name="model">the model's entities</param>
        public static void Validate(Type[] model)
        {
            ValidateDuplicates(model);
            for(var counter = 0; counter < model.Length; counter++)
            {
                EntityValidator.Validate(model[counter]);
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
                for (var counter = 0; counter < model.Length; counter++)
                {
                    tableNames.Add((model[counter].Name, model[counter].GetTableName()));
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
