using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace md3
{
    public static class MauiProgram
    { 
        // Non-nullable property "Configuration" must contain a non-null value when exiting constructor.
        public static IConfiguration Configuration { get; private set; }// = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            // https://montemagno.com/dotnet-maui-appsettings-json-configuration/
            //using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("md3.appsettings.json");
            //Configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();

            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            builder.Configuration.AddConfiguration(Configuration);
            builder.Services.AddSingleton<IConfiguration>(Configuration);

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
