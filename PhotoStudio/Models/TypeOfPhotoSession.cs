using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Models
{
    public enum TypeOfPhotoSession
    {
        Family,
        Birthday,
        Wedding,
        Personal
    }

    public override string ToString()
    {
        string Master = "";

        

        return $"{Human.FirstName} {Human.LastName}";
    }
}
