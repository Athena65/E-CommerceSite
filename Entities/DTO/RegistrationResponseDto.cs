namespace Entities.DTO
{
    public class RegistrationResponseDto
    {
        public bool IsSuccessfulRegistiration { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
