namespace md3
{
    public interface IAlertService
    {
        Task ShowAlertAsync(string title, string message, string cancel = "Ok");
    }
}
