using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseManagementLib;
using System.Collections.ObjectModel;

namespace md3.ViewModel
{
    [QueryProperty(nameof(SelectedSubmission), "SelectedSubmission")]
    [QueryProperty(nameof(IsNewSubmission), "IsNewSubmission")]
    public partial class SubmissionViewModel : ObservableObject
    {
        private readonly IDataManager _dataManager;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private Submission selectedSubmission;

        [ObservableProperty]
        private bool isNewSubmission;

        [ObservableProperty]
        private ObservableCollection<Assignment> assignments;

        [ObservableProperty]
        private ObservableCollection<Student> students;

        // kad iesnieguma īpašība tiek aizpildīta no QueryProperty, tad jāpaziņo lai arī
        // time picker tiek aizpildīts ar pareizo vērtību
        partial void OnSelectedSubmissionChanged(Submission value)
        {
            OnPropertyChanged(nameof(SubmissionTime));
        }

        // time picker pieņem tikai TimeSpan vērtības, nevis visu DateTime, tāpēc nepieciešama atsevišķa
        // īpašība, kura pārveido izvēlētā iesnieguma DateTime uz TimeSpan un otrādi
        public TimeSpan SubmissionTime
        {
            get => SelectedSubmission?.SubmissionTime.TimeOfDay ?? TimeSpan.Zero;
            set
            {
                if (SelectedSubmission != null)
                {
                    SelectedSubmission.SubmissionTime = SelectedSubmission.SubmissionTime.Date.Add(value);
                }
            }
        }

        public SubmissionViewModel(IServiceProvider provider)
        {
            _dataManager = provider.GetRequiredService<IDataManager>();
            _alertService = provider.GetRequiredService<IAlertService>();
            Assignments = new ObservableCollection<Assignment>(_dataManager.GetAllAssignments());
            Students = new ObservableCollection<Student>(_dataManager.GetAllStudents());
        }

        [RelayCommand]
        private Task Save()
        {
            if (SelectedSubmission.Assignment == null)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Lūdzu izvēlaties uzdevumu!");
            }
            if (SelectedSubmission.Student == null)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Lūdzu izvēlaties studentu!");
            }

            if (IsNewSubmission)
            {
                if (!_dataManager.AddSubmission(SelectedSubmission))
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
