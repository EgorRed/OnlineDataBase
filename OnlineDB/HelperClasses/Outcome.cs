namespace OnlineDB.HelperClasses
{
    public class Outcome<Data>
    {
        public Status status;
        public Data data;

        public Outcome(Status _status, Data _data)
        {
            status = _status;
            data = _data;
        }
    }
}
