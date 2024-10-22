using System.Diagnostics;

namespace md2;

public partial class DataPage : ContentPage
{
	IDataManager dataManager = DependencyService.Get<IDataManager>();
	public List<Student> Students {
		get
		{
			Debug.WriteLine(dataManager.Data.Students);
			return dataManager.Data.Students;
		}
	}

	public List<Teacher> Teachers
	{
		get
		{
			return dataManager.Data.Teachers;
		}
	}

	public List<Course> Courses
	{
		get
		{
			return dataManager.Data.Courses;
		}
	}

	public List<Assignment> Assignments
	{
		get
		{
			return dataManager.Data.Assignments;
		}
	}

	public List<Submission> Submissions
	{
		get
		{
			return dataManager.Data.Submissions;
		}
	}

	public DataPage()
	{
		BindingContext = this;
		InitializeComponent();
	}

	public async void OnAssignmentEditClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("Assignment Edit clicked");
        if (sender is Button btn)
        {
            if (btn.BindingContext is Assignment assignment)
            {
                var AssignmentEditPage = new AssignmentEditPage(assignment);
                await Navigation.PushAsync(AssignmentEditPage);
            }
        }
    }
	public void OnAssignmentDeleteClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("Assignment Delete clicked");
	}
	public async void OnSubmissionEditClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("Submission Edit clicked");
        if (sender is Button btn)
        {
            if (btn.BindingContext is Assignment assignment)
            {
                var AssignmentEditPage = new AssignmentEditPage(assignment);
                await Navigation.PushAsync(AssignmentEditPage);
            }
        }
    }
	public void OnSubmissionDeleteClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("Submission Delete clicked");
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