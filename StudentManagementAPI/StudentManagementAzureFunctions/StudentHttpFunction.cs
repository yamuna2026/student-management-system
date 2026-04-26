using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using StudentManagementAPI.Managers;

namespace StudentManagementAzureFunctions
{
    public class StudentHttpFunction
    {
        private readonly ILogger _logger;
        private readonly IStudentManager _studentManager;

        public StudentHttpFunction(ILoggerFactory loggerFactory, IStudentManager studentManager)
        {
            _logger = loggerFactory.CreateLogger<StudentHttpFunction>();
            _studentManager = studentManager;
        }

        [Function("GetStudentByIdFunction")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("HTTP trigger function processed a request.");

            // ✅ STEP 1: Get ID from query
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            string idParam = query["id"];

            if (string.IsNullOrEmpty(idParam))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Please provide id");
                return badResponse;
            }

            if (!int.TryParse(idParam, out int id))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid ID");
                return badResponse;
            }

            // ✅ STEP 2: Call Manager
            var student = await _studentManager.GetStudentById(id);

            if (student == null)
            {
                var notFound = req.CreateResponse(HttpStatusCode.NotFound);
                await notFound.WriteStringAsync("Student not found");
                return notFound;
            }

            // ✅ STEP 3: Return response
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(student);

            return response;
        }
    }
}