﻿using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.Validators.AnnotationValidators
{
    /// <summary>
    /// Performs a validation to a property that should have a range
    /// </summary>
    public static class RangeValidator
    {
        /// <summary>
        /// Checks if a range is valid
        /// </summary>
        /// <param name="type">the model type</param>
        /// <param name="property">the property that holds the range attribute</param>
        public static void Validate(Type type, PropertyInfo property)
        {
            var ranges = property.GetRanges();
            if (ranges == null || ranges.Length == 0)
            {
                return;
            }
            ValidateBucket(type, property, ranges[0]);
            ValidateRange(type, property, ranges[0]);
        }

        /// <summary>
        /// Checks if the number of buckets is valid
        /// </summary>
        /// <param name="type">the model type</param>
        /// <param name="property">the property that holds the column attribute</param>
        /// <param name="range">the range attribute</param>
        public static void ValidateBucket(Type type, PropertyInfo property, RangeAttribute range)
        {
            if (range.Buckets < 1)
            {
                throw new InvalidBucketException(type.Name, property.Name, range.Buckets);
            }
        }

        /// <summary>
        /// Checks if the range has valid maximum and minimum values
        /// </summary>
        /// <param name="type">the model type</param>
        /// <param name="property">the property that holds the range attribute</param>
        /// <param name="range">the range attribute</param>
        public static void ValidateRange(Type type, PropertyInfo property, RangeAttribute range)
        {
            if (range.Minimum > range.Maximum)
            {
                throw new InvalidRangeException(type.Name, property.Name, range.Minimum, range.Maximum);
            }
        }
    }
}
