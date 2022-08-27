namespace Core.Dto
{
    public class AppUserDto
    {
        public string Id;
        public string UserName;
        public string Email;
        public string Photo;
        public AppUserDto(string _id, 
            string _userName, 
            string _email, 
            string _photo)
        {
            Id = _id;
            UserName = _userName;
            Email = _email;
            Photo = _photo;
        }
    }
}
