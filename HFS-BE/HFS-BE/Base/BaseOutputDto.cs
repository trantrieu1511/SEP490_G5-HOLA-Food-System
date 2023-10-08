namespace HFS_BE.Base
{
    public class BaseOutputDto
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<string>? Errors { get; set; } = null;
    }
}
