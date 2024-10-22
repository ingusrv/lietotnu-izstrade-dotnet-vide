using System.Diagnostics;

namespace md2;

public partial class AssignmentEditPage : ContentPage
{
	private Assignment _assignment;
	private IDataManager _dataManager = DependencyService.Get<IDataManager>();

	public List<Course> Courses { 
		get
		{
			return _dataManager.Data.Courses;
		}
	}

	public AssignmentEditPage(Assignment assignment)
	{
		InitializeComponent();

		_assignment = assignment;

		AssignmentDeadline.Date = DateTime.Parse(assignment.Deadline.ToString());
		AssignmentCourseName.SelectedItem = assignment.Course;
		AssignmentDescription.Text = assignment.Description.ToString();

		BindingContext = this;
	}

	public async void OnSaveClicked(object sender, EventArgs e) {
		var date = DateOnly.FromDateTime(AssignmentDeadline.Date);
		var course = AssignmentCourseName.SelectedItem as Course;
		var description  = AssignmentDescription.Text;
		Debug.WriteLine(date);
		Debug.WriteLine(course);
		Debug.WriteLine(description);

		if (course != null)
		{
			_assignment.Deadline = date;
			_assignment.Course = course;
			_assignment.Description = description;

			await DisplayAlert("Saved", "Assignment updated!", "Ok");
		}
		else
		{
			await DisplayAlert("Error", "Please select a Course!", "Ok");
		}
	}

	public async void OnCancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}