namespace Demo.BLL.Common.Services.GetUsereLogin
{
    public interface IGetuserLogin
    {
        public Task<string> GetUserIdLoginAsync();
        public Task<string> GetUserNameLoginAsync();
    }
}
