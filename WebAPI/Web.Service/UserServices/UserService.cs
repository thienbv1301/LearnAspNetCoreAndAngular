using AutoMapper;
using System;
using System.Linq;
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
    }
}
