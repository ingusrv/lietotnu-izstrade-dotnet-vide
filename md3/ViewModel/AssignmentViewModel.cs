using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseManagementLib;
using System.Collections.ObjectModel;

namespace md3.ViewModel
{
    [QueryProperty(nameof(SelectedAssignment), "SelectedAssignment")]
    [QueryProperty(nameof(IsNewAssignment), "IsNewAssignment")]
    public partial class AssignmentViewModel : ObservableObject
    {
        private readonly IDataManager _dataManager;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private Assignment selectedAssignment;

        [ObservableProperty]
        private bool isNewAssignment;

        [ObservableProperty]
        private ObservableCollection<Course> courses;

        // kad uzdevuma īpašība tiek aizpildīta no QueryProperty, tad
        // jāaizpilda date picker ar jauno vērtību
        partial void OnSelectedAssignmentChanged(Assignment value)
        {
            OnPropertyChanged(nameof(Deadline));
        }

        // datepicker prasa DateTime, bet uzdevuma Deadline ir DateOnly tips
        // tāpēc nepieciešama atsevišķa īpašība, kura pārveido uz DateTime un otrādi
        public DateTime Deadline
        {
            get => SelectedAssignment?.Deadline.ToDateTime(new TimeOnly(0, 0)) ?? DateTime.Now;
            set
            {
                if (SelectedAssignment != null)
                {
                    SelectedAssignment.Deadline = DateOnly.FromDateTime(value);
                }
            }
        }

        public AssignmentViewModel(IServiceProvider provider)
        {
            _dataManager = provider.GetRequiredService<IDataManager>();
            _alertService = provider.GetRequiredService<IAlertService>();
            Courses = new ObservableCollection<Course>(_dataManager.GetAllCourses());
        }
        

        [RelayCommand]
        private Task Save()
        {
            if (SelectedAssignment.Course == null)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Lūdzu izvēlaties kursu!");
            }

            if (IsNewAssignment)
            {
                if (!_dataManager.AddAssignment(SelectedAssignment))
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
