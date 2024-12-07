using CourseManagementLib;
using System.Diagnostics;

namespace md3;

public partial class StudentEditPage : ContentPage
{
	private readonly Student _student;
	private readonly bool _isNewStudent;
	//private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();
	private readonly DBDataManager _dataManager = App.DBDataManager;

	public StudentEditPage(Student student, bool isNewStudent)
	{
		InitializeComponent();

		_student = student;
		_isNewStudent = isNewStudent;

		GenderPicker.SelectedIndex = 0;

		if (!_isNewStudent)
		{
			Name.Text = _student.Name;
			Surname.Text = _student.Surname;
			StudentIdNumber.Text = _student.StudentIdNumber;
			GenderPicker.SelectedItem = _student.Gender.ToString();
		}
	}

	public async void OnSaveClicked(object sender, EventArgs e) {
		try {
			var name = Name.Text;
			var surname = Surname.Text;
			var studentIdNumber = StudentIdNumber.Text;
			var gender = (Gender)Enum.Parse(typeof(Gender), (String)GenderPicker.SelectedItem);

			_student.Name = name;
			_student.Surname = surname;
			_student.StudentIdNumber = studentIdNumber;
			_student.Gender = gender;

			if (_isNewStudent)
			{
				_dataManager.Students.Add(_student);
			}

			_dataManager.SaveChanges();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Kïûda", "Neizdevâs saglabât: " + ex.Message, "Ok");
		}

		await Navigation.PopAsync();
	}

	public async void OnCancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}