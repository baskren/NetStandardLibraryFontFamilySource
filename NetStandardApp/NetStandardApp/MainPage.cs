using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

#if NETSTANDARD
namespace NetStandardApp
#else
namespace PclApp
#endif
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            var localLabel = new Label
            {
                Text = "This is the locaLabel"
            };
            CustomFontEffect.ApplyTo(localLabel);
            

            var supportLabel = new Label
            {
                Text = "This is the supportLabel"
            };
            SupportLibrary.CustomFontEffect.ApplyTo(supportLabel);

            var entry = new Entry
            {
                Text = "This is the entry"
            };
            SupportLibrary.CustomFontEffect.ApplyTo(entry);

            var button = new Button
            {
                Text = "This is a button"
            };
            SupportLibrary.CustomFontEffect.ApplyTo(button);

            var stack = new StackLayout
            {
                Children =
                {
                    localLabel,
                    supportLabel,
                    entry,
                    button
                }
            };
            Content = stack;
        }
    }
}
