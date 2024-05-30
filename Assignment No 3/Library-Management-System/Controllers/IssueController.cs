using Library_Management_System.Entities;
using Library_Management_System.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management_System.Controllers
{
    public class IssueController
    {
        [Route("api/[Controller]/[Action]")]
        [ApiController]

        public class BookController : Controller
        {

            public Container Container;

            public BookController()
            {
                Container = GetContainer();
            }

            private Container GetContainer()
            {
                string URI = "https://localhost:8081";
                string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                string DatabaseName = "LibraryDB";
                string ContainerName = "Books";

                CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
                Database database = cosmosClient.GetDatabase(DatabaseName);
                Container container = database.GetContainer(ContainerName);

                return container;
            }

            [HttpPost]
            public async Task<IssueModel> IssueBook(IssueModel issueModel)
            {
                
                IssueEntity issue = new IssueEntity
                {
                    BookId = issueModel.BookId,
                    MemberId = issueModel.MemberId,
                    IssueDate = issueModel.IssueDate,
                    ReturnDate = issueModel.ReturnDate,
                    IsReturned = issueModel.IsReturned,
                    Id = Guid.NewGuid().ToString(),
                    UId = Guid.NewGuid().ToString(),
                    DocumentType = "issue",
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now,
                    UpdatedBy = "Admin",
                    UpdatedOn = DateTime.Now,
                    Version = 1,
                    Active = true,
                    Archived = false
                };

                
                IssueEntity response = await Container.CreateItemAsync(issue);

                
                IssueModel responseModel = new IssueModel
                {
                    UId = response.UId,
                    BookId = response.BookId,
                    MemberId = response.MemberId,
                    IssueDate = response.IssueDate,
                    ReturnDate = response.ReturnDate,
                    IsReturned = response.IsReturned
                };
                return responseModel;
            }

            [HttpGet]
            public async Task<IssueModel> GetIssueByUId(string UId)
            {
                
                var issue = Container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archived == false).FirstOrDefault();

                
                    IssueModel issueModel = new IssueModel
                    {
                        UId = issue.UId,
                        BookId = issue.BookId,
                        MemberId = issue.MemberId,
                        IssueDate = issue.IssueDate,
                        ReturnDate = issue.ReturnDate,
                        IsReturned = issue.IsReturned
                    };
                    return issueModel;
               
            }

            [HttpPost]
            public async Task<IssueModel> UpdateIssue(IssueModel issue)
            {
               
                var existingIssue = Container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == issue.UId && q.Active == true && q.Archived == false).FirstOrDefault();

               
                    existingIssue.Archived = true;
                    existingIssue.Active = false;
                    await Container.ReplaceItemAsync(existingIssue, existingIssue.Id);

                   
                    existingIssue.Id = Guid.NewGuid().ToString();
                    existingIssue.UpdatedBy = "Admin";
                    existingIssue.UpdatedOn = DateTime.Now;
                    existingIssue.Version = existingIssue.Version + 1;
                    existingIssue.Active = true;
                    existingIssue.Archived = false;

                        existingIssue.BookId = issue.BookId;
                    existingIssue.MemberId = issue.MemberId;
                    existingIssue.IssueDate = issue.IssueDate;
                    existingIssue.ReturnDate = issue.ReturnDate;
                    existingIssue.IsReturned = issue.IsReturned;

                    
                    existingIssue = await Container.CreateItemAsync(existingIssue);

                    
                    IssueModel response = new IssueModel
                    {
                        UId = existingIssue.UId,
                        BookId = existingIssue.BookId,
                        MemberId = existingIssue.MemberId,
                        IssueDate = existingIssue.IssueDate,
                        ReturnDate = existingIssue.ReturnDate,
                        IsReturned = existingIssue.IsReturned
                    };
                    return response;
                }
               

        }

        }
}
