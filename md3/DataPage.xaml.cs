using CourseManagementLib;
using System.Diagnostics;
using System.Linq.Expressions;

namespace md3;

public partial class DataPage : ContentPage
{
    //private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();
	private readonly DBDataManager _dataManager = App.DBDataManager;

	public List<Student> Students {
		get
		{
			return _dataManager.Students.ToList();
		}
	}

	public List<Teacher> Teachers
	{
		get
		{
			return _dataManager.Teachers.ToList();
		}
	}

	public List<Course> Courses
	{
		get
		{
			return _dataManager.Courses.ToList();
		}
	}

	public List<Assignment> Assignments
	{
		get
		{
			return _dataManager.Assignments.ToList();
		}
	}

	public List<Submission> Submissions
	{
		get
		{
			return _dataManager.Submissions.ToList();
		}
	}

	public DataPage()
	{
		//BindingContext = this;
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
		try
		{
			if (_dataManager.Teachers.Count() == 0)
			{
				await DisplayAlert("Kļūda", "Lūdzu izveidojiet vismaz vienu pasniedzēju!", "Ok");
				return;
			}

			var CourseEditPage = new CourseEditPage(new Course(), true);
			await Navigation.PushAsync(CourseEditPage);
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Kļūda", "Neizdevās izveidot savienojumu ar datubāzi: " + ex.Message, "Ok");
		}
    }

	public async void OnAssignmentCreateClicked(object sender, EventArgs e)
	{
		try
		{
			if (_dataManager.Courses.Count() == 0)
			{
				await DisplayAlert("Kļūda", "Lūdzu izveidojiet vismaz vienu kursu!", "Ok");
				return;
			}

			var AssignmentEditPage = new AssignmentEditPage(new Assignment(), true);
			await Navigation.PushAsync(AssignmentEditPage);
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Kļūda", "Neizdevās izveidot savienojumu ar datubāzi: " + ex.Message, "Ok");
		}
    }

	public async void OnSubmissionCreateClicked(object sender, EventArgs e)
	{
		try
		{
			if (_dataManager.Students.Count() == 0)
			{
				await DisplayAlert("Kļūda", "Lūdzu izveidojiet vismaz vienu studentu!", "Ok");
				return;
			}
			if (_dataManager.Assignments.Count() == 0)
			{
				await DisplayAlert("Kļūda", "Lūdzu izveidojiet vismaz vienu uzdevumu!", "Ok");
				return;
			}

			var SubmissionEditPage = new SubmissionEditPage(new Submission(), true);
			await Navigation.PushAsync(SubmissionEditPage);
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Kļūda", "Neizdevās izveidot savienojumu ar datubāzi: " + ex.Message, "Ok");
		}
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

	public async void OnTeacherEditClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Teacher teacher)
            {
                var TeacherEditPage = new TeacherEditPage(teacher, false);
                await Navigation.PushAsync(TeacherEditPage);
            }
        }
    }
	
	public async void OnCourseEditClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Course course)
            {
                var CourseEditPage = new CourseEditPage(course, false);
                await Navigation.PushAsync(CourseEditPage);
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

	// delete pogu funkcionalitāte - kad grib izdzēst ierakstu
	public async void OnStudentDeleteClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Student student)
            {
				try
				{
					_dataManager.Students.Remove(student);
					_dataManager.SaveChanges();
				}
				catch (Exception ex)
				{
					await DisplayAlert("Kļūda", "Neizdevās izdzēst studentu: " + ex.Message, "Ok");
				}

				// atsvaidzinām skatu
				BindingContext = null;
				BindingContext = this;
            }
        }
	}

	public async void OnTeacherDeleteClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Teacher teacher)
            {
				try
				{
					_dataManager.Teachers.Remove(teacher);
					_dataManager.SaveChanges();
				}
				catch (Exception ex)
				{
					await DisplayAlert("Kļūda", "Neizdevās izdzēst pasniedzēju: " + ex.Message, "Ok");
				}

				// atsvaidzinām skatu
				BindingContext = null;
				BindingContext = this;
            }
        }
	}

	public async void OnCourseDeleteClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Course course)
            {
				try
				{
					_dataManager.Courses.Remove(course);
					_dataManager.SaveChanges();
				}
				catch (Exception ex)
				{
					await DisplayAlert("Kļūda", "Neizdevās izdzēst kursu: " + ex.Message, "Ok");
				}

				// atsvaidzinām skatu
				BindingContext = null;
				BindingContext = this;
            }
        }
	}

	public async void OnAssignmentDeleteClicked(object sender, EventArgs e)
	{
		if (sender is Button btn)
		{
			if (btn.BindingContext is Assignment assignment)
			{
				try
				{
					_dataManager.Assignments.Remove(assignment);
					_dataManager.SaveChanges();
				}
				catch (Exception ex)
				{
					await DisplayAlert("Kļūda", "Neizdevās izdzēst uzdevumu: " + ex.Message, "Ok");
				}

				// atsvaidzinām skatu
				BindingContext = null;
				BindingContext = this;
			}
		}
	}

	public async void OnSubmissionDeleteClicked(object sender, EventArgs e)
	{
        if (sender is Button btn)
        {
            if (btn.BindingContext is Submission submission)
            {
				try { 
					_dataManager.Submissions.Remove(submission);
					_dataManager.SaveChanges();
				}
				catch (Exception ex)
				{
					await DisplayAlert("Kļūda", "Neizdevās izdzēst iesniegumu: " + ex.Message, "Ok");
				}
				
				// atsvaidzinām skatu
				BindingContext = null;
				BindingContext = this;
            }
        }
	}

	// ļoti slikta mājaslapa reklāmu ziņā, bet noderīga informācijas ziņā
	// https://trycatchdebug.net/news/1172680/net-8-maui-listview-refresh
	protected override async void OnAppearing()
    {
		Debug.WriteLine("on appear");

		// ļoti hacky veids kā sākotnēji iztestēt vai datubāze strādā
		// ja nestrādā tad uzmetam paziņojumu ka nevarēja savienoties
		try
		{
			if (!_dataManager.Database.CanConnect())
			{
				Debug.WriteLine("nevaram savienoties");
				await DisplayAlert("Kļūda", "Neizdevās izveidot savienojumu ar datubāzi!", "Ok");
				return;
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine("datubāze vispār nestrādā");
			await DisplayAlert("Kļūda", "Neizdevās izveidot savienojumu ar datubāzi: " + ex.Message, "Ok");
			return;
		}

        base.OnAppearing();
		BindingContext = null;
		BindingContext = this;
    }
}