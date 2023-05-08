using Client.Interfeices;
using SFML.Graphics;
using SFML.Window;
using System;

namespace Client{
    class Program{
        static void Main(string[] args){
            
            RenderWindow window = new RenderWindow(new VideoMode(1080, 720), "MVC_SFML");

            Router router = Router.Init();
            router.graphicsController.Activation(router.gameControl, window);

            Console.Read();
        }
    }
}


//if (MathF.Sin((angle * MathF.PI) / 180) >= 0)
//{
//    if (MathF.Cos((angle * MathF.PI) / 180) >= 0)
//    {
//        ReyVertical = ReyPushStrategy(new LeftRey(), Position, angle + 90);
//        ReyHorizontal = ReyPushStrategy(new TopRey(), Position, angle);
//    }
//    else
//    {
//        ReyVertical = ReyPushStrategy(new TopRey(), Position, angle);
//        ReyHorizontal = ReyPushStrategy(new RightRey(), Position, angle - 90);
//    }
//}
//else
//{
//    if (MathF.Cos((angle * MathF.PI) / 180) <= 0)
//    {
//        ReyVertical = ReyPushStrategy(new RightRey(), Position, angle - 90);
//        ReyHorizontal = ReyPushStrategy(new DownRey(), Position, angle - 180);
//    }
//    else
//    {
//        ReyVertical = ReyPushStrategy(new DownRey(), Position, angle - 180);
//        ReyHorizontal = ReyPushStrategy(new LeftRey(), Position, angle + 90);
//
//    }
//}