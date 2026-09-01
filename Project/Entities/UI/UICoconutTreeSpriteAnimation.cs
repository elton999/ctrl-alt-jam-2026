using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine.GameSettings;
using Microsoft.Xna.Framework;

namespace Project.Entities.UI
{
	public class UICoconutTreeSpriteAnimation : GameObject
	{
		[ShowEditor] private Sprite _sprite;

		public override void Start()
		{
			Tag = nameof(UICoconutTreeSpriteAnimation);
			var atlas = GameSettingsProperty.GetProperty<AtlasGameSettings>(@"Content/AtlasGameSettings");
			if (atlas.TryGetSpriteByName("coqueiro", out var spriteAtlas))
			{
				_sprite = new Sprite(spriteAtlas.Name, spriteAtlas.Path, spriteAtlas.GetRectangle());
				_sprite.SetContentManager(Scene.Content);
				Sprite = _sprite.Texture;
				Body = _sprite.Body;

				Origin = new Vector2(((float)Body.Width).Half(), Body.Height);
				return;
			}
		}
	}
}
