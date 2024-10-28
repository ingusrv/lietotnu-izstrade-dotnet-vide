using System.Diagnostics;

namespace md2;

public partial class DataPage : ContentPage
{
	private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();
	public List<Student> Students {
		get
		{
			return _dataManager.Data.Students;
		}
	}

	public List<Teacher> Teachers
	{
		get
		{
			return _dataManager.Data.Teachers;
		}
	}

	public List<Course> Courses
	{
		get
		{
			return _dataManager.Data.Courses;
		}
	}

	public List<Assignment> Assignments
	{
		get
		{
			return _dataManager.Data.Assignments;
		}
	}

	public List<Submission> Submissions
	{
		get
		{
			return _dataManager.Data.Submissions;
		}
	}

	public DataPage()
	{
		BindingContext = this;
		InitializeComponent();
	}

	// add pogu funkcionalitāte - kad grib pievienot jaunu ierakstu
	public async void OnTeacherCreateClicked(object sender, EventArgs e)
	{
		var TeacherEditPage = new TeacherEditPage(new Teacher(), true);
		await Navigation.PushAsync(TeacherEditPage);
    }
	public async void OnStudentCreateClicked(object sender, EventArgs e)
	{
		var StudentEditPage = new StudentEditPage(new Student(), true);
		await Navigation.PushAsync(StudentEditPage);
    }

	public async void OnCourseCreateClicked(object sender, EventArgs e)
	{
		if (_dataManager.Data.Teachers.Count == 0)
		{
			await DisplayAlert("Kļūda", "Lūdzu izveidojiet vismaz vienu pasniedzēju!", "Ok");
			return;
		}

		var CourseEditPage = new CourseEditPage(new Course(), true);
		await Navigation.PushAsync(CourseEditPage);
    }

	public async void OnAssignmentCreateClicked(object sender, EventArgs e)
	{
		if (_dataManager.Data.Courses.Count == 0)
		{
			await DisplayAlert("Kļūda", "Lūdzu izveidojiet vismaz vienu kursu!", "Ok");
			return;
		}

        var AssignmentEditPage = new AssignmentEditPage(new Assignment(), true);
        await Navigation.PushAsync(AssignmentEditPage);
    }

	public async void OnSubmissionCreateClicked(object sender, EventArgs e)
	{
		if (_dataManager.Data.Students.Count == 0)
		{
			await DisplayAlert("Kļūda", "Lūdzu izveidojiet vismaz vienu studentu!", "Ok");
			return;
		}
		if (_dataManager.Data.Assignments.Count == 0)
		{
			await DisplayAlert("Kļūda", "Lūdzu izveidojiet vismaz vienu uzdevumu!", "Ok");
			return;
		}

		var SubmissionEditPage = new SubmissionEditPage(new Submission(), true);
		await Navigation.PushAsync(SubmissionEditPage);
    }
	
	// edit pogu funkcionalitāte - kad grib rediģēt esošu ierakstu
	public async void OnStudentEditClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Student student)
            {
                var StudentEditPage = new StudentEditPage(student, false);
                await Navigation.PushAsync(StudentEditPage);
            }
        }
    }

	public async void OnAssignmentEditClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Assignment assignment)
            {
                var AssignmentEditPage = new AssignmentEditPage(assignment, false);
                await Navigation.PushAsync(AssignmentEditPage);
            }
        }
    }

	// delete pogu funkcionalitāte - kad grib izdzēst ierakstu
	public void OnAssignmentDeleteClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Assignment assignment)
            {
				_dataManager.Data.Assignments.Remove(assignment);

				// atsvaidzinām skatu
				BindingContext = null;
				BindingContext = this;
            }
        }
	}

	public async void OnSubmissionEditClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Submission submission)
            {
                var SubmissionEditPage = new SubmissionEditPage(submission, false);
                await Navigation.PushAsync(SubmissionEditPage);
            }
        }
    }

	public void OnSubmissionDeleteClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Submission submission)
            {
				_dataManager.Data.Submissions.Remove(submission);
				
				// atsvaidzinām skatu
				BindingContext = null;
				BindingContext = this;
            }
        }
	}

	// ļoti slikta mājaslapa reklāmu ziņā, bet noderīga informācijas ziņā
	// https://trycatchdebug.net/news/1172680/net-8-maui-listview-refresh
	protected override void OnAppearing()
    {
        base.OnAppearing();
		Debug.WriteLine("on appear");
		BindingContext = null;
		BindingContext = this;
    }
}