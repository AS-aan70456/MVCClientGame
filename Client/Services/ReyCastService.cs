using Client.Models;
using Client.ViewModel;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Services{
    class ReyCastService {

        private Maps map;

        public int CountRey { get; private set; }

        public ReyCastService(Maps map) {
            this.map = map;
            CountRey = 220;
        }

        public ReyCastViewModel[] ReyCast(Entity entity) {

            ReyCastViewModel[] result = new ReyCastViewModel[CountRey];

            float ReySpead = 0.005f;
            float depth = 12;

            for (int i = 0; i < CountRey; i++) {

                result[i] = new ReyCastViewModel();

                float ReyAngle = entity.angle + entity.fov / 2 - i * entity.fov / CountRey;

                float ReyX = (float)Math.Cos(((ReyAngle * Math.PI) / 180));
                float ReyY = (float)Math.Sin(((ReyAngle * Math.PI) / 180));

                float distance = 0;

                while (true) { 

                    distance += ReySpead;

                    float testX = (entity.Position.X + ReyX * distance);
                    float testY = (entity.Position.Y + ReyY * distance);
                    char Cell = ' ';
                    try{
                         Cell = map.GetMap()[(int)testY * map.Size.X + (int)testX];
                    }
                    catch (Exception e){
                    
                    }


                    if (distance > depth) {
                        result[i].ReyDistance = depth;
                        result[i].ReyPoint = new Vector2f(testX, testY);
                        break;
                    }
                    else if (Cell != ' '){
                        result[i].ReyDistance = distance;
                        result[i].ReyPoint = new Vector2f(testX, testY);
                        result[i].Wall = Cell;

                        if ((testX - (int)testX) > 0.9 || (testX - (int)testX) < 0.1) 
                            result[i].offset = (testY - (int)testY);
                        else 
                            result[i].offset = (testX - (int)testX);
                        break;
                    }
                        
                        
                    //Console.WriteLine((int)(reyCastModel[i].offsetHorizontal * 100) + " : " + (int)(reyCastModel[i].V * 100));
                }
            }
            return result;
        }

        
    }
}
