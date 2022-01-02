using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Initalize__Log(new Log_Arguments(throwOnError: false));

            Xerxes_OpenTK.Run<Rogue_Like_Window>(new SA__Configure_Game(arguments: args));
        }
    }
}
