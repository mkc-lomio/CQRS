using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyFirstWebAPI.Application.CountryManagement.Queries;
using MyFirstWebAPI.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FileStreamController : ControllerBase
    {

        /// <summary>
        /// ISCAN FileStream Output
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("DoctorFile/Stream", Name = "viewDoctorFile")]
        [ProducesResponseType(typeof(FileResultFromStream), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetDoctorFileStream()
        {
            try
            {
              
                byte[] bytes = new byte[2];

                var file = new FileResultFromStream(
                    "DoctoreFile" + ".pdf",
                    new MemoryStream(bytes),
                    "application/octet-stream");

                return file;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Rivington Report
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("CS1MonthlyTransactionReport")]
        public async Task<ActionResult> GenerateCS1MonthlyTransactionReport(int year)
        {
            try
            {
                byte[] cS1MonthlyTransactionReport = new byte[2];

                System.DateTime month = System.DateTime.Now.AddMonths(-1);
                month = new System.DateTime(month.Year, month.Month, 1);

                var cS1MonthlyTransactionReportFile = new FileResultFromStream(
                    $"CS1_MonthlyTransaction_{month.Month}_{year}.xlsx",
                    new MemoryStream(cS1MonthlyTransactionReport),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                );

                return cS1MonthlyTransactionReportFile;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Rivington Centauri 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Test/IVANSHome")]
        public async Task<ActionResult> TestIVANSHome()
        {
            string homeXML = "<TEST>Hello</TEST>";
            var bytes = Encoding.UTF8.GetBytes(homeXML);

            return new FileResultFromStream(
                        $"home.xml",
                        new MemoryStream(bytes),
                        "application/xml");
        }


    }
}
