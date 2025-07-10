using Brony.Constants;
using Brony.Domain;
using Brony.Extensions;
using Brony.Helpers;
using Brony.Models;
using Brony.Services.Bookings;

namespace Brony.Services.Stadiums;

public class StadiumService : IStadiumService
{

    BookingService bookingService;
    public StadiumService(BookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    public void Create(StadiumCreateModel stadiumCreateModel)
    {
        var stadiums = ReadFromFileAndConvertToStadiumModel();

        var existingStadium = stadiums.Find(x => x.Name == stadiumCreateModel.Name);

        if (existingStadium != null)
        {
            throw new Exception($"Stadium with this name <{stadiumCreateModel.Name}> already exists");
        }

        if (!string.IsNullOrEmpty(stadiumCreateModel.PhoneNumber))
        {
            throw new Exception("Phone should not be null or empty");
        }

        if (stadiumCreateModel.PhoneNumber.Length != 13)
        {
            throw new Exception("Phone number should be 13 characters");
        }

        if (!stadiumCreateModel.PhoneNumber.StartsWith("+998"))
        {
            throw new Exception("Phone number should start with '+998'");
        }


        string content = $"{IdGeneration.IdGenerate(PathHolder.StadiumsFilePath)},{stadiumCreateModel.Name}," +
            $"{stadiumCreateModel.Width},{stadiumCreateModel.Length},{stadiumCreateModel.Price}," +
            $"{stadiumCreateModel.Location},{stadiumCreateModel.PhoneNumber},{stadiumCreateModel.Description}\n";

        File.WriteAllText(PathHolder.StadiumsFilePath, content);
    }

    public void Update(StadiumUpdateModel stadiumUpdateModel)
    {
        var stadiums = ReadFromFileAndConvertToStadiumModel();

        var stadiumForUpdation = stadiums.Find(x => x.Id == stadiumUpdateModel.Id);

        if (stadiumForUpdation == null)
        {
            throw new Exception("Stadium was not found");
        }

        var alreadyExistStadium = stadiums.Find(x => x.Name == stadiumUpdateModel.Name);

        if (alreadyExistStadium != null)
        {
            throw new Exception($"Stadium already exists with this name = {stadiumUpdateModel.Name}");
        }

        if (!string.IsNullOrEmpty(stadiumUpdateModel.PhoneNumber))
        {
            throw new Exception("Phone should not be null or empty");
        }

        if (stadiumUpdateModel.PhoneNumber.Length != 12)
        {
            throw new Exception("Phone number should be 12 characters");
        }

        if (!stadiumUpdateModel.PhoneNumber.StartsWith("+998"))
        {
            throw new Exception("Phone number should start with '+998'");
        }


        stadiumForUpdation.Name = stadiumUpdateModel.Name;
        stadiumForUpdation.Location = stadiumUpdateModel.Location;
        stadiumForUpdation.Length = stadiumUpdateModel.Length;
        stadiumForUpdation.Width = stadiumUpdateModel.Width;
        stadiumForUpdation.PhoneNumber = stadiumUpdateModel.PhoneNumber;
        stadiumForUpdation.Price = stadiumUpdateModel.Price;
        stadiumForUpdation.Description = stadiumUpdateModel.Description;

        List<string> stadiumsInStringListFormat = ToFileFormatExtensions.ToFileFormat<Stadium>(stadiums);
        File.WriteAllLines(PathHolder.StadiumsFilePath, stadiumsInStringListFormat);
    }

    public void Delete(int id)
    {
        var stadiums = ReadFromFileAndConvertToStadiumModel();

        var existStadium = stadiums.Find(x => x.Id == id);

        if (existStadium == null)
        {
            throw new Exception("Stadium was not found");
        }

        stadiums.Remove(existStadium);

        List<string> stadiumsInStringListFormat = ToFileFormatExtensions.ToFileFormat<Stadium>(stadiums);
        File.WriteAllLines(PathHolder.StadiumsFilePath, stadiumsInStringListFormat);
    }

    public StadiumViewModel Get(int id)
    {
        var stadiums = ReadFromFileAndConvertToStadiumModel();

        var existStadium = stadiums.Find(x => x.Id == id);

        if (existStadium == null)
        {
            throw new Exception("Stadium was not found");
        }

        return existStadium.ToStadiumViewModel();
    }

    public List<Stadium> GetAll()
    {
        var stadiums = ReadFromFileAndConvertToStadiumModel();

        return stadiums;
    }

    public List<StadiumViewModel> Search(string search)
    {
        var stadiums = ReadFromFileAndConvertToStadiumModel();

        var result = new List<StadiumViewModel>();

        if (!string.IsNullOrEmpty(search))
        {
            string trimedString = search.TrimStart(' ').ToLower();

            foreach (var stadium in stadiums)
            {
                if (stadium.Name.ToLower().Contains(trimedString))
                {
                    result.Add(stadium.ToStadiumViewModel());
                }
            }
        }

        return result;
    }

    public List<StadiumViewModel> GetFilteredList(
        string location,
        decimal? price,
        DateTime? startTime,
        DateTime? endTime)
    {
        var stadiums = ReadFromFileAndConvertToStadiumModel();

        var AllMatches = new List<Stadium>();

        var locationResult = new List<Stadium>();
        var priceResult = new List<Stadium>();
        var dateTimeResult = new List<Stadium>();

        if (!string.IsNullOrEmpty(location))
        {
            locationResult = GetAllByLocation(location);

            AllMatches = locationResult;
        }

        if (price != null)
        {
            if (locationResult.Any())
            {
                priceResult = GetAllByPrice(locationResult, Convert.ToDecimal(price));

                AllMatches.AddRange(priceResult);
            }
            else
            {
                priceResult = GetAllByPrice(stadiums, Convert.ToDecimal(price));

                AllMatches = priceResult;
            }
        }

        if (startTime != null && endTime != null)
        {
            if (priceResult.Any())
            {
                dateTimeResult = GetAllByDateTime(
                    priceResult,
                    Convert.ToDateTime(startTime),
                    Convert.ToDateTime(endTime));

                AllMatches = dateTimeResult;
            }
            else if (locationResult.Any())
            {
                dateTimeResult = GetAllByDateTime(
                    locationResult,
                    Convert.ToDateTime(startTime),
                    Convert.ToDateTime(endTime));

                AllMatches = dateTimeResult;
            }
            else
            {
                dateTimeResult = GetAllByDateTime(
                    stadiums,
                    Convert.ToDateTime(startTime),
                    Convert.ToDateTime(endTime));

                AllMatches = dateTimeResult;
            }
        }

        List<StadiumViewModel> result = new List<StadiumViewModel>();

        foreach(var stadium in AllMatches)
        {
            result.Add(stadium.ToStadiumViewModel());
        }

        return result;
    }

    private List<Stadium> GetAllByLocation(string location)
    {
        var stadiums = ReadFromFileAndConvertToStadiumModel();

        var result = new List<Stadium>();

        foreach (var stadium in stadiums)
        {
            if (stadium.Location.ToLower().Contains(location.ToLower()))
            {
                result.Add(stadium);
            }
        }

        return result;
    }

    private List<Stadium> GetAllByPrice(List<Stadium> stadiums, decimal price)
    {
        var result = new List<Stadium>();

        foreach (var stadium in stadiums)
        {
            if (stadium.Price == price)
            {
                result.Add(stadium);
            }
        }

        return result;
    }

    private List<Stadium> GetAllByDateTime(
        List<Stadium> stadiums,
        DateTime startTime,
        DateTime endTime)
    {
        var result = new List<Stadium>();

        foreach (var stadium in stadiums)
        {
            TimeSpan startTimeOfStadium = TimeSpan.Parse(stadium.StartWorkingTime);
            TimeSpan endTimeOfStadium = TimeSpan.Parse(stadium.EndWorkingTime);

            TimeSpan startTimeOfMatch = startTime.TimeOfDay;
            TimeSpan endTimeOfMatch = endTime.TimeOfDay;


            if (!(startTimeOfStadium > startTimeOfMatch || endTimeOfStadium < endTimeOfMatch))
            {
                if (!isStadiumBooked(stadium.Id,startTime,endTime))
                {
                    result.Add(stadium);
                }
            } 
        }
           
        return result;
    }
        
    private bool isStadiumBooked(int stadiumId, DateTime startTime,DateTime endTime)
    {
        var bookings = bookingService.GetAll();
        foreach (var booking in bookings)
        {
            if (booking.StadiumId == stadiumId)
            {
                if (booking.StartTime < endTime && booking.EndTime > startTime)
                {
                    return true;
                }
            }
        }

        return false;
    }
   
    public List<Stadium> ReadFromFileAndConvertToStadiumModel()
    {
        string text = File.ReadAllText(PathHolder.StadiumsFilePath);

        List<Stadium> convertedStadiums = text.ToStadium();

        return convertedStadiums;
    }
}