using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SuperbarButton
{
    public static class ControlExtender
    {
        public static bool GetUseColorOffset(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseColorOffsetProperty);
        }

        public static void SetUseColorOffset(DependencyObject obj, bool value)
        {
            obj.SetValue(UseColorOffsetProperty, value);
        }

        // Using a DependencyProperty as the backing store for UseColorOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseColorOffsetProperty = DependencyProperty.RegisterAttached("UseColorOffset", typeof(bool), typeof(ControlExtender), new UIPropertyMetadata(false, OnUseColorOffsetPropertyChanged));

        private static void OnUseColorOffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool newValue = (bool)e.NewValue;
            FrameworkElement element = d as FrameworkElement;
            if (element == null) return;

            if (newValue)
            {
                // Подписываемся на события мыши
                element.MouseMove += OnMousePositionChanged;
                element.MouseLeave += OnMousePositionChanged;
            }
            else
            {
                // Отписываемся от событий мыши
                element.MouseMove -= OnMousePositionChanged;
                element.MouseLeave -= OnMousePositionChanged;
            }
        }

        private static void OnMousePositionChanged(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null && element.ActualWidth > 0)
            {
                Point mousePos = Mouse.GetPosition(element as IInputElement);
                if (mousePos.X < 0) mousePos.X = 0;
                else if (mousePos.X > element.ActualWidth) mousePos.X = element.ActualWidth;
                // В зависимости от положения мыши выставляем X координату точкм в диапазоне от 0 до 1
                SetColorOffset(sender as DependencyObject, new Point(mousePos.X/element.ActualWidth, 1));
            }
        }

        public static Point GetColorOffset(DependencyObject obj)
        {
            return (Point)obj.GetValue(ColorOffsetProperty);
        }

        public static void SetColorOffset(DependencyObject obj, Point value)
        {
            obj.SetValue(ColorOffsetProperty, value);
        }

        // Using a DependencyProperty as the backing store for ColorOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorOffsetProperty =
            DependencyProperty.RegisterAttached("ColorOffset", typeof(Point), typeof(ControlExtender), new UIPropertyMetadata(new Point(0.5, 1)));        
    }
}
