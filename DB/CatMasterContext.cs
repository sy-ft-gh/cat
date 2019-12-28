using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using cat.Model;

namespace cat.DB {
    class CatMasterContext: DbContext {
        public DbSet<Cat> Cats { get; set; }
        public DbSet<CatObservation> CatObservations { get; set; }

        public CatMasterContext(): base("CatDatabase") {}
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Add(new DefaultValueAttributeConvention());

            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// Get DataBase Current DateTime
        /// </summary>
        /// <returns>Current DateTime</returns>
        public DateTime GetDBDate() {
            return this.Database.SqlQuery<DateTime>("SELECT GetDate()").Single();
        }

    }
}
