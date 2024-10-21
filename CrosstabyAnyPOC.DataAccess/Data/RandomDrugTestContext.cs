//using System;
//using System.Collections.Generic;
//using System.Net.NetworkInformation;
//using Microsoft.EntityFrameworkCore;
//using CrosstabAnyPOC.DataAccess;
//using CrosstabAnyPOC.DataAccess.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;



//namespace CrosstabAnyPOC.DataAccess.Data
//{

//    public partial class RandomDrugTestContext : DbContext
//    {

//        public RandomDrugTestContext(DbContextOptions<RandomDrugTestContext> options)
//            : base(options)
//        {
//        }


//        #region DBSets

//        public virtual DbSet<WorkdayEmployee> WorkdayEmployees { get; set; }

//        #endregion



//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<WorkdayEmployee>(entity =>
//            {
//                entity.HasKey(e => e.Id).HasName("PK__WorkdayEmployee__4434ECEDD58665B1");

//                entity.ToTable("WorkdayEmployee");  // Call directly without assigning it to a variable

//                entity.Property(e => e.Id).HasColumnName("ID");
//                entity.Property(e => e.EmployeeName).HasMaxLength(100);
//                entity.Property(e => e.EmployeeId).HasMaxLength(100);
//                entity.Property(e => e.Department).HasMaxLength(100);
//                entity.Property(e => e.JobCode).HasMaxLength(100);
//                entity.Property(e => e.JobTitle).HasMaxLength(100);
//                entity.Property(e => e.DateOfBirth).HasMaxLength(100);
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }


//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
