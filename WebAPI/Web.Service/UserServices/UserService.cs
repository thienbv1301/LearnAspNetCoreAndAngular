using AutoMapper;
using System;
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

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;

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
            User newUser = new User();
            CreatePasswordHash(newUserInfo.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.Account = newUserInfo.Account;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.Name = newUserInfo.Name;
            newUser.RoleId = 1;          
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
    }
}
