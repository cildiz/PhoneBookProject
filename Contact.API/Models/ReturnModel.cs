namespace Contact.API.Models
{
    public class ReturnModel
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Model { get; set; }
    }
}
