using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitialSeed();
            CaseCorrection();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }


        static void InitialSeed()
        {
            var assets = new Bogus.Faker<Asset>()
                .RuleFor(d => d.Id, d => Guid.NewGuid())
                .RuleFor(d => d.Name, d => d.Person.FullName)
                .RuleFor(d => d.Files, d => new Bogus.Faker<AssetFile>()
                                          .RuleFor(f => f.Name, f => f.Name.JobArea())
                                          .Generate(5))
                .Generate(10);


            using Db db = new Db();

            db.Assets.AddRange(assets);

            db.SaveChanges();
        }

        static void CaseReproduction()
        {
            Guid key = Guid.Empty;

            // we are using tempDb here only to acquire a randomized id for our asset used in this test case
            using (Db tempDb = new Db())
            {
                var assetToChange = tempDb.Assets.OrderBy(d => Guid.NewGuid()).First();
                key = assetToChange.Id;
            }

            Asset asset = new Asset()
            {
                Id = key,
                Name = "This is a changed asset",
                Files = new List<AssetFile>()
                {
                    new AssetFile()
                    {
                        Name = "file 01"
                    },
                    new AssetFile()
                    {
                        Name = "file 02"
                    }
                }
            };


            using Db db = new Db();

            db.Entry(asset).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
        }


        static void CaseCorrection()
        {
            Guid key = Guid.Empty;

            // we are using tempDb here only to acquire a randomized id for our asset used in this test case
            using (Db tempDb = new Db())
            {
                var assetToChange = tempDb.Assets.OrderBy(d => Guid.NewGuid()).First();
                key = assetToChange.Id;
            }

            //asset is created without its files
            Asset asset = new Asset()
            {
                Id = key,
                Name = "This is a changed asset",

            };


            using Db db = new Db();
            db.Attach(asset);
            //tell entity to load and know about this asset correction to asset files
            db.Entry(asset).Collection(d => d.Files).Load();

            //override the asset files, entity will probably flag the itens of the old collection as removed or something
            asset.Files = new List<AssetFile>()
            {
                new AssetFile()
                {
                    Name = "file 01"
                },
                new AssetFile()
                {
                    Name = "file 02"
                }
            };
            

            db.Entry(asset).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
        }
    }
}
