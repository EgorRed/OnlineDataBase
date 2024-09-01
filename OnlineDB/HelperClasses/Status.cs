namespace OnlineDB.HelperClasses
{
    public enum RequestStatus
    {
        OK,
        DataIsNull,
        DataIsNotFound,
        DataRepetition,
        AccessIsDenied,
        ThisDataFolderIsExist,
        StructureIsNotFound
    }


    public class Status
    {
        public Status(RequestStatus _statusCode, string _description)
        {
            StatusCode = _statusCode;
            Description = _description;
        }
        public RequestStatus StatusCode { get; }
        public string Description { get; }
    }
}
