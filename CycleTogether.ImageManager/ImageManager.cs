using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CycleTogether.Contracts;
using DAL.Contracts;
using System;
using System.Collections.Generic;
using WebModels;
using AutoMapper;
using DAL.Models;
using System.Linq;

namespace CycleTogether.ImageManager
{
    public class ImageManager : IPicture
    {
        private readonly Account _account;
        private readonly Cloudinary _cloudinary;
        private readonly IImageRepository _images;
        private readonly IMapper _mapper;
        private readonly IUserRepository _users;
        private readonly IRouteRepository _routes;
        public ImageManager(IImageRepository images, IMapper mapper, IUserRepository users, IRouteRepository routes)
        {
            _account = new Account("diroaq4wp", "712983898652981", "tFVw4-kYq09AwH9srE84eNWBEnk");
            _cloudinary = new Cloudinary(_account);
            _images = images;
            _mapper = mapper;
            _users = users;
            _routes = routes;

        }


        public Picture Upload(string imagePath, string routeId)
        {
            var image = UploadToCoudinary(imagePath);
            return Save(image, routeId);
        }

        private Picture Save(ImageUploadResult image, string routeId)
        {
            var newImage = new Picture()
            {
                Path = image.SecureUri.ToString(),
                RouteId = Guid.Parse(routeId),
                PublicId = image.PublicId
            };
            var createdImage = _images.Create(_mapper.Map<PictureEntry>(newImage));
            AddImageToRoute(routeId, createdImage);
            return _mapper.Map<Picture>(createdImage);
        }

        private ImageUploadResult UploadToCoudinary(string imagePath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@imagePath),
            };
            return _cloudinary.Upload(uploadParams);
        }

        public Picture Get(string id)
        {
            var image = _images.GetById(Guid.Parse(id));
            var map = _mapper.Map<Picture>(image);
            return map;

        }

        public List<Picture> GetAll(string routeId)
        {
            return _routes.GetById(Guid.Parse(routeId)).
                   Images.Select(img => _mapper.
                   Map<Picture>(img)).
                   ToList();
        }

        public void Delete(string imageId, string currentUserId, string routeId)
        {
            if (IsRouteCreator(currentUserId, routeId))
            {
                var publicId = _images.GetById(Guid.Parse(imageId)).PublicId;
                DeleteFromCloudinary(publicId);
                _images.Delete(Guid.Parse(imageId));                                
            }

        }

        private void DeleteFromCloudinary(string publicId)
        {
            var image = new DelResParams() { PublicIds = new List<string>() { publicId } };
            _cloudinary.DeleteResources(image);
        }

        private bool IsRouteCreator(string currentUserId, string routeId)
        {
            return _users.GetById(Guid.Parse(currentUserId)).Routes.Any(r => r.Id == Guid.Parse(routeId));
        }

        private void AddImageToRoute(string routeId, PictureEntry newImage)
        {
            _routes.AddPicture(Guid.Parse(routeId), newImage);
        }
    }
}
