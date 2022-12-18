using Aiyo_Core.Utilities.Constants;
using Aiyo_Core.Utilities.ErrorManagement;

namespace Aiyo_Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
