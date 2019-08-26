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
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserEquipmentRepository _userEquipments;

        public Authenticator(IMapper mapper, IUserRepository users, ITokenGenerator tokenGenerator, IUserEquipmentRepository userEquipments)
        {
            _users = users;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _userEquipments = userEquipments;
        }

        public void Register(User user)
        {
            if (!_users.GetAll().Any(u => u.Email == user.Email))
            {
                user.Password = this._tokenGenerator.HashPassword(user.Password);
                var savedUser = SaveUser(user);
                SaveUserEquipments(savedUser.Id, user.Equipments);
            }
            
        }

        private void SaveUserEquipments(Guid id, List<Guid> equipments)
        {
            if (equipments != null)
            foreach (var equipment in equipments)
            {
                _userEquipments.Create(new UserEquipmentEntry { UserId = id, EquipmentId = equipment });
            }
        }

        public string Authenticate(string email, string password)
        {
            return _tokenGenerator.Generate(email, password);
        }

        private User SaveUser(User user)
        {
            var newUser = _users.Create(_mapper.Map<UserEntry>(user));            
            return _mapper.Map<User>(newUser);
        }
    }
}
