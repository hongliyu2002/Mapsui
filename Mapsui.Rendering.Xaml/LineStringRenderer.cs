﻿using System;
using System.Windows.Media;
using Mapsui.Geometries;
using Mapsui.Styles;

namespace Mapsui.Rendering.Xaml
{
    public static class LineStringRenderer
    {
        public static System.Windows.Shapes.Shape RenderLineString(LineString lineString, IStyle style, IViewport viewport)
        {
            if (!(style is VectorStyle)) throw new ArgumentException("Style is not of type VectorStyle");
            var vectorStyle = style as VectorStyle;

            System.Windows.Shapes.Path path = CreateLineStringPath(vectorStyle);
            path.Data = lineString.ToXaml();
            path.RenderTransform = new MatrixTransform { Matrix = GeometryRenderer.CreateTransformMatrix1(viewport) };
            GeometryRenderer.CounterScaleLineWidth(path, viewport.Resolution);
            return path;
        }

        public static System.Windows.Shapes.Path CreateLineStringPath(VectorStyle style)
        {
            var path = new System.Windows.Shapes.Path();
            if (style.Outline != null)
            {
                //todo: render an outline around the line. 
            }
            path.Stroke = new SolidColorBrush(style.Line.Color.ToXaml());
            path.StrokeDashArray = style.Line.PenStyle.ToXaml();
            var penStrokeCap = style.Line.PenStrokeCap.ToXaml();
            path.StrokeEndLineCap = penStrokeCap;
            path.StrokeStartLineCap = penStrokeCap;
            // To make similar to skia: path.StrokeLineJoin = PenLineJoin.Round;
            path.Tag = style.Line.Width; // see #linewidthhack
            path.IsHitTestVisible = false;
            return path;
        }
    }
}