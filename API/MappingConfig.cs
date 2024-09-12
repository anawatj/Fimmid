using AutoMapper;
using Core.Domains;
using Core.Dtos;

namespace API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AccountDto, Account>()
                .ForMember(t=>t.Id,t=>t.MapFrom(t=>Guid.Empty))
                .ForMember(t => t.Balance, t => t.MapFrom(t => t.Balance));
                config.CreateMap<Account, AccountDto>()
                .ForMember(t => t.Id, t => t.MapFrom(t => t.Id.ToString()))
                .ForMember(t => t.Balance, t => t.MapFrom(t => t.Balance));

                config.CreateMap<TransactionDto, Transaction>()
                .ForMember(t=>t.Id,t=>t.MapFrom(t=>Guid.Empty))
                .ForMember(t => t.FromAccountId, t => t.MapFrom(t => Guid.Parse(t.FromAccountId)))
                .ForMember(t => t.ToAccountId, t => t.MapFrom(t => Guid.Parse(t.ToAccountId)))
                .ForMember(t => t.Amount, t => t.MapFrom(t => t.Amount));

                config.CreateMap<Transaction, TransactionDto>()
                .ForMember(t => t.Id, t => t.MapFrom(t => t.Id.ToString()))
                .ForMember(t => t.FromAccountId, t => t.MapFrom(t => t.FromAccountId.ToString()))
                .ForMember(t => t.ToAccountId, t => t.MapFrom(t => t.ToAccountId.ToString()))
                .ForMember(t => t.Amount, t => t.MapFrom(t => t.Amount));
            });
            return mappingConfig;
        }
    }
}
