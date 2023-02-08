using Still.Models;
using System.Collections.Generic;

namespace Still.Repositories
{
    public interface IPageRepository
    {
        List<Page> GetAllPagesByUser(int userProfileId);
    }
}