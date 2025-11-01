using ToDoManagerMobile.Models;
using ToDoManagerMobile.Services;

namespace ToDoManagerMobile.Views;

public partial class DetailToDoPage : ContentPage
{
    private readonly ToDoApiService _service;
    private ToDoTask _task;

    public DetailToDoPage(ToDoTask task)
    {
        InitializeComponent();
        _service = new ToDoApiService();
        _task = task;
        BindingContext = _task;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // You can refresh data here if needed
    }
}