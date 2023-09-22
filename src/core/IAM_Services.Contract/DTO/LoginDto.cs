using AccessManagement.Entities;

namespace AccessManagement.Contract
{
    public class LoginDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ApplicationUserDto User { get; set; }
    }
}
