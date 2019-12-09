using System;
using System.Threading.Tasks;
using CIS174WebApp.Entity;
using CIS174WebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CIS174WebApp.Controllers
{
    public class AuthorController : Controller
    {
         private readonly AuthorServices _author; // create an instance of AuthorService class
        private readonly UserManager<AppUser> _userServices;
        private readonly IAuthorizationService _authService;
       readonly ILogger<AuthorController> _log;

        //Create a PersonInfo constructor
        public AuthorController(AuthorServices author, UserManager<AppUser> userServices, 
            IAuthorizationService authService, ILogger<AuthorController> log)
        {
            _author = author;
            _userServices = userServices;
            _authService = authService;
            _log = log;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var models = _author.GetAuthor();

            return View(models);
        }
        
       
       [Authorize]
        public async Task<IActionResult> View(int id)
        {
            var model = _author.GetAuthorDetails(id);

            if (model == null)
            {
                _log.LogWarning("Author with Id: {id} found", NotFound());
                return NotFound(); //Return not found (404 friendly page) if model is null
               
            }
            var author = _author.GetAuthor(id);
            var iSAuthorized = await _authService.AuthorizeAsync(User, author, "CanManageAuthor");
            var authResult = await _authService.AuthorizeAsync(User, author, "IsAdmin");
            model.CanManageAuthor = iSAuthorized.Succeeded;
            model.CanManageAuthor = authResult.Succeeded;
            _log.LogWarning("User does not have permission to edit author");
            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new CreateAuthorCommand());
        }       
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateAuthorCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var appUser = await _userServices.GetUserAsync(User);
                    var id = _author.CreateAuthor(command, appUser); 
                    return RedirectToAction(nameof(View), new {id = id});
                }
            }
            catch (Exception)
            {
                
                // Add a model-level error by using an empty string key
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured saving author"
                );
                _log.LogError("An error occured saving author");
            }

            //If we got to here, something went wrong
            return View(command);
        }
        [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var person = _author.GetAuthor(id);
            var authResult = await _authService.AuthorizeAsync(User, person, "CanManageAuthor");
          //  var authResult1 = await _authService.AuthorizeAsync(User, person, "CanEditPerson");
          //  var authResult2 = await _authService.AuthorizeAsync(User, person, "ContentEditor");


            if (!authResult.Succeeded )
            {
                return new ForbidResult();
            }

            var model = _author.GetAuthorForUpdate(id);

            if (model == null)
            {
                _log.LogWarning("Author with Id: {id} found", NotFound());

                return NotFound(); //Return not found (404 friendly page) if model is null
            }

            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuthorCommand command)
        {
            try
            {
                
                var author = _author.GetAuthor(command.AuthorId);
                var authResult = await _authService.AuthorizeAsync(User, author, "CanManageAuthor");
              //  var authResult1 = await _authService.AuthorizeAsync(User, person, "CanEditPerson");
                var authResult2 = await _authService.AuthorizeAsync(User, author, "ContentEditor");


                if (!authResult.Succeeded || !authResult2.Succeeded)
                {
                    return new ForbidResult();
                }
                
                if (ModelState.IsValid)
                {
                    _author.UpdateAuthor(command);
                    return RedirectToAction(nameof(View), new { id = command.AuthorId });
                }
            }
            catch (Exception)
            {

                // Add a model-level error by using an empty string key
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured updating author"
                );
                _log.LogError("An error occured updating author");

            }

            //If we got to here, something went wrong
            return View(command);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult DeleteAuthor(int id)
        {
            _author.DeleteAuthor(id);
            return RedirectToAction(nameof(Index));
        }
        /*
        [Authorize("IsAdmin")]
        public async Task<IActionResult> Admin()
        {
            var author = _author.GetAuthor();
            var isAuthorized = await _authService.AuthorizeAsync(User, author, "IsAdmin");
            if (!isAuthorized.Succeeded)
                return  new ForbidResult();
            return View(author);
        }
        public IActionResult AdminView(int id)
        {
            var model = _author.GetAuthorDetails(id);

            if (model == null)
            {
                _log.LogWarning("Author with Id: {id} found", NotFound());

                return NotFound(); //Return not found (404 friendly page) if model is null
            }

            return View(model);
        }
        */
    }
}