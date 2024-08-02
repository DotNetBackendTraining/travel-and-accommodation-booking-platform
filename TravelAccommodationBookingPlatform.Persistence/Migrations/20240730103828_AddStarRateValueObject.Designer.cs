﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAccommodationBookingPlatform.Persistence;

#nullable disable

namespace TravelAccommodationBookingPlatform.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240730103828_AddStarRateValueObject")]
    partial class AddStarRateValueObject
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookingRoom", b =>
                {
                    b.Property<Guid>("BookingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookingsId", "RoomsId");

                    b.HasIndex("RoomsId");

                    b.ToTable("BookingRoom");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ComplexProperty<Dictionary<string, object>>("Checking", "TravelAccommodationBookingPlatform.Domain.Entities.Booking.Checking#Checking", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("CheckInDate")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("CheckOutDate")
                                .HasColumnType("datetime2");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("NumberOfGuests", "TravelAccommodationBookingPlatform.Domain.Entities.Booking.NumberOfGuests#NumberOfGuests", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Adults")
                                .HasColumnType("int")
                                .HasAnnotation("Max", 99)
                                .HasAnnotation("Min", 1);

                            b1.Property<int>("Children")
                                .HasColumnType("int")
                                .HasAnnotation("Max", 99)
                                .HasAnnotation("Min", 0);
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpecialRequest", "TravelAccommodationBookingPlatform.Domain.Entities.Booking.SpecialRequest#SpecialRequest", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Request")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("PaymentId")
                        .IsUnique()
                        .HasFilter("[PaymentId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.ComplexProperty<Dictionary<string, object>>("Country", "TravelAccommodationBookingPlatform.Domain.Entities.City.Country#Country", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PostOffice", "TravelAccommodationBookingPlatform.Domain.Entities.City.PostOffice#PostOffice", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("nvarchar(1000)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("HotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Hotel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Amenities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.ComplexProperty<Dictionary<string, object>>("Coordinates", "TravelAccommodationBookingPlatform.Domain.Entities.Hotel.Coordinates#Coordinates", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<double>("Latitude")
                                .HasPrecision(18, 15)
                                .HasColumnType("float(18)")
                                .HasAnnotation("Max", 90.0)
                                .HasAnnotation("Min", -90.0);

                            b1.Property<double>("Longitude")
                                .HasPrecision(18, 15)
                                .HasColumnType("float(18)")
                                .HasAnnotation("Max", 180.0)
                                .HasAnnotation("Min", -180.0);
                        });

                    b.ComplexProperty<Dictionary<string, object>>("StarRate", "TravelAccommodationBookingPlatform.Domain.Entities.Hotel.StarRate#StarRate", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Rate")
                                .HasColumnType("int")
                                .HasAnnotation("Max", 5)
                                .HasAnnotation("Min", 1);
                        });

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int")
                        .HasAnnotation("Max", 9999)
                        .HasAnnotation("Min", 1);

                    b.Property<int>("RoomType")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.ComplexProperty<Dictionary<string, object>>("MaxNumberOfGuests", "TravelAccommodationBookingPlatform.Domain.Entities.Room.MaxNumberOfGuests#NumberOfGuests", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Adults")
                                .HasColumnType("int")
                                .HasAnnotation("Max", 99)
                                .HasAnnotation("Min", 1);

                            b1.Property<int>("Children")
                                .HasColumnType("int")
                                .HasAnnotation("Max", 99)
                                .HasAnnotation("Min", 0);
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Price", "TravelAccommodationBookingPlatform.Domain.Entities.Room.Price#Price", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<double>("Value")
                                .HasColumnType("float")
                                .HasAnnotation("Min", 0.0);
                        });

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookingRoom", b =>
                {
                    b.HasOne("TravelAccommodationBookingPlatform.Domain.Entities.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAccommodationBookingPlatform.Domain.Entities.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Booking", b =>
                {
                    b.HasOne("TravelAccommodationBookingPlatform.Domain.Entities.Payment", "Payment")
                        .WithOne()
                        .HasForeignKey("TravelAccommodationBookingPlatform.Domain.Entities.Booking", "PaymentId");

                    b.HasOne("TravelAccommodationBookingPlatform.Domain.Entities.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.City", b =>
                {
                    b.OwnsOne("TravelAccommodationBookingPlatform.Domain.ValueObjects.Image", "ThumbnailImage", b1 =>
                        {
                            b1.Property<Guid>("CityId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(2048)
                                .HasColumnType("nvarchar(2048)");

                            b1.HasKey("CityId");

                            b1.ToTable("Cities");

                            b1.WithOwner()
                                .HasForeignKey("CityId");
                        });

                    b.Navigation("ThumbnailImage")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Discount", b =>
                {
                    b.HasOne("TravelAccommodationBookingPlatform.Domain.Entities.Hotel", null)
                        .WithMany("Discounts")
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Hotel", b =>
                {
                    b.HasOne("TravelAccommodationBookingPlatform.Domain.Entities.City", "City")
                        .WithMany("Hotels")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("TravelAccommodationBookingPlatform.Domain.ValueObjects.Image", "Images", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("HotelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(2048)
                                .HasColumnType("nvarchar(2048)");

                            b1.HasKey("Id");

                            b1.HasIndex("HotelId");

                            b1.ToTable("Hotels_Images");

                            b1.WithOwner()
                                .HasForeignKey("HotelId");
                        });

                    b.OwnsOne("TravelAccommodationBookingPlatform.Domain.ValueObjects.Image", "ThumbnailImage", b1 =>
                        {
                            b1.Property<Guid>("HotelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(2048)
                                .HasColumnType("nvarchar(2048)");

                            b1.HasKey("HotelId");

                            b1.ToTable("Hotels");

                            b1.WithOwner()
                                .HasForeignKey("HotelId");
                        });

                    b.OwnsMany("TravelAccommodationBookingPlatform.Domain.ValueObjects.Review", "Reviews", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("HotelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)");

                            b1.HasKey("Id");

                            b1.HasIndex("HotelId");

                            b1.ToTable("Review");

                            b1.WithOwner()
                                .HasForeignKey("HotelId");
                        });

                    b.Navigation("City");

                    b.Navigation("Images");

                    b.Navigation("Reviews");

                    b.Navigation("ThumbnailImage")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Room", b =>
                {
                    b.HasOne("TravelAccommodationBookingPlatform.Domain.Entities.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("TravelAccommodationBookingPlatform.Domain.ValueObjects.Image", "Images", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("HotelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(2048)
                                .HasColumnType("nvarchar(2048)");

                            b1.HasKey("Id");

                            b1.HasIndex("HotelId");

                            b1.ToTable("Rooms_Images");

                            b1.WithOwner()
                                .HasForeignKey("HotelId");
                        });

                    b.Navigation("Hotel");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.City", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.Hotel", b =>
                {
                    b.Navigation("Discounts");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("TravelAccommodationBookingPlatform.Domain.Entities.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}