using Brony.Domain;
using Brony.Models;

namespace Brony.Services.Stadiums;

public interface IStadiumService
{
    void Create(StadiumCreateModel stadiumCreateModel);
    
    void Update(
        StadiumUpdateModel stadiumUpdateModel);
    
    void Delete(int id);
    
    StadiumViewModel Get(int id);
    
    List<Stadium> GetAll();
    
    List<StadiumViewModel> Search(string search);
    
    List<StadiumViewModel> GetFilteredList(string location, decimal? price, DateTime? startDate, DateTime? endDate);
}
