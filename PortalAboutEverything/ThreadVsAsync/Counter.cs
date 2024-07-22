namespace ThreadVsAsync
{
    public class Counter
    {
        public int Value { get; set; }

        private object LockObj = new object();

        public void Count(string name)
        {
            while (true)
            {
                //Liza wait
                lock (LockObj) // Ivan into the method
                {

                    if (Value % 2 == 0)
                    {
                        Console.WriteLine($"{name}: {Value}");
                    }
                    else
                    {
                        Console.WriteLine($"{name}: ODD {(Value % 2 == 1 ? Value : "****************")}");
                    }
                    GetAge(1);
                    Value++;
                }
            }
        }

        public int GetAge(double tick)
        {
            // Ivan wait
            lock (LockObj) // Lize into the method
            {
                var a = 2;//
                Count("q"); // Liza
            }
            return 1;
        }

        public void Do()
        {
            Console.Write("Done");
        }
    }
}
