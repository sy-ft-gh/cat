namespace cat.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cats",
                c => new
                    {
                        CatId = c.Int(nullable: false, identity: true),
                        CatName = c.String(maxLength: 50),
                        HairPattern = c.String(maxLength: 50),
                        Gender = c.Byte(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "DefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "0")
                                },
                            }),
                        BodyType = c.Byte(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "DefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        FaceType = c.String(maxLength: 100),
                        Age = c.Byte(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "DefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "0")
                                },
                            }),
                        Personality = c.String(maxLength: 200),
                        LostFlag = c.String(maxLength: 1),
                        RegistDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "DefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "GETDATE()")
                                },
                            }),
                        UpdateDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "DefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "GETDATE()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.CatId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cats",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "Age",
                        new Dictionary<string, object>
                        {
                            { "DefaultValue", "0" },
                        }
                    },
                    {
                        "BodyType",
                        new Dictionary<string, object>
                        {
                            { "DefaultValue", "1" },
                        }
                    },
                    {
                        "Gender",
                        new Dictionary<string, object>
                        {
                            { "DefaultValue", "0" },
                        }
                    },
                    {
                        "RegistDate",
                        new Dictionary<string, object>
                        {
                            { "DefaultValue", "GETDATE()" },
                        }
                    },
                    {
                        "UpdateDate",
                        new Dictionary<string, object>
                        {
                            { "DefaultValue", "GETDATE()" },
                        }
                    },
                });
        }
    }
}
