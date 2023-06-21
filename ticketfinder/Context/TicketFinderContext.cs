﻿using Microsoft.EntityFrameworkCore;
using ticketfinder.Models.ORM;

namespace ticketfinder.Context
{
    public class TicketFinderContext : DbContext
    {

        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDetail> EventDetails { get; set; }
        public DbSet<EventSeat> EventSeats { get; set; }

        public DbSet<Place> Places { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Stage> Stages { get; set; }
      
        public DbSet<Ticket> Tickets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.;Database=ticketfinder1;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);

        }


    }


}
