using Demo.DAL.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Demo.BLL.Common.Services.GetUsereLogin
{
    public class GetuserLogin : IGetuserLogin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetuserLogin(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> GetUserIdLoginAsync()
        {
            var userLogin = _httpContextAccessor.HttpContext?.User;
            if (userLogin == null)
                return "User not found";
            var user = await _userManager.GetUserAsync(userLogin);
            if (user == null)
                return "User not found";
            return user.Id;
        }

        public async Task<string> GetUserNameLoginAsync()
        {
            var userLogin = _httpContextAccessor.HttpContext?.User;
            if (userLogin == null)
                return "User not found";
            var user = await _userManager.GetUserAsync(userLogin);
            if (user == null)
                return "User not found";
            return $"{user.FName} {user.LName}";
        }
    }
}
