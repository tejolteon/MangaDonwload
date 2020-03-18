using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MDR.Source.Utils
{
    public static class Zoom
    {
        private static Point startPosition;
        private static Point originPosition;
        private static Vector imageSize;

        private static TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private static ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        private static Point GetPosition(UIElement element)
        {
            var fe = ToFElement(element);
            return new Point(fe.ActualWidth / 2, fe.ActualHeight / 2);
        }

        private static FrameworkElement ToFElement(UIElement element) =>
            element as FrameworkElement;

        public static double MouseZoom(UIElement element, MouseWheelEventArgs e)
        {
            if (element is null || e is null)
                return 0;

            var st = GetScaleTransform(element);

            double zoom = (e.Delta > 0 ? .2 : -.2) + st.ScaleX;

            if (zoom > 5)
                return 5;
            else if (zoom < 1)
                return 1;
            else
                return zoom;
        }

        public static void SliderZoom(UIElement element, RoutedPropertyChangedEventArgs<double> e)
        {
            if (element is null || e is null)
                return;

            ZoomChange(element, e.NewValue);
        }

        private static void ZoomChange(UIElement element, double zoom)
        {
            var st = GetScaleTransform(element);
            var tt = GetTranslateTransform(element);


            double abosuluteX;
            double abosuluteY;

            var relative = GetPosition(element);

            abosuluteX = relative.X * st.ScaleX + tt.X;
            abosuluteY = relative.Y * st.ScaleY + tt.Y;

            st.ScaleX = zoom;
            st.ScaleY = zoom;

            tt.X = abosuluteX - relative.X * st.ScaleX;
            tt.Y = abosuluteY - relative.Y * st.ScaleY;
        }

        public static void Reset(UIElement element)
        {
            if (element is null)
                return;

            // reset pan
            var tt = GetTranslateTransform(element);
            tt.X = 0.0;
            tt.Y = 0.0;
        }

        public static void Move(UIElement element, Point parent)
        {
            if (element is null)
                return;

            var tt = GetTranslateTransform(element);
            var fe = ToFElement(element);

            if (fe is null)
                return;

            Vector v = startPosition - parent;
            var newX = originPosition.X - v.X;
            var newY = originPosition.Y - v.Y;

            var imageX = Math.Round(imageSize.X, 2);
            var imageY = Math.Round(imageSize.Y, 2);

            //if (newX > imageSize.X * -1)
            //    newX = imageSize.X * -1;
            //else if (newX > imageSize.X / 2 * -1)
            //    newX = imageSize.X / 2 * -1;

            if (newY > 0)
                newY = 0;
            else if (newY < imageY * -1)
                newY = imageY * -1;

            if (fe.ActualWidth < imageX)
                tt.X = newX;

            tt.Y = newY;

        }

        public static void ClickMoveDown(UIElement element, Point parent, Vector imgSize)
        {
            if (element is null)
                return;

            imageSize = imgSize;
            var tt = GetTranslateTransform(element);
            startPosition = parent;
            originPosition = new Point(tt.X, tt.Y);
        }
    }
}
