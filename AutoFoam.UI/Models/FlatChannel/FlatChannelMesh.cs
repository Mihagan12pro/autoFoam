using Microsoft.VisualBasic;
using System.ComponentModel;

namespace AutoFoam.UI.Models.FlatChannel
{
    public class FlatChannelMesh : MeshBase 
    {
        [DisplayName("height")]
        public double Height { get; }

        [DisplayName("channel_top_point")]
        public double ChannelTopPoint { get; }

        [DisplayName("channel_bottom_point")]
        public double ChannelBottomPoint { get; }

        [DisplayName("triangle_height")]
        public double TriangleHeight { get; }


        [DisplayName("inlet_width")]
        public double InletWidth { get; }

        [DisplayName("triangle_left_point")]
        public double TriangleLeftPoint { get; }

        [DisplayName("triangle_right_point")]
        public double TriangleRightPoint { get; }

        [DisplayName("half_of_channel_width")]
        public double HalfOfChannelWidth { get; }

        [DisplayName("construction_width")]
        public double ConstructionWidth { get; }


        [DisplayName("U")]
        public double InletSpeed { get; }

        public FlatChannelMesh(FlatChannel channel, double inletSpeed)
        {
            InletSpeed = -inletSpeed;

            Height = channel.ChannelHeight;
            TriangleHeight = channel.TriangleHeight;
            ChannelBottomPoint = channel.LegHeight;
            ChannelTopPoint = channel.LegHeight + channel.OutletWidth;

            InletWidth = channel.InletWidth;
            HalfOfChannelWidth = InletWidth / 2;
            TriangleLeftPoint = HalfOfChannelWidth - channel.TriangleBase / 2;
            TriangleRightPoint = HalfOfChannelWidth + channel.TriangleBase / 2;
            ConstructionWidth = channel.InletWidth + channel.OutletLength;
        }
    }
}
