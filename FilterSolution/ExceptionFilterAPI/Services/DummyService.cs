namespace ExceptionFilterAPI.Services
{
    public class DummyService
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int FindGreatest(int a, int b)
        {
            if(a == b)
            {
                throw new InvalidOperationException("Both numbers are equal.");
            }
            return a > b ? a : b;
        }
    }
}
