using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BudgetTracker.Database.Entities;
using BudgetTracker.Database.Interfaces;
using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BudgetTracker.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private string _jwtSecret;
    public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtSecret = configuration["Jwt:Secret"];
    }
    public async Task<bool> CreateUser(UserDTO userDTO)
    {
        var user = _mapper.Map<User>(userDTO);
        return await _userRepository.CreateUser(user);

    }

    public async Task<string> LoginUser(LoginDTO loginDTO)
    {

        var user = await _userRepository.AuthenticateUser(loginDTO.UserName);
        if (user == null)
        {
            return null;
        }
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null when generating JWT token.");
        }

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            issuer: "Paltech",
            audience: "BudgetTrackers",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}";
    }

    public async Task<UserDTO> UpdateUser(int id, UpdateDTO updatedUserDTO)
    {
        User existingUser = _mapper.Map<User>(updatedUserDTO);
        User newUser = await _userRepository.UpdateUser(id, existingUser);
        return _mapper.Map<UserDTO>(newUser);
    }

    public async Task<bool> DeleteUser(int id)
    {
        await _userRepository.DeleteUser(id);
        return true;
    }

    public async Task<List<UserDTO>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return _mapper.Map<List<UserDTO>>(users);
    }
}
