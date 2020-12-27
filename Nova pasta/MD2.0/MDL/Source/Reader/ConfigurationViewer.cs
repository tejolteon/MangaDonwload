using MDR.Source.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MDR.Source.Reader
{
    public static class ConfigurationViewer
    {
        static Reader reader;
        public static Point GridPosition { get; set; }
        //bool dual = false;
        //bool isCtrlPressed = false;

        public static BitmapImage CurrentImage { private set; get; }

        private static void SetImage(BitmapImage i) =>
            CurrentImage = i;

        public static void Begin()
        {
            LoadFolder();
            Next();

            //if (dual)
            //{
            //    dual = false;
            //    //ConfigDualPage();
            //}
            //else
            //{
            //    dual = true;
            //    //ConfigSinglePage();
            //}
        }

        public static TransformGroup DefineTransform()
        {
            TransformGroup result = new TransformGroup();
            ScaleTransform st = new ScaleTransform();
            result.Children.Add(st);
            TranslateTransform tt = new TranslateTransform();
            result.Children.Add(tt);

            return result;
        }

        static Vector GetImageSize(Grid grid)
        {
            if (grid is null)
                return new Vector();

            double w = 0;
            double h = 0;

            for (int i = 0; i < grid.Children.Count; i++)
            {
                var img = grid.Children[i] as Image;

                if (!(img.Source is null))
                {
                    if (i == 0)
                    {
                        w = img.ActualWidth;
                        h = img.ActualHeight;
                    }
                    else
                    {
                        w += img.ActualWidth;
                    }

                    //if (h < aux * ((zoomValue - 1) / zoomValue))
                    //    h = aux * ((zoomValue - 1) / zoomValue);
                }
            }

            return new Vector(w, h);
        }

        internal static void GridSize(Grid grid)
        {
            Zoom.Move(grid, GridPosition);
        }

        static Vector GetGridSize(Grid grid)
        {
            return new Vector(grid.ActualWidth, grid.ActualHeight);
        }

        private static void LoadFolder()
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png" };

            if (dialog.ShowDialog() == true)
            {
                reader = new Reader();

                reader.Loader(dialog.FileName);

            }
        }

        private static void Next()
        {
            reader.NextImage();
            reader.NextImage();

            if (!(reader.Image is null))
            {
                SetImage(reader.Image);
                //Reset();
            }
            else
                return;

            //if (dual)
            //{
            //    reader.NextImage();
            //    imageLeft.Source = reader.Image;
            //}
        }

        public static void SetZoom(Grid grid, double value)
        {
            if (grid is null)
                return;

           // Zoom.DefImageSize(GetImageSize(grid));
           // Zoom.ZoomChange(grid, value);
        }



        public static void MoveDown(Grid grid)
        {
            if (grid is null)
                return;

            //Zoom.DefGridSize(GetGridSize(grid));
           // Zoom.ClickMoveDown(grid, GridPosition);
            grid.CaptureMouse();
        }

        public static void MoveUp(Grid grid)
        {
            if (grid is null)
                return;

            grid.ReleaseMouseCapture();
        }

        public static void MoveImage(Grid grid)
        {
            if (grid is null)
                return;

            if (grid.IsMouseCaptured)
                Zoom.Move(grid, GridPosition);
        }

        //private void SldZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    var imgp = GetImageSize(imageRight);
        //    imgp.X += GetImageSize(imageLeft).X;

        //    Zoom.DefImageSize(imgp);

        //    Zoom.SliderZoom(gridAllImage, e);
        //}

        //public void Previous()
        //{
        //    if (dual)
        //    {
        //        reader.PreviousImage();
        //        reader.PreviousImage();
        //    }

        //    reader.PreviousImage();
        //    if (!(reader.Image is null))
        //    {
        //        imageRight.Source = reader.Image;
        //        Reset();
        //    }

        //    if (dual)
        //    {
        //        reader.NextImage();
        //        imageLeft.Source = reader.Image;
        //    }
        //}

        //public void ConfigSinglePage()
        //{
        //    if (dual)
        //    {
        //        dual = false;
        //        gridAllImage.ColumnDefinitions.Clear();

        //        gridImageRight.HorizontalAlignment = HorizontalAlignment.Center;
        //        imageRight.HorizontalAlignment = HorizontalAlignment.Center;
        //        imageLeft.Source = null;

        //        Previous();
        //    }
        //}

        //public void ConfigDualPage()
        //{
        //    if (!dual)
        //    {
        //        gridAllImage.ColumnDefinitions.Clear();

        //        var col = new ColumnDefinition
        //        {
        //            Width = new GridLength(1, GridUnitType.Star)
        //        };
        //        var col2 = new ColumnDefinition
        //        {
        //            Width = new GridLength(1, GridUnitType.Star)
        //        };

        //        gridAllImage.ColumnDefinitions.Add(col);
        //        gridAllImage.ColumnDefinitions.Add(col2);

        //        gridImageLeft.HorizontalAlignment = HorizontalAlignment.Right;
        //        imageRight.HorizontalAlignment = HorizontalAlignment.Right;
        //        gridImageRight.HorizontalAlignment = HorizontalAlignment.Left;
        //        imageLeft.HorizontalAlignment = HorizontalAlignment.Left;

        //        if (!(reader.CurrentPage % 2 == 0))
        //            reader.SetPosition(-2);
        //        else if (reader.CurrentPage >= 0)
        //            reader.SetPosition(-1);

        //        dual = true;
        //        Next();
        //    }
        //}

        //private void Win_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Right)
        //    {
        //        if (!(imageRight.Source is null))
        //            Next();
        //    }
        //    else if (e.Key == Key.Left)
        //    {
        //        if (!(imageRight.Source is null))
        //            Previous();
        //    }

        //    if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
        //        isCtrlPressed = true;
        //}

        //private void Win_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
        //        isCtrlPressed = false;
        //}

        //private void MitSingle_Click(object sender, RoutedEventArgs e)
        //{
        //    ConfigSinglePage();
        //}

        //private void MitDual_Click(object sender, RoutedEventArgs e)
        //{
        //    ConfigDualPage();
        //}

        //private void MitOpenFolder_Click(object sender, RoutedEventArgs e)
        //{
        //    Init();
        //}





        //private void BtnReset_Click(object sender, RoutedEventArgs e)
        //{
        //    Reset();
        //}

        //void Reset()
        //{
        //    sldZoom.Value = 1;
        //    Zoom.Reset(gridAllImage);
        //}
    }
}
