using Elevate.BL;
using Elevate.BL.Models;
using Elevate.UI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Controllers
{

    public class UserController : Controller
    {

        public IActionResult Seed()
        {
            UserManager.Seed();
            return View();

        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnurl"] = returnUrl;
            return View();
        }

        public IActionResult Logout()
        {
            SetUser(null);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: User/ForgotPassword
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                string resetCode = UserManager.GenerateResetCode(email);

                HttpContext.Session.SetString("ResetEmail", email);
                HttpContext.Session.SetString("ResetCode", resetCode);
                return RedirectToAction("ResetPassword");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("ResetConfirmation");
            }
        }

        // GET: User/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            var model = new User { Email = email };
            return View(model);
        }

        // POST: User/ResetPassword
        [HttpPost]
        public IActionResult ResetPassword(User model)
        {
            string email = HttpContext.Session.GetString("ResetEmail");
            string sessionResetCode = HttpContext.Session.GetString("ResetCode");

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ViewBag.Message = "Passwords do not match.";
                return View(model);
            }

            if (UserManager.ValidateResetCode(email, sessionResetCode))
            {
                UserManager.UpdatePassword(email, model.NewPassword);
                TempData["Message"] = "Your password has been reset. You can now log in.";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Invalid or expired reset code.";
                return View(model);
            }
        }

        public IActionResult Reset()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                if (UserManager.Login(user))
                {
                    user = UserManager.LoadByEmail(user.Email); // Ensure we have a full user object
                    if (user.EmailConfirmed != 1)
                    {
                        TempData["Message"] = "You must confirm your email address before logging in.";
                        SetUser(null);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetUser(user); // Store the UserId in session
                        if (TempData["returnurl"] != null)
                            return Redirect(TempData["returnurl"].ToString());
                        else
                        {
                            ViewBag.Message = "You are now logged in.";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                TempData["Message"] = "Invalid login credentials.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Login");
            }
        }


        private void SetUser(User user)
        {
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetObject("user", user);
                HttpContext.Session.SetString("fullname", $"Welcome {user.FirstName} {user.LastName}");
            }
            else
            {
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Remove("user");
                HttpContext.Session.Remove("fullname");
            }
        }

        private int GetUserIdFromSession()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                return userId.Value;
            }
            throw new Exception("User is not authenticated or UserId is missing in session.");
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View(UserManager.Load());
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View(UserManager.LoadById(id));
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            // use EmailExists method to determine if this email address has been used already
            if (UserManager.EmailExists(user.Email))
            {

                TempData["Message"] = "Email already registered.  Please log in or reset your password.";
                return RedirectToAction("Index", "Home");

            }
            else
            {
                try
                {

                    // insert new user into db
                    int results = UserManager.Insert(user);

                    // load the user as the ConfirmationCode is generated by the Insert method
                    user = UserManager.LoadByEmail(user.Email);

                    // Confirmation email body
                    string body = "Welcome to Elevate!";
                    body += "<br /><br />Please click the following link to activate your account";
                    body += $"<br /><a href = '" + string.Format("{0}://{1}/User/Activation/{2}", HttpContext.Request.Scheme, HttpContext.Request.Host, user.ConfirmationCode) + "'>Click here to activate your account.</a>";

                    // send the email using the EmailService SendConfirmationCodeEmail method
                    EmailService.SendConfirmationCodeEmail(body, user.Email, user.ConfirmationCode);

                    TempData["Message"] = "Signup successful! Please check your email for a confirmation link.";
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();

                }
            }
        }

        public ActionResult Activation()
        {
            ViewBag.Message = "Invalid Confirmation code.";

            // if id (ConfirmationCode) is passed to Activation view
            if (RouteData.Values["id"] != null)
            {
                // parse code passed to view
                string activationCode = RouteData.Values["id"].ToString();

                // search db for the code and load that user
                User testUser = UserManager.LoadByConfirmationCode(activationCode);

                // if user is found, set EmailConfirmed == 1 (true) and update the db
                if (testUser != null)
                {
                    testUser.EmailConfirmed = 1;

                    UserManager.Update(testUser);

                    ViewBag.Message = "Email comfirmed, you may now log in.";
                }
            }

            return View();
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

}

