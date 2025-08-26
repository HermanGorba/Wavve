namespace Wavve.Core.Models
{
    public class ApiError
    {
        public string Code { get; set; } = "BadRequest";
        public List<string> Messages { get; set; } = new();
    }
}
