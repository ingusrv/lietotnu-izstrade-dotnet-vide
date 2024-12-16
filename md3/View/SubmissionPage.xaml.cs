using md3.ViewModel;

namespace md3;

public partial class SubmissionPage : ContentPage
{
	private readonly SubmissionViewModel _viewModel;

	public SubmissionPage(SubmissionViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}
}