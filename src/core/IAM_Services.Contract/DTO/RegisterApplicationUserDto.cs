namespace AccessManagement.Contract
{
    public class RegisterApplicationUserDto
    {
        public string Username;
        public string Password;
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
