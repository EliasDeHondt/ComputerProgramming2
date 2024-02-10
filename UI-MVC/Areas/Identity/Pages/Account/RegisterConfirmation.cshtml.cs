/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PadelClubManagement.UI.Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    public RegisterConfirmationModel(UserManager<IdentityUser> userManager)
    {
            _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return NotFound("Unable to load user with email '" + email + "'.");
        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
        return RedirectToPage("/");
    }
}