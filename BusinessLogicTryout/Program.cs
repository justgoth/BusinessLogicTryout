using BusinessLogicTryout.Controllers;
using Eto.Forms;

namespace BusinessLogicTryout;

public class MyApp : Form
{
    private readonly MainFormController MainForm;

    private MyApp()
    {
        MainForm = new MainFormController();
    }

    [STAThread]
    static void Main()
    {
        new Application().Run(new MyApp().MainForm);
    }
}