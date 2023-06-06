using Client.Models;
using CoreEngine.Entitys;
using CoreEngine.System;
using SFML.System;
using System;

namespace CoreEngine.ReyCast{

    // service for calculating the length distance between the player and the walls
    public class ReyCastService {


        private TopRey topReyStrategic;
        private DownRey downReyStrategic;
        private LeftRey leftReyStrategic;
        private RightRey rightReyStrategic;


        private Level level;
        private bool isTransparantTextures;

        public ReyCastService(Level level, bool isTransparantTextures) {
            this.level = level;
            this.isTransparantTextures = isTransparantTextures;

            topReyStrategic = new TopRey();
            downReyStrategic = new DownRey();
            leftReyStrategic = new LeftRey();
            rightReyStrategic = new RightRey();
        }


        //the service interface executes the firing of beams according to the given instructions
        public Rey[] ReyCastWall(Entity entity, float fov, float depth, int CountRey) {
            Rey[] result = new Rey[CountRey];

            for (int i = 0; i < CountRey; i++) {
                float ReyAngle = (entity.angle + fov / 2 - i * fov / CountRey);
                Vector2f vector2F = entity.Position + (entity.Size / 2);
                ReySettings reySettings = new ReySettings(){
                    Position = new Vector2f(vector2F.X, vector2F.Y),
                    angle = ReyAngle,
                    originAngle = ReyAngle,
                    entityAngle = entity.angle,
                    depth = depth
                };

                result[i] = ReyPush(reySettings);
            }

            return result;
        }

        //internal function for emitting rays
        private Rey ReyPush(ReySettings settings) {

            Rey ReyVertical = new Rey();
            Rey ReyHorizontal = new Rey();

            if (MathF.Sin((settings.angle * MathF.PI) / 180) > 0) {
                if (MathF.Cos((settings.angle * MathF.PI) / 180) > 0) {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(leftReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(topReyStrategic));
                }
                else {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(topReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(rightReyStrategic));
                }
            }
            else {
                if (MathF.Cos((settings.angle * MathF.PI) / 180) < 0) {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(rightReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(downReyStrategic));
                }
                else {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(downReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(leftReyStrategic));
                }
            }

            ReyVertical = GetDistance(settings.Position, ReyVertical, ((settings.angle - settings.entityAngle) * MathF.PI) / 180);
            ReyHorizontal = GetDistance(settings.Position, ReyHorizontal, ((settings.angle - settings.entityAngle) * MathF.PI) / 180);


            //length check between vertical and horizontal reys
            if (ReyVertical.ReyDistance < ReyHorizontal.ReyDistance)
                return ReyVertical;
            else
                return ReyHorizontal;
        }

        //function iterates beam radiation
        private Rey ReyPushStrategy(ReySettings settings) {
            Rey result = new Rey();

            Vector2f ReyPos = settings.strategy.StartReyPos(settings.Position, settings.angle);

            for (int i = 0; i < settings.depth; i++) {

                if (ReyPos.X < 0 || ReyPos.Y < 0 || ReyPos.X > level.Size.Y - 1|| ReyPos.Y > level.Size.X - 1) {
                    result.ReyDistance = settings.depth;
                    result.ReyPoint = ReyPos;
                    return result;
                }

                try { result.Wall = level.Map[(int)ReyPos.Y, (int)ReyPos.X]; }
                catch (Exception) {
                    break;
                }

                if (!Level.IsVoid(result.Wall)) {

                    if (Level.Ishalf(result.Wall) && isTransparantTextures)
                        ReyPos += settings.strategy.NextReyPos(settings.angle) / 2;

                    if (Level.IsTransparent(result.Wall) && isTransparantTextures){
                        settings.Position = ReyPos;
                        settings.angle = settings.originAngle;
                        result.rey = ReyPush(settings);
                        
                    }

                    result.offset = settings.strategy.GetOfset(ReyPos);
                    result.ReyPoint = ReyPos;

                    return result;
                }
                ReyPos += settings.strategy.NextReyPos(settings.angle);
            }
            result.offset = settings.strategy.GetOfset(ReyPos);
            result.ReyPoint = ReyPos;

            return result;
        }

        //length check between vertical and horizontal beam reys
        private Rey GetDistance(Vector2f Position, Rey rey, float angle) {

            if (rey.rey != null) {
                rey.rey = GetDistance(Position, rey.rey, angle);
            }

            float a = (rey.ReyPoint.Y - Position.Y);
            float b = (rey.ReyPoint.X - Position.X);
            rey.ReyDistance = MathF.Abs(MathF.Sqrt((a * a) + (b * b))) * MathF.Cos(angle);
            return rey;
        }

    }

    class ReySettings {
        public IStrategyReyCanculate strategy { get; private set; }
        public Vector2f Position { get; set; }
        public float angle { get; set; }
        public float originAngle { get; set; }
        public float entityAngle { get; set; }
        public float depth { get; set; }

        public ReySettings() {


        }

        public ReySettings(ReySettings settings) {
            Position = settings.Position;
            angle = settings.angle;
            entityAngle = settings.entityAngle;
            originAngle = settings.originAngle;
            depth = settings.depth;
        }

        public ReySettings SetStrategy(IStrategyReyCanculate strategy) {
            this.strategy = strategy;
            if (strategy.GetType() == new LeftRey().GetType())
                angle = -(angle + 90);
            else if (strategy.GetType() == new RightRey().GetType())
                angle = -(angle - 90);
            else if (strategy.GetType() == new DownRey().GetType())
                angle -= 180;
            return this;
        }
    }

}