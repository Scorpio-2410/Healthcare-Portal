namespace Healthcare_Patient_Portal.Features
{
    public class UserCreateDTO
    {
        public string RoleType { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly Dob { get; set; }
    }
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public string RoleType { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly Dob { get; set; }
    }
}
