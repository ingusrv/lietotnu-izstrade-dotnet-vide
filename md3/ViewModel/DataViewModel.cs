using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseManagementLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md3.ViewModel
{
    [QueryProperty(nameof(ShouldRefresh), "ShouldRefresh")]
    public partial class DataViewModel : ObservableObject
    {
        private IDataManager _dataManager;
        private IAlertService _alertService;

        [ObservableProperty]
        private ObservableCollection<Student> students;

        [ObservableProperty]
        private ObservableCollection<Teacher> teachers;

        [ObservableProperty]
        private ObservableCollection<Course> courses;

        [ObservableProperty]
        private ObservableCollection<Assignment> assignments;

        [ObservableProperty]
        private ObservableCollection<Submission> submissions;

        [ObservableProperty]
        private bool shouldRefresh;

        partial void OnShouldRefreshChanged(bool value)
        {
            Debug.WriteLine("ShouldRefresh changed");
            if (value == true)
            {
                Debug.WriteLine("list refresh");
                Refresh();
                ShouldRefresh = false;
            }
        }

        public DataViewModel(IServiceProvider provider) : base()
        {
            _dataManager = provider.GetService<IDataManager>()!;
            _alertService = provider.GetService<IAlertService>()!;

            try
            {
                Students = new ObservableCollection<Student>(_dataManager.GetAllStudents());
                Teachers = new ObservableCollection<Teacher>(_dataManager.GetAllTeachers());
                Courses = new ObservableCollection<Course>(_dataManager.GetAllCourses());
                Assignments = new ObservableCollection<Assignment>(_dataManager.GetAllAssignments());
                Submissions = new ObservableCollection<Submission>(_dataManager.GetAllSubmissions());
            }
            catch (Exception ex)
            {
                _alertService.ShowAlertAsync("Kļūda", "Neizdevās ielādēt datus no datubāzes: " + ex.Message);
                // ja nestrādā datubāze, tad vienkārši inicializējam kā tukšaas lai tālāk .Count pārbaudes strādātu
                Students = new ObservableCollection<Student>();
                Teachers = new ObservableCollection<Teacher>();
                Courses = new ObservableCollection<Course>();
                Assignments = new ObservableCollection<Assignment>();
                Submissions = new ObservableCollection<Submission>();
            }
        }

        private void Refresh()
        {
            Students = new ObservableCollection<Student>(_dataManager.GetAllStudents());
            Teachers = new ObservableCollection<Teacher>(_dataManager.GetAllTeachers());
            Courses = new ObservableCollection<Course>(_dataManager.GetAllCourses());
            Assignments = new ObservableCollection<Assignment>(_dataManager.GetAllAssignments());
            Submissions = new ObservableCollection<Submission>(_dataManager.GetAllSubmissions());
        }

        // add
        [RelayCommand]
        private Task AddStudent()
        {
            return Shell.Current.GoToAsync(nameof(StudentPage), new Dictionary<string, object>
            {
                {"SelectedStudent", new Student() },
                {"IsNewStudent", true }
            });
        }
        [RelayCommand]
        private Task AddTeacher()
        {
            return Shell.Current.GoToAsync(nameof(TeacherPage), new Dictionary<string, object>
            {
                {"SelectedTeacher", new Teacher { ContractDate = DateOnly.FromDateTime(DateTime.Now) } },
                {"IsNewTeacher", true }
            });
        }
        [RelayCommand]
        private Task AddCourse()
        {
            if (Teachers.Count == 0)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Lūdzu pievienojiet vismaz vienu pasniedzēju!");
            }

            return Shell.Current.GoToAsync(nameof(CoursePage), new Dictionary<string, object>
            {
                {"SelectedCourse", new Course() },
                {"IsNewCourse", true }
            });
        }
        [RelayCommand]
        private Task AddAssignment()
        {
            if (Courses.Count == 0)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Lūdzu pievienojiet vismaz vienu kursu!");
            }

            return Shell.Current.GoToAsync(nameof(AssignmentPage), new Dictionary<string, object>
            {
                {"SelectedAssignment", new Assignment{ Deadline = DateOnly.FromDateTime(DateTime.Now) } },
                {"IsNewAssignment", true }
            });
        }
        [RelayCommand]
        private Task AddSubmission()
        {
            if (Students.Count == 0)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Lūdzu izveidojiet vismaz vienu studentu!");
            }

            if (Assignments.Count == 0)
            {
                return _alertService.ShowAlertAsync("Kļūda", "Lūdzu izveidojiet vismaz vienu uzdevumu!");
            }

            return Shell.Current.GoToAsync(nameof(SubmissionPage), new Dictionary<string, object>
            {
                {"SelectedSubmission", new Submission { SubmissionTime = DateTime.Now } },
                {"IsNewSubmission", true }
            });
        }

        // edit
        [RelayCommand]
        private Task EditStudent(Student student)
        {
            return Shell.Current.GoToAsync(nameof(StudentPage), new Dictionary<string, object>
            {
                {"SelectedStudent", student },
                {"IsNewStudent", false }
            });
        }
        [RelayCommand]
        private Task EditTeacher(Teacher teacher)
        {
            return Shell.Current.GoToAsync(nameof(TeacherPage), new Dictionary<string, object>
            {
                {"SelectedTeacher", teacher },
                {"IsNewTeacher", false }
            });
        }
        [RelayCommand]
        private Task EditCourse(Course course)
        {
            return Shell.Current.GoToAsync(nameof(CoursePage), new Dictionary<string, object>
            {
                {"SelectedCourse", course },
                {"IsNewCourse", false }
            });
        }
        [RelayCommand]
        private Task EditAssignment(Assignment assignment)
        {
            return Shell.Current.GoToAsync(nameof(AssignmentPage), new Dictionary<string, object>
            {
                {"SelectedAssignment", assignment },
                {"IsNewAssignent", false }
            });
        }
        [RelayCommand]
        private Task EditSubmission(Submission submission)
        {
            return Shell.Current.GoToAsync(nameof(SubmissionPage), new Dictionary<string, object>
            {
                {"SelectedSubmission", submission },
                {"IsNewSubmission", false }
            });
        }

        // remove
        [RelayCommand]
        private Task RemoveStudent(Student student)
        {
            if (!_dataManager.RemoveStudent(student))
            {
                return _alertService.ShowAlertAsync("Kļūda", "Neizdevās dzēst studentu!");
            }
            ShouldRefresh = true;
            return Task.CompletedTask;
        }
        [RelayCommand]
        private Task RemoveTeacher(Teacher teacher)
        {
            if (!_dataManager.RemoveTeacher(teacher))
            {
                return _alertService.ShowAlertAsync("Kļūda", "Neizdevās dzēst pasniedzēju!");
            }
            ShouldRefresh = true;
            return Task.CompletedTask;
        }
        [RelayCommand]
        private Task RemoveCourse(Course course)
        {
            if (!_dataManager.RemoveCourse(course))
            {
                return _alertService.ShowAlertAsync("Kļūda", "Neizdevās dzēst kursu!");
            }
            ShouldRefresh = true;
            return Task.CompletedTask;
        }
        [RelayCommand]
        private Task RemoveAssignment(Assignment assignment)
        {
            if (!_dataManager.RemoveAssignment(assignment))
            {
                return _alertService.ShowAlertAsync("Kļūda", "Neizdevās dzēst uzdevumu!");
            }
            ShouldRefresh = true;
            return Task.CompletedTask;
        }
        [RelayCommand]
        private Task RemoveSubmission(Submission submission)
        {
            if (!_dataManager.RemoveSubmission(submission))
            {
                return _alertService.ShowAlertAsync("Kļūda", "Neizdevās dzēst iesniegumu!");
            }
            ShouldRefresh = true;
            return Task.CompletedTask;
        }
    }
}
