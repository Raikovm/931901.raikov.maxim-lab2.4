using Microsoft.AspNetCore.Mvc;
using lab4.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace lab4.Controllers;

public class MockupsController : Controller
{
    private readonly Random rand = new();

    public IActionResult Index()
    {
        return View();
    }

    [AcceptVerbs("GET", "POST")]
    public IActionResult CheckEmail(string email) => 
        Json(!AccountsBase.Get().ContainsKey(email));

    [HttpGet]
    public IActionResult SignUp1() => 
        View();

    [HttpPost]
    public IActionResult SignUp1(AuthModel a) =>
        ModelState["FirstName"]!.ValidationState == ModelValidationState.Valid &
        ModelState["LastName"]!.ValidationState == ModelValidationState.Valid &
        ModelState["Gender"]!.ValidationState == ModelValidationState.Valid
            ? RedirectToAction("SignUp2", a)
            : View();

    [HttpGet]
    public IActionResult SignUp2() => 
        View();

    [HttpPost]
    public IActionResult SignUp2(AuthModel a)
    {
        if (!(ModelState["Email"]!.ValidationState == ModelValidationState.Valid &
              ModelState["Password"]!.ValidationState == ModelValidationState.Valid &
              ModelState["ConfirmPassword"]!.ValidationState == ModelValidationState.Valid))
        {
            return View();
        }

        if (AccountsBase.Get().ContainsKey(a.Email))
        {
            return View(a);
        }

        AccountsBase.Get().Add(a.Email, a);
        return View("Result", a);
    }

    [HttpGet]
    public IActionResult ResetStageOne() => 
        View();

    [HttpPost]
    public IActionResult ResetStageOne(string Email, string action)
    {
        if (Email == null)
        {
            ViewBag.Code = "Email not specified";
            return View();
        }
        if (AccountsBase.Get().TryGetValue(Email, out var a))
        {
            if (action != "Send me a code")
            {
                return RedirectToAction("ResetStageTwo", a);
            }

            string b = Convert.ToString(rand.Next(0, 10)) + Convert.ToString(rand.Next(0, 10)) + Convert.ToString(rand.Next(0, 10)) + Convert.ToString(rand.Next(0, 10));
            ViewBag.Code = b;
            AccountsBase.Get()[Email].Code = b;
            return View();

        }

        ViewBag.Code = "You are not registred";
        return View();
    }

    [HttpGet]
    public IActionResult ResetStageTwo() => 
        View();

    [HttpPost]
    public IActionResult ResetStageTwo(AuthModel a, string myTextbox)
    {

        if (a.Code == myTextbox)
        {
            return RedirectToAction("PasswordChange", a);
        }

        ViewBag.Check = "Wrong code";
        return View(a);
    }
    [HttpGet]
    public IActionResult PasswordChange() => 
        View();

    [HttpPost]
    public IActionResult PasswordChange(AuthModel a)
    {

        if (ModelState["Password"]!.ValidationState == ModelValidationState.Valid &
            ModelState["ConfirmPassword"]!.ValidationState == ModelValidationState.Valid)
            return View("ResetResult");
        {
            View(a);
        }

        return View();
    }
        
}