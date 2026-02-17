using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project.Entities.Obstacles
{
    public class Barrel : ObstacleGameObject
    {
        public override void Start()
        {
            SpriteColor = Color.Brown;
            base.Start();
        }
    }
}
