namespace TransactionAppletaApi.Oss
{
    public class OssBadRequest
    {
        public string Message { get; set; }
        public string ToMessage(string statusCode)
        {
            if (string.IsNullOrEmpty(this.Message))
                return statusCode;
            return string.Format("{0}:{1}", statusCode, this.Message);
        }
    }
}