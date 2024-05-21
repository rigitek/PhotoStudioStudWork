using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Models
{
    public class TypeOfPhotoSession
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
    }
}
