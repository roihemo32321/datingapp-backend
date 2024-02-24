using System.ComponentModel.DataAnnotations;

namespace dating_backend.Entities
{
    public class User
    {
        internal string Name;

        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; } 
       
    }
}
