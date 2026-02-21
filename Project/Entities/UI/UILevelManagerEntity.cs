using System;
using System.Collections.Generic;
using System.Text;
using UmbrellaToolsKit;

namespace Project.Entities.UI
{
    public class UILevelManagerEntity : GameObject
    {
        public override void Start()
        {
            tag = nameof(UILevelManagerEntity);
        }
    }
}
