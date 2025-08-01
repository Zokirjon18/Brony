using Brony.Domain;
using Brony.Models;
using Brony.Models.Stadiums;

namespace Brony.Services.Stadiums;

public interface IStadiumService
{
    void Create(StadiumCreateModel stadiumCreateModel);
    void Update(StadiumUpdateModel stadiumUpdateModel);
    void Delete(int id);
    Stadium Get(int id);
    List<Stadium> GetAll(
        string search,
        decimal? price,
        DateTime? startTime,
        DateTime? endTime);
}
