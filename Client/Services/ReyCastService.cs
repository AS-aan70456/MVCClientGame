using Client.Models;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Client.Services{

    // service for calculating the length distance between the player and the walls
    class ReyCastService {

        private Level level;

        public ReyCastService(Level level) {
            this.level = level;
        }


        //the service interface executes the firing of beams according to the given instructions
        public Rey[] ReyCastWall(Entity entity, float fov, float depth, int CountRey) {
            Rey[] result = new Rey[CountRey];

            for (int i = 0; i < CountRey; i++) {
                float ReyAngle = (entity.angle + fov / 2 - i * fov / CountRey);
                result[i] = ReyPush(entity.Position + (entity.Size / 2), ReyAngle);
                //result[i].ReyDistance /= MathF.Cos(i/fov);
            }

            return result;
        }

        //internal function for emitting rays
        private Rey ReyPush(Vector2f Position, float angle) {

            Rey ReyVertical = new Rey();
            Rey ReyHorizontal = new Rey();

            if (MathF.Sin((angle * MathF.PI) / 180) > 0) {
                if (MathF.Cos((angle * MathF.PI) / 180) > 0) {
                    ReyVertical = ReyPushStrategy(new LeftRey(), Position, -(angle + 90), angle);
                    ReyHorizontal = ReyPushStrategy(new TopRey(), Position, angle, angle);
                }
                else {
                    ReyVertical = ReyPushStrategy(new TopRey(), Position, angle, angle);
                    ReyHorizontal = ReyPushStrategy(new RightRey(), Position, -(angle - 90), angle);
                }
            }
            else {
                if (MathF.Cos((angle * MathF.PI) / 180) < 0) {
                    ReyVertical = ReyPushStrategy(new RightRey(), Position, -(angle - 90), angle);
                    ReyHorizontal = ReyPushStrategy(new DownRey(), Position, angle - 180, angle);
                }
                else {
                    ReyVertical = ReyPushStrategy(new DownRey(), Position, angle - 180, angle);
                    ReyHorizontal = ReyPushStrategy(new LeftRey(), Position, -(angle + 90), angle);
                }
            }

            ReyVertical = GetDistance(Position, ReyVertical);
            ReyHorizontal = GetDistance(Position, ReyHorizontal);


            //length check between vertical and horizontal reys
            if (ReyVertical.ReyDistance < ReyHorizontal.ReyDistance)
                return ReyVertical;
            else
                return ReyHorizontal;
        }

        //function iterates beam radiation
        private Rey ReyPushStrategy(IStrategyReyCanculate strategy, Vector2f Position, float angle, float originAngle) {
            Router router = Router.Init();
            Rey result = new Rey();

            Vector2f ReyPos = strategy.StartReyPos(Position, angle);

            for (int i = 0; i < 64; i++) {

                if (ReyPos.X < 0 || ReyPos.Y < 0 || ReyPos.X >= level.Size.X || ReyPos.Y >= level.Size.Y) {
                    result.ReyDistance = 1512;
                    result.ReyPoint = ReyPos;
                    return result;
                }

                result.Wall = level.Map[(int)ReyPos.Y * level.Size.X + (int)ReyPos.X];

                if (!level.IsVoid(result.Wall)) {

                    result.offset = strategy.GetOfset(ReyPos);
                    result.ReyPoint = ReyPos;

                    if (level.IsTransparent(result.Wall) && Config.config.isTransparantTextures)
                        result.rey = ReyPush(ReyPos, originAngle);
 

                    return result;
                }
                ReyPos += strategy.NextReyPos(angle);
            }
            return result;
        }

        //length check between vertical and horizontal beam reys
        private Rey GetDistance(Vector2f Position, Rey rey) {

            if (rey.rey != null) {
                rey.rey = GetDistance(Position, rey.rey);
            }

            float a = (rey.ReyPoint.Y - Position.Y);
            float b = (rey.ReyPoint.X - Position.X);
            rey.ReyDistance = MathF.Abs(MathF.Sqrt((a * a) + (b * b)));

            return rey;
        }

    }

    //interface strategy for defining the beam radiation formula
    interface IStrategyReyCanculate {

        Vector2f StartReyPos(Vector2f Position, float angle);
        Vector2f NextReyPos(float angle);
        float GetOfset(Vector2f pos);
    }

    class LeftRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle){

            Vector2f deltePosition = new Vector2f(){
                X = (Position.X - (int)Position.X),
                Y = (Position.Y - (int)Position.Y)
            };

            Position.X += -deltePosition.X;
            Position.Y += ((-deltePosition.X) / MathF.Tan((angle * MathF.PI) / 180));

            Position -= new Vector2f(0.00001f, 0.00001f);

            return Position;
        }

        public Vector2f NextReyPos(float angle){
            return new Vector2f(
                -1f,
                -(1f / MathF.Tan((angle * MathF.PI) / 180))
            );
        }

        public float GetOfset(Vector2f pos){
            return (float)(pos.Y - (int)pos.Y);
        }
    }

    class TopRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle){
            Vector2f deltePosition = new Vector2f(){
                X = (Position.X - (int)Position.X),
                Y = (Position.Y - (int)Position.Y),
            };

            Position.X += ((-deltePosition.Y) / MathF.Tan((angle * MathF.PI) / 180));
            Position.Y += -deltePosition.Y;

            Position -= new Vector2f(0.00001f, 0.00001f);

            return Position;
        }

        public Vector2f NextReyPos(float angle){
            return new Vector2f(
                -(1f / MathF.Tan((angle * MathF.PI) / 180)),
                -1f
            );
        }

        public float GetOfset(Vector2f pos)
        {
            return (float)(pos.X - (int)pos.X);
        }
    }

    class RightRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle){
            Vector2f deltePosition = new Vector2f(){
                X = 1 - (Position.X - (int)Position.X),
                Y = 1 - (Position.Y - (int)Position.Y),
            };

            Position.X -= (-deltePosition.X);
            Position.Y -= (((-deltePosition.X)) / MathF.Tan((angle * MathF.PI) / 180));

            return Position;
        }

        public Vector2f NextReyPos(float angle){
            return new Vector2f(
                1f,
                (1f / MathF.Tan((angle * MathF.PI) / 180))
            );
        }

        public float GetOfset(Vector2f pos){
            return (float)(pos.Y - (int)pos.Y);
        }
    }

    class DownRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle){
            Vector2f deltePosition = new Vector2f(){
                X = 1 - (Position.X - (int)Position.X),
                Y = 1 - (Position.Y - (int)Position.Y),
            };

            Position.X -= (((-deltePosition.Y)) / MathF.Tan((angle * MathF.PI) / 180));
            Position.Y -= (-deltePosition.Y);

            return Position;
        }

        public Vector2f NextReyPos(float angle){
            return new Vector2f(
                (1f / MathF.Tan((angle * MathF.PI) / 180)),
                1f
            );
        }

        public float GetOfset(Vector2f pos){
            return (float)(pos.X - (int)pos.X);
        }
    }

}