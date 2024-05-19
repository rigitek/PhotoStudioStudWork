﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Models
{
    internal class PhotoSession
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public TimeOnly Time { get; set; }
        public enum TypeOfPhotoSession
        {
            Family,
            Birthday,
            Wedding,
            Personal
        }

        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int PhotographerId { get; set; }
        public Photographer? Photographer { get; set; }
    }
}