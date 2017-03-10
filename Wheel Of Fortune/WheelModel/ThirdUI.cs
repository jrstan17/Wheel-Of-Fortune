using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wheel_Of_Fortune {
    public class ThirdUI : Shape {

        //https://wpf.2000things.com/2014/09/05/1152-a-custom-pie-slice-shape/ Credit: Sean Sexton

        internal Third Third;

        public ThirdUI(Third baseThird) {
            Third = baseThird;
            Third.BackColorChanger.ColorChanged += BackColorChanger_ColorChanged;
        }

        private void BackColorChanger_ColorChanged(object sender, ColorArgs e) {
            SolidColorBrush brush = new SolidColorBrush(e.Color);
            this.Fill = brush;
        }

        // Angle that arc starts at
        public double StartAngle {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        // DependencyProperty - StartAngle
        private static PropertyMetadata startAngleMetadata =
                new PropertyMetadata(
                    0.0,     // Default value
                    null,    // Property changed callback
                    new CoerceValueCallback(CoerceAngle));   // Coerce value callback

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(ThirdUI), startAngleMetadata);

        // Angle that arc ends at
        public double EndAngle {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }

        // DependencyProperty - EndAngle
        private static System.Windows.PropertyMetadata endAngleMetadata =
                new PropertyMetadata(
                    90.0,     // Default value
                    null,    // Property changed callback
                    new CoerceValueCallback(CoerceAngle));   // Coerce value callback

        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register("EndAngle", typeof(double), typeof(ThirdUI), endAngleMetadata);

        private static object CoerceAngle(DependencyObject depObj, object baseVal) {
            double angle = (double)baseVal;
            angle = Math.Min(angle, 359.9);
            angle = Math.Max(angle, 0.0);
            return angle;
        }

        protected override Geometry DefiningGeometry {
            get {
                double maxWidth = Math.Max(0.0, RenderSize.Width - StrokeThickness);
                double maxHeight = Math.Max(0.0, RenderSize.Height - StrokeThickness);
                //Console.WriteLine(string.Format("* maxWidth={0}, maxHeight={1}", maxWidth, maxHeight));

                double xStart = maxWidth / 2.0 * Math.Cos(StartAngle * Math.PI / 180.0);
                double yStart = maxHeight / 2.0 * Math.Sin(StartAngle * Math.PI / 180.0);

                double xEnd = maxWidth / 2.0 * Math.Cos(EndAngle * Math.PI / 180.0);
                double yEnd = maxHeight / 2.0 * Math.Sin(EndAngle * Math.PI / 180.0);

                StreamGeometry geom = new StreamGeometry();
                using (StreamGeometryContext ctx = geom.Open()) {
                    ctx.BeginFigure(
                        new Point((RenderSize.Width / 2.0) + xStart,
                                  (RenderSize.Height / 2.0) - yStart),
                        true,   // Filled
                        true);  // Closed
                    ctx.ArcTo(
                        new Point((RenderSize.Width / 2.0) + xEnd,
                                  (RenderSize.Height / 2.0) - yEnd),
                        new Size(maxWidth / 2.0, maxHeight / 2),
                        0.0,     // rotationAngle
                        (EndAngle - StartAngle) > 180,   // greater than 180 deg?
                        SweepDirection.Counterclockwise,
                        true,    // isStroked
                        false);
                    ctx.LineTo(new Point((RenderSize.Width / 2.0), (RenderSize.Height / 2.0)), true, false);
                }

                return geom;
            }
        }
    }
}
