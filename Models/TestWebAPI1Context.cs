using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class T5PWebAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // Map one-to-zero or one relationship 
            modelBuilder.Entity<empself>()
                .HasRequired(t => t.EmpSickCur)
                .WithRequiredPrincipal();
                
            modelBuilder.Entity<empself>()
                .HasRequired(t => t.Empanlv)
                .WithRequiredPrincipal();


            modelBuilder.Entity<emphr>()
                .HasRequired(t => t.EmpSick)
                .WithRequiredPrincipal();

            modelBuilder.Entity<emphr>()
                .HasRequired(t => t.Empanlv)
                .WithRequiredPrincipal();


        } 
        public T5PWebAPIContext() : base("name=T5PWebAPIContext")
        {
            Database.SetInitializer<T5PWebAPIContext>(new CreateDatabaseIfNotExists<T5PWebAPIContext>());
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<T5PWebAPI.Models.Author> Authors { get; set; }

        public System.Data.Entity.DbSet<T5PWebAPI.Models.Book> Books { get; set; }

        public System.Data.Entity.DbSet<T5PWebAPI.Models.payrollgroups> payrollgroups { get; set; }

        public System.Data.Entity.DbSet<T5PWebAPI.Models.emphr> emphrs { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.empself> empself { get; set; }

        public System.Data.Entity.DbSet<T5PWebAPI.Models.parameter> parameters { get; set; }

        public System.Data.Entity.DbSet<T5PWebAPI.Models.organization> organizations { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.position> positions { get; set; }

        public System.Data.Entity.DbSet<T5PWebAPI.Models.empposition> empposition { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.empleavedetail_ongoing> empleavedetail_ongoing { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.empleavedata> empleavedata { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.empleavedata_cancel> empleavedata_cancel { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.empanlv> empanlv { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.emp_sick_cur> emp_sick { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.workflowinstance> workflowinstance { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.workflowinstancedetail> workflowinstancedetail { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.forminstance> forminstance { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.forminstancedetail> forminstancedetail { get; set; }
        public System.Data.Entity.DbSet<T5PWebAPI.Models.leavetype> leavetype { get; set; }
    }
}
