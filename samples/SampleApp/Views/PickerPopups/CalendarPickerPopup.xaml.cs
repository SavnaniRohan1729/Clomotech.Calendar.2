using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
namespace SampleApp.Views;

public partial class CalendarPickerPopup : Popup
{
	readonly Action<CalendarPickerResult> onClosedPopup;

	public CalendarPickerPopup(Action<CalendarPickerResult> onClosedPopup)
	{
		this.onClosedPopup = onClosedPopup;
		InitializeComponent();
		this.Opened += UponOpened;
		this.Closed += UponClosed;

		if (BindingContext is CalendarPickerPopupViewModel vm)
		{
			// Set reference to this popup
			vm.ParentPopup = this;

			// Or subscribe to the closure event
			vm.PopupClosureRequested += (sender, result) =>
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					await this.CloseAsync(result);
				});
			};
		}
	}

	public void UponClosed(object sender, PopupClosedEventArgs e)
	{
		if (BindingContext is CalendarPickerPopupViewModel vm)
		{
			vm.Closed -= onClosedPopup;
		}
	}

	public void UponOpened(object sender, EventArgs e)
	{
		if (BindingContext is CalendarPickerPopupViewModel vm)
		{
			vm.Closed += onClosedPopup;
		}
	}
}