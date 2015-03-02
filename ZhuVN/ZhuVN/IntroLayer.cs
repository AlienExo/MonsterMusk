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
			var label = new CCLabelTTF("You feel yourself grow...", "MarkerFelt", 22);

			// position the label on the center of the screen
			label.Position = CCDirector.SharedDirector.WinSize.Center;

			// add the label as a child to this Layer
			AddChild(label);

			NPC Bowser = new NPC();
			Bowser.name = "Bowser";
			Bowser.dominance = 255;
			Bowser.species = "Koopa";

			NPC Exo = new NPC();
			Exo.name = "Exo";
			Exo.species = "Tetramand";
			Exo.STR = 75;
			Exo.INT = 60;
			Exo.CHA = 40;
			Exo.PER = 90;
			Exo.dominance = 50;
			Exo.body.chest.Pecs.musk.workout = 100;
			NPC Zhu = new NPC();
			Zhu.name = "Zhu";
			Zhu.species = "Beast";

			Musk e = Exo.body.crotch.dicks[0].musk;
			e.origin = Exo.name;
			Bowser.body.crotch.pucker.visitors.Add(e);
			e = Zhu.body.crotch.dicks[0].musk;
			e.origin = Zhu.name;
			Bowser.body.crotch.pucker.visitors.Add(e);
			Console.WriteLine(Bowser.describePart(Bowser.body.arms.Armpit));
			Console.WriteLine(Bowser.describePart(Bowser.body.crotch.pucker));

			CCLabelTTF label2 = new CCLabelTTF(Bowser.describePart(Bowser.body.arms.Armpit), "MarkerFelt", 22);
			label2.Position = new CCPoint(CCDirector.SharedDirector.WinSizeInPixels.Height/2 + 100, CCDirector.SharedDirector.WinSizeInPixels.Width/2 + 100);
			AddChild(label2);

			CCLabelTTF label3 = new CCLabelTTF(Bowser.describePart(Bowser.body.crotch.pucker), "MarkerFelt", 22);
			label3.Position = new CCPoint(CCDirector.SharedDirector.WinSizeInPixels.Height/2 + 100, CCDirector.SharedDirector.WinSizeInPixels.Width/2 - 50);
			AddChild(label3);

			CCLabelTTF label4 = new CCLabelTTF(Bowser.describePart(Bowser.body.crotch.prostate), "MarkerFelt", 22);
			label4.Position = new CCPoint(CCDirector.SharedDirector.WinSizeInPixels.Height / 2 + 100, CCDirector.SharedDirector.WinSizeInPixels.Width / 2 - 200);
			AddChild(label4);

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

