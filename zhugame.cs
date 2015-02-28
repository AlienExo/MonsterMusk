using System;
using System.Windows.Media; //for color class


/*
IDEAS
	Attraction: For each body part, get difference between what you have and what they want. Define a threshold, modify based on their arousal (the higher their arousal, the less the differences matter). Under threshold - remaining difference becomes a malus. Over threshold - it's a bonus. In either case, value is multiplied their (dis)like of this body part. Attraction = Affection (raised through previous interaction) - (Sigma (UnderThreshold * Malus) + Sigma (OverThreshold * Bonus))
	
	If you wanna go crazy, then treat clothes as having restictions for certain sizes, eg. class Shirt : Clothing, Pants : Clothing, Underwear : Clothing
		bursting one layer bursts the one above too!
	
	Effects and iterative growth will allow interactive clothes bursting! :O
		
	Elasticity 0 - 999
		damage = bodyPartSize - 
	damage 0 - 100
	

	Equipment:
		Head
			Headwear
			Glasses
			Piercings:
				Nose (ring)
				Eyebrow (stud, twin studs)
				Ear (ring, stud)
				Lip (Ring)
				Under lip (stud, twin studs)
				Tongue (stud)
			
		Chest
			Nipple (twin rings, twin studs)
			Layer 1 - undershirt
			Layer 2 - shirt/vest
			Layer 3 - jacket
			Layer 4 - cloak/cape
			
		Hands
			
			
			
	do activities
		Scenes/ Events
	observe changes
		Core component: Text Engine that lovingly describes your newly-acquired ridiculous proportions
	???
	PROFIT

	Inventory
		Item 
		Quantity
		Effect

		
	temperature + hair + fat * metabolism = sweat
	
	transparency and layers WOULD allow you to, yanno, have the paperdolls at different sizes/stages of freakiness ,but the artists would still have to draw the parts
		and or animate them
		head would remain p. much the same size and type
			if aligned right - hairstyles? beards? eyecolor? skin color?
		FUND MY KICKSTARTER - 
			5000$ - full animations for Zhu
			10000$ Turtle King Bonus Character
	
/*

namespace MonsterEngine{

	public List<NPC> Actors;
	

	enum Muscles : byte {tongue, }
	
	/*
	tongue
	latissimus dorsi
	trapezius
	sphincter ani
	bulbospongiosus
	pectoralis major
	biceps brachii
	triceps brachii
	gluteus maximus
	hamstring
	*/

	/*	
	A) Class or struct body
		Pro: Fine control
		Con: Have to write it all myself even though it's pretty much just a List<Muscle>
		
	B) Iterage over enum or list, for each make a New Muscle and add to List<Muscle>
		Pro: Fast, east, modifiable
		Con: No fine control? Default values?
		
	todo: Constraints. Height/Weight ratio, ratio of X to Y. Long long list.
	*/
	
	public static string FuzzyColor(Color c){
		byte R, G, B
		R = c.R;
		G = c.G;
		B = c.B;
		
		
	}
	
	struct Musk{
		byte sex;		//increased by libido and having sex
		byte lazy;		//increased by being lazy, and generally over time
		byte workout;	//guess what increases this. Any type of hard work (sex too)
		
		
		
	}
	
	abstract static class Bodypart{
		string name;		//name for text
		string color;		//color for text output 
		//todo: consider saving color as a hex and doing that cartesian-plane 3D noisy color namer thingy. 
		//Like, the color is located on a three-axis plane, then all three axes get a little shake up, then the new poisition is located as to which point it is closest to and that color name is used.
		
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
		
	}
	
	class Penis : Bodypart{
		ushort girthH;
		ushort girthW;
	}
	
	class Muscle : Bodypart{
	
	}

	public interface ICharacter, ISerializable{
		public string name
		//public char[30] name
		public int gender
		/* 	Not intended to code for other than male. Appearance, maybe?
			0 - male
			1 - female [NOT IMPLEMENTED]
			2 - neutral
		*/
		public byte age 			//aging -> growth, hair going grey
		public string species
		
		public List<string> adjectives
		
		//core stats
		public byte strength		//physical power. Strength + indiv. muscle size (assumption: size = density here, lol as if) = strength for that particular bodypart. todo: randomly generate e.g. chicken leg enemies who can easily be leg-locked.
		public byte perception		//five senses. Susceptibility to musk, but also ability to gauge others and their reaction(s)
		public byte endurance		//how much HP you get, how much stamina you got. 
		public byte charisma		//social skills. high charisma gives you easier affection gain and opens up special dialogue options
		public byte intelligence
		public byte agility
		public byte luck
		
		public ushort maxHP
		public ushort HP
		public ushort maxStamina
		public ushort Stamina
		
		public ushort maxLust
		public ushort Lust
		
		public byte muscle 	//summation; need some form of object or struct for per-body-part values
		public byte fat 	//summation
		
		public byte metabolism
		
		
		//cock
		public byte cLength
		public byte cGirth
		
		//balls
		public byte bVolume
		public byte bProduction
		
		//pucker
		public ushort aCapacity
		public byte aProtrusion
		
		//nipples
		public byte nLength
		public byte nGirth
		public ushort nProduction
		
		//pecs
		public byte pVolume
		
		//jaw
		public byte lanternJaw

		public ushort sweat
		
		//personality
		public byte dominance{get; set;} //todo -100 - total sub. 0 - switch. 100 - total dom
		public byte openness{get; set;} //todo: affection threshold for talking about ~certain subjects~
		
		//RPG	
		public List<Item> inventory
		public ushort money
		
		//utility functions
		public static string describeFull(){
			
		}
		
		public static string describePart(Bodypart b){
			//todo: get b.name from this NPC's body object, describe
		}
		
		

	}
	
	public interface IContainer{
		int capacity
		bool private
		
		public static add(Item item){}
		public static take(Item item){}
		
		
	}

	public class Inventory : IContainer{
		//item, count. remove. drop on over X?
	}
	
	public class Event : ISerializable{
	
	}
	
	public class NPC : ICharacter{
		//Schedule
	
	}

	public class Protagonist : ICharacter{
		//always perfect estimation of own wants, size, stat
		Protagonist(){
			this.inventory = new Inventory()
		}

	}
	
	public abstract class Item : IComparable, ISerializable{
		public delegate Effect(Character target){}
		public string description
		public int price
		public int ID
		
	}
	
	public class Recipe : Item{
		public Item craft(){
			/*Rudimentary crafting:
			/	Combine two items
			/	Resulting item has a combination of both reagent's properties, some enhanced?
			/	Ponder meta-values that'll allow this sort of synthesis
			/		Body - Mind - Soul system?
			/		Muscle - Genitalia - Secondary Characteristics - Personality - Kinky Shit?
			*/
		}
	
	}
	
	public class Equipment : Item{
		/*
			Base lust growth of observers in area on tightness of clothing, their preferences, etc
			Apparel, Armor, Equipment - all the same
		*/
	}
	
	public class Food : Item{
	
	}
}

namespace Game{

	public static void Main(argv[]){
		Bowser = new Character()
	
	}

}