using md3.ViewModel;

namespace md3;

public partial class DataPage : ContentPage
{
	private readonly DataViewModel _vm;

	public DataPage(DataViewModel vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
	}
}