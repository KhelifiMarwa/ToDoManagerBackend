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

    private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ToDoTask selectedTask)
        {
            await Navigation.PushAsync(new DetailToDoPage(selectedTask));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}