using OnlineDB.HelperClasses;
using OnlineDB.Model.SettingsManagee;
using OnlineDB.Model.StructureManager;
using System.Text;

namespace OnlineDB.Service
{
    public class AppSettingsManager
    {
        public async Task<Status> CreateSettingsFile(CreateSettingsFileModel model)
        {
            List<Columns> listOfColumns = new List<Columns>();
            using (BinaryReader br = new BinaryReader(File.Open($"{model.path}\\listOfTypes.inf", FileMode.OpenOrCreate)))
            {
                while (br.PeekChar() > -1)
                {
                    string colName = br.ReadString();
                    string colType = br.ReadString();
                    Columns col = new Columns(colName, colType);
                    listOfColumns.Add(col);
                }
            }


            foreach (ColumnsModel item in model.dataBaseColumns)
            {
                Columns columns = new Columns(item.name, item.type);
                listOfColumns.Add(columns);
            }


            using (BinaryWriter bw = new BinaryWriter(File.Open($"{model.path}\\listOfTypes.inf", FileMode.Open)))
            {
                foreach (var item in listOfColumns)
                {
                    bw.Write(item.name);
                    bw.Write(item.type.ToString());
                }
            }
            return new Status(RequestStatus.OK, "Creating of column is success!");
        }

        public async Outcome<OutGetAppSettingsDataModel> GetAppSettingsData(InGetAppSettingsDataModel model)
        {
            OutGetAppSettingsDataModel outGetAppSettingsDataModel = new OutGetAppSettingsDataModel();
            //List<Columns> listOfColumns = new List<Columns>();
            using (BinaryReader br = new BinaryReader(File.Open($"{model.path}\\listOfTypes.inf", FileMode.OpenOrCreate)))
            {
                while (br.PeekChar() > -1)
                {
                    ColumnsModel col = new ColumnsModel();
                    col.name = br.ReadString();
                    col.type = br.ReadString();
                     
                    outGetAppSettingsDataModel.dataBaseColumns.Add(col);
                }
            }

            return new Outcome<OutGetAppSettingsDataModel>(new Status(RequestStatus.OK, "ok"), outGetAppSettingsDataModel);
        }
    }
}
