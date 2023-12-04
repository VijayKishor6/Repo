using CRUD.Repository.Models;
using CRUD.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IValidator<Register> _validator;
        private readonly IValidator<Login> _loginValidator;

        public RegisterController(IProductRepository productRepo, IValidator<Register> validator, IValidator<Login> loginValidator)
        {
            _productRepo = productRepo;
            _validator = validator;
            _loginValidator = loginValidator;
        }



        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            var validator = new RegisterValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            if (ModelState.IsValid)
            {
                await _productRepo.RegisterAdd(model);
                return RedirectToAction("AlertMessage");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {
            var validationResult = _loginValidator.Validate(model);

            if (validationResult.IsValid)
            {
                // Validation passed, proceed with your login logic
                if (_productRepo.UserValid(model))
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            else
            {
                // Validation failed, return errors to the view
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(model);
            }

            return View(); // Handle other scenarios as needed
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult AlertMessage()
        {
            return View();
        }
    }
}
