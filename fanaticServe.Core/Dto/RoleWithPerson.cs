using fanaticServe.Core.Models;

namespace fanaticServe.Core.Dto
{
    public class RoleWithPerson
    {
        required public Role Role { get; set; }

        required public Person Person { get; set; }
    }
}
