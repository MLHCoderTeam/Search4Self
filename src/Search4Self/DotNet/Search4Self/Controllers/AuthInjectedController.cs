using Search4Self.DAL;
using Search4Self.DAL.Models;
using Search4Self.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Search4Self.Controllers
{
    public class AuthInjectedController : ApiController
    {
        private const string COOKIE = "userses";

        protected internal UnitOfWork unitOfWork;
        private UserEntity _user = null;
        protected internal new UserEntity User => _user ?? (_user = GetLoggedUser());
        protected internal string Token { get; set; }

        public AuthInjectedController()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool Authorize()
        {
            if (User == null)
                Unauthorized();

            return true;
        }

        private UserEntity GetLoggedUser()
        {
            var cookie = CookieHelper.GetCookie(COOKIE);
            UserEntity user = null;

            if (!string.IsNullOrEmpty(cookie))
            {
                Guid session;
                if (Guid.TryParse(cookie, out session))
                {
                    user = unitOfWork.UserRepository.GetBySession(session);
                }
            }

            if (user == null)
            {
                var session = Guid.NewGuid();
                user = unitOfWork.UserRepository.Insert(new UserEntity()
                {
                    Session = session
                });

                CookieHelper.SetCookie(COOKIE, session.ToString(), DateTime.Now.AddYears(10));
            }

            return user;
        }

    }
}