using CourseManagementLib;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace md3
{
    public partial class App : Application
    {
        public static DBDataManager DBDataManager { get; set; }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            string? connectionString = MauiProgram.Configuration.GetConnectionString("db") ?? throw new Exception("Konfigurācijā norādītais connectionString nav derīgs!");
            Debug.Write(connectionString);

            // nebija laika lai noskaidrotu vai DependencyService strādās ar db datamanager versiju tāpēc uztaisīju statisko īpašību
            DBDataManager = new DBDataManager(connectionString);

            // https://stackoverflow.com/questions/74789634/how-to-share-data-between-pages-in-maui
            DependencyService.Register<IDataManager, DataManager>();
        }
    }
}
