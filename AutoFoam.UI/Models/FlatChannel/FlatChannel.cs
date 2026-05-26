using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoFoam.UI.Models.FlatChannel
{
    public class FlatChannel : ModelBase
    {
        public double InletSpeed { get; set; }
        
        public double InletWidth { get; set; }
        
        public double ChannelHeight { get; set; }
        
        public double LegHeight { get; set; }
        
        public double TriangleHeight { get; set; }
        
        public double TriangleBase { get; set; }
        
        public double OutletWidth { get; set; }

        public double OutletLength { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (LegHeight <= TriangleHeight)
                errors.Add(new ValidationResult("Высота треугольника должна быть меньше длины ножки!"));

            if (ChannelHeight <= LegHeight + OutletWidth)
                errors.Add(new ValidationResult("Высота канала должна быть больше суммы длины ножки и ширины выхода!"));

            if (TriangleBase >= InletWidth)
                errors.Add(new ValidationResult("Основание треугольника должна быть меньше толщины входа!"));

            return errors;
        }
    }
}
