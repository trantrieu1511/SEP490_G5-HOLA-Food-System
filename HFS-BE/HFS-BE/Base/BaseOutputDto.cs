using HFS_BE.Utils;
using System.Text.Json.Serialization;

namespace HFS_BE.Base
{
    /// <summary>
    /// The base output use for response body of an API
    /// </summary>
    public class BaseOutputDto
    {
        public string? Message { get; set; } = null;
        public bool Success { get; set; } = true;
        public ErrorsMessage? Errors { get; set; } = null;      
        //public Dictionary<string, List<string>> ValdationErros2 { get; set; }
    }

    public class ErrorsMessage
    {
        public ICollection<ValidationErrorDto>? ValidationErrors { get; set; } = null;
        public ICollection<string>? SystemErrors { get; set; } = null;
    }
}
