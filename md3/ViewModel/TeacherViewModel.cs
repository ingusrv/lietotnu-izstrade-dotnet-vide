using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseManagementLib;

namespace md3.ViewModel
{
    [QueryProperty(nameof(SelectedTeacher), "SelectedTeacher")]
    [QueryProperty(nameof(IsNewTeacher), "IsNewTeacher")]
    public partial class TeacherViewModel : ObservableObject
    {
        private readonly IDataManager _dataManager;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private Teacher selectedTeacher;

        [ObservableProperty]
        private bool isNewTeacher;

        [ObservableProperty]
        private List<Gender> genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

        partial void OnSelectedTeacherChanged(Teacher value)
        {
            OnPropertyChanged(nameof(ContractDate));
        }

        public DateTime ContractDate
        {
            get => SelectedTeacher?.ContractDate.ToDateTime(new TimeOnly(0,0)) ?? DateTime.Now;
            set
            {
                if (SelectedTeacher != null)
                {
                    SelectedTeacher.ContractDate = DateOnly.FromDateTime(value);
                }
            }
        } 

        public TeacherViewModel(IServiceProvider provider)
        {
            _dataManager = provider.GetRequiredService<IDataManager>();
            _alertService = provider.GetRequiredService<IAlertService>();
        }

        [RelayCommand]
        private Task Save()
        {
            if (IsNewTeacher)
            {
                if (!_dataManager.AddTeacher(SelectedTeacher))
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
