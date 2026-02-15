using fanaticServe.Core.Data;
using fanaticServe.Core.Dto;


namespace fanaticServe.Back;

public class PeopleService : IPeople
{
    private readonly IFanaticServeContext _context;

    public PeopleService(IFanaticServeContext context)
    {
        _context = context;
    }

    public ShowablePerson GetPerson(Guid id)
    {
        var person =
     _context.People
         .Where(p => p.Person_Id == id)
         .Select(p => new ShowablePerson
         {
             Person_Id = p.Person_Id,
             Name = p.Name,
             Kana = p.Kana
         }
         )
         .FirstOrDefault(new ShowablePerson());

        var songs =
        _context.RoleOnSongs
            .Where(r => r.Person_Id == person.Person_Id)
            .Join(
            _context.Songs,
            r => r.Song_Id,
            s => s.Song_Id, (r, s) => new { r, s })
            .Join(_context.Roles,
            rs => rs.r.Role_Id,
            role => role.Role_Id,
            (rs, role) => new RoleWithSong() {role=role,song=rs.s});

        person.Songs.AddRange(songs);

        return person;
    }
}
