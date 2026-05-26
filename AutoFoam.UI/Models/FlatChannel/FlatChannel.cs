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
            throw new System.NotImplementedException();
        }
    }
}
