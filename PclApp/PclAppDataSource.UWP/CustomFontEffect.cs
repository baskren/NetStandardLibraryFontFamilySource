using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using PclAppDataSource.UWP;
using System.IO;
using System.Reflection;

[assembly: ResolutionGroupName("PclAppDataSource")]
[assembly: ExportEffect(typeof(CustomFontEffect), "CustomFontEffect")]
namespace PclAppDataSource.UWP
{
    class CustomFontEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var appDataPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            var fontPath = Path.Combine(appDataPath, "Pacifico.ttf");
            if (!File.Exists(fontPath))
            {
                using (var stream = Xamarin.Forms.Application.Current.GetType().GetTypeInfo().Assembly.GetManifestResourceStream("PclAppDataSource.Pacifico.ttf")) 
                {
                    using (var fileStream = new FileStream(fontPath, FileMode.Create))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                        System.Diagnostics.Debug.WriteLine("DownloadTask: file written [" + fontPath + "]");
                    }
                }
            }
            if (!File.Exists(fontPath))
                throw new Exception("Font file not found");
            var textBlock = Control as Windows.UI.Xaml.Controls.TextBlock;
            var fontFamilyName = "ms-appdata:///local/Pacifico.ttf#Pacifico";
            textBlock.FontFamily = new Windows.UI.Xaml.Media.FontFamily(fontFamilyName);
        }

        protected override void OnDetached()
        {
         //   throw new NotImplementedException();
        }
    }
}
