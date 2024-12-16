using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseManagementLib;
using System.Collections.ObjectModel;

namespace md3.ViewModel
{
    [QueryProperty(nameof(SelectedCourse), "SelectedCourse")]
    [QueryProperty(nameof(IsNewCourse), "IsNewCourse")]
    public partial class CourseViewModel : ObservableObject
    {
        private readonly IDataManager _dataManager;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private Course selectedCourse;

        [ObservableProperty]
        private bool isNewCourse;

        [ObservableProperty]
        private ObservableCollection<Teacher> teachers;

        public CourseViewModel(IServiceProvider provider)
        {
            _dataManager = provider.GetRequiredService<IDataManager>();
            _alertService = provider.GetRequiredService<IAlertService>();
            Teachers = new ObservableCollection<Teacher>(_dataManager.GetAllTeachers()); 
        }

        [RelayCommand]
        private Task Save()
        {
            if (SelectedCourse.Teacher == null)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Lūdzu izvēlaties pasniedzēju!");
            }

            if (SelectedCourse.Name == null)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Kursa nosaukums ir tukšs!");
            }

            if (IsNewCourse)
            {
                if (!_dataManager.AddCourse(SelectedCourse))
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
