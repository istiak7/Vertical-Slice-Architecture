namespace Vertical_Slice_Architecture.Features.Users.Shared.Dtos
{
    public class UserLoginResponseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
