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
    class CustomFontEffect : RoutingEffect
    {
        protected CustomFontEffect()
#if __PCL__
        : base("PclApp.CustomFontEffect")
#else
        :base("NetStandardApp.CustomFontEffect")
#endif
        {
        }

        static public void ApplyTo(Xamarin.Forms.Label label)
        {
            var effect = new CustomFontEffect();
            label.Effects.Add(effect);
        }

    }
}
