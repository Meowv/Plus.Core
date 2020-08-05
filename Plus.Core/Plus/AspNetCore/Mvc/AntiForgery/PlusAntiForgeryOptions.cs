namespace Plus.AspNetCore.Mvc.AntiForgery
{
    public class PlusAntiForgeryOptions
    {
        /// <summary>
        /// Get/sets cookie name to transfer Anti Forgery token between server and client.
        /// Default value: "XSRF-TOKEN".
        /// </summary>
        public string TokenCookieName { get; set; }

        /// <summary>
        /// Get/sets header name to transfer Anti Forgery token from client to the server.
        /// Default value: "X-XSRF-TOKEN". 
        /// </summary>
        public string TokenHeaderName { get; set; }

        public PlusAntiForgeryOptions()
        {
            TokenCookieName = "XSRF-TOKEN";
            TokenHeaderName = "X-XSRF-TOKEN";
        }
    }
}