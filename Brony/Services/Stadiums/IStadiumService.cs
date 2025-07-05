using Brony.Domain;

namespace Brony.Services.Stadiums;

public interface IStadiumService
{
    void Create(
        string name,
        float width,
        float length,
        decimal price,
        string location, 
        string phoneNumber,
        string description);
    
    void Update(
        int id,
        string name,
        float width,
        float length,
        decimal price,
        string location,
        string phoneNumber,
        string description);
    
    void Delete(int id);
    
    Stadium Get(int id);
    
    List<Stadium> GetAll();
    
    List<Stadium> Search(string search);
    
    List<Stadium> GetFilteredList(string location, decimal? price, DateTime? startDate, DateTime? endDate);
}
