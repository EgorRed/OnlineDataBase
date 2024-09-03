using OnlineDB.HelperClasses;
using OnlineDB.Model.SettingsManagee;
using OnlineDB.Model.StructureManager;
using OnlineDB.Service.Interfaces;

namespace OnlineDB.Service
{
    public class StructureManagerService : IStructureManagerService
    {
        private string _basePath = "UserStructure";
        private AppSettingsManager _appSettingsManager = new AppSettingsManager();

        public async Task<Status> CreateStructureForUser(CreateStructureForUserModel model, string userID)
        {
            
            string fullpath = $"{_basePath}\\{userID}\\{model.dataBaseName}";
            if (!Directory.Exists(fullpath))
            {
                Directory.CreateDirectory(fullpath);
                if (model.dataBaseColumns.Count != 0)
                {
                    foreach (var item in model.dataBaseColumns)
                    {
                        using (FileStream fs = new FileStream($"{fullpath}\\{item.name}.dat", FileMode.Create)) { }
                    }

                    CreateSettingsFileModel createSettingsFileModel = new CreateSettingsFileModel();
                    createSettingsFileModel.path = fullpath;
                    createSettingsFileModel.dataBaseColumns = model.dataBaseColumns;
                    _appSettingsManager.CreateSettingsFile(createSettingsFileModel);

                }
                else
                {
                    return new Status(RequestStatus.DataIsNull, "Data is null!");
                }
            }
            else
            {
                return new Status(RequestStatus.ThisDataFolderIsExist, "This DataFolder is already exist!");
            }
            return new Status(RequestStatus.OK, "success");
        }

        public async Task<Status> DeleteDataFileForUser(DeleteDataFileForUserModel model, string userID)
        {
            {
                string basepath = $"{_basePath}\\{userID}\\{model.dataBaseName}\\{model.columnName}.dat";
                if (File.Exists(basepath))
                {
                    File.Delete(basepath);
                }
                else
                {
                    return new Status(RequestStatus.DataIsNotFound, $"Data column {model.columnName} is not found!");
                }
                return new Status(RequestStatus.OK, $"Deleting of data folder {model.columnName} is success!");
            }
        }

        public async Task<Status> DeleteStructureForUser(DeleteStructureForUserModel model, string userID)
        {
            {
                string basepath = $"{_basePath}\\{userID}\\{model.dataBaseName}";
                if (Directory.Exists(basepath))
                {
                    DirectoryInfo dir = new DirectoryInfo(basepath);
                    foreach (var item in dir.GetFiles())
                    {
                        File.Delete($"{item}");
                    }
                    Directory.Delete(basepath);
                }
                else
                {
                    Console.WriteLine("Eblan");
                    return new Status(RequestStatus.StructureIsNotFound, $"Structure {model.dataBaseName} is not found");
                }
                return new Status(RequestStatus.OK, $"Deleting of user's structure {model.dataBaseName} is success!");
            }
        }

        public async Task<Outcome<StructureModel>> GetTableStructure(GetTableStructureModel model, string userID)
        {
            string basepath = $"{_basePath}\\{userID}\\{model.dataBaseName}";
            DirectoryInfo dir = new DirectoryInfo(basepath);
            StructureModel structureInfo = new StructureModel();
            structureInfo.listOfColumns = new List<string>();

            structureInfo.listOfColumns.Intersect(structureInfo.listOfColumns);

            foreach (var item in dir.GetFiles())
            {
                structureInfo.listOfColumns.Add(item.Name);
            }
            structureInfo.dataBaseName = model.dataBaseName;
            structureInfo.countOfColumns = structureInfo.listOfColumns.Count;
            return new Outcome<StructureModel>(new Status(RequestStatus.OK, "Success!"), structureInfo);
        }

        public async Task<Status> CreateNewColumnsForUser(CreateNewColumnsForUserModel model, string userID)
        {
            string basepath = $"{_basePath}\\{userID}\\{model.dataBaseName}";
            if (Directory.Exists(basepath))
            {
                DirectoryInfo dir = new DirectoryInfo(basepath);
                var listOfFiles = dir.GetFiles().Select(x => x.Name).ToList();
                var userNameList = model.dataBaseColumns.Select(x => x.name + ".dat").ToList();
                if (listOfFiles.Intersect(userNameList).Any())
                {
                    return new Status(RequestStatus.DataRepetition, "Repetitions is founded!");
                }
                else
                {
                    foreach (var item in model.dataBaseColumns)
                    {
                        using (FileStream fs = new FileStream($"{basepath}\\{item.name}.dat", FileMode.Create)) { }
                    }

                    CreateSettingsFileModel createSettingsFileModel = new CreateSettingsFileModel();
                    createSettingsFileModel.path = basepath;
                    createSettingsFileModel.dataBaseColumns = model.dataBaseColumns;
                    _appSettingsManager.CreateSettingsFile(createSettingsFileModel);
                }
            }
            else
            {
                return new Status(RequestStatus.StructureIsNotFound, $"Data folder {model.dataBaseName} is not found!");
            }

            return new Status(RequestStatus.OK, $"Data file is added successful!");
        }
    }
}
