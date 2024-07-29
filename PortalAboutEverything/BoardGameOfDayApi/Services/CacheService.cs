namespace BoardGameOfDayApi.Services
{
    public class CacheService
    {
        private int _boardGameOfDayId;
        private DateTime _dayOfChange;

        public int GetBoardGameOfDayId(List<int> allId)
        {
            if (_boardGameOfDayId == 0 || _dayOfChange.Date != DateTime.Now.Date) 
            {
                var random = new Random();
                var index = random.Next(allId.Count);
                _dayOfChange = DateTime.Now;
                _boardGameOfDayId = allId[index];
            }

            return _boardGameOfDayId;
        }
    }
}
