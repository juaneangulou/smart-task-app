using AutoMapper;
using SmartTaskApp.Auth.WebApi.Application.Commands.RegisterUser;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.WebApi.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserCommand, User>();
        }
    }
}