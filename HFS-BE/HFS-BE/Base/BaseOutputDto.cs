using HFS_BE.Ultis;

namespace HFS_BE.Base
{
    public class BaseOutputDto
    {
        public string? Message { get; set; } = null;
        public bool Success { get; set; } = true;
        public List<string>? Errors { get; set; } = null;
        public List<ValidationErrorDto>? ValidationErrors1 { get; set; } = null;

        public Dictionary<string, List<string>> ValdationErros2 { get; set; }
    }
}
