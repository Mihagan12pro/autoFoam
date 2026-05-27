using System;
using System.ComponentModel;

namespace AutoFoam.UI.Models.FlatChannel
{
    public class FlatChannelMesh : MeshBase 
    {
        [DisplayName("cell_x1")]
        public double CellX1 { get; }

        [DisplayName("cell_x2")]
        public double CellX2 { get; }

        [DisplayName("cell_y1")]
        public double CellY1 { get; }

        [DisplayName("cell_y2")]
        public double CellY2 { get; }

        [DisplayName("cell_y3")]
        public double CellY3 { get; }

        [DisplayName("cell_y4")]
        public double CellY4 { get; }


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

        [DisplayName("left_triangle_point")]      
        public double TriangleLeftPoint { get; }

        [DisplayName("right_triangle_point")]     
        public double TriangleRightPoint { get; }

        [DisplayName("half_of_channel_width")]
        public double HalfOfChannelWidth { get; }

        [DisplayName("construction_width")]
        public double ConstructionWidth { get; }



        [DisplayName("U")]
        public double InletSpeed { get; }



        private double _baseCellSize = 1;

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

            CellX1 = Math.Round(InletWidth / _baseCellSize / 2);

            CellX2 = Math.Round(
                (ConstructionWidth - InletWidth) / _baseCellSize);

            CellY1 = Math.Round(
                TriangleHeight / _baseCellSize);

            CellY2 = Math.Round(
                (ChannelBottomPoint - TriangleHeight) / _baseCellSize);

            CellY3 = Math.Round(
                (ChannelTopPoint - ChannelBottomPoint) / _baseCellSize);

            CellY4 = Math.Round(
                (Height - ChannelTopPoint) / _baseCellSize);
        }
    }
}
