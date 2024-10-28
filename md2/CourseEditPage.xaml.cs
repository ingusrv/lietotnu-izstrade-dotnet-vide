using System.Diagnostics;

namespace md2;

public partial class CourseEditPage : ContentPage
{
	private readonly Course _course;
	private readonly bool _isNewCourse;
	private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();

	public CourseEditPage(Course course, bool isNewCourse)
	{
		InitializeComponent();

		_course = course;
		_isNewCourse = isNewCourse;

		TeacherPicker.ItemsSource = _dataManager.Data.Teachers;
	}

	public async void OnSaveClicked(object sender, EventArgs e) {
		var name = CourseName.Text;
		var teacher = TeacherPicker.SelectedItem as Teacher;

		if (name == null)
		{
			await DisplayAlert("Kļūda", "Kursa nosaukums ir tukšs!", "Ok");
			return;
		}
        if (teacher == null)
        {
			await DisplayAlert("Kļūda", "Lūdzu izvēlaties pasniedzēju!", "Ok");
			return;
        }

        _course.Name = name;
		_course.Teacher = teacher;

		if (_isNewCourse)
		{
			_dataManager.Data.Courses.Add(_course);
		}

		await Navigation.PopAsync();
	}

	public async void OnCancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}