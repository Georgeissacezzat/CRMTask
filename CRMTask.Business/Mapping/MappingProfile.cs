using AutoMapper;
using CRMTask.DataAccess.DTOs.CampaignDTOs;
using CRMTask.DataAccess.DTOs.MailSTOs;
using CRMTask.DataAccess.DTOs.TemplateDTOs;
using CRMTask.DataAccess.DTOs.UserDTOs;
using CRMTask.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Template, TemplateDto>().ReverseMap();
            CreateMap<CreateTemplateDto, Template>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MailchimpTemplateId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateTemplateDto, Template>()
                .ForMember(dest => dest.UpdatedAt,opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember, context) =>srcMember != null));

            CreateMap<Mail, MailDto>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.Campaign.Name))
                .ForMember(dest => dest.TemplateName, opt => opt.MapFrom(src => src.Template.Name));

			CreateMap<CreateCampaignDto, Campaign>();
			CreateMap<UpdateCampaignDto, Campaign>();
			CreateMap<Campaign, CampaignDto>();

			CreateMap<CreateUserDTO, User>();
			CreateMap<User, UserBaseDTO>();


			CreateMap<UpdateUserDTO, User>();

			CreateMap<User, UserListDTO>()
				.ForMember(dest => dest.FullName,
						   opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
		}
    }
}
