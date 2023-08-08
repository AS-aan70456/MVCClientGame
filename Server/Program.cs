using SFML.Window;
using System;

namespace Server{
    class Program{
        static void Main(string[] args){
            Window window = new Window(new VideoMode(3, 3), "MVC_SFML");

            Console.WriteLine("Hello World!");
        }
    }
}
