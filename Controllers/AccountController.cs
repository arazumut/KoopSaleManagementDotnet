using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using KoopSatis.Models.Identity;
using KoopSatis.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KoopSatis.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                
                if (result.Succeeded)
                {
                    // Aktiviteyi kaydet
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    await LogUserActivityAsync(user.Id, "Login", "Kullanıcı giriş yaptı");
                    
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                
                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                    return View(model);
                }
            }
            
            return View(model);
        }
        
        // GET: /Account/Lockout
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }
        
        // GET: /Account/Register
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View();
        }
        
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmployeePosition = model.EmployeePosition,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true
                };
                
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(user, model.SelectedRole);
                    }
                    
                    // Aktiviteyi kaydet
                    await LogUserActivityAsync(
                        User.FindFirstValue(ClaimTypes.NameIdentifier),
                        "UserCreated",
                        $"Kullanıcı oluşturuldu: {user.Email}",
                        "ApplicationUser",
                        user.Id);
                    
                    return RedirectToAction(nameof(UserList));
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View(model);
        }
        
        // GET: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await LogUserActivityAsync(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                "Logout",
                "Kullanıcı çıkış yaptı");
                
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
        // GET: /Account/UserList
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();
            
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    EmployeePosition = user.EmployeePosition,
                    IsActive = user.IsActive,
                    Roles = string.Join(", ", roles)
                });
            }
            
            return View(userViewModels);
        }
        
        // GET: /Account/Edit/id
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            var userRoles = await _userManager.GetRolesAsync(user);
            
            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmployeePosition = user.EmployeePosition,
                IsActive = user.IsActive,
                SelectedRole = userRoles.FirstOrDefault()
            };
            
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View(model);
        }
        
        // POST: /Account/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }
                
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.EmployeePosition = model.EmployeePosition;
                user.IsActive = model.IsActive;
                
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // Rol güncelleme
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    
                    if (!string.IsNullOrEmpty(model.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(user, model.SelectedRole);
                    }
                    
                    // Şifre değiştir (eğer şifre boş değilse)
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        // Şifreyi değiştir
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var resetResult = await _userManager.ResetPasswordAsync(
                            user, token, model.NewPassword);
                            
                        if (!resetResult.Succeeded)
                        {
                            foreach (var error in resetResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            ViewBag.Roles = _roleManager.Roles.ToList();
                            return View(model);
                        }
                    }
                    
                    // Aktiviteyi kaydet
                    await LogUserActivityAsync(
                        User.FindFirstValue(ClaimTypes.NameIdentifier),
                        "UserUpdated",
                        $"Kullanıcı güncellendi: {user.Email}",
                        "ApplicationUser",
                        user.Id);
                        
                    return RedirectToAction(nameof(UserList));
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View(model);
        }
        
        // GET: /Account/Delete/id
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            var userRoles = await _userManager.GetRolesAsync(user);
            
            var model = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmployeePosition = user.EmployeePosition,
                IsActive = user.IsActive,
                Roles = string.Join(", ", userRoles)
            };
            
            return View(model);
        }
        
        // POST: /Account/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            // Kullanıcı sayısını kontrol et
            var userCount = await _userManager.Users.CountAsync();
            if (userCount <= 1) 
            {
                TempData["ErrorMessage"] = "Sistemde en az bir yönetici kalmalıdır.";
                return RedirectToAction(nameof(UserList));
            }
            
            // Aktiviteyi kaydet
            await LogUserActivityAsync(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                "UserDeleted",
                $"Kullanıcı silindi: {user.Email}",
                "ApplicationUser",
                user.Id);
                
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(UserList));
            }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            
            return View();
        }
        
        // Kullanıcı aktivitelerini kaydetmek için yardımcı metod
        private async Task LogUserActivityAsync(
            string userId,
            string activityType,
            string description,
            string entityName = null,
            string entityId = null)
        {
            var log = new UserActivityLog
            {
                UserId = userId,
                ActivityType = activityType,
                Description = description,
                Timestamp = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                UserAgent = Request.Headers["User-Agent"].ToString(),
                EntityName = entityName,
                EntityId = entityId != null ? (int?)int.Parse(entityId) : null
            };
            
            _context.UserActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
    
    // View Modeller
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Şifre gereklidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }
    }
    
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad gereklidir")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Soyad gereklidir")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }
        
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Pozisyon")]
        public string EmployeePosition { get; set; }
        
        [Required(ErrorMessage = "Şifre gereklidir")]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Şifre (Tekrar)")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Rol")]
        public string SelectedRole { get; set; }
    }
    
    public class EditUserViewModel
    {
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Ad gereklidir")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Soyad gereklidir")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }
        
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Pozisyon")]
        public string EmployeePosition { get; set; }
        
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre (boş bırakılabilir)")]
        public string NewPassword { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre (Tekrar)")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmNewPassword { get; set; }
        
        [Display(Name = "Rol")]
        public string SelectedRole { get; set; }
    }
    
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeePosition { get; set; }
        public bool IsActive { get; set; }
        public string Roles { get; set; }
    }
} 