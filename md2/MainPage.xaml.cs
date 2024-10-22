namespace md2
{
    //public class TestClass(string name)
    //{
    //    public string Name { get; set; } = name;
    //}
    public partial class MainPage : ContentPage
    {
        // https://stackoverflow.com/questions/74789634/how-to-share-data-between-pages-in-maui
        IDataManager dataManager = DependencyService.Get<IDataManager>();
        //public List<TestClass> Test;
        public MainPage()
        {
            InitializeComponent();
            //Test = new List<TestClass>();
            //Test.Add(new TestClass("foo"));
            //Test.Add(new TestClass("bar"));
            //Test.Add(new TestClass("baz"));

            //TestList.ItemsSource = Test;
        }
        private void OnLogButtonClicked(object sender, EventArgs e)
        {
            //SemanticScreenReader.Announce(LogBtn.Text);

            LogOutput.Text = dataManager.Print();
            if (dataManager.Data.Students.Any())
                LogOutput.Text += "\n" + dataManager.Data.Students.First().ToString();
        }
        private void OnSeedDataButtonClicked(object sender, EventArgs e)
        {
            dataManager.CreateTestData();
        }
        private void OnResetButtonClicked(object sender, EventArgs e)
        {
            dataManager.Reset();
        }
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            dataManager.Save("C:\\Windows\\Temp\\data.json");
        }
        private void OnLoadButtonClicked(object sender, EventArgs e)
        {
            dataManager.Load("C:\\Windows\\Temp\\data.json");
        }
    }

}
