using Library_Management_System.Entities;
using Library_Management_System.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management_System.Controllers
{
    public class MemberController
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
            public async Task<MemberModel> AddMember(MemberModel memberModel)
            {
                
                MemberEntity member = new MemberEntity
                {
                    Name = memberModel.Name,
                    DateOfBirth = memberModel.DateOfBirth,
                    Email = memberModel.Email,
                    Id = Guid.NewGuid().ToString(),
                    UId = Guid.NewGuid().ToString(),
                    DocumentType = "member",
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now,
                    UpdatedBy = "Admin",
                    UpdatedOn = DateTime.Now,
                    Version = 1,
                    Active = true,
                    Archived = false
                };

               
                MemberEntity response = await Container.CreateItemAsync(member);

               
                MemberModel responseModel = new MemberModel
                {
                    UId = response.UId,
                    Name = response.Name,
                    DateOfBirth = response.DateOfBirth,
                    Email = response.Email
                };
                return responseModel;
            }

            [HttpGet]
            public async Task<MemberModel> GetMemberByUId(string UId)
            {
              
                var member = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archived == false).FirstOrDefault();

                
                    MemberModel memberModel = new MemberModel
                    {
                        UId = member.UId,
                        Name = member.Name,
                        DateOfBirth = member.DateOfBirth,
                        Email = member.Email
                    };
                    return memberModel;
               
            }

            [HttpGet]
            public async Task<List<MemberModel>> GetAllMembers()
            {
                
                var members = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "member").ToList();

                
                List<MemberModel> memberModels = new List<MemberModel>();

                foreach (var member in members)
                {
                    MemberModel model = new MemberModel
                    {
                        UId = member.UId,
                        Name = member.Name,
                        DateOfBirth = member.DateOfBirth,
                        Email = member.Email
                    };
                    memberModels.Add(model);
                }

               
                return memberModels;
            }

            [HttpPost]
            public async Task<MemberModel> UpdateMember(MemberModel member)
            {
                
                var existingMember = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == member.UId && q.Active == true && q.Archived == false).FirstOrDefault();

                
                    existingMember.Archived = true;
                    existingMember.Active = false;
                    await Container.ReplaceItemAsync(existingMember, existingMember.Id);

                    
                    existingMember.Id = Guid.NewGuid().ToString();
                    existingMember.UpdatedBy = "Admin";
                    existingMember.UpdatedOn = DateTime.Now;
                    existingMember.Version = existingMember.Version + 1;
                    existingMember.Active = true;
                    existingMember.Archived = false;

                    
                    existingMember.Name = member.Name;
                    existingMember.DateOfBirth = member.DateOfBirth;
                    existingMember.Email = member.Email;

                   
                    existingMember = await Container.CreateItemAsync(existingMember);

                   
                    MemberModel response = new MemberModel
                    {
                        UId = existingMember.UId,
                        Name = existingMember.Name,
                        DateOfBirth = existingMember.DateOfBirth,
                        Email = existingMember.Email
                    };
                    return response;
               
            }


        }

        }

    }
