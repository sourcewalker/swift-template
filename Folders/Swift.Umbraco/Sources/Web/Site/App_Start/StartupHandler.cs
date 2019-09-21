using Swift.Umbraco.Models.Domain;
using $safeprojectname$.Controllers.Render;
using System;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web.Mvc;

namespace $safeprojectname$
{
    public class StartupHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarting(
            UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(BaseController));
        }

        protected override void ApplicationStarted(
            UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            var dbContext = applicationContext.DatabaseContext;
            var dbSchemaHelper = new DatabaseSchemaHelper(
                                    dbContext.Database,
                                    ApplicationContext.Current.ProfilingLogger.Logger,
                                    dbContext.SqlSyntax);

            CreateTableScript(dbSchemaHelper);
            SeedDatabase(dbContext);
        }

        private void CreateTableScript(DatabaseSchemaHelper dbSchemaHelper)
        {
            if (!dbSchemaHelper.TableExist<Prize>())
            {
                dbSchemaHelper.CreateTable<Prize>(false);
            }

            if (!dbSchemaHelper.TableExist<Country>())
            {
                dbSchemaHelper.CreateTable<Country>(false);
            }

            if (!dbSchemaHelper.TableExist<Consumer>())
            {
                dbSchemaHelper.CreateTable<Consumer>(false);
            }

            if (!dbSchemaHelper.TableExist<InstantWinMoment>())
            {
                dbSchemaHelper.CreateTable<InstantWinMoment>(false);
            }

            if (!dbSchemaHelper.TableExist<Participant>())
            {
                dbSchemaHelper.CreateTable<Participant>(false);
            }

            if (!dbSchemaHelper.TableExist<Participation>())
            {
                dbSchemaHelper.CreateTable<Participation>(false);
            }

            if (!dbSchemaHelper.TableExist<FailedTransaction>())
            {
                dbSchemaHelper.CreateTable<FailedTransaction>(false);
            }
        }

        private void SeedDatabase(DatabaseContext dbContext)
        {
            var database = dbContext.Database;
            var sqlProvider = ApplicationContext.Current.DatabaseContext.SqlSyntax;

            #region Create Countries
            var ukQuery = new Sql()
                            .Select("*")
                            .From<Country>(sqlProvider)
                            .Where<Country>(c => c.Culture == "en-GB", sqlProvider);
            var uk = database.Fetch<Country>(ukQuery).FirstOrDefault();

            if (uk == null)
            {
                var ukId = Guid.NewGuid();

                var ukCountry = new Country
                {
                    Id = ukId,
                    Name = "United Kingdom",
                    Code = "UK",
                    Culture = "en-GB",
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(ukCountry);
            }

            var ieQuery = new Sql()
                            .Select("*")
                            .From<Country>(sqlProvider)
                            .Where<Country>(c => c.Culture == "en-IE", sqlProvider);
            var ie = database.Fetch<Country>(ieQuery).FirstOrDefault();

            if (ie == null)
            {
                var ieId = Guid.NewGuid();

                var ieCountry = new Country
                {
                    Id = ieId,
                    Name = "Ireland",
                    Code = "IE",
                    Culture = "en-IE",
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(ieCountry);
            }

            #endregion

            #region Create Prizes

            var fivePoundsQuery = new Sql()
                            .Select("*")
                            .From<Prize>(sqlProvider)
                            .Where<Prize>(c => c.Name == "£5", sqlProvider);
            var fivePounds = database.Fetch<Prize>(fivePoundsQuery).FirstOrDefault();



            if (fivePounds == null)
            {
                var fiveId = Guid.NewGuid();
                var number = 604;

                var fivePoundPrize = new Prize
                {
                    Id = fiveId,
                    Name = "£5",
                    Value = 5,
                    TotalNumber = number,
                    Remaining = number,
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(fivePoundPrize);
            }

            var tenPoundsQuery = new Sql()
                            .Select("*")
                            .From<Prize>(sqlProvider)
                            .Where<Prize>(c => c.Name == "£10", sqlProvider);
            var tenPounds = database.Fetch<Prize>(tenPoundsQuery).FirstOrDefault();

            if (tenPounds == null)
            {
                var tenId = Guid.NewGuid();
                var number = 418;

                var tenPoundPrize = new Prize
                {
                    Id = tenId,
                    Name = "£10",
                    Value = 10,
                    TotalNumber = number,
                    Remaining = number,
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(tenPoundPrize);
            }

            var twentyPoundsQuery = new Sql()
                            .Select("*")
                            .From<Prize>(sqlProvider)
                            .Where<Prize>(c => c.Name == "£20", sqlProvider);
            var twentyPounds = database.Fetch<Prize>(twentyPoundsQuery).FirstOrDefault();

            if (twentyPounds == null)
            {
                var twentyId = Guid.NewGuid();
                var number = 220;

                var twentyPoundPrize = new Prize
                {
                    Id = twentyId,
                    Name = "£20",
                    Value = 20,
                    TotalNumber = number,
                    Remaining = number,
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(twentyPoundPrize);
            }

            var fiftyPoundsQuery = new Sql()
                            .Select("*")
                            .From<Prize>(sqlProvider)
                            .Where<Prize>(c => c.Name == "£50", sqlProvider);
            var fiftyPounds = database.Fetch<Prize>(fiftyPoundsQuery).FirstOrDefault();

            if (fiftyPounds == null)
            {
                var fiftyId = Guid.NewGuid();
                var number = 83;

                var fiftyPoundPrize = new Prize
                {
                    Id = fiftyId,
                    Name = "£50",
                    Value = 50,
                    TotalNumber = number,
                    Remaining = number,
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(fiftyPoundPrize);
            }

            var hundredPoundsQuery = new Sql()
                            .Select("*")
                            .From<Prize>(sqlProvider)
                            .Where<Prize>(c => c.Name == "£100", sqlProvider);
            var hundredPounds = database.Fetch<Prize>(hundredPoundsQuery).FirstOrDefault();

            if (hundredPounds == null)
            {
                var hundredId = Guid.NewGuid();
                var number = 20;

                var hundredPoundPrize = new Prize
                {
                    Id = hundredId,
                    Name = "£100",
                    Value = 100,
                    TotalNumber = number,
                    Remaining = number,
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(hundredPoundPrize);
            }

            var twoHundredPoundsQuery = new Sql()
                            .Select("*")
                            .From<Prize>(sqlProvider)
                            .Where<Prize>(c => c.Name == "£250", sqlProvider);
            var twoHundredPounds = database.Fetch<Prize>(twoHundredPoundsQuery).FirstOrDefault();

            if (twoHundredPounds == null)
            {
                var twoHundredPoundsId = Guid.NewGuid();
                var number = 15;

                var twoHundredPoundsPrize = new Prize
                {
                    Id = twoHundredPoundsId,
                    Name = "£250",
                    Value = 250,
                    TotalNumber = number,
                    Remaining = number,
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(twoHundredPoundsPrize);
            }

            var fivehundredPoundsQuery = new Sql()
                            .Select("*")
                            .From<Prize>(sqlProvider)
                            .Where<Prize>(c => c.Name == "£500", sqlProvider);
            var fivehundredPounds = database.Fetch<Prize>(fivehundredPoundsQuery).FirstOrDefault();

            if (fivehundredPounds == null)
            {
                var fivehundredId = Guid.NewGuid();
                var number = 3;

                var fivehundredPoundPrize = new Prize
                {
                    Id = fivehundredId,
                    Name = "£500",
                    Value = 500,
                    TotalNumber = number,
                    Remaining = number,
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(fivehundredPoundPrize);
            }

            var thousandPoundsQuery = new Sql()
                            .Select("*")
                            .From<Prize>(sqlProvider)
                            .Where<Prize>(c => c.Name == "£1,000", sqlProvider);
            var thousandPounds = database.Fetch<Prize>(thousandPoundsQuery).FirstOrDefault();

            if (thousandPounds == null)
            {
                var thousandId = Guid.NewGuid();
                var number = 2;

                var thousandPoundPrize = new Prize
                {
                    Id = thousandId,
                    Name = "£1,000",
                    Value = 1000,
                    TotalNumber = number,
                    Remaining = number,
                    CreatedOn = DateTime.UtcNow
                };

                database.Insert(thousandPoundPrize);
            }

            #endregion
        }
    }
}