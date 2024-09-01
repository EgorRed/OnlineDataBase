using OnlineDB.HelperClasses;
using OnlineDB.Model.DataManage;

namespace OnlineDB.Service.Interfaces
{
    public interface IDataManagerService
    {
        Task<Status> WriteData(WriteDataModel model, string userID);
        Task<Outcome<DataModel>> ReadData(ReadDataModel model, string userID);
    }
}
