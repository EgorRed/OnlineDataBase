using OnlineDB.HelperClasses;
using OnlineDB.Model.StructureManager;

namespace OnlineDB.Service.Interfaces
{
    public interface IStructureManagerService
    {
        public Task<Status> CreateStructureForUser(CreateStructureForUserModel model, string userID);
        public Task<Status> CreateNewColumnsForUser(CreateNewColumnsForUserModel model, string userID);
        public Task<Status> DeleteStructureForUser(DeleteStructureForUserModel model, string userID);
        public Task<Status> DeleteDataFileForUser(DeleteDataFileForUserModel model, string userID);
        public Task<Outcome<StructureModel>> GetTableStructure(GetTableStructureModel model, string userID);
    }
}
