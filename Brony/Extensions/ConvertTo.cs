using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static UserRegisterModel ConvertingTo(this User obj)
{
    return new UserRegisterModel
    {
        FirstName = obj.FirsName,
        LastName = obj.FirsName,
        PhoneNumber = obj.PhoneNumber
    };
}
