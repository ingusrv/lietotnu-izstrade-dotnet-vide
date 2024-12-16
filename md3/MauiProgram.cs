using CourseManagementLib;
using md3.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace md3
{
    public static class MauiProgram
    { 
        // Non-nullable property "Configuration" must contain a non-null value when exiting constructor.
        public static IConfiguration Configuration { get; private set; } = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
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

            //Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            builder.Configuration.AddConfiguration(Configuration);
            builder.Services.AddSingleton<IConfiguration>(Configuration);

            builder.Services.AddTransient<MainPage>();

            // viewmodels
            builder.Services.AddTransient<DataViewModel>();
            builder.Services.AddTransient<StudentViewModel>();
            builder.Services.AddTransient<TeacherViewModel>();
            builder.Services.AddTransient<CourseViewModel>();
            builder.Services.AddTransient<AssignmentViewModel>();
            builder.Services.AddTransient<SubmissionViewModel>();
            // views
            builder.Services.AddTransient<DataPage>();
            builder.Services.AddTransient<StudentPage>();
            builder.Services.AddTransient<TeacherPage>();
            builder.Services.AddTransient<CoursePage>();
            builder.Services.AddTransient<AssignmentPage>();
            builder.Services.AddTransient<SubmissionPage>();

            // https://www.devgem.io/posts/dependency-injection-in-maui-passing-parameters-to-service-constructors
            builder.Services.AddSingleton<IDataManager>(provider =>
            {
                string? connectionString = Configuration.GetConnectionString("db") ?? throw new Exception("Konfigurācijā norādītais connectionString nav derīgs!");
                return new DBDataManager(connectionString);
            });

            // https://stackoverflow.com/questions/72429055/how-to-displayalert-in-a-net-maui-viewmodel
            // lai varētu parādīt paziņojumus caur viewmodel
            builder.Services.AddSingleton<IAlertService, AlertService>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
