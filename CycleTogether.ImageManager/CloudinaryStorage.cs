using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CycleTogether.BindingModels;
using CycleTogether.Contracts;
using DAL.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using WebModels;

namespace CycleTogether.ImageManager
{
    public class CloudinaryStorage : IGallery
    {
        private readonly Account _account;
        private readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly CloudinaryAccount _credentials;
        private readonly IUnitOfWork _db;
   
        public CloudinaryStorage(IUnitOfWork db,
                                 IMapper mapper,
                                 CloudinaryAccount credentials)
        {
            _db = db;
            _credentials = credentials;
            _account = new Account(_credentials.Cloud, _credentials.ApiKey, _credentials.ApiSecret);
            _cloudinary = new Cloudinary(_account);
            _mapper = mapper;

        }

        public Picture Upload(IFormFile pic, string routeId)
        {
            var image = UploadToCoudinary(pic);
            return Save(image, routeId);
        }

        public Picture Get(string id)
        {
            return _mapper.Map<Picture>(_db.Images.GetById(Guid.Parse(id)));
        }

        public List<Picture> GetAll(string routeId)
        {
            return _db.Routes.GetById(Guid.Parse(routeId)).
                   Pictures.Select(img => _mapper.
                   Map<Picture>(img)).
                   ToList();
        }

        public void Delete(string imageId, string currentUserId, string routeId)
        {
            if (IsRouteCreator(currentUserId, routeId))
            {
                var publicId = _db.Images.GetById(Guid.Parse(imageId)).PublicId;
                DeleteFromCloudinary(publicId);
                _db.Images.Delete(Guid.Parse(imageId));
                _db.SaveChanges();
            }
        }

        private void DeleteFromCloudinary(string publicId)
        {
            var image = new DelResParams() { PublicIds = new List<string>() { publicId } };
            _cloudinary.DeleteResources(image);
        }

        private bool IsRouteCreator(string currentUserId, string routeId)
        {
            return _db.Users.GetById(Guid.Parse(currentUserId)).Routes.Any(r => r.Id == Guid.Parse(routeId));
        }

        private void AddImageToRoute(string routeId, PictureEntry newImage)
        {
            _db.Routes.AddPicture(Guid.Parse(routeId), newImage);
        }

        private Picture Save(ImageUploadResult image, string routeId)
        {
            var newImage = FillImageProperties(image, routeId);
            var createdImage = _db.Images.Create(_mapper.Map<PictureEntry>(newImage));
            AddImageToRoute(routeId, createdImage);
            _db.SaveChanges();
            return _mapper.Map<Picture>(createdImage);
        }

        private static Picture FillImageProperties(ImageUploadResult image, string routeId)
        {
            return new Picture()
            {
                Path = image.SecureUri.ToString(),
                RouteId = Guid.Parse(routeId),
                PublicId = image.PublicId
            };
        }

        private ImageUploadResult UploadToCoudinary(IFormFile pic)
        {
            var image = new ImageUploadParams() {File = new FileDescription(pic.Name, pic.OpenReadStream()) };           
            return _cloudinary.Upload(image);
        }

    }
}
