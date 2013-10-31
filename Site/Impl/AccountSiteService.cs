using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.ComponentModel.Composition;

using Component.Tools;
using Core;
using Core.Impl;
using Core.Models;
using Site.Models;

namespace Site.Impl
{
    /// <summary>
    /// 账户模块站点业务实现
    /// </summary>
    [Export(typeof(IAccountSiteContract))]
    public class AccountSiteService : AccountService, IAccountSiteContract
    {
        public OperationResult Login(LoginModel loginModel)
        {
            LoginInfo loginInfo = new LoginInfo
            {
                Access = loginModel.Account,
                Password = loginModel.Password,
                IpAddress = HttpContext.Current.Request.UserHostAddress
            };
            OperationResult result = base.Login(loginInfo);

            if (result.ResultType == OperationResultType.Success)
            {
                Member member = (Member)result.AppendData;
                DateTime expiration = loginModel.IsRememberLogin
                    ? DateTime.Now.AddDays(7)
                    : DateTime.Now.Add(FormsAuthentication.Timeout);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, member.UserName, DateTime.Now, expiration, true, member.NickName, FormsAuthentication.FormsCookiePath);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                if (loginModel.IsRememberLogin)
                {
                    cookie.Expires = DateTime.Now.AddDays(7);
                }
                HttpContext.Current.Response.Cookies.Set(cookie);
                result.AppendData = null;
            }
            return result;

        }
        /// <summary>
        ///  用户退出
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
