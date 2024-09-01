using OnlineDB.HelperClasses;
using OnlineDB.Model.DataManage;
using OnlineDB.Service.Interfaces;
using System.IO;
using System.Linq.Expressions;

namespace OnlineDB.Service
{
    public class DataManagerService : IDataManagerService
    {
        private string _basePath = "..\\..\\..\\UserStructure";

        public async Task<Status> WriteData(WriteDataModel model, string userID)
        {
            string fullpath = $"{_basePath}\\{userID}\\{model.dataBaseName}";
            if (Directory.Exists(fullpath) && File.Exists($"{fullpath}\\{model.columnsName}.dat"))
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open($"{fullpath}\\{model.columnsName}.dat", FileMode.Open)))
                {
                    foreach (var elem in model.data)
                    {
                        writer.Write(elem);
                    }
                }

            }
            else
            {
                return new Status(RequestStatus.StructureIsNotFound, $"The path [{fullpath}\\{model.columnsName}] was not found");
            }

            return new Status(RequestStatus.OK, "ok");
        }
        

        public async Task<Outcome<DataModel>> ReadData(ReadDataModel model, string userID)
        {
            string fullpath = $"{_basePath}\\{userID}\\{model.dataBaseName}";

            DataModel dataModel = new DataModel();
            dataModel.Data = new List<string>();

            if (Directory.Exists(fullpath) && File.Exists($"{fullpath}\\{model.columnsName}.dat"))
            {

                using (BinaryReader reader = new BinaryReader(File.Open($"{fullpath}\\{model.columnsName}.dat", FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string data = reader.ReadString();
                        dataModel.Data.Add(data);
                    }
                }
            }
            else
            {
                return new Outcome<DataModel>(new Status(RequestStatus.StructureIsNotFound, $"The path [{fullpath}\\{model.columnsName}] was not found"), new DataModel());
            }

            return new Outcome<DataModel>(new Status(RequestStatus.OK, "ok"), dataModel);
        }


    }
}
