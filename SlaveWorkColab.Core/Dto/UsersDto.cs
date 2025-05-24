using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Core.Dto;

public class UsersDto : DtoBase 
{
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public bool IsDeleted { get; set; }

        public UsersDto() { }

        public UsersDto(Users user)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Password = user.Password;
            IsDeleted = user.IsDeleted;
        }
}