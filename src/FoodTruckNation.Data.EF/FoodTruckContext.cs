using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;

namespace FoodTruckNation.Data.EF
{
    public class FoodTruckContext : DbContext
    {

        public FoodTruckContext(DbContextOptions options) : base(options)
        {

        }




        public DbSet<FoodTruck> FoodTrucks { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Locality> Localities { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<SocialMediaPlatform> SocialMediaPlatforms { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureFoodTruck(modelBuilder);
            ConfigureTag(modelBuilder);
            ConfigureFoodTruckTag(modelBuilder);
            ConfigureReview(modelBuilder);
            ConfigureLocality(modelBuilder);
            ConfigureLocation(modelBuilder);
            ConfigureSchedule(modelBuilder);
            ConfigureSocialMediaPlatform(modelBuilder);
            ConfigureSocialMediaAccount(modelBuilder);
        }






        private void ConfigureTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                .ToTable("Tags")
                .HasKey(t => t.TagId);

            modelBuilder.Entity<Tag>()
                .Ignore(t => t.ObjectState);

            modelBuilder.Entity<Tag>().Property(p => p.TagId)
                .HasField("_tagId")
                .UseIdentityColumn()
                .HasColumnName("TagId");

            modelBuilder.Entity<Tag>().Property(p => p.Text)
                .HasField("_tagText")
                .HasColumnName("TagName");
        }


        private void ConfigureFoodTruckTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodTruckTag>()
                .ToTable("FoodTruckTags")
                .HasKey(f => f.FoodTruckTagId);

            modelBuilder.Entity<FoodTruckTag>()
                .Ignore(t => t.ObjectState);

            modelBuilder.Entity<FoodTruckTag>().Property(p => p.FoodTruckTagId)
               .HasField("_foodTruckTagId")
               .UseIdentityColumn()
               .HasColumnName("FoodTruckTagId");

            modelBuilder.Entity<FoodTruckTag>()
                .HasOne<FoodTruck>(x => x.FoodTruck)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.FoodTruckId);


            modelBuilder.Entity<FoodTruckTag>()
                .HasOne<Tag>(x => x.Tag)
                .WithMany()
                .HasForeignKey(x => x.TagId);
        }


        private void ConfigureLocality(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locality>()
                .ToTable("Localities")
                .HasKey(l => l.LocalityCode);

            modelBuilder.Entity<Locality>()
                .Ignore(l => l.ObjectState);

            modelBuilder.Entity<Locality>().Property(p => p.LocalityCode)
                .HasField("_localityCode")
                .HasColumnName("LocalityCode");

            modelBuilder.Entity<Locality>().Property(p => p.Name)
                .HasField("_localityName")
                .HasColumnName("LocalityName");
        }


        private void ConfigureLocation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .ToTable("Locations")
                .HasKey(l => l.LocationId);

            modelBuilder.Entity<Location>()
                .Ignore(l => l.ObjectState);

            modelBuilder.Entity<Location>().Property(p => p.LocationId)
                .HasField("_locationId")
                .UseIdentityColumn()
                .HasColumnName("LocationId");

            modelBuilder.Entity<Location>().Property(p => p.Name)
                .HasField("_locationName")
                .HasColumnName("LocationName");

            modelBuilder.Entity<Location>().Property(p => p.StreetAddress)
                .HasField("_streetAddress")
                .HasColumnName("StreetAddress");

            modelBuilder.Entity<Location>().Property(p => p.City)
                .HasField("_city")
                .HasColumnName("City");

            modelBuilder.Entity<Location>().Property(p => p.State)
                .HasField("_state")
                .HasColumnName("State");

            modelBuilder.Entity<Location>().Property(p => p.ZipCode)
                .HasField("_zipCode")
                .HasColumnName("ZipCode");

            modelBuilder.Entity<Location>().Property(p => p.Latitude)
                .HasField("_latitude")
                .HasColumnName("Latitude");

            modelBuilder.Entity<Location>().Property(p => p.Longitude)
                .HasField("_longitude")
                .HasColumnName("Longitude");

            // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#one-to-many-without-navigation-to-dependents
            modelBuilder.Entity<Location>()
                .HasOne(e => e.Locality)
                .WithMany()
                .HasForeignKey(e => e.LocalityCode)
                .IsRequired();
        }


        private void ConfigureFoodTruck(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodTruck>()
                .ToTable("FoodTrucks")
                .HasKey(f => f.FoodTruckId);

            modelBuilder.Entity<FoodTruck>()
                .Ignore(f => f.ObjectState);

            modelBuilder.Entity<FoodTruck>().Property(p => p.FoodTruckId)
                .HasField("_foodTruckId")
                .UseIdentityColumn()
                .HasColumnName("FoodTruckId");

            modelBuilder.Entity<FoodTruck>().Property(p => p.Name)
                .HasField("_name")
                .HasColumnName("TruckName");

            modelBuilder.Entity<FoodTruck>().Property(p => p.Description)
                .HasField("_description")
                .HasColumnName("Description");

            modelBuilder.Entity<FoodTruck>().Property(p => p.Website)
                .HasField("_website")
                .HasColumnName("Website");

            modelBuilder.Entity<FoodTruck>().Property(p => p.LastModifiedDate)
                .HasField("_lastModifiedDate")
                .HasColumnName("RowValidFrom")
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();

            // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#one-to-many-without-navigation-to-dependents
            modelBuilder.Entity<FoodTruck>()
                .HasOne(e => e.Locality)
                .WithMany()
                .HasForeignKey(e => e.LocalityCode)
                .IsRequired();

            // So EF can set the backing field on the navigation property
            // https://blog.oneunicorn.com/2016/10/28/collection-navigation-properties-and-fields-in-ef-core-1-1/
            var navigationTags = modelBuilder.Entity<FoodTruck>().Metadata
                .FindNavigation(nameof(FoodTruck.Tags));
            navigationTags.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigationSocialAccounts = modelBuilder.Entity<FoodTruck>().Metadata
                .FindNavigation(nameof(FoodTruck.SocialMediaAccounts));
            navigationSocialAccounts.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigationReviews = modelBuilder.Entity<FoodTruck>().Metadata
                .FindNavigation(nameof(FoodTruck.Reviews));
            navigationReviews.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigationSchedules = modelBuilder.Entity<FoodTruck>().Metadata
                .FindNavigation(nameof(FoodTruck.Schedules));
            navigationSchedules.SetPropertyAccessMode(PropertyAccessMode.Field);
        }




        private void ConfigureReview(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .ToTable("Reviews")
                .HasKey(r => r.ReviewId);

            modelBuilder.Entity<Review>()
                .Ignore(r => r.ObjectState);

            modelBuilder.Entity<Review>().Property(x => x.ReviewId)
                .HasField("_reviewId")
                .UseIdentityColumn()
                .HasColumnName("ReviewId");

            modelBuilder.Entity<Review>().Property(x => x.FoodTruckId)
                .HasField("_foodTruckId")
                .HasColumnName("FoodTruckId");

            modelBuilder.Entity<Review>().Property(x => x.ReviewDate)
                .HasField("_reviewDate")
                .HasColumnName("ReviewDate");

            modelBuilder.Entity<Review>().Property(x => x.Rating)
                .HasField("_rating")
                .HasColumnName("Rating");


            modelBuilder.Entity<Review>().Property(x => x.Details)
                .HasField("_details")
                .HasColumnName("Details");

            modelBuilder.Entity<Review>()
                .HasOne<FoodTruck>(x => x.FoodTruck)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.FoodTruckId);
        }


        private void ConfigureSchedule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .ToTable("Schedules")
                .HasKey(s => s.ScheduleId);

            modelBuilder.Entity<Schedule>()
                .Ignore(s => s.ObjectState);

            modelBuilder.Entity<Schedule>().Property(x => x.ScheduleId)
                .HasField("_scheduleId")
                .UseIdentityColumn()
                .HasColumnName("ScheduleId");

            modelBuilder.Entity<Schedule>().Property(x => x.FoodTruckId)
                .HasField("_foodTruckId")
                .HasColumnName("FoodTruckId");

            modelBuilder.Entity<Schedule>().Property(x => x.LocationId)
                .HasField("_locationId")
                .HasColumnName("LocationId");

            modelBuilder.Entity<Schedule>().Property(x => x.ScheduledStart)
                .HasField("_scheduleStart")
                .HasColumnName("StartTime");

            modelBuilder.Entity<Schedule>().Property(x => x.ScheduledEnd)
                .HasField("_scheduleEnd")
                .HasColumnName("EndTime");



            modelBuilder.Entity<Schedule>()
                .HasOne<FoodTruck>(x => x.FoodTruck)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => x.FoodTruckId);

            modelBuilder.Entity<Schedule>()
                .HasOne<Location>(x => x.Location);
        }



        private void ConfigureSocialMediaPlatform(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialMediaPlatform>()
                .ToTable("SocialMediaPlatforms")
                .HasKey(p => p.PlatformId);

            modelBuilder.Entity<SocialMediaPlatform>()
               .Ignore(p => p.ObjectState);

            modelBuilder.Entity<SocialMediaPlatform>().Property(p => p.PlatformId)
                .HasField("_platformId")
                .UseIdentityColumn()
                .HasColumnName("PlatformId");

            modelBuilder.Entity<SocialMediaPlatform>().Property(p => p.Name)
                .HasField("_name")
                .HasColumnName("PlatformName");

            modelBuilder.Entity<SocialMediaPlatform>().Property(p => p.UrlTemplate)
                .HasField("_urlTemplate")
                .HasColumnName("UrlTemplate");
        }



        private void ConfigureSocialMediaAccount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialMediaAccount>()
                .ToTable("SocialMediaAccounts")
                .HasKey(a => a.SocialMediaAccountId);

            modelBuilder.Entity<SocialMediaAccount>()
                .Ignore(a => a.ObjectState);

            modelBuilder.Entity<SocialMediaAccount>().Property(p => p.SocialMediaAccountId)
               .HasField("_socialMediaAccountId")
               .UseIdentityColumn()
               .HasColumnName("SocialMediaAccountId");

            modelBuilder.Entity<SocialMediaAccount>().Property(p => p.AccountName)
                .HasField("_accountName")
                .HasColumnName("AccountName");

            modelBuilder.Entity<SocialMediaAccount>()
                .HasOne<FoodTruck>(x => x.FoodTruck)
                .WithMany(x => x.SocialMediaAccounts)
                .HasForeignKey(x => x.FoodTruckId);

            modelBuilder.Entity<SocialMediaAccount>()
                .HasOne<SocialMediaPlatform>(x => x.Platform)
                .WithMany()
                .HasForeignKey(x => x.PlatformId);
        }


    }
}
