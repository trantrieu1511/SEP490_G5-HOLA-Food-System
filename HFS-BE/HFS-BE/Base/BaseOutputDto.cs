namespace HFS_BE.Base
{
    public class BaseOutputDto
    {
        public BaseOutputDto() { }

        public BaseOutputDto(string message, bool success, List<string>? errors)
        {
            Message = message;
            Success = success;
            Errors = errors;
        }

        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<string>? Errors { get; set; } = null;

        public T Create<T>() where T : BaseOutputDto, new()
        {
            return new T
            {
                Message = this.Message,
                Success = this.Success,
                Errors = this.Errors
            };
        }
    }
}
