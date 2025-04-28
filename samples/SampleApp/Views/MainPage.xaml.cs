using CommunityToolkit.Maui.Views;
using Mopups.Services;

namespace SampleApp.Views;


public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
		BindingContext = this;
    }

	string text = DateTime.Today.Date.ToString();
	public string Text
	{
		get => text;
		set
		{
			if (value != text)
			{
				text = value;
				OnPropertyChanged(nameof(Text));
			}
		}
	}

	async void PickerPopup(object sender, EventArgs e)
	{
		var popup = new CalendarPickerPopup(calendarPickerResult =>
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				string message = calendarPickerResult.IsSuccess
					? $"Received date from popup: {calendarPickerResult.SelectedDate:dd/MM/yy}"
					: "Calendar Picker Canceled!";
				await AppShell.Current.DisplayAlert("Popup result", message, "Ok");
			});
		});

		await this.ShowPopupAsync(popup);
	}

}
