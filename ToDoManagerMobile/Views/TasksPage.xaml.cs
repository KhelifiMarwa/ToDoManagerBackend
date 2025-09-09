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
    private async void LoadTasks()
    {
        var tasks = await _apiService.GetAllAsync();
        TasksCollection.ItemsSource = tasks;
    }

    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        var newTask = new ToDoTask
        {
            Title = "Nouvelle tâche",
            Description = "Description...",
            IsCompleted = false,
            DueDate = DateTime.Now.AddDays(1)
        };

        await _apiService.CreateAsync(newTask);
        LoadTasks(); // Recharge la liste
    }
}