namespace md2;

public partial class TeacherEditPage : ContentPage
{
	private readonly Teacher _teacher;
	private readonly bool _isNewTeacher;
	private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();

	public TeacherEditPage(Teacher teacher, bool isNewTeacher)
	{
		InitializeComponent();

		_teacher = teacher;
		_isNewTeacher = isNewTeacher;

		GenderPicker.SelectedIndex = 0;

		if (!_isNewTeacher)
		{
			Name.Text = _teacher.Name;
			Surname.Text = _teacher.Surname;
			GenderPicker.SelectedItem = _teacher.Gender.ToString();
			ContractDate.Date = DateTime.Parse(_teacher.ContractDate.ToString());
		}
	}

	public async void OnSaveClicked(object sender, EventArgs e) {
		var name = Name.Text;
		var surname = Surname.Text;
		var gender = (Gender)Enum.Parse(typeof(Gender), (String)GenderPicker.SelectedItem);
		var contractDate = DateOnly.FromDateTime(ContractDate.Date);

		_teacher.Name = name;
		_teacher.Surname = surname;
		_teacher.Gender = gender;
		_teacher.ContractDate = contractDate;

		if (_isNewTeacher)
		{
			_dataManager.Data.Teachers.Add(_teacher);
		}

		await Navigation.PopAsync();
	}

	public async void OnCancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}