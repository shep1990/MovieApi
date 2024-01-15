namespace MovieApi.Handlers
{
    public class HandlerResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
    }

    public class HandlerResponse<TData> : HandlerResponse
    {
        public TData Data { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class HandlerResponseList<TData> : HandlerResponse
    {
        public List<TData> Data { get; set; }
    }
}
