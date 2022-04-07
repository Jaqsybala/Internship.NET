namespace Week2.LINQTask
{
    public class Consumer
    {
        public int ConsumerCode { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }

        public Consumer(int consumerCode, int yearOfBirth, string address)
        { 
            ConsumerCode = consumerCode;
            YearOfBirth = yearOfBirth;
            Address = address;
        }
    }
}
