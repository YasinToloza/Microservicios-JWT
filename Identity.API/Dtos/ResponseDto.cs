namespace Identity.API.Dtos
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public DateTime Date { get; set; }
    }
}
