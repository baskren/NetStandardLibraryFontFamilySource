using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

#if __PCL__
namespace PclApp
#else
namespace NetStandardApp
#endif
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //InitializeComponent();
            var localLabel = new Label
            {
                Text = "This is the locaLabel"
            };
            CustomFontEffect.ApplyTo(localLabel);

            var supportLabel = new Label
            {
                Text = "This is the supportLabel"
            };
            SupportLibrary.CustomFontEffect.ApplyTo(localLabel);

            var stack = new StackLayout
            {
                Children =
                {
                    localLabel,
                    supportLabel
                }
            };
            Content = stack;
        }
    }
}
