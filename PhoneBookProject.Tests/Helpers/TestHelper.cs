using Contact.API.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Report.API.Contexts;
using System;

namespace PhoneBookProject.Tests.Helpers
{
    public static class TestHelper
    {
        public static int? GetStatusCodeFromActionResult<T>(ActionResult<T> actionResult)
        {
           return ((ObjectResult)actionResult.Result).StatusCode;
        }


        public static DbContextOptions<ContactContext> GetContactContextForInMemoryDb()
        {
            return new DbContextOptionsBuilder<ContactContext>()
                .UseInMemoryDatabase(databaseName: "Contact" + Guid.NewGuid())
                .Options;
        }

        public static DbContextOptions<ReportContext> GetReportContextForInMemoryDb()
        {
            return new DbContextOptionsBuilder<ReportContext>()
                .UseInMemoryDatabase(databaseName: "Report" + Guid.NewGuid())
                .Options;
        }
    }
}
