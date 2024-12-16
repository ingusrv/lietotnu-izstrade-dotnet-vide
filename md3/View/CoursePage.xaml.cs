using md3.ViewModel;

namespace md3;

public partial class CoursePage : ContentPage
{
	private readonly CourseViewModel _viewModel;

	public CoursePage(CourseViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}
}