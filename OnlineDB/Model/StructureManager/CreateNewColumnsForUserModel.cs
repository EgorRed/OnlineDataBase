using OnlineDB.HelperClasses;

namespace OnlineDB.Model.StructureManager
{
    public class CreateNewColumnsForUserModel
    {
        public string? dataBaseName { get; set; }
        public List<ColumnsModel> dataBaseColumns { get; set; }
    }
}
