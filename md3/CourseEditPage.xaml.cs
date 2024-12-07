using CourseManagementLib;
using System.Diagnostics;

namespace md3;

public partial class CourseEditPage : ContentPage
{
	private readonly Course _course;
	private readonly bool _isNewCourse;
	//private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();
	private readonly DBDataManager _dataManager = App.DBDataManager;

	public CourseEditPage(Course course, bool isNewCourse)
	{
		InitializeComponent();

		_course = course;
		_isNewCourse = isNewCourse;

		TeacherPicker.ItemsSource = _dataManager.Teachers.ToList();

		if (!_isNewCourse)
		{
			CourseName.Text = course.Name;
			TeacherPicker.SelectedItem = course.Teacher;
		}
	}

	public async void OnSaveClicked(object sender, EventArgs e) {
		try
		{
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
				_dataManager.Courses.Add(_course);
			}

			_dataManager.SaveChanges();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Kļūda", "Neizdevās saglabāt: " + ex.Message, "Ok");
		}

		await Navigation.PopAsync();
	}

	public async void OnCancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}