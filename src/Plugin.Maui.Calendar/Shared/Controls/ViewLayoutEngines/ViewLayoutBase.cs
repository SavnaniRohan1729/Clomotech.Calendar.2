using System.Globalization;
using System.Windows.Input;
using Plugin.Maui.Calendar.Models;

namespace Plugin.Maui.Calendar.Controls.ViewLayoutEngines;

abstract class ViewLayoutBase(DayOfWeek firstDayOfWeek)
{
	protected const int numberOfDaysInWeek = 7;

	protected DateTime GetFirstDateOfWeek(DateTime dateInWeek)
	{
		var difference = (7 + (dateInWeek.DayOfWeek - firstDayOfWeek)) % 7;
		return dateInWeek.AddDays(-1 * difference).Date;
	}

	protected static Grid GenerateWeekLayout(
			List<DayView> dayViews,
			object bindingContext,
			string daysTitleLabelStyleeBindingName,
			ICommand dayTappedCommand,
			int numberOfWeeks
	)
	{
		var rowDefinition = new RowDefinition();

		var grid = new Grid
		{
			ColumnSpacing = 0d,
			RowSpacing = 3d,
			RowDefinitions =
			[
				rowDefinition,
			],
			ColumnDefinitions =
			{
				// Week number column
				new ColumnDefinition(){ Width = GridLength.Star },
				// Day columns
				new ColumnDefinition(){ Width = GridLength.Star},
				new ColumnDefinition(){ Width = GridLength.Star},
				new ColumnDefinition(){ Width = GridLength.Star},
				new ColumnDefinition(){ Width = GridLength.Star},
				new ColumnDefinition(){ Width = GridLength.Star},
				new ColumnDefinition(){ Width = GridLength.Star},
				new ColumnDefinition(){ Width = GridLength.Star},
			}
		};

		for (int i = 0; i < numberOfDaysInWeek; i++)
		{
			var label = new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				BindingContext = bindingContext
			};
			label.SetBinding(VisualElement.StyleProperty, daysTitleLabelStyleeBindingName);

			grid.Add(label, i + 1, 0);
		}

		dayViews.Clear();

		for (int i = 1; i <= numberOfWeeks; i++)
		{
			rowDefinition = new RowDefinition();
			grid.RowDefinitions.Add(rowDefinition);

			var weekLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center
			};
			grid.Add(weekLabel, 0, i);

			for (int ii = 0; ii < 7; ii++)
			{
				var dayView = new DayView();
				var dayModel = new DayModel();
				dayView.BindingContext = dayModel;
				dayModel.DayTappedCommand = dayTappedCommand;

				dayViews.Add(dayView);
				grid.Add(dayView, ii + 1, i);
			}
		}

		return grid;
	}
}
