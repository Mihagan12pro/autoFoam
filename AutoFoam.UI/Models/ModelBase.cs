using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            Type targetType = this.GetType();

            var props = targetType.GetProperties()
                .Select(p => p.Name);

            foreach (var sourcePropertyName in sourcePropertyNames)
            {
                PropertyInfo? sourceProperty = source.GetType().GetProperty(sourcePropertyName);

                string targetPropertyName = sourcePropertyName.Replace("Text", "");

                PropertyInfo? targetProperty = targetType.GetProperty(targetPropertyName);

                object? sourceValue = sourceProperty.GetValue(source);

                object converted;

                if ( (targetProperty.PropertyType == typeof(double)))
                {
                    converted = Convert.ToDouble(sourceValue);
                }
                else if (targetProperty.PropertyType == typeof(int))
                {
                    converted= Convert.ToInt32(sourceValue);
                }
                else
                {
                    converted = source;
                }

                targetProperty.SetValue(this, converted);
            }
        }
    }
}
