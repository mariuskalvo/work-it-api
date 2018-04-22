using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.RepositoryInterfaces
{
    public interface IGroupThreadRepository
    {
        Task<bool> Add(GroupThread groupThread);
    }
}
