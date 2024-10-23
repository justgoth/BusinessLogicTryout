using System.Collections.ObjectModel;
using BusinessLogicTryout.Actions;
using Eto.Forms;
using Eto.Drawing;

namespace BusinessLogicTryout.Controllers;

using BusinessLogicTryout.Objects;
using BusinessLogicTryout.Repositories;

public class MainFormController : Form
{
    private DataContext _context;
    private TabControl _ctabcontrol;
    private TabPage _cparameterspage;
    private DynamicLayout _cparameterslayout;
    private TabPage _cobjectspage;
    private DynamicLayout _cobjectslayout;
    private TabPage _cinstancepage;
    private DynamicLayout _cinstancelayout;
    private TabPage _actionspage;
    private DynamicLayout _actionslayout;

    private GridView _parametergridview;
    private TextBox _parametername;
    private TextBox _parameterdescription;
    private DropDown _parametertype;
    private Button _parametersave;
    private Button _parameteraddnew;
    private ListBox _parameterassociatedobject;

    private GridView _objectgridview;
    private TextBox _objectname;
    private TextBox _objectdescription;
    private GridView _objectparametergridview;
    private Button _objectsave;
    private Button _objectaddnew;
    private Button _objectcreateinstance;
    private GridView _objectparameterlistgridview;
    private GridView _objectparametersgridview;
    private DynamicLayout _objectparameterslayout;

    private GridView _actionsgridview;
    private Button _actionrun;

    private GridView _instancegridview;
    private GridView _instancedetailsgridview;

    private Button _addparameter;
    private Button _removeparameter;

    private CParameter _curparameter;
    private CObject _curobject;

    public MainFormController()
    {
        InitializeData();
        Title = "Демонстрация идеи с универсальными объектами";
        ClientSize = new Size(1600, 1000);
        Maximizable = false;
        Resizable = false;
        InitializeControllers();
    }

    private void InitializeWithItems()  // инициализирует с набором объектов для управления изделиями
    {
        // тип объекта - изделие
        CObject itemObject = _context.CObjects.AddNewObject("Изделие", "Объект - изделие");
        itemObject.AddVisibleValueParameter(_context.CParameters.AddNewParameter("Обозначение", "Обозначение изделия", _context.CParameters.Types.GetById(2)));
        itemObject.AddParameter(_context.CParameters.AddNewParameter("Наименование", "Наименование изделия",
            _context.CParameters.Types.GetById(2)));
        // тип объекта - тип изделия
        CObject itemTypeObject =
            _context.CObjects.AddNewObject("Тип изделия", "Объект - элемент справочника типов изделия");
        itemTypeObject.AddVisibleValueParameter(_context.CParameters.AddNewParameter("Тип",
            "Тип изделия (значение справочника)", _context.CParameters.Types.GetById(2)));
        // тип объекта - склад
        CObject storeObject = _context.CObjects.AddNewObject("Склад", "Объект - склад (для справочника складов)");
        storeObject.AddVisibleValueParameter(_context.CParameters.AddNewParameter("Имя склада", "Имя склада", _context.CParameters.Types.GetById(2)));

        itemObject.AddParameter(_context.CParameters.AddNewParameter("Тип", "Тип изделия (для изделия)",
            _context.CParameters.Types.GetById(4), itemTypeObject));
        itemObject.AddParameter(_context.CParameters.AddNewParameter("Основной склад", "Склад, на который изделие принимается по-умолчанию", _context.CParameters.Types.GetById(4), storeObject));

        // типы изделия
        ObjectInstance itemTypeMake = _context.Objects.AddNewObjectInstance(itemTypeObject);
        itemTypeMake.SetParameterValueByName("Тип", "Изготавливаемое");
        ObjectInstance itemTypeBuy = _context.Objects.AddNewObjectInstance(itemTypeObject);
        itemTypeBuy.SetParameterValueByName("Тип", "Закупаемое");
        // склад
        ObjectInstance storeMain = _context.Objects.AddNewObjectInstance(storeObject);
        storeMain.SetParameterValueByName("Имя склада", "Главный склад предприятия");
        _context.Objects.AddNewObjectInstance(storeObject).SetParameterValueByName("Имя склада", "Локальный склад участка сборки");
        // изделие
        ObjectInstance itemInstance = _context.Objects.AddNewObjectInstance(itemObject);
        itemInstance.SetParameterValueByName("Обозначение", "TI000024");
        itemInstance.SetParameterValueByName("Наименование", "Тестовое изделие");
        itemInstance.SetParameterValueByName("Тип", itemTypeMake);
        itemInstance.SetParameterValueByName("Основной склад", storeMain);
    }

    private void InitializeWithHR() // инициализирует с набором объектов для управления персоналом
    {
        // тип объекта - подразделение
        CObject departmentObject = _context.CObjects.AddNewObject("Отдел", "Отдел (элемент справочника)");
        departmentObject.AddVisibleValueParameter(_context.CParameters.AddNewParameter("Наименование", "Наименование отдела", _context.CParameters.Types.GetByName("Строка")));
        // и 2 подразделения
        ObjectInstance dep1 = _context.Objects.AddNewObjectInstance(departmentObject);
        dep1.SetParameterValueByName("Наименование", "Бухгалтерия");
        ObjectInstance dep2 = _context.Objects.AddNewObjectInstance(departmentObject);
        dep2.SetParameterValueByName("Наименование", "Юридический отдел");

        // тип объекта - должность
        CObject positionObject = _context.CObjects.AddNewObject("Должность", "Должность (элемент справочника)");
        positionObject.AddVisibleValueParameter(_context.CParameters.AddNewParameter("Наименование", "Наименование должности", _context.CParameters.Types.GetByName("Строка")));
        positionObject.AddParameter(_context.CParameters.AddNewParameter("Заработная плата", "Размер заработной платы (до налогообложения)", _context.CParameters.Types.GetByName("Дробное число")));
        // и 2 должности
        ObjectInstance pos1 = _context.Objects.AddNewObjectInstance(positionObject);
        pos1.SetParameterValueByName("Наименование", "Руководитель");
        pos1.SetParameterValueByName("Заработная плата", 200000);
        ObjectInstance pos2 = _context.Objects.AddNewObjectInstance(positionObject);
        pos2.SetParameterValueByName("Наименование", "Бухгалтер");
        pos2.SetParameterValueByName("Заработная плата", 120000);
        
        // тип объекта - сотрудник
        CObject personObject = _context.CObjects.AddNewObject("Работник", "Сотрудник");
        personObject.AddParameter(_context.CParameters.AddNewParameter("Табельный номер", "Табельный номер сотрудника", _context.CParameters.Types.GetByName("Целое число")));
        personObject.AddVisibleValueParameter(_context.CParameters.AddNewParameter("Фамилия", "Фамилия сотрудника", _context.CParameters.Types.GetByName("Строка")));
        personObject.AddParameter(_context.CParameters.AddNewParameter("Имя", "Имя сотрудника", _context.CParameters.Types.GetByName("Строка")));
        personObject.AddParameter(_context.CParameters.AddNewParameter("Отчество", "Отчество сотрудника", _context.CParameters.Types.GetByName("Строка")));
        personObject.AddParameter(_context.CParameters.AddNewParameter("Подразделение", "Подразделение, в которое трудоустроен сотрудник", _context.CParameters.Types.GetByName("Выбор из списка"), departmentObject));
        personObject.AddParameter(_context.CParameters.AddNewParameter("Должность", "Должность, занимаемая сотрудником", _context.CParameters.Types.GetByName("Выбор из списка"), positionObject));
        personObject.AddParameter(_context.CParameters.AddNewParameter("Заработная плата",
            "Заработная плата сотрудника до налогообложения", _context.CParameters.Types.GetByName("Дробное число")));
        // и 2 сотрудника
        ObjectInstance pers1 = _context.Objects.AddNewObjectInstance(personObject);
        pers1.SetParameterValueByName("Табельный номер", 1);
        pers1.SetParameterValueByName("Фамилия", "Уваров");
        pers1.SetParameterValueByName("Имя", "Михаил");
        pers1.SetParameterValueByName("Отчество", "Михайлович");
        ObjectInstance pers2 = _context.Objects.AddNewObjectInstance(personObject);
        pers2.SetParameterValueByName("Табельный номер", 666);
        pers2.SetParameterValueByName("Фамилия", "Иванов");
        pers2.SetParameterValueByName("Имя", "Иван");
        pers2.SetParameterValueByName("Отчество", "Иваныч");
        
        // тип объекта - документ
        CObject docObject =
            _context.CObjects.AddNewObject("Приказ о назначении", "Документ - приказ о назначении на должность");
        docObject.AddVisibleValueParameter(_context.CParameters.AddNewParameter("Номер приказа", "Номер приказа о назначении", _context.CParameters.Types.GetByName("Целое число")));
        docObject.AddParameter(_context.CParameters.AddNewParameter("Сотрудник", "Назначаемый на должность сотрудник", _context.CParameters.Types.GetByName("Выбор из списка"), personObject));
        docObject.AddParameter(_context.CParameters.AddNewParameter("Должность", "Назначается на должность", _context.CParameters.Types.GetByName("Выбор из списка"), positionObject));
        docObject.AddParameter(_context.CParameters.AddNewParameter("Подразделение", "В подразделение", _context.CParameters.Types.GetByName("Выбор из списка"), departmentObject));
        // и экземпляр документа
        ObjectInstance doc1 = _context.Objects.AddNewObjectInstance(docObject);
        doc1.SetParameterValueByName("Номер приказа", 666);
        doc1.SetParameterValueByName("Сотрудник", pers2);
        doc1.SetParameterValueByName("Должность", pos2);
        doc1.SetParameterValueByName("Подразделение", dep1);
        
        // шаблон действия "подготовить назначение" - выбирается номер приказа, сотрудник, должность, подразделение - создаётся приказ
        CAction hireAction1 =
            _context.CActions.AddNewAction("Подготовить назначение", "Создать приказ о назначении на должность");
        ActionObject? hireAction1doc = hireAction1.AddObject(docObject, _context.CActions.ObjectTypes.GetByName("Создаётся"));
        hireAction1.AddParameter(docObject.GetParameterByName("Номер приказа"), hireAction1doc, true, true, true);
        hireAction1.AddParameter(docObject.GetParameterByName("Сотрудник"), hireAction1doc, true, true, true);
        hireAction1.AddParameter(docObject.GetParameterByName("Должность"), hireAction1doc, true, true, true);
        hireAction1.AddParameter(docObject.GetParameterByName("Подразделение"), hireAction1doc, true, true, true);
        ActionResult hireAction1OkResult = new ActionResult("Создать приказ");
        hireAction1OkResult.AddAutomation(_context.CActions.AutomationTypes.GetByName("Сохранить объект"), null, null,
            hireAction1doc);
        hireAction1.AddResult(hireAction1OkResult);
        ActionResult hireAction1CancelResult = new ActionResult("Не создавать приказ");
        hireAction1CancelResult.AddAutomation(_context.CActions.AutomationTypes.GetByName("Завершить действие"), null, null, null);
        hireAction1.AddResult(hireAction1CancelResult);

        // шаблон действия "провести назначение" - проводится приказ, сотрудник назначается на должность
        CAction hireAction2 =
            _context.CActions.AddNewAction("Провести назначение",
                "Назначить сотрудника на должность в соответствии с приказом");
        ActionObject? hireAction2doc =
            hireAction2.AddObject(docObject, _context.CActions.ObjectTypes.GetByName("Выбирается"));
        hireAction2.AddParameter(docObject.GetParameterByName("Номер приказа"), hireAction2doc, false, true, true);
        ActionParameter? hireAction2PersonParameter = hireAction2.AddParameter(docObject.GetParameterByName("Сотрудник"), hireAction2doc, false, true, true);
        ActionParameter? hireAction2PositionParameter = hireAction2.AddParameter(docObject.GetParameterByName("Должность"), hireAction2doc, false, true, true);
        ActionParameter? hireAction2DepartmentParameter = hireAction2.AddParameter(docObject.GetParameterByName("Подразделение"), hireAction2doc, false, true, true);
        ActionObject? hireAction2person = hireAction2.AddObject(personObject, _context.CActions.ObjectTypes.GetByName("На основании"));
        hireAction2person.SourceParameter = hireAction2PersonParameter;
        ActionObject? hireAction2position =
            hireAction2.AddObject(positionObject, _context.CActions.ObjectTypes.GetByName("На основании"));
        hireAction2position.SourceParameter = hireAction2PositionParameter;
        ActionObject? hireAction2department =
            hireAction2.AddObject(positionObject, _context.CActions.ObjectTypes.GetByName("На основании"));
        hireAction2department.SourceParameter = hireAction2DepartmentParameter;
        ActionParameter? hireAction2PersonPosition = hireAction2.AddParameter(hireAction2person.Object.GetParameterByName("Должность"), 
            hireAction2person, false, false, false);
        ActionParameter? hireAction2PersonDepartment = hireAction2.AddParameter(hireAction2person.Object.GetParameterByName("Подразделение"), hireAction2person,
            false, false, false);
        ActionParameter? hireAction2PositonSalary = hireAction2.AddParameter(hireAction2person.Object.GetParameterByName("Заработная плата"), hireAction2position,
            false, false, false);
        ActionParameter? hireAction2PersonSalary = hireAction2.AddParameter(hireAction2person.Object.GetParameterByName("Заработная плата"), hireAction2person,
            false, false, false);
        ActionResult hireAction2OkResult = new ActionResult("Провести приказ");
        hireAction2OkResult.AddAutomation(_context.CActions.AutomationTypes.GetByName("Принять значение параметра"), hireAction2PersonPosition, hireAction2PositionParameter,
            hireAction2person);
        hireAction2OkResult.AddAutomation(_context.CActions.AutomationTypes.GetByName("Принять значение параметра"), hireAction2PersonDepartment, hireAction2DepartmentParameter,
            hireAction2person);
        hireAction2OkResult.AddAutomation(_context.CActions.AutomationTypes.GetByName("Обновить объект"), null, null,
            hireAction2person);
        hireAction2.AddResult(hireAction2OkResult);
        ActionResult hireAction2CancelResult = new ActionResult("Не проводить приказ");
        hireAction2.AddResult(hireAction2CancelResult);
    }
    private void InitializeData()   // инициализирует данные
    {
        _context = new DataContext();

        InitializeWithHR();
    }

    private void InitializeControllers()    // инициализирует страницы интерфейса
    {
        _ctabcontrol = new TabControl();
        InitializeCParametersPage();
        InitializeCObjectsPage();
        InitializeInstancePage();
        InitializeActionsPage();
        Content = _ctabcontrol;
    }

    private void InitializeActionsPage()    // инициализирует страницу управления действиями
    {
        _actionspage = new TabPage { Text = "Вызов действий"};
        _ctabcontrol.Pages.Add(_actionspage);

        _actionsgridview = new GridView
        {
            AllowDrop = true,
            AllowMultipleSelection = false,
            DataStore = _context.CActions.Actions,
            ShowHeader = true,
            Width = 400,
            Height = 800
        };
        _actionsgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CAction, string>(a => a.Name)
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Наименование действия",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _actionsgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CAction, string>(a => a.Description)
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Описание действия",
            Resizable = true,
            Sortable = true,
            Expand = true
        });

        _actionslayout = new DynamicLayout();
        _actionspage.Content = _actionslayout;
        _actionslayout.BeginHorizontal();
        _actionslayout.AddRow(_actionsgridview);
        _actionslayout.EndHorizontal();
        _actionslayout.BeginHorizontal();
        _actionrun = new Button
        {
            Text = "Выполнить выбранное действие"
        };
        _actionrun.Click += CActionRunClick;
        
        _actionslayout.AddRow(_actionrun);
        _actionslayout.EndHorizontal();

    }
    
    private void InitializeCParametersPage()    // инициализирует страницу управления параметрами
    {
        _cparameterspage = new TabPage();
        _cparameterspage.Text = "Управление параметрами";
        _ctabcontrol.Pages.Add(_cparameterspage);

        _parametergridview = new GridView
        {
            AllowDrop = true,
            AllowMultipleSelection = false,
            DataStore = _context.CParameters.CParameters,
            ShowHeader = true,
            Height = 400,
            Width = 1600
        };
        _parametergridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.Id.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "#",
            Resizable = true,
            Sortable = true, 
            Expand = true
        });
        _parametergridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.Name)
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Наименование параметра",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _parametergridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.Description)
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Описание параметра",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _parametergridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.Type.Name.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Тип параметра",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _parametergridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.ObjectType!.Name)
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Связанный объект",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _cparameterslayout = new DynamicLayout();
        _cparameterslayout.BeginVertical();
        _cparameterslayout.AddRow(_parametergridview);
        _cparameterslayout.EndVertical();
        _cparameterslayout.AddRow(new Label());
        _cparameterslayout.BeginVertical();
        _parametername = new TextBox { Text = "", Width = 800, Height = 40 };
        _cparameterslayout.AddRow(new Label { Text = "Наименование:   ", Width = 300, Height = 40 }, _parametername,
            new Label());
        _cparameterslayout.AddRow(new Label { Height = 10 });
        _parameterdescription = new TextBox { Text = "", Width = 800, Height = 40 };
        _cparameterslayout.AddRow(new Label { Text = "Описание:   ", Width = 300, Height = 40 }, _parameterdescription,
            new Label());
        _cparameterslayout.AddRow(new Label { Height = 10 });
        _parametertype = new DropDown { Height = 40 };
        _parametertype.DataStore = _context.CParameters.Types.Types;
        _parametertype.ItemTextBinding = Binding.Property<ParameterType, string>(p => p.Name.ToString());
        _parametertype.Width = 800;
        _parametertype.SelectedValueChanged += CParameterTypeChange;
        _cparameterslayout.AddRow(new Label { Text = "Тип:   ", Width = 300 }, _parametertype, new Label());
        _cparameterslayout.AddRow(new Label { Height = 10 });
        _parameterassociatedobject = new ListBox { Height = 300, Width = 800, Visible = false };
        _parameterassociatedobject.DataStore = _context.CObjects.CObjects;
        _parameterassociatedobject.ItemTextBinding =
            Binding.Property<CObject, string>(p => p.Name.ToString() + " (" + p.Description.ToString() + ")");
        _cparameterslayout.AddRow(new Label(), _parameterassociatedobject, new Label());
        _cparameterslayout.EndVertical();
        _cparameterslayout.BeginVertical();
        _cparameterslayout.AddRow(new Label());
        _parametersave = new Button
        {
            Width = 300,
            Text = "Сохранить изменения"
        };
        _parametersave.Click += CParametersSaveClick;
        _parameteraddnew = new Button
        {
            Width = 300,
            Text = "Добавить новый параметр"
        };
        _parameteraddnew.Click += CParametersAddNewClick;
        _cparameterslayout.AddRow(new Label { Width = 20 }, _parametersave, new Label { Width = 20 }, _parameteraddnew,
            new Label { Width = 20 });
        _cparameterslayout.AddRow();
        _cparameterslayout.EndVertical();
        _cparameterspage.Content = _cparameterslayout;
        _parametergridview.SelectedItemsChanged += CParametersGridSelectItem;
        CParametersGridSelectItem(this, null);
    }

    private void InitializeCObjectsPage()   // инициализирует страницу управления объектами
    {
        _cobjectspage = new TabPage();
        _cobjectspage.Text = "Управление объектами";
        _ctabcontrol.Pages.Add(_cobjectspage);

        _objectgridview = new GridView
        {
            AllowDrop = true,
            AllowMultipleSelection = false,
            DataStore = _context.CObjects.CObjects,
            ShowHeader = true,
            Height = 300,
            Width = 1600
        };
        _objectgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CObject, string>(c => c.Id.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "#",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _objectgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CObject, string>(c => c.Name)
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Наименование объекта",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _objectgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CObject, string>(c => c.Description)
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Описание объекта",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _objectgridview.SelectedItemsChanged += CObjectsGridSelectItem;

        _cobjectslayout = new DynamicLayout();
        _cobjectslayout.BeginVertical();
        _cobjectslayout.AddRow(_objectgridview);
        _cobjectslayout.EndVertical();
        _cobjectslayout.BeginVertical();
        _cobjectslayout.AddRow(new Label());
        _objectname = new TextBox { Text = "", Width = 800, Height = 40 };
        _cobjectslayout.AddRow(new Label { Text = "Наименование:   ", Width = 300, Height = 40 }, _objectname,
            new Label());
        _cobjectslayout.AddRow(new Label { Height = 10 });
        _objectdescription = new TextBox { Text = "", Width = 800, Height = 40 };
        _cobjectslayout.AddRow(new Label { Text = "Описание:   ", Width = 300, Height = 40 }, _objectdescription,
            new Label());
        _cobjectslayout.EndVertical();
        _cobjectslayout.BeginVertical();
        _objectsave = new Button
        {
            Width = 300,
            Text = "Сохранить изменения",
            Enabled = false
        };
        _objectsave.Click += CObjectsSaveClick;
        _objectaddnew = new Button
        {
            Width = 300,
            Text = "Добавить новый объект"
        };
        _objectaddnew.Click += CObjectsAddNewClick;
        _objectcreateinstance = new Button
        {
            Width = 300,
            Text = "Создать новый экземпляр",
            Enabled = false
        };
        _cobjectslayout.AddRow(new Label());
        _cobjectslayout.AddRow(new Label { Width = 20 }, _objectsave, new Label { Width = 20 }, _objectaddnew,
            new Label { Width = 20 }, _objectcreateinstance, new Label());
        _cobjectslayout.EndVertical();
        _cobjectslayout.BeginVertical();
        _cobjectslayout.AddRow(new Label());
        _objectparameterslayout = new DynamicLayout { Visible = false };
        _objectparameterslayout.BeginVertical();
        _objectparameterlistgridview = new GridView
        {
            AllowDrop = true,
            AllowMultipleSelection = false,
            DataStore = _context.CParameters.CParameters,
            ShowHeader = true,
            Height = 300,
            Width = 790
        };
        _objectparameterlistgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.Name.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Наименование параметра",
            Resizable = true,
            Sortable = true,
            Expand = true
        });        
        _objectparameterlistgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.Description.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Описание параметра",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _objectparameterlistgridview.SelectedItemsChanged += CParameterListGridSelectItem;
        _objectparametersgridview = new GridView
        {
            AllowDrop = true,
            AllowMultipleSelection = false,
            DataStore = _context.CParameters.CParameters,
            ShowHeader = true,
            Height = 300,
            Width = 790
        };
        _objectparametersgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.Name.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Наименование параметра",
            Resizable = true,
            Sortable = true,
            Expand = true
        });        
        _objectparametersgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<CParameter, string>(c => c.Description.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Описание параметра",
            Resizable = true,
            Sortable = true,
            Expand = true
        });
        _objectparametersgridview.SelectedItemsChanged += CObjectParametersGridSelectItem;
        _objectparameterslayout.AddRow(
            new Label { Text = "Общий перечень параметров", TextAlignment = TextAlignment.Center, Width = 790 },
            new Label { Text = "Параметры объекта", TextAlignment = TextAlignment.Center });
        _objectparameterslayout.AddRow(_objectparameterlistgridview, _objectparametersgridview);
        _addparameter = new Button { Text = "Добавить параметр", Enabled = false };
        _addparameter.Click += CAddParameterClick;
        _removeparameter = new Button { Text = "Удалить параметр", Enabled = false };
        _removeparameter.Click += CRemoveParameterClick;
        _objectparameterslayout.AddRow(_addparameter, _removeparameter);
        _objectparameterslayout.AddRow(new Label());
        _objectparameterslayout.EndVertical();
        _cobjectslayout.AddRow(_objectparameterslayout);
        _cobjectslayout.EndVertical();

        _cobjectslayout.BeginVertical();
        _cobjectslayout.EndVertical();
        _cobjectspage.Content = _cobjectslayout;
    }

    private void InitializeInstancePage()   // инициализирует страницу управления экземплярами объектов
    {
        _cinstancepage = new TabPage();
        _cinstancepage.Text = "Экземпляры объектов";
        _ctabcontrol.Pages.Add(_cinstancepage);

        _cinstancelayout = new DynamicLayout();
        _cinstancelayout.BeginVertical();

        _instancegridview = new GridView
        {
            AllowDrop = true,
            AllowMultipleSelection = false,
            DataStore = _context.Objects.Objects,
            ShowHeader = true,
            Height = 300,
            Width = 1600
        };
        _instancegridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<ObjectInstance, string>(c => c.Name.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Тип объекта",
            Resizable = true,
            Sortable = true,
            Expand = true            
        });
        _instancegridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<ObjectInstance, string>(c => c.Description.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Описание типа объекта",
            Resizable = true,
            Sortable = true,
            Expand = true            
        });
        _instancegridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<ObjectInstance, string>(c => c.VisibleValue.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Объект",
            Resizable = true,
            Sortable = true,
            Expand = true            
        });
        _instancegridview.SelectedItemsChanged += CInstanceGridSelectItem;
        
        _cinstancelayout.AddRow(_instancegridview);

        _instancedetailsgridview = new GridView
        {
            AllowDrop = true,
            AllowMultipleSelection = false,
            ShowHeader = true,
            Width = 1600
        };
        _instancedetailsgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<ObjectParameter, string>(c => c.Name.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Наименование параметра",
            Resizable = true,
            Sortable = true,
            Expand = true            
        });
        _instancedetailsgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<ObjectParameter, string>(c => c.Description.ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Описание параметра",
            Resizable = true,
            Sortable = true,
            Expand = true            
        });
        _instancedetailsgridview.Columns.Add(new GridColumn
        {
            DataCell = new TextBoxCell
            {
                Binding = Binding.Property<ObjectParameter, string>(c => c.GetVisibleValue().ToString())
            },
            AutoSize = true,
            Editable = false,
            HeaderText = "Значение параметра",
            Resizable = true,
            Sortable = true,
            Expand = true            
        });
        
        _cinstancelayout.AddRow(new Label());
        _cinstancelayout.AddRow(_instancedetailsgridview);

        _cinstancelayout.EndVertical();
        _cinstancepage.Content = _cinstancelayout;
    }

    // дальше - обработчики событий интерфейса
    private void CInstanceGridSelectItem(object sender, EventArgs e)
    {
        if (_instancegridview.SelectedRow >= 0)
        {
          _instancedetailsgridview.DataStore = ((ObjectInstance)_instancegridview.SelectedItem).Parameters;   
        }
        else
        {
            _instancedetailsgridview.DataStore = null;
        }
    }
    private void CParametersGridSelectItem(object sender, EventArgs e)
    {
        if (_parametergridview.SelectedRow >= 0)
        {
            _curparameter = (CParameter)_parametergridview.SelectedItem;
            _parametersave.Enabled = true;
        }
        else
        {
            _curparameter = null;
            _parametersave.Enabled = false;
        }

        RefreshParameterFields();
    }

    private void CObjectsGridSelectItem(object sender, EventArgs e)
    {
        if (_objectgridview.SelectedRow >= 0)
        {
            _curobject = (CObject)_objectgridview.SelectedItem;
            _objectsave.Enabled = true;
            _objectparameterslayout.Visible = true;
            _objectparametersgridview.DataStore = _curobject.Parameters;
        }
        else
        {
            _curobject = null;
            _objectsave.Enabled = false;
            _objectparameterslayout.Visible = false;
        }

        RefreshObjectFields();
    }

    private void CParameterListGridSelectItem(object sender, EventArgs e)
    {
        if (_objectparameterlistgridview.SelectedRow >= 0)
        {
            _addparameter.Enabled = true;
        }
        else
        {
            _addparameter.Enabled = false;
        }
    }

    private void CObjectParametersGridSelectItem(object sender, EventArgs e)
    {
        if (_objectparametersgridview.SelectedRow >= 0)
        {
            _removeparameter.Enabled = true;
        }
        else
        {
            _removeparameter.Enabled = false;
        }
    }

    private void RefreshParameterFields()
    {
        _parametername.Text = _parametersave.Enabled ? _curparameter.Name : "";
        _parameterdescription.Text = _parametersave.Enabled ? _curparameter.Description : "";
        _parametertype.SelectedValue = _parametersave.Enabled ? _curparameter.Type : null;
        _parameterassociatedobject.SelectedValue = _parametersave.Enabled ? (CObject)_curparameter.ObjectType : null;
    }

    private void RefreshObjectFields()
    {
        _objectname.Text = _objectsave.Enabled ? _curobject.Name : "";
        _objectdescription.Text = _objectsave.Enabled ? _curobject.Description : "";
    }

    private void CParametersSaveClick(object sender, EventArgs e)
    {
        if (_parameterassociatedobject.Visible)
        {
            if (_parameterassociatedobject.SelectedValue != null)
            {
                _context.CParameters.UpdateById(_curparameter, _parametername.Text, _parameterdescription.Text,
                    (ParameterType)_parametertype.SelectedValue, (CObject)_parameterassociatedobject.SelectedValue);
                _parametergridview.SelectedRow--;
                CParametersGridSelectItem(sender, e);
            }
            else
            {
                MessageBox.Show(this, "Не выбран тип связанного объекта!\n Данные не сохранены!");
            }
        }
        else
        {
            _context.CParameters.UpdateById(_curparameter, _parametername.Text, _parameterdescription.Text,
                (ParameterType)_parametertype.SelectedValue);
            _parametergridview.SelectedRow--;
            CParametersGridSelectItem(sender, e);    
        }
    }

    private void CObjectsSaveClick(object sender, EventArgs e)
    {
        _context.CObjects.UpdateById(_curobject, _objectname.Text, _objectdescription.Text);
        _objectgridview.SelectedRow--;
        CObjectsGridSelectItem(sender, e);
    }

    private void CParametersAddNewClick(object sender, EventArgs e)
    {
        if (_parameterassociatedobject.Visible)
        {
            if (_parameterassociatedobject.SelectedValue != null)
            {
                _context.CParameters.AddNewParameter(_parametername.Text, _parameterdescription.Text,
                    (ParameterType)_parametertype.SelectedValue, (CObject)_parameterassociatedobject.SelectedValue);
                _parametergridview.SelectedRow = _parametergridview.DataStore.Count() - 1;
            }
            else
            {
                MessageBox.Show(this, "Не выбран тип связанного объекта!\n Данные не сохранены!");
            }
        }
        else
        {
            _context.CParameters.AddNewParameter(_parametername.Text, _parameterdescription.Text,
                (ParameterType)_parametertype.SelectedValue);
            _parametergridview.SelectedRow = _parametergridview.DataStore.Count() - 1;    
        }
    }

    private void CObjectsAddNewClick(object sender, EventArgs e)
    {
       
        _context.CObjects.AddNewObject(_objectname.Text, _objectdescription.Text);
        _objectgridview.SelectedRow = _objectgridview.DataStore.Count() - 1;
    }

    private void CAddParameterClick(object sender, EventArgs e)
    {
        _curobject.AddParameter(_objectparameterlistgridview.SelectedItem as CParameter);
        _objectparametersgridview.DataStore = _curobject.Parameters;
    }

    private void CRemoveParameterClick(object sender, EventArgs e)
    {
        _curobject.Parameters.Remove(_objectparametersgridview.SelectedItem as CParameter);
        _objectparametersgridview.DataStore = _curobject.Parameters;
    }

    private void CParameterTypeChange(object sender, EventArgs e)
    {
        if (_parametertype.SelectedIndex != -1) 
        {
            if ((((ParameterType)_parametertype.SelectedValue).Id == 4) ||
                (((ParameterType)_parametertype.SelectedValue).Id == 5))
            {
                _parameterassociatedobject.Visible = true;
                if (_curparameter != null) if (_curparameter.ObjectType != null)
                    _parameterassociatedobject.SelectedValue = _curparameter.ObjectType;
                    else _parameterassociatedobject.SelectedValue = null;
            } else
            {
                _parameterassociatedobject.Visible = false;
                _parameterassociatedobject.SelectedValue = null;
            }
        } else
        {
            _parameterassociatedobject.Visible = false;
            _parameterassociatedobject.SelectedValue = null;
        }
    }

    private void CActionRunClick(object sender, EventArgs e)
    {
        if (_actionsgridview.SelectedItem != null)
        {
            ActionRunController NewAction = new ActionRunController((CAction)_actionsgridview.SelectedItem, _context);
            NewAction.Initialize();
            NewAction.Show();
        }
    }
}

