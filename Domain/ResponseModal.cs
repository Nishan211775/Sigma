namespace Domain
{
    public class ResponseModal
    {
        public string Message { get; set; } = string.Empty;
        public ResponseType ResponseType { get; set; }
    }

    public class ResponseModal<T> : ResponseModal
    {
        public T? Data { get; set; }
    }



    public enum ResponseType
    {
        Success,
        Failed
    }
}
