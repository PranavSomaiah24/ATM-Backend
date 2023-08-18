using AutoMapper;
using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Profiles
{
    public class BalanceProfile : Profile
    {
        public BalanceProfile()
        {
            CreateMap<BalanceDTO, Balance>().ReverseMap();
            //CreateMap<Balance, BalanceDTO>();
        }
    }
}