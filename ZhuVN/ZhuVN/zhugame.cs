using System;						//
using System.Collections.Generic;	//
using System.Runtime.Serialization;	//
using System.Drawing;				//for Color class
using System.Xml.Serialization;


/* IDEAS
 * Attraction: For each body part, get difference between what you have and what they want. Define a threshold, modify based on their arousal (the higher their arousal, the less the differences matter). Under threshold - remaining difference becomes a malus. Over threshold - it's a bonus. In either case, value is multiplied their (dis)like of this body part. Attraction = Affection (raised through previous interaction) - (Sigma (UnderThreshold * Malus) + Sigma (OverThreshold * Bonus))
 *	
 * If you wanna go crazy, then treat clothes as having restictions for certain sizes, eg. class Shirt : Clothing, Pants : Clothing, Underwear : Clothing
 *	bursting one layer bursts the one above too!
 *	
 * Effects and iterative growth will allow interactive clothes bursting! :O
 * 
 * Wearing or sniffing clothes from someone will slowly change you into their size/species.
 *	Sniffing quicker than wearing?
 *	
 * Elasticity 0 - 999
 *	damage = bodyPartSize - 
 *	damage 0 - 100
 *	
 * Equipment:
 *	Head
 *		Headwear
 *			Glasses
 * 			Piercings:
 * 				Nose (ring)
 * 				Eyebrow (stud, twin studs)
 * 				Ear (ring, stud)
 * 				Lip (Ring)
 * 				Under lip (stud, twin studs)
 * 				Tongue (stud)
 * 			
 * 		Chest
 * 			Nipple (twin rings, twin studs)
 * 			Layer 1 - undershirt
 * 			Layer 2 - shirt/vest
 * 			Layer 3 - jacket
 * 			Layer 4 - cloak/cape
 * 			
 * 		Hands
 *  			
 * 	do activities
 * 		Scenes/ Events
 * 	observe changes
 * 		Core component: Text Engine that lovingly describes your newly-acquired ridiculous proportions
 * 	???
 * 	PROFIT
 * 
 * 	Inventory
 * 		Item 
 * 		Quantity
 * 		Effect
 * 		
 * 	temperature + hair + fat * metabolism = sweat
 * 	
 * 	transparency and layers WOULD allow you to, yanno, have the paperdolls at different sizes/stages of freakiness ,but the artists would still have to draw the parts
 * 		and or animate them
 * 		head would remain p. much the same size and type
 * 			if aligned right - hairstyles? beards? eyecolor? skin color?
 * 		LOL FUND MY KICKSTARTER
 * 			5000$ - full animations for Zhu
 * 			10000$ Turtle King Bonus Character (totally not bowser)	
 * 			 *
 * 		tongue
 * 		latissimus dorsi
 * 		trapezius
 * 		sphincter ani
 * 		bulbospongiosus
 * 		pectoralis major
 * 		biceps brachii
 * 		triceps brachii
 * 		gluteus maximus
 * 		hamstring
 * 
 * 		A) Class or struct body
 * 			Pro: Fine control
 * 			Con: Have to write it all myself even though it's pretty much just a List<Muscle>
 * 		
 * 		B) Iterage over enum or list, for each make a New Muscle and add to List<Muscle>
 * 			Pro: Fast, east, modifiable
 * 			Con: No fine control? Default values?
 * 		
 * 		todo: Constraints. Height/Weight ratio, ratio of X to Y. Long long list.
 */

namespace MonsterEngine{

	class Game{

		struct Constraints
		{
			//Optional - constraint certain attributes or sized to be no bigger than X. Game modes are Big, Hyper, Stagor, Zhu, representing increasing maxima.
			byte cLength;
			byte cGirth;
			//todo: write up
		}
		public List<NPC> Actors;
	}
	//enum Muscles : byte {tongue}
	
	public class ZhuEngine{
		public static void FuzzyColor(Color c){
			byte R = c.R;
			byte G = c.G;
			byte B = c.B;
		//Todo: Fuzzy values, pick new by least-distance
		}
	}

	public interface IContainer
	{
		int capacity;
		bool IsPrivate;

		public static void add(Item item) { }
		public static void take(Item item) { }
	}

	public class Inventory : IContainer
	{
		//item, count. remove. drop on over X?
		//Carrying capacity?
	}

	struct Musk{
		//Yeah good luck describing smell in words, let alone numbers.
		byte sex;		//increased by libido and having sex
		byte lazy;		//increased by being lazy, and generally over time
		byte workout;	//guess what increases this. Any type of hard work (sex too)		
	}

	abstract static class Bodypart{
		string name;		//name for text
		string color;		//color for text output 
		
		//string adjective;	//name for text
		ushort ID;			//short ID for ref
		
		ushort sizeH; 		//size have
		ushort sizeW; 		//size want
		ushort hairH; 		//hair have
		ushort hairW; 		//hair want
		ushort attraction;	//importance of this body part to self
		ushort musk;		//potency of musk in this area - 
	}
	
	class Prostate : Bodypart{
		ushort LocationX;	//find the prostate minigame. because why the fuck not.
		ushort LocationY;
	}
	
	class Testicle : Bodypart{
		ushort production;	//liters per day

		public ushort calcProduction(Character c, int timeDelta){
			throw new NotImplementedException("Cum production not yet implemented.");
		}
	}
	
	class Penis : Bodypart{
		ushort girthH;
		ushort girthW;
	}
	
	class Muscle : Bodypart{
		
	}

	/// <summary>
	/// The base for all NPC and Protagonist stats, methods and other things.
	/// </summary>
	abstract class Character{
		internal string name;
		//internal char[30] name
		internal int gender;
		/* 	Not intended to code for other than male. Appearance, maybe?
			0 - male
			1 - female [NOT IMPLEMENTED]
			2 - neutral
		*/
		internal byte age; 			//aging -> growth, hair going grey
		internal string species;
		
		internal List<string> adjectives;
		
		//core stats
		internal byte strength;		//physical power. Strength + indiv. muscle size (assumption: size = density here, lol as if) = strength for that particular bodypart. todo: randomly generate e.g. chicken leg enemies who can easily be leg-locked.
		internal byte perception;		//five senses. Susceptibility to musk, but also ability to gauge others and their reaction(s)
		internal byte endurance;		//how much HP you get, how much stamina you got. 
		internal byte charisma;		//social skills. high charisma gives you easier affection gain and opens up special dialogue options
		internal byte intelligence;
		internal byte agility;
		internal byte luck;
		
		internal ushort maxHP;
		internal ushort HP;
		internal ushort maxStamina;
		internal ushort Stamina;
		
		internal ushort maxLust;
		internal ushort Lust;
		
		internal byte muscle; 	//summation; need some form of object or struct for per-body-part values
		internal byte fat;	 	//summation
		
		internal byte metabolism;
		
		
		//cock
		internal byte cLength;
		internal byte cGirth;
		
		//balls
		internal byte bVolume;
		internal byte bProduction;
		
		//pucker
		internal ushort aCapacity;
		internal byte aProtrusion;
		internal byte aMoisture;
		
		//nipples
		internal byte nLength;
		internal byte nGirth;
		internal ushort nProduction;
		
		//pecs
		internal byte pVolume;
		
		//jaw
		internal byte lanternJaw;

		internal ushort sweat;
		Musk musk;
		
		//personality
		internal byte dominance{get; set;} //todo -100 - total sub. 0 - switch. 100 - total dom
		internal byte openness{get; set;} //todo: affection threshold for talking about ~certain subjects~
		
		//RPG	
		internal Inventory inventory;
		internal ushort money;
		
		//utility functions
		internal virtual static string describeFull(){
			throw new NotImplementedException();
		}
		
		internal virtual static string describePart(Bodypart b){
			//todo: get b.name from this NPC's body object, describe
			throw new NotImplementedException();
		}

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context);

		public Character(SerializationInfo info, StreamingContext context);

		internal Character(){
			//DEBUG CONSTRUTOR
			this.gender = 0;
			this.name = "Zhu";
			this.species = "Beast";
			this.sweat = 255;
		}
	}
	
	public class Event{
	
	}
	
	public class NPC : Character{
		//Schedule
	
	}

	public class Protagonist : Character{
		//Different from NPS in that you always have a perfect estimation of own wants, size, stat
		//Also, yanno, inventory and stuff
		Protagonist() : base(){					//runs base constructor before these additional events:
			this.inventory = new Inventory();
		}

		//// Implement this method to serialize data. The method is called 
		//// on serialization.
		//public void GetObjectData(SerializationInfo info, StreamingContext context)
		//{
		//// Use the AddValue method to specify serialized values.
		//info.AddValue("props", , typeof(string));

		//}

		//// The special constructor is used to deserialize values.
		//public Protagonist(SerializationInfo info, StreamingContext context)
		//{
		//// Reset the property value using the GetValue method.
		// = (string) info.GetValue("props", typeof(string));
		//}
	}

	public abstract class Item : IComparable, ISerializable{
		public delegate void invokeEffect(Character target);	//bind the actual effect code here
		public string description;				
							
		public string describe(){
			throw new NotImplementedException();
		}								
		public int price;										
		int ID;													//ID Number
		//int potency; //todo: potency system for synthesis OR boil down two items into one with stronger effect
		
	}
	
	public class Recipe : Item{
		public Item craft(){
			/*Rudimentary crafting:
			/	Combine two items
			/	Resulting item has a combination of both reagent's properties, some enhanced?
			/	Ponder meta-values that'll allow this sort of synthesis
			/		Body - Mind - Soul system?
			/		Muscle - Genitalia - Secondary Characteristics - Personality - Kinky Stuff?
			*/
			throw new NotImplementedException();
		}
	}
	
	public class Equipment : Item{
		/*
			Base lust growth of observers in area on tightness of clothing, their preferences, etc
			Apparel, Armor, Equipment - all the same
		*/
		byte equipSlot;		//depending on the slot it binds to, the item will have to watch for different changes e.g. chest items have to watch out for chest and pecs
		byte elasticity;	
		byte price;			
		Color color;		
		byte ID;			
		string name;		
		string description;	
		Musk musk;			//
		bool infectSpecies;	//Will wearing/sniffing this item transform your species?
		bool infectSize;	//Will wearing/sniffing this item make you grow?
		delegate void infectOther(); //binding for other effects from this piece of clothing

		public string describe(){
			throw new NotImplementedException();
		}

		public string sniff(){
			throw new NotImplementedException();
		}
	}
	
	public class Food : Item{
	
	}
}