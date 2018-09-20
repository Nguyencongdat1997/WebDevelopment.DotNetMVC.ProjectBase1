using DotNetMVCBaseProject.DAO;
using DotNetMVCBaseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace DotNetMVCBaseProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        AccountDAO accountDAO = new AccountDAO();
                        Account account = accountDAO.GetOne(username);
                        if (account != null)
                        {
                            roles = account.Role.ToString();
                        }

                        //Let us set the Pricipal with our user specific details
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));                        
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in Global.asax.cs,  FormsAuthentication_OnAuthenticate(): " + ex.Message);
                        throw ex;

                    }
                }
            }
        }

    }
}
