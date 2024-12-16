using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseManagementLib;

namespace md3.ViewModel
{
    [QueryProperty(nameof(SelectedStudent), "SelectedStudent")]
    [QueryProperty(nameof(IsNewStudent), "IsNewStudent")]
    public partial class StudentViewModel : ObservableObject
    {
        private readonly IDataManager _dataManager;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private Student selectedStudent;
        [ObservableProperty]
        private bool isNewStudent;
        // par gender sarakstu pajautāju ChatGPT kā to pareizi izdarīt
        [ObservableProperty]
        private List<Gender> genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

        public StudentViewModel(IServiceProvider provider)
        {
            _dataManager = provider.GetRequiredService<IDataManager>();
            _alertService = provider.GetRequiredService<IAlertService>();
        }

        [RelayCommand]
        private Task Save()
        {
            if (IsNewStudent)
            {
                if (!_dataManager.AddStudent(SelectedStudent))
                {
                    return _alertService.ShowAlertAsync("Kļūda", "Neizdevās pievienot!");
                }
            }
            else
            {
                if (!_dataManager.Save())
                {
                    return _alertService.ShowAlertAsync("Kļūda", "Neizdevās saglabāt!");
                }
            }
            return Shell.Current.GoToAsync("//DataPage", new Dictionary<string, object> {
                { "ShouldRefresh", true}
            });
        }
        [RelayCommand]
        private Task Cancel()
        {
            return Shell.Current.GoToAsync("//DataPage", new Dictionary<string, object> {
                { "ShouldRefresh", false}
            });
        }
    }
}
