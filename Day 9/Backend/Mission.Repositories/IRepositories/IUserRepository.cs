﻿using Mission.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<string> DeleteUser(int id);
        Task<UserResponseModel> GetUserById(int id);

        Task<string> UpdateUser(int id, UserRequestModel model);

        Task<List<UserResponseModel>> GetAllUsers();
    }
}
