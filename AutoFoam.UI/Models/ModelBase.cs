using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AutoFoam.UI.Models
{
    public abstract class ModelBase : IValidatableObject
    {
        protected readonly List<string> errors = new List<string>();

        public IReadOnlyCollection<string> Errors
            => errors.AsReadOnly();

        public bool HasErrors 
            => errors.Count > 0;

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        public void SetValues(object source, params string[] sourcePropertyNames)
        {
            ModelBase target = this;
            Type targetType = target.GetType();

            foreach (var sourcePropertyName in sourcePropertyNames)
            {
                PropertyInfo? sourceProperty = source.GetType().GetProperty(sourcePropertyName);

                string targetPropertyName = sourcePropertyName.Replace("Text", "");

                PropertyInfo? targetProperty = targetType.GetType().GetProperty(targetPropertyName);

                object? sourceValue = sourceProperty.GetValue(source);
                targetProperty.SetValue(target, sourceValue);
            }
        }
    }
}
