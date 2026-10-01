using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;

namespace Project.Entities
{
	public class FinalCredits : GameObject
	{
		private GameObject _background;
		private Color _backgroundColor = (new Vector3(64f, 73f, 115f)).ToColor();

		public override void Start()
		{
			_background = new GameObject();
			Scene.AddGameObject(_background, Layers.UI);
			_background.AddComponent<UIBackgroundSpriteComponent>().SetSprite(SquareSprite.SquareTexture, _backgroundColor);
		}
	}
}
