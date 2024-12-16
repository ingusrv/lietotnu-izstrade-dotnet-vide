using CourseManagementLib;
using System.Data;

namespace md3
{
    public partial class MainPage : ContentPage
    {
        private readonly IDataManager _dataManager;

        public MainPage(IServiceProvider provider)
        {
            _dataManager = provider.GetRequiredService<IDataManager>();
            InitializeComponent();
        }

        private void OnLogButtonClicked(object sender, EventArgs e)
        {
            LogOutput.Text = _dataManager.Print();
        }

        private async void OnSeedDataButtonClicked(object sender, EventArgs e)
        {
            if (_dataManager.CreateTestData())
            {
                await DisplayAlert("Paziņojums", "Testa dati izveidoti!", "Ok");
            } else
            {
                await DisplayAlert("Kļūda", "Neizdevās izveidot testa datus!", "OK");
            }
        }

        private async void OnResetButtonClicked(object sender, EventArgs e)
        {
            if (_dataManager.Reset())
            {
                await DisplayAlert("Paziņojums", "Visi dati izdēsti!", "Ok");
            } else
            {
                await DisplayAlert("Kļūda", "Neizdevās izdzēst datus no datubāzes!", "Ok");
            }
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (_dataManager.Save("C:\\Windows\\Temp\\data.json"))
            {
                await DisplayAlert("Paziņojums", "Dati saglabāti!", "Ok");
            }
            else
            {
                await DisplayAlert("Kļūda", "Neizdevās saglabāt datus!", "Ok");
            }
        }

        private async void OnLoadButtonClicked(object sender, EventArgs e)
        {
            if (_dataManager.Load("C:\\Windows\\Temp\\data.json"))
            {
                await DisplayAlert("Paziņojums", "Dati ielādēti!", "Ok");
            }
            else
            {
                await DisplayAlert("Kļūda", "Neizdevās ielādēt datus!", "Ok");
            }
        }
    }

}
