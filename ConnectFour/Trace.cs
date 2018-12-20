using System;
namespace ConnectFour {
    public class Trace {
        public static bool ON = false;
        public static void println(String str) {
            if (ON) { Console.WriteLine(str); }
        }
    }
}