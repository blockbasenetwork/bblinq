using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BlockBase.BBLinq.Validators
{
    public static class ModelElementValidator
    {
        public static void Validate(object obj)
        {
            TableValidator.Validate(obj);
            PrimaryKeyValidator.Validate(obj);
            var type = obj.GetType();
            var properties = type.GetProperties();

            var listForDuplicates = new List<(PropertyInfo, string)>();

            foreach(var property in properties)
            {
                listForDuplicates.Add((property, property.GetColumnName()));
                ColumnValidator.Validate(type, property);
                RangeValidator.Validate(type, property);
                EncryptedValidator.Validate(type, property);
            }

            ValidateDuplicateProperties(type, listForDuplicates);
        }

        private static void ValidateDuplicateProperties(Type type, List<(PropertyInfo, string)> nameList)
        {
            var duplicates = new Dictionary<PropertyInfo, string>();
            for (var nameCounter = 0; nameCounter < nameList.Count; nameCounter++)
            {
                for (var comparisonNameCounter = nameCounter + 1; comparisonNameCounter < nameList.Count; comparisonNameCounter++)
                {
                    if (nameList[nameCounter].Item2 == nameList[comparisonNameCounter].Item2)
                    {
                        duplicates.Add(nameList[nameCounter].Item1, nameList[nameCounter].Item2);
                    }
                }
            }
            if (duplicates.Count > 0)
            {
                throw new DuplicatedColumnsOnTableException(type, duplicates);
            }
        }
    }
}
