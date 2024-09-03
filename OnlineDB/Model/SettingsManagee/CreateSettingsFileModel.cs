using OnlineDB.Model.StructureManager;

namespace OnlineDB.Model.SettingsManagee
{
    public class CreateSettingsFileModel
    {
        public string path { get; set; }
        public List<ColumnsModel> dataBaseColumns { get; set; }
    }
}
