using OnlineDB.HelperClasses;

namespace OnlineDB.Model.StructureManager
{
    public class CreateStructureForUserModel
    {
        public string dataBaseName { get; set; }
        public List<ColumnsModel>? dataBaseColumns { get; set; } 
    }
}
