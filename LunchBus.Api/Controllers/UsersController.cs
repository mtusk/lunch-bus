using LunchBus.Model;
using Microsoft.Azure.AppService.ApiApps.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LunchBus.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        [HttpGet]
        public async Task<User> GetAsync()
        {
            var runtime = Runtime.FromAppSettings(Request);
            var user = runtime.CurrentUser;
            var token = await user.GetRawTokenAsync("facebook");

            var whoAmI = await this.GetFacebookWhoAmIAsync(token);
            return whoAmI;
        }

        // Temporary: Trying out retrieval of Facebook info of logged in user
        private async Task<User> GetFacebookWhoAmIAsync(TokenResult token)
        {
            string id, name;

            // Facebook Claim Type URIs: https://msdn.microsoft.com/en-us/library/azure/gg185967.aspx
            token.Claims.TryGetValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", out id);
            token.Claims.TryGetValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", out name);

            var profilePictureUrl = await this.GetFacebookProfilePictureUrlAsync(id);

            var user = new User
            {
                //Id = id,
                Name = name,
                PhotoUrl = profilePictureUrl
            };

            return user;
        }

        // Temporary: Trying out retrieval of Facebook profile photo of logged in user
        private async Task<string> GetFacebookProfilePictureUrlAsync(string userId)
        {
            var url = string.Format("https://graph.facebook.com/{0}/picture?type=large&redirect=false", userId);
            var webClient = new WebClient();
            var responseJson = await webClient.DownloadStringTaskAsync(new Uri(url));

            var response = JObject.Parse(responseJson);
            var profilePictureUrl = response["data"]["url"].Value<string>();

            return profilePictureUrl;
        }
    }
}
