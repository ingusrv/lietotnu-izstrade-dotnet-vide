namespace md3
{
    public class AlertService : IAlertService
    {
        public Task ShowAlertAsync(string title, string message, string cancel = "Ok")
        {
            return Application.Current!.MainPage!.DisplayAlert(title, message, cancel);
        }
    }
}
