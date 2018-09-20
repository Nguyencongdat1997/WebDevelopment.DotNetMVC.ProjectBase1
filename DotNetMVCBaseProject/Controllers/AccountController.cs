using DotNetMVCBaseProject.DAO;
using DotNetMVCBaseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DotNetMVCBaseProject.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult ViewLogin(string message)
        {
            //If there has been already an account saved in cookie => skip login
            if (CheckCookie())
            {
                return RedirectToAction("Index", "Home");
            }
            //Else show login page
            if (message != null)
                ViewBag.Message = message;
            else
                ViewBag.Message = "";
            return View();
        }
        public ActionResult CheckLogin(string username, string password)
        {
            ClearCookieAndSession();

            //Validate login
            AccountDAO accountDAO = new AccountDAO();
            Account account = accountDAO.GetOne(username, password);
            if (account != null)
            {// login successful
                //Save into cookie
                FormsAuthentication.SetAuthCookie(username, false);

                //Save account into session
                if (Session["CurrentAccount"] == null)
                {
                    Session.Add("CurrentAccount", account);
                }
                else
                {
                    Session["CurrentAccount"] = account;
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {// login failed => reload                
                return RedirectToAction("ViewLogin", "Account", new { message = "Invalid Username or Password" });
            }
        }
        public ActionResult ViewRegister(string message)
        {
            //If there has been already an account saved in cookie => skip login
            if (CheckCookie())
            {
                return RedirectToAction("Index", "Home");
            }
            //Else show login page
            if (message != null)
                ViewBag.Message = message;
            else
                ViewBag.Message = "";
            return View();
        }
        public ActionResult CheckRegister(Account newAccount)
        {
            ClearCookieAndSession();

            //Validate register
            AccountDAO accountDAO = new AccountDAO();
            newAccount.Role = Common.Enums.AccountRole.User;
            int result = accountDAO.Insert(newAccount);

            if (result == 1)
            {// register successful
                //Save into cookie
                FormsAuthentication.SetAuthCookie(newAccount.Username, false);

                //Save account into session
                if (Session["CurrentAccount"] == null)
                {
                    Session.Add("CurrentAccount", newAccount);
                }
                else
                {
                    Session["CurrentAccount"] = newAccount;
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {// register failed => reload                
                return RedirectToAction("ViewRegister", "Account", new { message = "Account has already existed" });
            }
        }
        public ActionResult Logout()
        {
            ClearCookieAndSession();
            return RedirectToAction("ViewLogin", "Account");
        }
        private bool CheckCookie()
        {
            //TODO:
            //  Return true if there is a valid account being saved in cookie
            //  Return false if otherwise
            AccountDAO accountDAO = new AccountDAO();
            {
                Account savedAccount = null;
                if (FormsAuthentication.CookiesSupported == true)
                {
                    if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                    {
                        try
                        {
                            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                            savedAccount = accountDAO.GetOne(username);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in Global.asax.cs,  FormsAuthentication_OnAuthenticate(): " + ex.Message);
                            throw ex;

                        }
                    }
                }
                if (savedAccount != null)
                {
                    //Save account into session
                    if (Session["CurrentAccount"] == null)
                    {
                        Session.Add("CurrentAccount", savedAccount);
                    }
                    else
                    {
                        Session["CurrentAccount"] = savedAccount;
                    }
                    return true;
                }
                return false ;
            }
        }
        private void ClearCookieAndSession()
        {
            //Clear cookie
            FormsAuthentication.SetAuthCookie(String.Empty, false);

            //Clear session
            if (Session["CurrentAccount"] != null)
            {
                Session["CurrentAccount"] = null;
            };
        }
        
    }
}