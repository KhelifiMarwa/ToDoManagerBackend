using ToDoManagerMobile.Models;
using ToDoManagerMobile.Services;

namespace ToDoManagerMobile.Views;

public partial class TasksPage : ContentPage
{
    private readonly ToDoApiService _apiService;

    public TasksPage()
	{
		InitializeComponent();
        _apiService = new ToDoApiService();
        LoadTasks();
    }
    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddToDoPage));
    }
    private async void LoadTasks()
    {
        var tasks = await _apiService.GetAllAsync();
        TasksCollection.ItemsSource = tasks;
    }
    private async Task LoadTasksAsync()
    {
        try
        {
            var tasks = await _apiService.GetAllAsync();
            TasksCollection.ItemsSource = tasks;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load tasks: {ex.Message}", "OK");
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is int id)
        {
            bool confirm = await DisplayAlert("Confirm", "Delete this task?", "Yes", "No");
            if (!confirm)
                return;

            try
            {
                await _apiService.DeleteAsync(id);
                await DisplayAlert("Deleted", "Task removed successfully", "OK");
                await LoadTasksAsync(); // refresh list
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete: {ex.Message}", "OK");
            }
        }
    }


    private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ToDoTask selectedTask)
        {
            await Navigation.PushAsync(new DetailToDoPage(selectedTask));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}