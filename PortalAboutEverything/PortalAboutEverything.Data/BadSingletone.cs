namespace PortalAboutEverything.Data
{
    // Anti pattern. Do not repeat it
    public class BadSingletone
    {
        private BadSingletone() { }

        private static BadSingletone _instance;

        public static BadSingletone GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BadSingletone();
            }

            return _instance;
        }
    }
}
