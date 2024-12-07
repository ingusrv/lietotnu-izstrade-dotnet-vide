using CourseManagementLib;
using System.ComponentModel;
using System.Diagnostics;

namespace md3;

public partial class SubmissionEditPage : ContentPage
{
	private readonly Submission _submission;
	private readonly bool _isNewSubmission;
	//private readonly IDataManager _dataManager = DependencyService.Get<IDataManager>();
	private readonly DBDataManager _dataManager = App.DBDataManager;

	public SubmissionEditPage(Submission submission, bool isNewSubmission)
	{
		InitializeComponent();

		_submission = submission;
		_isNewSubmission = isNewSubmission;

		StudentPicker.ItemsSource = _dataManager.Students.ToList();
		AssignmentPicker.ItemsSource = _dataManager.Assignments.ToList();

		if (!isNewSubmission)
		{
			StudentPicker.SelectedItem = _submission.Student;
			AssignmentPicker.SelectedItem = _submission.Assignment;

			SubmissionDate.Date = _submission.SubmissionTime;
			// https://stackoverflow.com/questions/17959440/convert-datetime-to-timespan
			// iepriekš nezināju kā pārveidot, jo time picker prasa TimeSpan
			SubmissionTime.Time = _submission.SubmissionTime.TimeOfDay;
			Score.Text = _submission.Score.ToString();
		}
	}

	public async void OnSaveClicked(object sender, EventArgs e) {
		try {
			var d = DateOnly.FromDateTime(SubmissionDate.Date);
			var t = TimeOnly.FromTimeSpan(SubmissionTime.Time);
			var date = new DateTime(d, t);
			var assignment = AssignmentPicker.SelectedItem as Assignment;
			var student = StudentPicker.SelectedItem as Student;
			int score;
			try
			{
				score = int.Parse(Score.Text);
			}
			catch
			{
				await DisplayAlert("Kļūda", "Vērtējuma vērtība nav pareiza!", "Ok");
				return;
			}

			if (assignment == null)
			{
				await DisplayAlert("Kļūda", "Lūdzu izvēlaties uzdevumu!", "Ok");
				return;
			}
			if (student == null)
			{
				await DisplayAlert("Kļūda", "Lūdzu izvēlaties studentu!", "Ok");
				return;
			}

			_submission.SubmissionTime = date;
			_submission.Student = student;
			_submission.Assignment = assignment;
			_submission.Score = score;

			if (_isNewSubmission)
			{
				_dataManager.Submissions.Add(_submission);
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