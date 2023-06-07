using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace AzFnBlobReader
{
    public class BlobReaderFn
    {
        /// <summary>
        /// BlobTrigger: This will be triggered when the
        /// container 'mycontainer' has the file 
        /// The file will be read as stream  Stream myBlob
        /// </summary>
        /// <param name="myBlob"></param>
        /// <param name="name"></param>
        /// <param name="log"></param>
        [FunctionName("FileRedaer")]
        public async Task Run([BlobTrigger("mycontainer/{name}", Connection = "blobconnstr")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            // 1. Make sure that the File is more than 0 bytes

            if (myBlob.Length > 0)
            {
                // 2. Use StreamReader to start reading the Blob
                using (StreamReader reader = new StreamReader(myBlob))
                { 
                    // 3. Read the First Line as Column Header
                    var columnHeader = await reader.ReadLineAsync();

                    // 4. Set the Start number as 1 so that we will increament it for each line read from
                    // csv file
                    int startLineNumber = 1;

                    // 5. Start reading with the CurrentLine
                    var currentLine = await reader.ReadLineAsync();
                    // 6. Start reading
                    while (currentLine != null)
                    {
                        // 7. Read the Line
                        currentLine = await reader.ReadLineAsync();
                        // 8. Call the Database to Perform Insert operations
                        await AddRecordToTableAsync(currentLine, log);
                        // 9. Increment
                        startLineNumber++;
                    }
                }
            }
            else
            {
                log.LogInformation($"The File is 0 byte in length means its is empty");
            }

        }


        private async Task AddRecordToTableAsync(string curLine, ILogger logger)
        {
            // 8.a. Make sure that the Line is Not Null or Empty with whitespaces
            if (string.IsNullOrWhiteSpace(curLine))
            {
                logger.LogInformation($"The Current Line is not having data");
                return;
            }
            // 8.b. Get Column values by splitting the line by ','
            var columns = curLine.Split(',');
            // 8.c. Connect to Database and Perform Insert operation
            SqlConnection conn = new SqlConnection("CONN STRING");
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into PersonInfo (FirstName,LastName) values (@FirstName,@LastName)";
            cmd.Parameters.AddWithValue("@FirstName", columns[0]); // FirstName
            cmd.Parameters.AddWithValue("@LastName", columns[1]); // LastName
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
