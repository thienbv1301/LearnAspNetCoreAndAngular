using AutoMapper;
using System;
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
            if(user == null)
            {
                throw new NullReferenceException();             
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}
