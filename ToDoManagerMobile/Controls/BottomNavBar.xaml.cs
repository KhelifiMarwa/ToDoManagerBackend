using Microsoft.Maui;

namespace ToDoManagerMobile.Controls;

public partial class BottomNavBar : ContentView
{
    private List<Image> _icons;

    public BottomNavBar()
    {
        InitializeComponent();

        // 1️⃣ Lister toutes les icônes
        _icons = new List<Image> { HomeIcon, TasksIcon, SettingsIcon, ProfileIcon };

        // 2️⃣ Sélectionner Home par défaut (opacity à 1)
        SetSelectedIcon(HomeIcon);

        // 3️⃣ Naviguer vers Home après que le ContentView est attaché à la page
        this.Loaded += async (s, e) =>
        {
            if (Shell.Current != null)
                await Shell.Current.GoToAsync("///home");
        };
    }

    private async void OnNavItemTapped(object sender, EventArgs e)
    {
        if (sender is Image tappedIcon)
        {
            // Animation
            await tappedIcon.ScaleTo(1.2, 100);
            await tappedIcon.ScaleTo(1, 100);

            // Changer icône sélectionnée
            SetSelectedIcon(tappedIcon);

            // Naviguer vers la page correspondante
            string route = tappedIcon == HomeIcon ? "home" :
                           tappedIcon == TasksIcon ? "tasks" :
                           tappedIcon == SettingsIcon ? "settings" :
                           tappedIcon == ProfileIcon ? "profile" : null;

            if (!string.IsNullOrEmpty(route))
                await Shell.Current.GoToAsync($"///{route}");
        }
    }

    private void SetSelectedIcon(Image selectedIcon)
    {
        foreach (var icon in _icons)
            icon.Opacity = 0.5; // non sélectionné
        selectedIcon.Opacity = 1; // sélectionné
    }
}

