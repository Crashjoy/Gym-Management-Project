namespace GymManagement.Utilities
{
    public static class MaintainURL
    {
        /// <summary>
        /// Maintain the URL for an Index View including filter, sort and page information.
        /// Depends on the CookieHelper Utility.
        /// </summary>
        /// <param name="httpContext">the HttpContext from the Controller</param>
        /// <param name="ControllerName">The Name of the Controller</param>
        /// <returns>The Index URL with paramerters if required</returns>
        public static string ReturnURL(HttpContext httpContext, string ControllerName)
        {
            string cookieName = ControllerName + "URL";
            string SearchText = "/" + ControllerName + "?";
            //Get the URL of the page that sent us here
            string returnURL = httpContext.Request.Headers["Referer"].ToString();
            if (returnURL.Contains(SearchText))
            {
                //Came here from the Index with some parameters
                //Save the Parameters in a Cookie
                returnURL = returnURL[returnURL.LastIndexOf(SearchText)..];
                CookieHelper.CookieSet(httpContext, cookieName, returnURL, 30);
                return returnURL;
            }
            else
            {
                //Get it from the Cookie
                returnURL = httpContext.Request.Cookies[cookieName] ?? "/" + ControllerName;
                return returnURL;
            }
        }
    }

}
