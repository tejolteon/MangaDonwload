using System.Threading.Tasks;

namespace System
{
    static class Awaiter
    {
        public static void DelaySeconds(int seconds) =>
            Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
    }
}
