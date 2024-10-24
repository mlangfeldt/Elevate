using BL.Models;
using Elevate.BL;
using Elevate.UI.Extensions;
using Elevate.UI.ViewModels;
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
                UserManager.GenerateResetCode(email);
                return View("ResetConfirmation");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: User/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            var model = new ResetPasswordViewModel { Email = email };
            return View(model);
        }

        // POST: User/ResetPassword
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (model.NewPassword != model.ConfirmPassword)
            {
                ViewBag.Message = "Passwords do not match.";
                return View(model);
            }

            if (UserManager.ValidateResetCode(model.Email, model.Code))
            {
                UserManager.UpdatePassword(model.Email, model.NewPassword);
                TempData["Message"] = "Your password has been reset. You can now log in.";
                return RedirectToAction("Login");
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
                    SetUser(user);
                    if (TempData["returnurl"] != null)
                        return Redirect(TempData["returnurl"].ToString());
                    else
                    {
                        ViewBag.Message = "You are now logged in.";
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }

        private void SetUser(User user)
        {
            HttpContext.Session.SetObject("user", user);
            if (user != null)
            {
                HttpContext.Session.SetObject("fullname", "Welcome " + user.FirstName.ToString() + " " + user.LastName.ToString());
            }
            else
            {
                HttpContext.Session.SetObject("fullname", "");

            }
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
            try
            {
                int results = UserManager.Insert(user);
                TempData["Message"] = "Signup successful! You can now log in.";
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
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

