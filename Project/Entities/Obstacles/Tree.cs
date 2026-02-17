using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities.Obstacles
{
    public class Tree : ObstacleGameObject
    {
        public override void Start()
        {
            SpriteColor = Color.Green;
            base.Start();
        }
    }
}
