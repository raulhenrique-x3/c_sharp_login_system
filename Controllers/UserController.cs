using API_USER.Database;
using API_USER.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_USER.Services;

namespace API_USER.Controllers;
[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
  private readonly ApplicationDBContext _context;
  private readonly GenerateJwtService _generateJwtService;

  public UserController(ApplicationDBContext context, GenerateJwtService generateJwtService)
  {
    _context = context;
    _generateJwtService = generateJwtService;
  }

  [HttpPost("register", Name = "Register")]
  public async Task<IActionResult> Register([FromBody] UserDTO userDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    try
    {
      var userExistis = await _context.Users.AnyAsync(u => u.Email == userDto.Email);
      if (userExistis)
      {
        return BadRequest("User already exists");
      }

      var User = new User
      {
        Name = userDto.Name,
        Email = userDto.Email,
        Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
        CreatedAt = DateTime.Now
      };

      _context.Users.Add(User);
      await _context.SaveChangesAsync();
      return Ok(userDto);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPost("login", Name = "Login")]
  public async Task<IActionResult> Login([FromBody] AuthDTO authDTO)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    try
    {
      var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == authDTO.Email);
      if (user == null)
      {
        return BadRequest("Invalid Email or Password");
      }

      var token = _generateJwtService.GenerateJwtToken(user);
      return Ok(new { token });
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}