using AutoMapper;
using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDTO, Customer>().ReverseMap();
            // CreateMap<Customer, CustomerDTO>();
        }
    }
}