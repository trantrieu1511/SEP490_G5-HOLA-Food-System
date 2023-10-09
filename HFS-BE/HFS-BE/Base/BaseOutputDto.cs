using HFS_BE.Ultis;
using System.Text.Json.Serialization;

namespace HFS_BE.Base
{
    public class BaseOutputDto
    {
        public string? Message { get; set; } = null;
        public bool Success { get; set; } = true;
        public ErrorsMessage? Errors { get; set; } = null;      
        //public Dictionary<string, List<string>> ValdationErros2 { get; set; }
    }

    public class ErrorsMessage
    {
        public List<ValidationErrorDto>? ValidationErrors { get; set; } = null;
        public List<string>? SystemErrors { get; set; } = null;
    }
}
