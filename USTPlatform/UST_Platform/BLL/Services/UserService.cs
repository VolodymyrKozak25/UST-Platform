using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        

        //public List<User> FindAll()
        //{
        //    return _userRepository.FindAll().ToList();
        //}

        //public User FindById(int id)
        //{
        //    return _userRepository.GetById(id);
        //}

        //public void RegisterUser(User user)
        //{
        //    _userRepository.Create(user);
        //}

        //public bool LoginUser(string login, string password)
        //{
        //    var user = _userRepository.FindAll().FirstOrDefault(u => u.Login == login);

        //    if (user == null) return false;


        //    return password == user.Password;
        //}

        //public void Delete(int id)
        //{
        //    _userRepository.Delete(id);
        //}

        //public User FindByLogin(string login)
        //{
        //    return _iUserRepository.GetByLogin(login);
        //}
    }
}
