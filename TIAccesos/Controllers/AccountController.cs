using AppUsersModel;using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TIAccesos.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Login data, string returnUrl)
        {
            ActionResult Result;

            // Autenticar al usuario
            var Repository = new Repository();
            var User = Repository.FindUser(data.Email, data.Password);

            // Si el usuario fue autenticado, iniciar su sesión
            if (User != null)
            {
                Result = SignInUser(User, data.RememberMe, returnUrl);
            }
            else
            {
                Result = View(data);
            }
            return Result;
        }

        private ActionResult SignInUser(User User, bool rememberMe, string returnUrl)
        {
            ActionResult Result;
            var Claims = new List<Claim>
            {
                // Crear el Claim NameIdentifier obligatorio.
                // En nuestro ejemplo, el ID es el identificador del usuario.
                new Claim(ClaimTypes.NameIdentifier, User.ID.ToString()),

                // Crear los Claims adicionales del usuario que nos interese almacenar                
                new Claim(ClaimTypes.Email, User.Email),

                // Claim Name utilizado para autorizar Usuarios específicos.
                new Claim(ClaimTypes.Name, User.FirstName),
                new Claim("FullName", $"{User.FirstName} {User.LastName}")
            };

            if (User.Roles != null && User.Roles.Any())
            {
                Claims.AddRange(User.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));
            }

            // Crear la identidad basada en Claims.
            var Identity = new ClaimsIdentity(Claims,DefaultAuthenticationTypes.ApplicationCookie);

            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = rememberMe
            }, Identity);

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }

            Result = Redirect(returnUrl);
            return Result;
        }
        public ActionResult LogOff()
        {
            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }
    }
}