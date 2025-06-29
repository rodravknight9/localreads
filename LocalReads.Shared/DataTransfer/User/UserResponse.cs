using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalReads.Shared.DataTransfer.User;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime MemberSince { get; set; }
    public string PersonalIntroduction { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int FavoriteBooksCount { get; set; } 
    public int CurrentlyReading { get; set; }
}
