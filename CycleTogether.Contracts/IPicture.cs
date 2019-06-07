using System;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IPicture
    {
        Picture Upload(string imagePath, string routeId);
        Picture Get(string publicId);
        List<Picture> GetAll(string routeId);
        void Delete(string publicId, string userId, string routeId);

    }
}
