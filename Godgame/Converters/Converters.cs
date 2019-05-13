﻿using Godgame.Model.API;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Godgame.Converters
{
    public class IDrawableToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return MainPage.bitmapImages["void.png"];
            else
                return MainPage.bitmapImages[(value as IDrawable).Path];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }

    public class ItemToStackPanelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var image = new Image
            {
                Source = MainPage.bitmapImages[(value as Item).Path],
                Height = 50
            };
            var text = new TextBlock()
            {
                Text = (value as Item).Name
            };

            var panel = new StackPanel()
            {
                Children = {
                    image, text
                }
            };
            //panel.Children.Add(image);
            //panel.Children.Add(text);

            return panel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();

    }

    public class ItemAmountToStackPanelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var image = new Image
            {
                Source = MainPage.bitmapImages[(value as ItemAmount).Item.Path],
                Width = 50
            };
            var text = new TextBlock()
            {
                Text = value.GetType().ToString()
            };

            var panel = new StackPanel()
            {
                Children = {
                    image, text
                }
            };
            //panel.Children.Add(image);
            //panel.Children.Add(text);

            return panel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();

    }
}

