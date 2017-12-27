using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SupportLibrary
{
    public class CustomFontEffect : RoutingEffect
    {
        protected CustomFontEffect() : base("Lib.CustomFontEffect")
        {
        }

        static public void ApplyTo(Xamarin.Forms.VisualElement label)
        {
            var effect = new CustomFontEffect();
            label.Effects.Add(effect);
        }

    }
}
