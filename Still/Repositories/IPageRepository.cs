using Still.Models;
using System.Collections.Generic;

namespace Still.Repositories
{
    public interface IPageRepository
    {
        List<Page> GetAllPagesByUser(int userProfileId);
        Page GetPageById(int id);
        int Add(Page page);
        void AddPagePicture(PagePicture pagePicture);
        void Delete(int id);
    }
}