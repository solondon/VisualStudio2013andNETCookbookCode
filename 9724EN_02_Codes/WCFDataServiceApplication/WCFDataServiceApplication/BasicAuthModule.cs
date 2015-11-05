using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFDataServiceApplication
{
    public class BasicAuthModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.AuthenticateRequest += new EventHandler(app_AuthenticateRequest);
        }
        void app_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if (!app.Request.IsSecureConnection)
            {
                BasicAuthModule.GenerateAutenticationFailedResponse(app, 403, 4, "Please connect the service using HTTPS");
                app.CompleteRequest();
            }
            else if (!app.Request.Headers.AllKeys.Contains("Authorization"))
            {
                BasicAuthModule.GenerateAutenticationFailedResponse(app, 401, 1,
                    "Please provide Authorization headers with your request.");
                app.CompleteRequest();
            }
            else if (!BasicAuthProvider.Authenticate(app.Context))
            {
                BasicAuthModule.GenerateAutenticationFailedResponse(app, 401, 1, "Logon failed.");
                app.CompleteRequest();
            }
        }
        private static void GenerateAutenticationFailedResponse(HttpApplication app, int code, int subCode, string description)
        {
            HttpResponse response = app.Context.Response;
            response.StatusCode = code;
            response.SubStatusCode = subCode;
            response.StatusDescription = description;
            response.AppendHeader("WWW-Authenticate", "Basic");
        }
        public void Dispose()
        {
        }
    }
}