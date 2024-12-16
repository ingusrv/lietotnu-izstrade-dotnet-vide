using CourseManagementLib;
using md3.ViewModel;

namespace md3;

public partial class AssignmentPage : ContentPage
{
	private readonly AssignmentViewModel _viewModel;

	public AssignmentPage(AssignmentViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}
}