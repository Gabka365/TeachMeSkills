namespace ReflectionExample
{
    public class User
    {
        public User() { }
        [Test]
        [Smile]
        public User(string name) { }

        private User(int a, int b) { }

        public int Coins { private get; set; }

        [Test]
        [Smile]
        public string Name { get; set; }

        private int _age = 20;

        public void Do() { }

        [Test]
        public int GetLuckyNumber() { return 42; }
    }

    public class TestAttribute : Attribute
    {

    }

    public class SmileAttribute : Attribute { }
}
