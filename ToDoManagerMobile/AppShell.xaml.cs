using ToDoManagerMobile.Views;

namespace ToDoManagerMobile
{
    public partial class AppShell :  SimpleToolkit.SimpleShell.SimpleShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddToDoPage), typeof(AddToDoPage));
        }
      
    }
}
