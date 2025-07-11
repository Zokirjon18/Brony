using Brony.Domain;
using Brony.Models;

namespace Brony.Services.Stadiums;

public interface IStadiumService
{
    void Create(StadiumCreateModel stadiumCreateModel);
    
    void Update(
        StadiumUpdateModel stadiumUpdateModel);
    
    void Delete(int id);
    
    Stadium Get(int id);
    
    List<Stadium> GetAll();
    
    List<Stadium> Search(string search);
    
    List<Stadium> GetFilteredList(string location, decimal? price, DateTime? startDate, DateTime? endDate);
}
