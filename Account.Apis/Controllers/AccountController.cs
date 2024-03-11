﻿using Account.Apis.Errors;
using Account.Core.Dtos.Account;
using Account.Core.Models.Account;
using Account.Core.Models.Identity;
using Account.Core.Services;
using Account.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Account.Apis.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register model)
        {
            var result = await _accountService.RegisterAsync(model, GenerateCallBackUrl);

            if (result.StatusCode == 200)
            {
                return Ok(result.Message);
            }
            else
            {
                return StatusCode(result.StatusCode, result.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountService.LoginAsync(dto);
            if (result.StatusCode == 400)
            {
                return BadRequest(result.Message); 
            }
            return Ok(result);
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromHeader][EmailAddress] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email address is required.");
            }
            try
            {
                var result = await _accountService.ForgetPassword(email);

                if (result.StatusCode == 200)
                {
                    return Ok("Password reset email sent successfully.");
                }
                else
                {
                    return StatusCode(result.StatusCode, result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("verfiyOtp")]
        public IActionResult VerfiyOtp(VerifyOtp dto)
        {
            var result = _accountService.VerfiyOtp(dto);

            if (result.StatusCode == 200)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message); // Return the error message directly
            }
        }
        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword dto)
        {
            var result = await _accountService.ResetPasswordAsync(dto);

            // Handle different response statuses
            switch (result.StatusCode)
            {
                case 200:
                    return Ok(result.Message);
                case 400:
                    return BadRequest(result.Message);
                case 500:
                    return StatusCode(500, result.Message);
                default:
                    return StatusCode(500, "An unexpected error occurred.");
            }
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmUserEmail(string userId, string confirmationToken)
        {
            var result = await _accountService.ConfirmUserEmailAsync(userId!, confirmationToken!);

            if (result)
            {
                return RedirectPermanent(@"https://www.google.com/webhp?authuser=0");
            }
            else
            {
                return BadRequest("Failed to confirm user email.");
            }
        }

        private string GenerateCallBackUrl(string token, string userId)
        {
            var encodedToken = Uri.EscapeDataString(token);
            var encodedUserId = Uri.EscapeDataString(userId);
            var callBackUrl = $"{Request.Scheme}://{Request.Host}/api/Account/confirm-email?userId={encodedUserId}&confirmationToken={encodedToken}";
            return callBackUrl;
        }
    }
}
