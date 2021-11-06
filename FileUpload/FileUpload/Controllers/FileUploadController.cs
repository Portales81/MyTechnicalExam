using FileUpload.BusinessLogic;
using FileUpload.Data.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace FileUpload.Controllers
{
    public class FileUploadController : Controller
    {
        private ITransactionsRepository _transactionRepository;

        public FileUploadController(ITransactionsRepository transactionsRepository)
        {
            _transactionRepository = transactionsRepository;
        }

        [Route("api/[controller]")]
        [HttpPost("upload")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            //bool IsValid = false;
                     
                if (CheckIFileIsValid(file))
                {               
                var filename = "tempfile";
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                //Create the file in your file system with the name you want.
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //Copy the uploaded file data to a memory stream
                        file.CopyTo(ms);
                        //Now write the data in the memory stream to the new file
                        fs.Write(ms.ToArray());
                        var csv = new CSVParsing();
                        csv.extractCSV(fs.Name);
                    }
                }
            }
            else
                {
                    return BadRequest(new { message = "Invalid File" });
                }
            return Ok();
        }


        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult Get()
        {
            var transactions = _transactionRepository.GetAllTransactions();
            return new OkObjectResult(transactions);
        }

        private bool CheckIFileIsValid(IFormFile file)
            {
                bool isValidFile;
                var extension = Path.GetExtension(file.FileName);   //"." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                long size = (file.Length / 1024) / 1024;
                isValidFile = (extension == ".csv" || extension == ".xml");
                if (size > 1)
                {
                    isValidFile = false;
                    //_logger.LogError("Size exceeds its limit");

                }                
                return isValidFile;
            }
        }
    }
