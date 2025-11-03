using System.Threading.Tasks;
using ToDoManagerMobile.Models;
using ToDoManagerMobile.Services;

namespace ToDoManagerMobile.Views;

[QueryProperty(nameof(TaskId), "taskId")]
public partial class EditToDoPage : ContentPage
{
    private readonly ToDoApiService _apiService;
    private ToDoTask _task;
    public EditToDoPage()
	{
		InitializeComponent();
      _apiService = new ToDoApiService();
        //_task = task;
        //TitleEntry.Text = task.Title;
        //DescriptionEditor.Text = task.Description;
        //DueDatePicker.Date = task.DueDate;
        //IsCompletedCheckBox.IsChecked = task.IsCompleted;
    }
    public string TaskId
    {
        get => _taskId;
        set
        {
            _taskId = value;
            if (int.TryParse(value, out int id))
                LoadTask(id);
        }
    }
    private string _taskId;

    private async void LoadTask(int id)
    {
        _task = await _apiService.GetByIdAsync(id);
        if (_task != null)
        {
            TitleEntry.Text = _task.Title;
            DescriptionEditor.Text = _task.Description;
            DueDatePicker.Date = _task.DueDate;
            IsCompletedCheckBox.IsChecked = _task.IsCompleted;
        }
    }


    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _task.Title = TitleEntry.Text;
        _task.Description = DescriptionEditor.Text;
        _task.DueDate = DueDatePicker.Date;
        _task.IsCompleted = IsCompletedCheckBox.IsChecked;

        try
        {
            await _apiService.UpdateAsync(_task);
            await DisplayAlert("Success", "Task updated successfully!", "OK");
            await Shell.Current.GoToAsync("///tasks");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to update: {ex.Message}", "OK");
        }
    }
}