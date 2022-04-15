using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace lab4.Models;

public class AuthModel
{
    [Required(ErrorMessage = "Email required")]
    [EmailAddress(ErrorMessage = "Incorrect email")]
    [Remote(action: "CheckEmail", controller: "Mockups")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password not specified")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Incorrect password")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name not specified")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name not specified")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Day not specified")]
    public int Day { get; set; }

    [Required(ErrorMessage = "Month not specified")]
    public string Month { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year not specified")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Gender not specified")]
    public string Gender { get; set; } = string.Empty;

    public bool Remember { get; set; }
    public string Code { get; set; } = string.Empty;
}