using System.Collections.ObjectModel;
using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Repositories;
using BusinessLogicTryout.Objects;

public class CParameterRepository
{
    private ObservableCollection<CParameter> _parameters;
    private ParameterTypes _types;

    public CParameterRepository()
    {
        _parameters = new ObservableCollection<CParameter>();
        _types = new ParameterTypes();
    }

    public CParameter AddNewParameter(string name, string description, ParameterType type)
    {
        _parameters.Add(new CParameter(name, description, type));
        _parameters.Last().SetId(_parameters.Count == 1 ? 0 : (_parameters[^2].Id + 1));
        return _parameters.Last();
    }

    public CParameter AddNewParameter(string name, string description, ParameterType type, CObject objectlink)
    {
        _parameters.Add(new CParameter(name, description, type, objectlink));
        _parameters.Last().SetId(_parameters.Count == 1 ? 0 : (_parameters[^2].Id + 1));
        return _parameters.Last();
    }

    public CParameter AddParameter(CParameter parameter)
    {
        _parameters.Add(parameter);
        _parameters.Last().SetId(_parameters.Count == 1 ? 0 : (_parameters[^2].Id + 1));
        return _parameters.Last();
    }
    
    public ObservableCollection<CParameter> CParameters => _parameters;

    public void UpdateById(CParameter parameter, string name, string description, ParameterType type)
    {
        CParameter _parameter = new(name, description, type);
        _parameter.SetId(parameter.Id);
        _parameters[_parameters.IndexOf(parameter)] = _parameter;
    }

    public void UpdateById(CParameter parameter, string name, string description, ParameterType type,
        CObject objectlink)
    {
        CParameter _parameter = new(name, description, type, objectlink);
        _parameter.SetId(parameter.Id);
        _parameters[_parameters.IndexOf(parameter)] = _parameter;
    }
    
    public ParameterTypes Types => _types;
}