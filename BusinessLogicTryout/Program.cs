using Eto.Forms;
using BusinessLogicTryout.Controllers;

public class MyApp : Form
{
    public MainFormController MainForm;
    public MyApp()
    {
        MainForm = new MainFormController();
    }

    [STAThread]
    static void Main()
    {
        new Application().Run(new MyApp().MainForm);
    }
}