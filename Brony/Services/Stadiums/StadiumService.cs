using Brony.Constants;
using Brony.Domain;
using Brony.Helpers;
using Brony.Models;
using Brony.Models.Stadiums;
using Brony.Services.Bookings;

namespace Brony.Services.Stadiums;

public class StadiumService : IStadiumService
{
    private readonly BookingService bookingService;
    public void Create(StadiumCreateModel stadiumCreateModel)
    {
        var convertedStadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);

        var existingStadium = convertedStadiums.Find(x => x.Name == stadiumCreateModel.Name);
        
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
        convertedStadiums.Add(new Stadium
        {
            Name = stadiumCreateModel.Name,
            PhoneNumber = stadiumCreateModel.PhoneNumber,
            Description = stadiumCreateModel.Description,
            Location = stadiumCreateModel.Location, 
            Length = stadiumCreateModel.Length,
            Price = stadiumCreateModel.Price,
            Width = stadiumCreateModel.Width,
        });
        FileHelper.WriteToFile(PathHolder.StadiumsFilePath, convertedStadiums);    
    }

    public void Update(StadiumUpdateModel model)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        var existStadium = stadiums.Find(x => x.Id == model.Id)
            ?? throw new Exception("Stadium is not found");


        var alreadyExistStadium = stadiums.Find(x => x.Name == model.Name);

        if (alreadyExistStadium != null)
        {
            throw new Exception($"Stadium already exists with this name = {model.Name}");
        }

        if (!string.IsNullOrEmpty(model.PhoneNumber))
        {
            throw new Exception("Phone should not be null or empty");
        }

        if (model.PhoneNumber.Length != 12)
        {
            throw new Exception("Phone number should be 12 characters");
        }

        if (!model.PhoneNumber.StartsWith("+998"))
        {
            throw new Exception("Phone number should start with '+998'");
        }

        existStadium.Name = model.Name;
        existStadium.Location = model.Location;
        existStadium.Length = model.Length;
        existStadium.Width = model.Width;
        existStadium.PhoneNumber = model.PhoneNumber;
        existStadium.Price = model.Price;
        existStadium.Description = model.Description;

        FileHelper.WriteToFile(PathHolder.StadiumsFilePath, stadiums);
    }

    public void Delete(int id)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        var existStadium = stadiums.Find(x => x.Id == id)
            ?? throw new Exception("Stadium is not found");

        stadiums.Remove(existStadium);

        FileHelper.WriteToFile(PathHolder.StadiumsFilePath, stadiums);
    }

    public Stadium Get(int id)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        var existStadium = stadiums.Find(x => x.Id == id)
            ?? throw new Exception("Stadium is not found");

        return existStadium;
    }

    public List<Stadium> GetAll(string search)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);

        if (!string.IsNullOrEmpty(search))
        {
            stadiums = Search(search);
        }
        return stadiums;
    }

    public List<Stadium> GetFilteredList(
        string location,
        decimal? price,
        DateTime? startTime,
        DateTime? endTime)
    {
        var result = new List<Stadium>();

        var locationResult = new List<Stadium>();
        var priceResult = new List<Stadium>();
        var dateTimeResult = new List<Stadium>();

        var stadiums = new List<Stadium>();

        if (!string.IsNullOrEmpty(location))
        {
            locationResult = GetAllByLocation(location);

            result = locationResult;
        }

        if (price != null)
        {
            if (locationResult.Any())
            {
                priceResult = GetAllByPrice(locationResult, Convert.ToDecimal(price));

                result = priceResult;
            }
            else
            {
                priceResult = GetAllByPrice(stadiums, Convert.ToDecimal(price));

                result = priceResult;
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

                result = dateTimeResult;
            }
            else if (locationResult.Any())
            {
                dateTimeResult = GetAllByDateTime(
                    locationResult,
                    Convert.ToDateTime(startTime),
                    Convert.ToDateTime(endTime));

                result = dateTimeResult;
            }
            else
            {
                dateTimeResult = GetAllByDateTime(
                    stadiums,
                    Convert.ToDateTime(startTime),
                    Convert.ToDateTime(endTime));

                result = dateTimeResult;
            }
        }

        return result;
    }
    
    private List<Stadium> GetAllByLocation(string location)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
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
            var startTimePeaces = stadium.StartWorkingTime;
            var startTimeHour = Convert.ToInt32(startTimePeaces);

            var endTimePeaces = stadium.EndWorkingTime;
            var endTimeHour = Convert.ToInt32(endTimePeaces);

            if (startTimeHour <= startTime.Hour || endTimeHour >= endTime.Hour)
            {
                var stadiumBookings = bookingService.GetAllByStadiumId(stadium.Id);

                foreach (var item in stadiumBookings)
                {
                    if (item.StartTime != startTime || item.EndTime != endTime)
                    {
                        result.Add(stadium);
                    }
                }
            }
        }
        
        return result;
    }

    private List<Stadium> Search(string search)
    {
        var stadiums = FileHelper.ReadFromFile<Stadium>(PathHolder.StadiumsFilePath);
        var result = new List<Stadium>();

        if (!string.IsNullOrEmpty(search))
        {
            string trimedString = search.TrimStart(' ').ToLower();

            foreach (var stadium in stadiums)
            {
                if (stadium.Name.ToLower().Contains(trimedString))
                {
                    result.Add(stadium);
                }
            }
        }

        return result;
    }
}