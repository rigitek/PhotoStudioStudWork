using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Models
{
    public class PhotoSession
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public TimeOnly Time { get; set; }
        public bool Complete{get; set;}

        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int PhotographerId { get; set; }
        public Photographer? Photographer { get; set; }
        public int TypeOfPhotoSessionId { get; set;}
        public TypeOfPhotoSession? TypeOfPhotoSession { get; set; }
    }
}
