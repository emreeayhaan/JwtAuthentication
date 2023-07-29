using AutoMapper;
using Data.Models;
using DataAccess;

namespace Business.Services
{
    public class AdminService
    {
        private MongoDB<AdminModels> adminService;
        private IMapper mapper;

        public AdminService(IMapper mapper)
        {
            adminService = new MongoDB<AdminModels>();
            this.mapper = mapper;
        }

        public void Add(AdminModels user)
        {
            user.Password = user.Password;
            adminService.Add(user);
        }

        public void Delete(string id)
        {
            adminService.Delete(x => x.Id == id);
        }

        public List<AdminModels> GetAll()
        {
            return adminService.GetAll();
        }

        public AdminModels GetById(string id)
        {
            return adminService.Get(x => x.Id == id);
        }

        public AdminModels GetByEmail(string email)
        {
            return adminService.Get(x => x.Email == email);
        }

        public void Update(AdminModels userForUpdate)
        {
            adminService.Update(x => x.Id == userForUpdate.Id, userForUpdate);
        }

        public AdminModels Login(LoginModel model)
        {
            return adminService.Get(x => x.Email == model.Email && x.Password == model.Password);
        }

        public AdminModels Register(AdminRegisterModel model)
        {
            var user = mapper.Map<AdminModels>(model);
            Add(user);
            return user;
        }
    }
}
