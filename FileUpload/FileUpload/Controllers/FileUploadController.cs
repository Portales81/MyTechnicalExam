using FileUpload.BusinessLogic;
using FileUpload.Data.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    [ApiController]
    public class FileUploadController :ControllerBase
    {
        private ITransactionsRepository _transactionRepository;

        public FileUploadController(ITransactionsRepository transactionsRepository)
        {
            _transactionRepository = transactionsRepository;
        }

        [Route("api/[controller]")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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


        [Route("api/FileUpload/transactions/all")]
        [HttpGet]
        public IActionResult Get()
        {
            var transactions = _transactionRepository.GetAllTransactions();
            if (transactions.Count() ==0)
            {
                return NotFound("No Records found");
            }
            return Ok(transactions);            
        }


        [Route("api/FileUpload/transactions/filter/bycurrency/{currency}")]
        [HttpGet]
        public IActionResult GetByCurrency(string currency)
        {
            var transactions = _transactionRepository.GetTransactionByCurrency(currency);
            if (transactions.Count() == 0)
            {
                return NotFound("No Records found");
            }
            return Ok(Serialize(transactions));
        }

        [Route("api/FileUpload/transactions/filter/bystatus/{status}")]
        [HttpGet]
        public IActionResult GetByStatus(string status)
        {
            var transactions = _transactionRepository.GetTransactionByStatus(status);
            if (transactions.Count() == 0)
            {
                return NotFound("No Records found");
            }
            return Ok(transactions);

        }

        #region Private Method Used
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
        private Object Serialize(object obj)
        {
            if (obj!=null)
            {
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                return json;   
            }
            return null;
        }
    } 
    #endregion
}
