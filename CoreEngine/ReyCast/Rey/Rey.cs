using CoreEngine.ReyCast;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreEngine.ReyCast
{
    // TDO Rey, reyservice into (controller, view)
    class Rey : ReyContainer
    {

        private List<Hit> HitPoints { get; set; }

        public Rey()
        {
            HitPoints = new List<Hit>();
        }

        // Combines the hits from two different rays and sorts them based on their distances.
        public static Rey HitDistribution(Rey reyHorizontal, Rey reyVertecal, ReySettings settings)
        {
            Rey resultRey = new Rey();

            int indexHorizontal = 0;
            int indexVertical = 0;

            int n = reyHorizontal.HitPoints.Count;
            int m = reyVertecal.HitPoints.Count;

            while ((indexHorizontal < n) || (indexVertical < m))
            {
                if ((indexHorizontal < n) && (indexVertical < m))
                {
                    if (reyHorizontal.HitPoints[indexHorizontal].ReyDistance < reyVertecal.HitPoints[indexVertical].ReyDistance)
                        resultRey.HitPoints.Add(reyHorizontal.HitPoints[indexHorizontal++]);
                    else
                        resultRey.HitPoints.Add(reyVertecal.HitPoints[indexVertical++]);
                }
                else
                {
                    if (indexHorizontal < n)
                        resultRey.HitPoints.Add(reyHorizontal.HitPoints[indexHorizontal++]);
                    else
                        resultRey.HitPoints.Add(reyVertecal.HitPoints[indexVertical++]);
                }
            }

            float angle = settings.angle - settings.entityAngle;
            foreach (var hit in resultRey.HitPoints)
                hit.ReyDistance *= MathF.Cos((angle * MathF.PI) / 180);

            resultRey.HitPoints.Remove(resultRey.HitPoints.Last());
            resultRey.HitPoints.Reverse();
            return resultRey;
        }

        // Adds a hit to the list of hit points.
        public void Hit(Hit hit) =>
            HitPoints.Add(hit);

        // Returns the last hit of the specified type.
        public Hit GetLastHit(Hit type) =>
             (HitPoints.FindAll(hit => hit.GetType() == type.GetType()).Last());

        // Returns the first hit of the specified type.
        public Hit GetFirstHit(Hit type) =>
             (HitPoints.FindAll(hit => hit.GetType() == type.GetType()).First());

        // Returns all wall hits.
        public IEnumerable<Hit> GetWallHit() =>
             HitPoints.Where(hit => hit.GetType() == new HitWall().GetType());

        // Returns all floor hits.
        public IEnumerable<Hit> GetFloreHit() =>
             HitPoints.Where(hit => hit.GetType() == new HitFlore().GetType());

    }
}
