using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Common.AppSettingModels;
using Web.Common.ExceptionModels;
using Web.Data.EntityModels;
using Web.Repository.UnitOfWork;
using Web.Service.DtoModels;

namespace Web.Service.UserServices
{

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }
        public UserDto GetUserByName(string name)
        {
            User user = _unitOfWork.UserRepository.GetUserByName(name);
            //IQueryable<User> users = _unitOfWork.UserRepository.Find(s => s.Name == name);
            if(user == null)
            {
                throw new NotFoundException($"User {name} does not exist.");             
            }
            return _mapper.Map<UserDto>(user);
        }

        public void Register(UserRegisterModel newUserInfo)
        {
            User newUser = _mapper.Map<User>(newUserInfo);
            newUser.RoleId = 1;
            CreatePasswordHash(newUserInfo.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            try
            {
                _unitOfWork.UserRepository.Add(newUser);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }         
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {         
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool AccountIsExist(string accName)
        {
            User user = _unitOfWork.UserRepository.GetUserByAccount(accName);
            if(user== null)
            {
                return false;
            }
            return true;
        }

        public User Authenticate(UserLoginModel userLogin)
        {
            User user = _unitOfWork.UserRepository.GetUserByAccount(userLogin.Account.Trim());
            if(!VerifyPassword(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role,user.Role.Name)

                }),
                Expires = DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {          
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
