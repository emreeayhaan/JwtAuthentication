using AutoMapper;
using Data.Models;
using DataAccess;

namespace Business.Services
{
    public class UserService
    {
        private MongoDB<UserModels> userService;
        private IMapper mapper;

        public UserService(IMapper mapper)
        {
            userService = new MongoDB<UserModels>();
            this.mapper = mapper;
        }

        public void Add(UserModels user)
        {
            user.Password = user.Password;
            userService.Add(user);
        }

        public void Delete(string id)
        {
            userService.Delete(x => x.Id == id);
        }

        public List<UserModels> GetAll()
        {
            return userService.GetAll();
        }

        public UserModels GetById(string id)
        {
            return userService.Get(x => x.Id == id);
        }

        public UserModels GetByEmail(string email)
        {
            return userService.Get(x => x.Email == email);
        }

        public void Update(UserModels userForUpdate)
        {
            userService.Update(x => x.Id == userForUpdate.Id, userForUpdate);
        }

        public UserModels Login(LoginModel model)
        {
            return userService.Get(x => x.Email == model.Email && x.Password == model.Password);
        }

        public UserModels Register(UserRegisterModel model)
        {
            var user = mapper.Map<UserModels>(model);
            Add(user);
            return user;
        }
    }
}
