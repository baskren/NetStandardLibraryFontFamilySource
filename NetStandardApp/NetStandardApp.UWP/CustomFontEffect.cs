using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using System.IO;
using System.Reflection;

#if SUPPORTLIB
[assembly: ResolutionGroupName("SupportLibrary")]
[assembly: ExportEffect(typeof(SupportLibrary.UWP.CustomFontEffect), "CustomFontEffect")]
namespace SupportLibrary.UWP
#else
[assembly: ResolutionGroupName("NetStandardDataSource")]
[assembly: ExportEffect(typeof(NetStandardAppDataSource.UWP.CustomFontEffect), "CustomFontEffect")]
namespace NetStandardAppDataSource.UWP
#endif

{
    public class CustomFontEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var appDataPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            var hashedFileName = "02fe60e0da81514d145d946ab9ad9b97";
            var fontPath = Path.Combine(appDataPath, hashedFileName);
            System.Diagnostics.Debug.WriteLine("[" + GetType() + "] fontPath: " + fontPath);
            if (!File.Exists(fontPath))
            {
#if NETSTANDARD
                var embeddedResourceId = "NetStandardAppDataSource.Pacifico.ttf";
#else
                var embeddedResourceId = "Pcl.App.PclApp.Pacifico.ttf";
#endif
                using (var stream = Xamarin.Forms.Application.Current.GetType().GetTypeInfo().Assembly.GetManifestResourceStream(embeddedResourceId)) 
                {
                    using (var fileStream = new FileStream(fontPath, FileMode.Create))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                        System.Diagnostics.Debug.WriteLine("["+GetType()+"] DownloadTask: file written [" + fontPath + "]");
                    }
                }
            }
            if (!File.Exists(fontPath))
                throw new Exception("[" + GetType() + "] Font file not found");
            else
                System.Diagnostics.Debug.WriteLine("[" + GetType() + "] Font file exists at: " + fontPath);
            var textBlock = Control as Windows.UI.Xaml.Controls.TextBlock;
            var fontFamilyName = "ms-appdata:///local/"+hashedFileName+"#Pacifico";
            System.Diagnostics.Debug.WriteLine("[" + GetType() + "] FontFamilyName: " + fontFamilyName);
            textBlock.FontFamily = new Windows.UI.Xaml.Media.FontFamily(fontFamilyName);
        }

        protected override void OnDetached()
        {
         //   throw new NotImplementedException();
        }
    }
}
