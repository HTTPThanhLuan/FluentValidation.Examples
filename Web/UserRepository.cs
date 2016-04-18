using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web
{
    public class UserRepository:IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            return new User[]
            {
                new User() {Id = 1, Email = "john@gmail.com", UserName = "john", Password = "123456"},
                new User() {Id = 2, Email = "rick@gmail.com", UserName = "rick", Password = "654321"},
            };
        }
    }

    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
    }
}