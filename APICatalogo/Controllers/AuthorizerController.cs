using APICatalogo.DTO;
using APICatalogo.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizerController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    private readonly GenerateToken generateToken;
    private readonly IConfiguration configuration;

    public AuthorizerController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,       
        IConfiguration configuration)   {
        this.userManager = userManager;
        this.signInManager = signInManager;  
        this.configuration = configuration;
        generateToken = new GenerateToken(this.configuration);
        
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> RegistrarUsuario(UserDTO user)
    {
      
        var identityUser = new IdentityUser
        {
            UserName = user.Email,
            Email = user.Email,
            EmailConfirmed = true
           
        };

        var result = await userManager.CreateAsync(identityUser,user.Password);
        if (!result.Succeeded)
        {
            return BadRequest("Falha ao criar usuário. Contacte o administrador ===>"+result.Errors);
        }
        await signInManager.SignInAsync(identityUser, false);
        return Ok(generateToken.GenerateUserToken(user));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUsuario(UserDTO user)
    {
         var result = await signInManager.PasswordSignInAsync(user.Email,
        user.Password, isPersistent:false,lockoutOnFailure:false);

        if (!result.Succeeded)
        {
            return BadRequest("Login inválido.");
        }
        return Ok(generateToken.GenerateUserToken(user));

    }

}
