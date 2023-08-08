using CoreEngine.Entitys; // Import the CoreEngine.Entitys namespace
using CoreEngine.System;  // Import the CoreEngine.System namespace
using SFML.System;        // Import the SFML.System namespace
using System;

namespace CoreEngine.ReyCast
{
    public class ReyCastService
    {

        // Strategies for ray calculations in different directions
        private TopRey topReyStrategic;
        private DownRey downReyStrategic;
        private LeftRey leftReyStrategic;
        private RightRey rightReyStrategic;

        // Level and transparency setting
        private Level level;
        private bool isTransparantTextures;

        // Constructor
        public ReyCastService(Level level, bool isTransparantTextures)
        {
            this.level = level;
            this.isTransparantTextures = isTransparantTextures;

            // Initialize ray direction strategies
            topReyStrategic = new TopRey();
            downReyStrategic = new DownRey();
            leftReyStrategic = new LeftRey();
            rightReyStrategic = new RightRey();
        }

        // Perform raycasting for walls
        public ReyContainer[] ReyCastWall(Entity entity, float fov, float depth, int CountRey)
        {
            Rey[] result = new Rey[CountRey];

            for (int i = 0; i < CountRey; i++)
            {
                // Calculate ray angle based on field of view and ray count
                float ReyAngle = (entity.angle + fov / 2 - i * fov / CountRey);

                // Create ray settings
                ReySettings reySettings = new ReySettings()
                {
                    Position = entity.Position + (entity.Size / 2),
                    angle = ReyAngle,
                    originAngle = ReyAngle,
                    entityAngle = entity.angle,
                    depth = depth
                };

                // Calculate ray and store the result
                result[i] = ReyPush(reySettings);
            }

            return result;
        }

        // Internal function to emit rays
        private Rey ReyPush(ReySettings settings)
        {
            // Initialize vertical and horizontal rays
            Rey ReyVertical = new Rey();
            Rey ReyHorizontal = new Rey();

            // Calculate ray direction and select appropriate strategies
            if (MathF.Sin((settings.angle * MathF.PI) / 180) > 0)
            {
                if (MathF.Cos((settings.angle * MathF.PI) / 180) > 0)
                {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(leftReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(topReyStrategic));
                }
                else
                {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(topReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(rightReyStrategic));
                }
            }
            else
            {
                if (MathF.Cos((settings.angle * MathF.PI) / 180) < 0)
                {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(rightReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(downReyStrategic));
                }
                else
                {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(downReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(leftReyStrategic));
                }
            }

            // Calculate ray length and return hit result
            return Rey.HitDistribution(ReyHorizontal, ReyVertical, settings);
        }

        // Function to iterate ray emissions based on a strategy
        private Rey ReyPushStrategy(ReySettings settings)
        {
            Rey result = new Rey();

            Vector2f reyPos = settings.strategy.StartReyPos(settings.Position, settings.angle);
            char reyWall = ' ';

            for (int i = 0; i < settings.depth; i++)
            {
                // Check if ray position is out of bounds
                if (reyPos.X < 0 || reyPos.Y < 0 || reyPos.X > level.Size.Y - 1 || reyPos.Y > level.Size.X - 1)
                {
                    result.Hit(new HitWall()
                    {
                        ReyDistance = GetDistance(reyPos, settings.Position),
                        ReyPoint = reyPos,
                        Wall = '0'
                    });
                    return result;
                }

                // Get wall type at current ray position
                reyWall = level.Map[(int)reyPos.Y, (int)reyPos.X];

                // Handle ray hits
                if (!Level.IsVoid(reyWall))
                {
                    // Create hit based on wall properties
                    HitWall hit = new HitWall(reyPos);

                    if (Level.IsHalf(reyWall))
                        hit.ReyPoint += settings.strategy.NextReyPos(settings.angle) / 2;

                    hit.ReyDistance = GetDistance(hit.ReyPoint, settings.Position);
                    hit.offset = settings.strategy.GetOfset(hit.ReyPoint);
                    hit.Wall = reyWall;

                    result.Hit(hit);

                    // Check transparency
                    if (!Level.IsTransparent(reyWall) || !isTransparantTextures)
                        return result;
                }

                // Hit the floor
                result.Hit(new HitFlore()
                {
                    ReyDistance = GetDistance(reyPos, settings.Position),
                    ReyPoint = reyPos,
                    Wall = reyWall,
                });

                reyPos += settings.strategy.NextReyPos(settings.angle);
            }

            // Hit a void wall at maximum depth
            result.Hit(new HitWall()
            {
                ReyDistance = GetDistance(
                    new Vector3f(reyPos.X, reyPos.Y, 0),
                    new Vector3f(settings.Position.X, settings.Position.Y, 0.5f)
                ),
                ReyPoint = reyPos,
                Wall = '0'
            });

            return result;
        }

        // Calculate distance between two 2D points
        private float GetDistance(Vector2f PosA, Vector2f PosB)
        {
            return MathF.Abs(MathF.Sqrt(MathF.Pow(PosA.X - PosB.X, 2) + MathF.Pow(PosA.Y - PosB.Y, 2)));
        }

        // Calculate distance between two 3D points
        private float GetDistance(Vector3f PosA, Vector3f PosB)
        {
            return MathF.Abs(MathF.Sqrt(MathF.Pow(PosA.X - PosB.X, 2) + MathF.Pow(PosA.Y - PosB.Y, 2) + MathF.Pow(PosA.Z - PosB.Z, 2)));
        }

    }

    // Settings for ray calculations
    class ReySettings
    {
        public IStrategyReyCanculate strategy { get; private set; }
        public Vector2f Position { get; set; }
        public float angle { get; set; }
        public float originAngle { get; set; }
        public float entityAngle { get; set; }
        public float depth { get; set; }

        // Constructors
        public ReySettings() { }

        public ReySettings(ReySettings settings)
        {
            Position = settings.Position;
            angle = settings.angle;
            entityAngle = settings.entityAngle;
            originAngle = settings.originAngle;
            depth = settings.depth;
        }

        // Set ray calculation strategy
        public ReySettings SetStrategy(IStrategyReyCanculate strategy)
        {
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
