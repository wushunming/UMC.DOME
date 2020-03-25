using System;

namespace UMC.Demo
{
    public class WebHandler : System.Web.IHttpHandler
    {

        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {

            new Web.WebServlet().ProcessRequest(new WebContext(context));
        }

        #endregion
    }
}
