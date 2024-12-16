using md3.ViewModel;

namespace md3;

public partial class TeacherPage : ContentPage
{
	private readonly TeacherViewModel _viewModel;

	public TeacherPage(TeacherViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}
}