using fanaticServe.Core.Dto;

namespace fanaticServe.Core.Data;

public interface IPeople
{
    public ShowablePerson GetPerson(Guid id);
}
