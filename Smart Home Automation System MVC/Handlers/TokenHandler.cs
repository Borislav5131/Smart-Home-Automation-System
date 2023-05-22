namespace Smart_Home_Automation_System_MVC.Handlers
{
    using Newtonsoft.Json.Linq;
    using System.Net.Http.Headers;
    using System.Web;

    public class TokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Retrieve the token from the cookie
            string token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            JObject? tokenObject = token == null ? null :JObject.Parse(token);
            string jwtToken = string.Empty;

            if (tokenObject != null )
            {
                if (tokenObject.TryGetValue("token", out JToken? tokenValue))
                {
                    jwtToken = tokenValue.ToString();
                }
            }

            // Add the token to the request headers
            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            // Call the inner handler to continue processing the request
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
