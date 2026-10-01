using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

			var thanksForPlayingText = new GameObject();

			thanksForPlayingText.Body = new Rectangle(0, 0, 200, 80);
			thanksForPlayingText.Position = Scene.Sizes.ToVector2().Half() - Vector2.UnitY * (((float)thanksForPlayingText.Body.Height).Half() + 60);
			thanksForPlayingText.Position = new Vector2(thanksForPlayingText.Position.X - ((float)thanksForPlayingText.Body.Width).Half(), thanksForPlayingText.Position.Y);
			thanksForPlayingText.tag = nameof(thanksForPlayingText);
			Scene.AddGameObject(thanksForPlayingText, Layers.UI);
			var text = thanksForPlayingText.AddComponent<UITextComponent>();
			text.SetFont(Content.Load<SpriteFont>("Fonts/FontUIText"));
			text.SetText("thanks for playing");
			text.SetFontSize(0.3f);
			text.SetTextFormt(UITextComponent.TextFormat.CENTER, UITextComponent.TextAlignment.MIDDLE);


			var gameBy = new GameObject();

			gameBy.Body = new Rectangle(0, 0, 200, 80);
			gameBy.Position = Scene.Sizes.ToVector2().Half() - Vector2.UnitY * (((float)gameBy.Body.Height).Half() + 20);
			gameBy.Position = new Vector2(gameBy.Position.X - ((float)gameBy.Body.Width).Half(), gameBy.Position.Y);
			gameBy.tag = nameof(gameBy);
			Scene.AddGameObject(gameBy, Layers.UI);
			text = gameBy.AddComponent<UITextComponent>();
			text.SetFont(Content.Load<SpriteFont>("Fonts/FontUIText"));
			text.SetText("A Game By Elton Silva");
			text.SetFontSize(0.2f);
			text.SetTextFormt(UITextComponent.TextFormat.CENTER, UITextComponent.TextAlignment.MIDDLE);



		}
	}
}
