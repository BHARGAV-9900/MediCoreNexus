using AutoMapper;
using InventoryEntity = MediCore.Domain.Entities.Inventory;

namespace MediCore.Application.Features.Inventory;

public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile()
    {
        CreateMap<InventoryEntity, InventoryDto>()
            .ForMember(
                dest => dest.MedicineName,
                opt => opt.MapFrom(
                    src => src.Medicine != null
                        ? src.Medicine.Name
                        : string.Empty));
    }
}