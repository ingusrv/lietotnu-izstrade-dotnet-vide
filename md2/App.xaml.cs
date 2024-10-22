namespace md2
{
    public partial class App : Application
    {
        //public IDataManager DataManager { get; set; }
        public App()
        {
            InitializeComponent();

            // https://stackoverflow.com/questions/74789634/how-to-share-data-between-pages-in-maui
            DependencyService.Register<IDataManager, DataManager>();

            //DataManager = new DataManager();
            //DataManager.CreateTestData();

            MainPage = new AppShell();
        }
    }
}
