using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using cat.Model;

namespace cat.DB {
    class CatMasterContext: DbContext {
        public DbSet<Cat> Cats { get; set; }

        public CatMasterContext(): base("CatDatabase") {}
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Add(new DefaultValueAttributeConvention());

            base.OnModelCreating(modelBuilder);
        }
    }
}
