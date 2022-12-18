using Aiyo_Core.Utilities.ErrorManagement;
using Aiyo_Core.Utilities.Constants;

namespace Aiyo_Client;

public partial class AppShell : Shell
{
    private const string ApplicationLifeFile = "Aiyo_life.dat";
    private static bool appStarted = false;

    private string ApplicationLifePath
    {
        get
        {
            var basePath = AiyoCoreConstants.DIAGNOSTICS_DIR;
            return $"{basePath}\\{ApplicationLifeFile}";
        }
    }


    public AppShell()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!appStarted)
        {
            appStarted = true;
            CheckForApplicationCrash();
            AppShell.Current.Window.Destroying += Window_Destroying;
        }
    }

    private void Window_Destroying(object sender, EventArgs e)
    {
        if (File.Exists(ApplicationLifePath))
        {
            File.Delete(ApplicationLifePath);
        }
        Logger.Log("App closed cleanly");
    }

    private void CheckForApplicationCrash()
    {
        Logger.Log("Checking for application crash");
        if (File.Exists(ApplicationLifePath))
        {
            Logger.Log("Application crash detected");
            TrySendingReport();
        }

        File.WriteAllText(ApplicationLifePath, DateTime.Now.ToString());
    }

    private void TrySendingReport()
    {
        Logger.Log("Application crash report detected");
        //Todo:Send Report here
    }
}
