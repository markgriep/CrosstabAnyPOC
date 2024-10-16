using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using CrosstabAnyPOC.DataAccess.;


namespace CrosstabAnyPOC.DataAccess.Data
{

    public partial class RandomDrugTestContext : DbContext
    {

        public RandomDrugTestContext(DbContextOptions<RandomDrugTestContext> options)
            : base(options)
        {
        }


        #region DBSets

        
        //public virtual DbSet<Models.RandomDrugTest> RandomDrugTests { get; set; }

        //public virtual DbSet<BasicValidation> BasicValidations { get; set; }

        //public virtual DbSet<EligibleForTesting> EligibleForTestings { get; set; }

        //public virtual DbSet<Employee> Employees { get; set; }

        ////public virtual DbSet<JobDepartmentsPoolCriteria> JobDepartmentsPoolCriteria { get; set; }

        //public virtual DbSet<NotEligibleForTesting> NotEligibleForTestings { get; set; }

        //public virtual DbSet<SelectionPercentage> SelectionPercentages { get; set; }

        //public virtual DbSet<SelectionPool> SelectionPools { get; set; }

        //public virtual DbSet<SpecialAssignment> SpecialAssignment { get; set; }


        //public virtual DbSet<TestCategory> TestCategories { get; set; }

        //public virtual DbSet<QualifiedJobCode> QualifiedJobCodes { get; set; }

        //public virtual DbSet<JobCodeToDepartmentMapping> JobCodeToDepartmentMappings { get; set; }


        //public virtual DbSet<TestType> TestTypes { get; set; }



        public virtual DbSet<WorkdayEmployee> WorkdayEmployees { get; set; }

        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<QualifiedJobCode>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Qualified__3214EC27A3A3D3A3");

            //    entity.ToTable("QualifiedJobCode");

            //    entity.Property(e => e.Id).HasColumnName("ID");
            //    entity.Property(e => e.DateActivated).HasColumnType("datetime");
            //    entity.Property(e => e.DateAddedToList).HasColumnType("datetime");
            //    entity.Property(e => e.IsRdt).HasColumnName("IsRDT");
            //    entity.Property(e => e.JobCode).HasMaxLength(50);
            //});


            //modelBuilder.Entity<BasicValidation>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Basic_Va__3214EC277A052790");

            //    entity.ToTable("Basic_Validation");

            //    entity.Property(e => e.Id).HasColumnName("ID");
            //    entity.Property(e => e.Name).HasMaxLength(100);
            //});


            //modelBuilder.Entity<EligibleForTesting>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Eligible__3214EC27E9E781E9");

            //    entity.ToTable("EligibleForTesting");

            //    entity.Property(e => e.Id).HasColumnName("ID");
            //    entity.Property(e => e.OtherAlcohol)
            //        .HasMaxLength(50)
            //        .HasColumnName("Other_Alcohol");
            //    entity.Property(e => e.TestNumber).HasMaxLength(50);
            //});


            //modelBuilder.Entity<Employee>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC27B332E70B");

            //    entity.ToTable("Employee");

            //    entity.Property(e => e.Id).HasColumnName("ID");
            //    entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            //    entity.Property(e => e.FirstName).HasMaxLength(100);
            //    entity.Property(e => e.FullName).HasMaxLength(100);
            //    entity.Property(e => e.JobTitle).HasMaxLength(50);
            //    entity.Property(e => e.LastName).HasMaxLength(100);
            //    entity.Property(e => e.MiddleName).HasMaxLength(100);
            //});


            //modelBuilder.Entity<JobDepartmentsPoolCriteria>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__JobDepar__3214EC274E32F568");

            //    entity.Property(e => e.Id).HasColumnName("ID");
            //    entity.Property(e => e.CostCenterId).HasColumnName("CostCenterID");
                
            //    entity.Property(e => e.JobCodeId)
            //        .HasMaxLength(50)
            //        .HasColumnName("JobCodeID");
            //    entity.Property(e => e.Setid)
            //        .HasMaxLength(50)
            //        .HasColumnName("SETID");
            //    entity.Property(e => e.TestingGroup).HasMaxLength(50);
            //    entity.Property(e => e.UpdateDateString).HasMaxLength(50);
            //});


    //        modelBuilder.Entity<JobCodeToDepartmentMapping>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK_JobCodeToDepartmentMapping");
    //            entity.ToTable("JobCodeToDepartmentMapping");

    //            entity.Property(e => e.Id)              .HasColumnName("ID").IsRequired();
				//entity.Property(e => e.CostCenterId)    .HasColumnName("CostCenterId").IsRequired().HasMaxLength(10);
    //            entity.Property(e => e.JobCodeId).IsRequired().HasMaxLength(10);

				//entity.Property(e => e.TestingGroup).IsRequired().HasMaxLength(10);
    //            entity.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired();
    //            entity.Property(e => e.EffectiveDate).IsRequired();
    //        });





    //        modelBuilder.Entity<NotEligibleForTesting>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__NotEligi__3214EC277D8F469D");

    //            entity.ToTable("NotEligibleForTesting");

    //            entity.Property(e => e.Id).HasColumnName("ID");
    //        });


    //        modelBuilder.Entity<SelectionPercentage>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__Selectio__3214EC27E467B26B");

    //            entity.ToTable("SelectionPercentage");

    //            entity.Property(e => e.Id).HasColumnName("ID");
    //            entity.Property(e => e.EffectiveStatus).HasMaxLength(50);
    //            entity.Property(e => e.TestGroup).HasMaxLength(50);
    //            entity.Property(e => e.TestType).HasMaxLength(50);
    //            entity.Property(e => e.UpdateDateString).HasMaxLength(50);
    //        });


    //        modelBuilder.Entity<SelectionPool>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__Selectio__3214EC27D9B2A5B2");

    //            entity.ToTable("SelectionPool");

    //            entity.Property(e => e.Id).HasColumnName("ID");
    //            entity.Property(e => e.Comments).HasMaxLength(255);
    //            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
    //            entity.Property(e => e.ExamResults).HasMaxLength(50);
    //            entity.Property(e => e.ExamTimeInString).HasMaxLength(50);
    //            entity.Property(e => e.ExamTimeOutString).HasMaxLength(50);
    //            entity.Property(e => e.TestNumber).HasMaxLength(50);
    //            entity.Property(e => e.TestType).HasMaxLength(50);
    //        });


    //        modelBuilder.Entity<SpecialAssignment>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__SpecialA__3214EC27CE6AF90B");

    //            entity.ToTable("SpecialAssignment");

    //            entity.Property(e => e.Id).HasColumnName("ID");
    //            entity.Property(e => e.EmployeeId)
    //                .HasMaxLength(50)
    //                .HasColumnName("EmployeeID");
    //            entity.Property(e => e.SpecialAssignmentGroup).HasMaxLength(50);
    //        });


    //        modelBuilder.Entity<Models.RandomDrugTest>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__Tests__3214EC272C26A830");

    //            entity.Property(e => e.Id).HasColumnName("ID");
    //            entity.Property(e => e.AlcoholTest).HasMaxLength(50);
    //            entity.Property(e => e.CreatorId).HasMaxLength(50);
    //            entity.Property(e => e.CreatorName).HasMaxLength(50);
    //            entity.Property(e => e.DateCreatedString).HasMaxLength(50);
    //            entity.Property(e => e.DrugTest).HasMaxLength(50);
    //            entity.Property(e => e.EmployeeId)
    //                .HasMaxLength(50)
    //                .HasColumnName("EmployeeID");
    //            entity.Property(e => e.TestCategory).HasMaxLength(50);
    //            entity.Property(e => e.TestGroup).HasMaxLength(50);
    //            entity.Property(e => e.TestNumber).HasMaxLength(50);
    //            entity.Property(e => e.TestType).HasMaxLength(50);
    //        });


    //        modelBuilder.Entity<TestCategory>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__TestCate__3214EC2791532157");

    //            entity.ToTable("TestCategory");

    //            entity.Property(e => e.Id).HasColumnName("ID");
    //            entity.Property(e => e.CategoryCode).HasMaxLength(50);
    //            entity.Property(e => e.Description).HasMaxLength(50);
    //        });


    //        modelBuilder.Entity<TestType>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__TestType__3214EC27ED890941");

    //            entity.ToTable("TestType");

    //            entity.Property(e => e.Id).HasColumnName("ID");
    //            entity.Property(e => e.Description).HasMaxLength(100);
    //            entity.Property(e => e.TestTypeCode).HasMaxLength(2);
    //        });



            modelBuilder.Entity<WorkdayEmployee>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__WorkdayEmployee__4434ECEDD58665B1");

                entity.ToTable("WorkdayEmployee");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.EmployeeName).HasMaxLength(100);
                entity.Property(e => e.EmployeeId).HasMaxLength(100);
                entity.Property(e => e.Department).HasMaxLength(100);
                entity.Property(e => e.JobCode).HasMaxLength(100);
                entity.Property(e => e.JobTitle).HasMaxLength(100);
                entity.Property(e => e.DateOfBirth).HasMaxLength(100);

               
            });




            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
