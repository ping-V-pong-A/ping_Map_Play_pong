using System.ComponentModel.DataAnnotations;

namespace ping_Map_Play_pong.Contracts;

public record RegistrationRequest(
    [Required] string Email,
    [Required] string Username,
    [Required] string Password);

