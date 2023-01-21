namespace Core.Dto
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public string? Avatar { get; set; }
        public string? Role { get; set; }
        public float? AvarageRating { get; set; }
        public DateTimeOffset? RegistrationDate { get; set; }
    }
}
