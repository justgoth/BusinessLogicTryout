using System.Collections.ObjectModel;
using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Repositories;
using BusinessLogicTryout.Objects;

public class CParameterRepository
{
    private readonly ObservableCollection<CParameter> _parameters = new();

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

    public CParameter AddNewParameter(string name, string description, ParameterType type, CObject objectlink,
        LinkType linktype, bool multiple)
    {
        _parameters.Add(new CParameter(name, description, type, objectlink, linktype, multiple));
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
        CParameter newParameter = new(name, description, type);
        newParameter.SetId(parameter.Id);
        _parameters[_parameters.IndexOf(parameter)] = newParameter;
    }

    public void UpdateById(CParameter parameter, string name, string description, ParameterType type,
        CObject objectLink)
    {
        CParameter newParameter = new(name, description, type, objectLink);
        newParameter.SetId(parameter.Id);
        _parameters[_parameters.IndexOf(parameter)] = newParameter;
    }

    public ParameterTypes Types { get; } = new();

    public LinkTypes LinkTypes { get; } = new();
}
    