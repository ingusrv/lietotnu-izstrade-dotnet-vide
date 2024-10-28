using System.Diagnostics;

namespace md2;

public partial class AssignmentEditPage : ContentPage
{
	private readonly Assignment _assignment;
	private readonly bool _isNewAssignment;
	private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();

	public AssignmentEditPage(Assignment assignment, bool isNewAssignment)
	{
		InitializeComponent();

		_assignment = assignment;
		_isNewAssignment = isNewAssignment;

		CoursePicker.ItemsSource = _dataManager.Data.Courses;

		if (!_isNewAssignment)
		{
			Deadline.Date = DateTime.Parse(_assignment.Deadline.ToString());
			CoursePicker.SelectedItem = _assignment.Course;
			Description.Text = _assignment.Description.ToString();
		}
	}

	public async void OnSaveClicked(object sender, EventArgs e) {
		var deadline = DateOnly.FromDateTime(Deadline.Date);
		var course = CoursePicker.SelectedItem as Course;
		var description = Description.Text;

		if (course == null)
		{
			await DisplayAlert("Kļūda", "Lūdzu izvēlaties kursu!", "Ok");
			return;
		}

		_assignment.Deadline = deadline;
		_assignment.Course = course;
		_assignment.Description = description;
		
		if (_isNewAssignment)
		{
			_dataManager.Data.Assignments.Add(_assignment);
		}

		await Navigation.PopAsync();
	}

	public async void OnCancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}