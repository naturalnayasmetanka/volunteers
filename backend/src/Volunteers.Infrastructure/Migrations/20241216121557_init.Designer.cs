﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volunteers.Infrastructure.Contexts;

#nullable disable

namespace Volunteers.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241216121557_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Pet.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("CommonDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("common_description");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<int>("HelpStatus")
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<string>("HelthDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("helth_description");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("nickname");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("integer")
                        .HasColumnName("phone_number");

                    b.Property<int>("Position")
                        .HasColumnType("integer")
                        .HasColumnName("position");

                    b.Property<string>("SpeciesBreed")
                        .HasColumnType("text")
                        .HasColumnName("species_breed");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<double>("ExperienceInYears")
                        .HasMaxLength(4)
                        .HasColumnType("double precision")
                        .HasColumnName("experience_in_years");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("integer")
                        .HasColumnName("phone_number");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("Volunteers.Domain.SpeciesManagment.Breed.Entities.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.Property<Guid>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("Volunteers.Domain.SpeciesManagment.Species.AggregateRoot.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Pet.Entities.Pet", b =>
                {
                    b.HasOne("Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer", "Volunteer")
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("Volunteers.Domain.PetManagment.Pet.ValueObjects.LocationDetails", "LocationDetails", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("LocationDetails");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("Volunteers.Domain.PetManagment.Pet.ValueObjects.Location", "Locations", b2 =>
                                {
                                    b2.Property<Guid>("LocationDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("City")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)");

                                    b2.Property<string>("Country")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)");

                                    b2.Property<int>("Floor")
                                        .HasMaxLength(3)
                                        .HasColumnType("integer");

                                    b2.Property<string>("HouseNumber")
                                        .IsRequired()
                                        .HasMaxLength(4)
                                        .HasColumnType("character varying(4)");

                                    b2.Property<string>("RoomNumber")
                                        .IsRequired()
                                        .HasMaxLength(5)
                                        .HasColumnType("character varying(5)");

                                    b2.Property<string>("Street")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)");

                                    b2.HasKey("LocationDetailsPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("LocationDetailsPetId")
                                        .HasConstraintName("fk_pets_pets_location_details_pet_id");
                                });

                            b1.Navigation("Locations");
                        });

                    b.OwnsOne("Volunteers.Domain.PetManagment.Pet.ValueObjects.PhotoDetails", "PhotoDetails", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("PhotoDetails");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("Volunteers.Domain.PetManagment.Pet.ValueObjects.PetPhoto", "PetPhoto", b2 =>
                                {
                                    b2.Property<Guid>("PhotoDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<bool>("IsMain")
                                        .HasColumnType("boolean");

                                    b2.Property<string>("Path")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)");

                                    b2.HasKey("PhotoDetailsPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("PhotoDetailsPetId")
                                        .HasConstraintName("fk_pets_pets_photo_details_pet_id");
                                });

                            b1.Navigation("PetPhoto");
                        });

                    b.OwnsOne("Volunteers.Domain.PetManagment.Pet.ValueObjects.PhysicalParametersDetails", "PhysicalParametersDetails", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("PhysicalParametersDetails");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("Volunteers.Domain.PetManagment.Pet.ValueObjects.PhysicalParameters", "PhysicalParameters", b2 =>
                                {
                                    b2.Property<Guid>("PhysicalParametersDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Breed")
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Color")
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Gender")
                                        .HasMaxLength(20)
                                        .HasColumnType("character varying(20)");

                                    b2.Property<double>("Height")
                                        .HasMaxLength(5)
                                        .HasColumnType("double precision");

                                    b2.Property<bool>("IsSterilized")
                                        .HasColumnType("boolean");

                                    b2.Property<bool>("IsVaccinated")
                                        .HasColumnType("boolean");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)");

                                    b2.Property<double>("Weight")
                                        .HasMaxLength(5)
                                        .HasColumnType("double precision");

                                    b2.HasKey("PhysicalParametersDetailsPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("PhysicalParametersDetailsPetId")
                                        .HasConstraintName("fk_pets_pets_physical_parameters_details_pet_id");
                                });

                            b1.Navigation("PhysicalParameters");
                        });

                    b.OwnsOne("Volunteers.Domain.PetManagment.Pet.ValueObjects.RequisitesDetails", "RequisitesDetails", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("RequisitesDetails");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("Volunteers.Domain.PetManagment.Pet.ValueObjects.PetRequisite", "PetRequisites", b2 =>
                                {
                                    b2.Property<Guid>("RequisitesDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)");

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("RequisitesDetailsPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisitesDetailsPetId")
                                        .HasConstraintName("fk_pets_pets_requisites_details_pet_id");
                                });

                            b1.Navigation("PetRequisites");
                        });

                    b.Navigation("LocationDetails");

                    b.Navigation("PhotoDetails");

                    b.Navigation("PhysicalParametersDetails");

                    b.Navigation("RequisitesDetails");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer", b =>
                {
                    b.OwnsOne("Volunteers.Domain.PetManagment.Volunteer.ValueObjects.RequisiteDetails", "RequisiteDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("RequisiteDetails");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("Volunteers.Domain.PetManagment.Volunteer.ValueObjects.VolunteerRequisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("RequisiteDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)");

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("RequisiteDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisiteDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_requisite_details_volunteer_id");
                                });

                            b1.Navigation("Requisites");
                        });

                    b.OwnsOne("Volunteers.Domain.PetManagment.Volunteer.ValueObjects.SocialNetworkDetails", "SocialNetworkDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("SocialNetworkDetails");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("Volunteers.Domain.PetManagment.Volunteer.ValueObjects.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)");

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("SocialNetworkDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_network_details_volunteer_id");
                                });

                            b1.Navigation("SocialNetworks");
                        });

                    b.Navigation("RequisiteDetails");

                    b.Navigation("SocialNetworkDetails");
                });

            modelBuilder.Entity("Volunteers.Domain.SpeciesManagment.Breed.Entities.Breed", b =>
                {
                    b.HasOne("Volunteers.Domain.SpeciesManagment.Species.AggregateRoot.Species", "Species")
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_breeds_species_species_id");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Volunteers.Domain.SpeciesManagment.Species.AggregateRoot.Species", b =>
                {
                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}
