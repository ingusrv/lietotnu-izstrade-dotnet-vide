﻿namespace md2
{
    public partial class MainPage : ContentPage
    {
        // https://stackoverflow.com/questions/74789634/how-to-share-data-between-pages-in-maui
        private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLogButtonClicked(object sender, EventArgs e)
        {
            LogOutput.Text = _dataManager.Print();
        }

        private async void OnSeedDataButtonClicked(object sender, EventArgs e)
        {
            _dataManager.CreateTestData();

			await DisplayAlert("Paziņojums", "Testa dati izveidoti!", "Ok");
        }

        private async void OnResetButtonClicked(object sender, EventArgs e)
        {
            _dataManager.Reset();

			await DisplayAlert("Paziņojums", "DataManager atiestatīts!", "Ok");
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            bool success = _dataManager.Save("C:\\Windows\\Temp\\data.json");

            if (success)
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
            bool success = _dataManager.Load("C:\\Windows\\Temp\\data.json");

            if (success)
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
