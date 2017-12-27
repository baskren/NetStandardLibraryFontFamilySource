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
[assembly: ResolutionGroupName("Lib")]
[assembly: ExportEffect(typeof(SupportLibrary.UWP.CustomFontEffect), "CustomFontEffect")]
namespace SupportLibrary.UWP
#elif NETSTANDARD
[assembly: ResolutionGroupName("App")]
[assembly: ExportEffect(typeof(NetStandardApp.UWP.CustomFontEffect), "CustomFontEffect")]
namespace NetStandardApp.UWP
#else
[assembly: ResolutionGroupName("App")]
[assembly: ExportEffect(typeof(PclApp.UWP.CustomFontEffect), "CustomFontEffect")]
namespace PclApp.UWP
#endif

{
    public class CustomFontEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
#if NETSTANDARD
            var embeddedResourceId = "NetStandardApp.Pacifico.ttf";
#else
                var embeddedResourceId = "PclApp.Pacifico.ttf";
#endif
            var appDataPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            //var appDataPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,"EmbeddedResourceCache");
            //var cachedFileName = "Pacifico.ttf";
            //var cachedFileName = "02fe60e0da81514d145d946ab9ad9b97";
            var cachedFileName = embeddedResourceId;
            var fontPath = Path.Combine(appDataPath, cachedFileName);
            System.Diagnostics.Debug.WriteLine("[" + GetType() + "] fontPath: " + fontPath);
            if (!File.Exists(fontPath))
            {
                if (!Directory.Exists(appDataPath))
                    Directory.CreateDirectory(appDataPath);
                using (var stream = Xamarin.Forms.Application.Current.GetType().GetTypeInfo().Assembly.GetManifestResourceStream(embeddedResourceId))
                {
                    using (var fileStream = new FileStream(fontPath, FileMode.Create))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                        System.Diagnostics.Debug.WriteLine("[" + GetType() + "] DownloadTask: file written [" + fontPath + "]");
                    }
                }
            }
            if (!File.Exists(fontPath))
                throw new Exception("[" + GetType() + "] Font file not found");
            else
                System.Diagnostics.Debug.WriteLine("[" + GetType() + "] Font file exists at: " + fontPath);

            var fontFamilyName = "ms-appdata:///local/" + cachedFileName + "#Pacifico";
            System.Diagnostics.Debug.WriteLine("[" + GetType() + "] FontFamilyName: " + fontFamilyName);

            var fontFamilyProperty = GetPropertyInfo(Control.GetType(), "FontFamily");
            if (fontFamilyProperty!=null)
                fontFamilyProperty.SetValue(Control, new Windows.UI.Xaml.Media.FontFamily(fontFamilyName));
            else
                System.Diagnostics.Debug.WriteLine("");
        }

        PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            var typeInfo = type?.GetTypeInfo();
            if (typeInfo == null)
                return null;
            var result = typeInfo.GetDeclaredProperty(propertyName);
            if (result != null)
                return result;
            return GetPropertyInfo(typeInfo.BaseType, propertyName);
        }

        protected override void OnDetached()
        {
            //   throw new NotImplementedException();
        }
    }
}
