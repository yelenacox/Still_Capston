using Still.Models;
using System.Collections.Generic;

namespace Still.Repositories
{
    public interface IPictureRepository
    {
        List<Picture> GetAllPictures();
        Picture GetPictureById(int id);
        void Add(Picture picture);
        void Update(Picture picture);
        void Delete(int id);
        List<Picture> GetUserPictures(int userProfileId);
    }
}