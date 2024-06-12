using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_USER.Models
{
  public class Auth
  {
    [Key]
    public required string Email { get; set; }
    public required string Password { get; set; }
  }
}