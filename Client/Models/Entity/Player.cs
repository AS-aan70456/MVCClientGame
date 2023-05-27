﻿using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Player : Entity{

        public float angleY { get; protected set; }

        public Player(Level level) : base(level){
            Position = (Vector2f)level.SpawnPoint;
            Size = new Vector2f(0.7f, 0.7f);
        }

        public void RotateY(float angle){
            this.angleY += angle;
        }
    }
}
