using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace CâblageCeff.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";

        public delegate Task ShowPatchDialogFunc();
        public ShowPatchDialogFunc ShowPatchDialog { get; set; } = null!;

        [RelayCommand]
        private async Task OpenPatchScreen()
        {
            if (ShowPatchDialog == null)
                return;
            await ShowPatchDialog();
        }

        [RelayCommand]
        private void ChangeTheme()
        {
            Application.Current!.RequestedThemeVariant = Application.Current.RequestedThemeVariant == ThemeVariant.Light
                ? ThemeVariant.Dark
                : ThemeVariant.Light;
            //System.Environment.Exit(0);
        }
    }
}
