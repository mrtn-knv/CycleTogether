using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CycleTogether.Contracts;
using DAL.Contracts;
using DAL.Models;
using WebModels;

namespace CycleTogether.Authentication
{
    public class Authenticator : IAuthentication
    {
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _db;

        public Authenticator(IMapper mapper, IUserRepository users, ITokenGenerator tokenGenerator, IUserEquipmentRepository userEquipments, IUnitOfWork db)
        {
            _db = db;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        public void Register(User user)
        {
            if (!_db.Users.GetAll().Any(u => u.Email == user.Email))
            {
                user.Password = this._tokenGenerator.HashPassword(user.Password);
                var savedUser = SaveUser(user);
                SaveUserEquipments(savedUser.Id, user.Equipments);
                _db.SaveChanges();
            }
            
        }

        private void SaveUserEquipments(Guid id, List<Guid> equipments)
        {
            if (equipments != null)
            foreach (var equipment in equipments)
            {
                _db.UserEquipments.Create(new UserEquipmentEntry { UserId = id, EquipmentId = equipment });
            }
        }

        public string Authenticate(string email, string password)
        {
            return _tokenGenerator.Generate(email, password);
        }

        private User SaveUser(User user)
        {
            var newUser = _db.Users.Create(_mapper.Map<UserEntry>(user));            
            return _mapper.Map<User>(newUser);
        }
    }
}
