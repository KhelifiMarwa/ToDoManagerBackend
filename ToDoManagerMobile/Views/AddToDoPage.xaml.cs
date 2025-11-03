using ToDoManagerMobile.Models;
using ToDoManagerMobile.Services;

namespace ToDoManagerMobile.Views;

public partial class AddToDoPage : ContentPage
{
	
         private readonly ToDoApiService _api = new();

    public AddToDoPage()
    {
        InitializeComponent();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlert("Error", "Title is required.", "OK");
            return;
        }

        var newTask = new ToDoTask
        {
            Title = TitleEntry.Text,
            Description = DescriptionEntry.Text,
            DueDate = DueDatePicker.Date,
            IsCompleted = CompletedCheckBox.IsChecked
        };

        try
        {
            var createdTask = await _api.CreateAsync(newTask);
            await DisplayAlert("Success", $"Task '{createdTask.Title}' added!", "OK");
            TitleEntry.Text = string.Empty;
            DescriptionEntry.Text = string.Empty;
            // Après avoir ajouté ou édité la tâche
            await Shell.Current.GoToAsync("///tasks");

            //await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add task:\n{ex.Message}", "OK");
        }
    }
}
