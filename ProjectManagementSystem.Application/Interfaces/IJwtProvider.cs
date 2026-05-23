using ProjectManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
