using ToDoManagerMobile.Services;

namespace ToDoManagerMobile.Views;

public partial class HomePage : ContentPage
{
    private readonly ToDoApiService _apiService = new();
    public HomePage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadRecentTasksAsync();
        LoadTaskSummaryAsync();
    }
    private async Task LoadTaskSummaryAsync()
    {
        try
        {
            var tasks = await _apiService.GetAllAsync();
            int total = tasks.Count();
            int completed = tasks.Count(t => t.IsCompleted);
            int pending = total - completed;
            TotalLabel.Text = total.ToString();
            CompletedLabel.Text = completed.ToString();
            PendingLabel.Text = pending.ToString();
            RecentTasksCollection.ItemsSource = tasks
                .OrderByDescending(t => t.CreatedAt) 
                .Take(5)
                .ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load tasks:\n{ex.Message}", "OK");
        }
    }




    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///addtodo");

        //await Shell.Current.GoToAsync(nameof(AddToDoPage));
    }

    private async void OnViewTasksClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TasksPage));
    }


    private async Task LoadRecentTasksAsync()
    {
        try
        {
            var recent = await _apiService.GetRecentAsync();
            RecentTasksCollection.ItemsSource = recent;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load recent tasks: {ex.Message}", "OK");
        }
    }
}