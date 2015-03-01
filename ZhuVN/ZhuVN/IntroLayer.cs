using System;
using Cocos2D;
using Microsoft.Xna.Framework;

namespace ZhuVN
{
	public class IntroLayer : CCLayerColor
	{
		public IntroLayer()
		{

			// create and initialize a Label
			var label = new CCLabelTTF("Soon to be a sweaty beast simulator...", "MarkerFelt", 22);

			// position the label on the center of the screen
			label.Position = CCDirector.SharedDirector.WinSize.Center;

			// add the label as a child to this Layer
			AddChild(label);

			NPC Bowser = new NPC();
			Bowser.name = "Bowser";
			Bowser.dominance = 255;
			Bowser.species = "Koopa";
			Console.WriteLine(Bowser.body.arms.Armpit.describeFull());
			Console.WriteLine(Bowser.body.crotch.pucker.describeFull());

			// setup our color for the background
			Color = new CCColor3B(Microsoft.Xna.Framework.Color.Black);
			Opacity = 200;

		}

		public static CCScene Scene
		{
			get
			{
				// 'scene' is an autorelease object.
				var scene = new CCScene();

				// 'layer' is an autorelease object.
				var layer = new IntroLayer();

				// add layer as a child to scene
				scene.AddChild(layer);

				// return the scene
				return scene;

			}

		}

	}
}

