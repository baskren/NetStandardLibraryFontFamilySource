using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SupportLibrary
{
    public class CustomFontEffect : RoutingEffect
    {
        protected CustomFontEffect() : base("SupportLibrary.CustomFontEffect")
        {
        }

        static public void ApplyTo(Xamarin.Forms.Label label)
        {
            var effect = new CustomFontEffect();
            label.Effects.Add(effect);
        }

    }
}
