namespace OnlineDB.HelperClasses
{
    public enum TypeColums
    {
        UNDEFINED,
        INTEGER,
        STRING
    }

    public class Columns
    {
        public Columns(string name, string type)
        {
            this.name = name;
            switch (type.ToUpper())
            {
                case "INTEGER":
                    this.type = TypeColums.INTEGER;
                    break;
                case "STRING":
                    this.type = TypeColums.STRING;
                    break;
                default:
                    this.type = TypeColums.UNDEFINED;
                    break;
            }
        }

        public string? name { get; set; }
        public TypeColums type {  get; set; }
    }
}
