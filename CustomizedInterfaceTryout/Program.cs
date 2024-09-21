using Eto.Forms;
using Eto.Drawing;

using CustomizedInterfaceTryout.Objects;

public class MyApp : Form
{
    public MyApp()
    {
        CObject itemObject = new CObject("Изделие", "Объект - изделие");
        itemObject.AddParameter(new CParameter("Наименование", "Наименование изделия", ParameterTypes.String));
        

        CObject itemTypeObject = new CObject("Тип изделия", "Объект - элемент справочника типов изделия");
        itemTypeObject.AddVisibleValueParameter(new CParameter("Тип", "Тип изделия", ParameterTypes.String));
        
        itemObject.AddParameter(new CParameter("Тип", "Тип изделия", ParameterTypes.ListItem, itemTypeObject));
        
        ObjectInstance itemTypeMake = new ObjectInstance(itemTypeObject);
        itemTypeMake.SetParameterValueByName("Тип", "Изготавливаемое");
        ObjectInstance itemTypeBuy = new ObjectInstance(itemTypeObject);
        itemTypeBuy.SetParameterValueByName("Тип", "Закупаемое");
        
        ObjectInstance itemInstance = new ObjectInstance(itemObject);
        itemInstance.SetParameterValueByName("Наименование", "Тестовое изделие");
        itemInstance.SetParameterValueByName("Тип", itemTypeMake);
        
        Title = "Customized Interface Tryout";
        ClientSize = new Size(800, 600);
        var container = new DynamicLayout();
        container.BeginVertical();
        foreach (ObjectParameter parameter in itemInstance.Parameters)
        {
            Console.WriteLine(parameter.Description);
            container.AddRow(new Label { Text = parameter.Description + ":" }, new TextBox { Text = (parameter.GetVisibleValue() ?? "")});
        }

        container.AddRow();
        container.EndVertical();
        
        Content = container;
    }

    [STAThread]
    static void Main()
    {
        new Application().Run(new MyApp());
    }
}