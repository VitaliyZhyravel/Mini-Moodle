﻿using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto.Account;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "{0} Can't be empty")]
    [MaxLength(40,ErrorMessage = "{0} can contain no more than 40 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} Can't be empty")]
    [EmailAddress(ErrorMessage = "Incorrect {0} format")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} сan't be empty")]
    [Phone(ErrorMessage = "{0} can contain only digits")]
    [Display(Name = "Phone number")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "{0} Can't be empty")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "{0} Can't be empty")]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}
