using System.Diagnostics;

namespace md2;

public partial class StudentEditPage : ContentPage
{
	private readonly Student _student;
	private readonly bool _isNewStudent;
	private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();

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
			_dataManager.Data.Students.Add(_student);
		}

		await Navigation.PopAsync();
	}

	public async void OnCancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}