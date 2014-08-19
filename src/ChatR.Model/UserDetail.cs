using System.Diagnostics;

namespace ChatR.Model
{
    [DebuggerDisplay("UserName: {UserName}, ConnectionId: {ConnectionId}")]
    public class UserDetail
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
    }
}