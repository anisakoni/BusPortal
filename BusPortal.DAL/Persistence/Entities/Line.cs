﻿namespace BusPortal.DAL.Persistence.Entities
{
    public class Line : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string StartCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTimes { get; set; }
        public decimal Price { get; set; }


    }
}
