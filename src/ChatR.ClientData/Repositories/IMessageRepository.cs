using System.Collections.Generic;
using ChatR.Model;

namespace ChatR.ClientData.Repositories
{
    public interface IMessageRepository
    {
        UserDetail CurrentUserDetail { get; set; }
        List<UserDetail> OtherUsers { get; set; }
    }
}