using AutoMapper;
using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionDTO, Transaction>().ReverseMap();
            // CreateMap<Transaction, TransactionDTO>();
        }
    }
}