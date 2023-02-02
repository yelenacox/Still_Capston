using Still.Models;
using System.Collections.Generic;

namespace Still.Repositories
{
    public interface IPictureRepository
    {
        List<Picture> GetAllPictures();
    }
}