using md3.ViewModel;

namespace md3;

public partial class StudentPage : ContentPage
{
	private readonly StudentViewModel _viewModel;

	public StudentPage(StudentViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}
}