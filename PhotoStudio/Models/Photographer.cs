﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Models
{
    public class Photographer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public List<PhotoSession> Sessions { get; set; }

        public override string ToString() => $"{FirstName} {LastName} \n{PhoneNumber}";
    }
}
