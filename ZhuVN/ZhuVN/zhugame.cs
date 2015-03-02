using System;						//
using System.Collections.Generic;	//
using System.Runtime.Serialization;	//
using Microsoft.Xna.Framework;		//for Color class
using System.Xml.Serialization;



/* IDEAS
 * Attraction: For each body part, get difference between what you have and what they want. Define a threshold, modify based on their arousal (the higher their arousal, the less the differences matter). Under threshold - remaining difference becomes a malus.
 * Over threshold - it's a bonus. In either case, value is multiplied their (dis)like of this body part. Attraction = Affection (raised through previous interaction) - (Sigma (UnderThreshold * Malus) + Sigma (OverThreshold * Bonus))
 *	
 * Effects and iterative growth will allow interactive clothes bursting! :O
 * 	If you wanna go crazy, then treat clothes as having restictions for certain sizes, eg. class Shirt : Clothing, Pants : Clothing, Underwear : Clothing
 *		bursting one layer bursts the one above too!
 * 			Clothes have an elasticity from 0 - 999
 *				damage = (new Body Size - size the clothing supports) / elasticity 
 *					damage		message
 *					0			[none]
 *					10			small tears/rips
 *					25			large rips
 *					50			torn apart
 *					75			shreds clinging to you
 *					90			
 *					>95			[remove item]
 *					
 * Wearing or sniffing clothes from someone will slowly change you into their size/species. (If that piece is flagged as infectious)
 *	Sniffing quicker than wearing?
 *	Transformation goes outwards from where the article of clothing is, e.g. a Zhu wristband will give you hands, then arms, then pecs, then the rest.
 *		Need a body map and pathfinding for that tho
 *	
 * When talking to characters about size, they look at their body and talk about a muscle whose size they want isn't like the size they have. They may also admire you if you have that size
 *	Quest: Bring X an item that'll grow their Y to the size they want
 *		Questgiver will look over item properties, simulate taking the item, compare. If it works, they'll actually take it
 *			And you get a nice growth scene.
 * 
 * 
 * EVENTS
 *	Steal jockstraps/singlets/shirts/wristbands from the locker room
 *	Beast competitions - average sum squares of distance from own muscles/hair/musk to average, and competitors
 *		Get given three people, see how many turn exposed to your musk
 *	Wrestling matches.
 *
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
 *			Cigar
 *
 *		Neck
 *			Scarf
 *			Necklace/Choker/Collar
 * 			
 * 		Chest
 * 			Nipple (twin rings, twin studs)
 * 			Layer 1 - undershirt
 * 			Layer 2 - shirt/vest
 * 			Layer 3 - jacket
 * 			Layer 4 - cloak/cape
 * 			
 * 		Hands
 *			Wristband(s)
 *			Glove(s)
 *			Ring(s)
 *  			
 * 	sensitivity stat for each body part. find the sweet spots!
 *
 *
 *	----
 *	INTERFACE
 *	
 *  todo: statScale - supply list of arguments, divide range of number by length of list, return item in list closest to that.
 *		e.g. musk goes up to 255, supply "none", "moist", "wet," "dripping", "reeking". 255 would be reeking, 0 would be none.
 * 
 * 
 * 	transparency and layers WOULD allow you to, yanno, have the paperdolls at different sizes/stages of freakiness ,but the artists would still have to draw the parts
 * 		and or animate them
 * 		head would remain p. much the same size and type
 * 			if aligned right - hairstyles? beards? eyecolor? skin color?
 *	assume same expression as last dialogue unless indicated otherwise
 *		speak(character, string text, [expression])
 *		endDialogue resets? 
 * 
 *	<joke>
 * 		LOL FUND MY KICKSTARTER
 * 			5000$ - full animations for Zhu
 * 			7500$ - we get genomo
 * 			10000$ Turtle King Bonus Character (totally not bowser)	
 * 	</joke>
 * 
 *	Main control scheme: Mouse. 
 *		iPod-ish: A list of choices is on the screen and using the scroll wheel either scrolls them horizontally or vertically. 
 *		Old ones drop off the end of the list and new ones come in from the botton - FIFO-like queue on the screen, plain old list in the code
 *		For those poor unfortunate souls with no scroll wheel in TYOOL 2015, add Left Arrow and A As Left, Right Arrow and D as Right, Up/W as Confirm, and Down/S as Deny.
 *		Left button Yes/Forward/Positive reply
 *			Whatever function returns the answer to a question (as an int, I presume) has to:
 *				Show the question text
 *				Generate a list of answers or series of buttons, show them on the UI
 *				Trap the mouse cursor/scroll action to them
 *				Have a defaultYes and optional defaultNo to map RightMouse and LeftMouse to
 *
 *	---- 
 *	OTHER
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
 * 	do activities
 * 		Scenes/ Events
 * 	observe changes
 * 		Core component: Text Engine that lovingly describes your newly-acquired ridiculous proportions and the transformations that lead there
 * 	???
 * 	PROFIT
 * 
 * 	Inventory
 * 		Item 
 * 		Quantity
 * 		Effect
 * 		
 * 	temperature + hair + fat * metabolism = sweat
 */
 
 /*
	A note on maximum size:
	byte		255
	ushort		
	short		
 
	Attempting to insert a number larger than that will cause a compilation error to occur. 
 */

namespace ZhuVN{

	class Core{

		struct Constraints
		{
			//Optional - constraint certain attributes or sized to be no bigger than X. Game modes are Big, Hyper, Stagor, Zhu, representing increasing maxima.
			byte cLength;
			byte cGirth;
			//todo: write up
			
		}
		public List<NPC> Actors;
		internal int NPCs = 1;
		internal int Items = 1;
		internal int Locations = 1;
	}
	
	public class ZhuEngine{
		public static void FuzzyColor(Color c){
			byte R = c.R;
			byte G = c.G;
			byte B = c.B;
		//Todo: Fuzzy values, pick new by least-distance
		}
	}

	public class Container
	{
		int capacity;
		bool IsPrivate;

		void add(Item item) { }
		void take(Item item) { }
	}

	public class Inventory : Container
	{
		//item, count. remove. drop on over X?
		//Carrying capacity?
	}

	internal class Musk{
		//Yeah good luck describing smell in words, let alone numbers.
		internal byte sex = 10;		//increased by libido and having sex
		internal byte lazy = 0;		//increased by being lazy, and generally over time
		internal byte workout = 10;	//guess what increases this. Any type of hard work (sex too)	
		internal ushort strength = 100;//potency of musk	
		internal string origin;	//whose is this

		internal Musk(){}
		internal Musk(Character c){
			this.origin = c.name;
		}

		public override string ToString()
		{
			return "sopping";
			//return base.ToString();
		}
	}
	
	/// <summary>
	/// Implements bodies for all Characters (NPC, Player) and perhaps monsters/enemies.
	/// </summary>
	internal class Body{
		//todo: consider subclassing for head, torso, arms, legs, crotch                                                      

		internal class Bodypart{
			internal string name;			//name for text
			internal string desc;			//special description, always added? todo: reference to a list/library of string/string formats so custom descriptions can be added.
			internal string color;			//color for text output 
			
			//string adjective;		//name for text
			ushort PartID;			//short ID for ref
			ushort group;			//Location on the body. Allows grouping for growth, clothes bursting, etc.

			internal ushort sizeH			//size you have
				{	get{return (ushort)((this.girth + this.length)/2);}

					set{ushort delta = (ushort)(value - this.sizeH); 
						this.girth = (ushort)(this.girth + delta/2);
						this.length = (ushort)(this.length + delta/2);}
				}
			internal ushort sizeW; 			//size you want

			internal ushort length;			//Don't have to use. Just use generic sizeH to be neutral - this is for things where girth counts.
			internal ushort girth;

			internal ushort hairH; 			//hair have
			internal ushort hairW; 			//hair want

			internal ushort attraction;		//importance of this body part to self

			internal Musk musk = new Musk();	//type of musk in this area

			//utility functions

			internal Bodypart(string _name, int _ID){
				//todo - constructor w/ limits based on id, also name
			}

			internal Bodypart(string _name){
				this.name = _name;
			}

			internal virtual string describeFull(Character owner)
			{
				return String.Format("You longingly gaze at {0}'s musky, muscular, {1}.\n", owner.name, this.name);
				//todo: Long, loving description of all features.
				//throw new NotImplementedException();
			}

			internal virtual string describePart(Character owner)
			{
				return String.Format("You longingly gaze at {0}'s {1}.\n", owner.name, this.name);
				//todo: smaller, shorter, more compact description - one line. From a collection so they vary.
				//throw new NotImplementedException();
			}
		}

		internal class Muscle : Bodypart{
			ushort density;		//is it just big or is it also solid?
			internal Muscle(string name) : base(name){}
		}

		internal class Prostate : Bodypart
		{
			ushort LocationX;	//find the prostate minigame. because why the fuck not.
			ushort LocationY;
			ushort Refractory;	//This isn't your fanfics. Prostates need to reload 'tween uses.

			internal Prostate(string name) : base(name){}

			internal override string describePart(Character owner)
			{
				return String.Format("From what you can feel, {0}'s prostate is a {1} lump, \nlocated about {2} up and {3} left inside {4}'s {5} {6}.\n", owner.name, "fat", owner.body.crotch.prostate.LocationX, owner.body.crotch.prostate.LocationY, owner.name, owner.body.crotch.pucker.musk.ToString(), owner.body.crotch.pucker.name);
				//return base.describePart(charname);
			}
		}

		internal class Testicles : Bodypart
		{
			ushort production;	//liters per day
			ushort stored;		//amount stored. Todo: If this exceeds testicle size, adjust size to match and add effect "Pent up" - stronger libido increase
			ushort nTesticles;	//For you Krogans and Exos out there

			public ushort calcProduction(Character c, int timeDelta)
			{
				throw new NotImplementedException("Cum production not yet implemented.\n");
				//timeDelta * production / 24
			}
			internal Testicles(string name) : base(name){}
		}

		internal class Penis : Bodypart
		{
			ushort girthH;
			ushort girthW;
			string shape;	//siiigh
			internal Penis(string name) : base(name){}
		}

		internal class Pucker : Bodypart
		{
			internal ushort capacity;
			internal byte protrusion;
			internal byte moisture;
			internal byte gape;
			internal List<Musk> visitors = new List<Musk>();
			internal Pucker(string name) : base(name){}

			internal override string describePart(Character owner)
			{
				string result = base.describePart(owner);
				//todo: Add Perception code.
				if (this.visitors.Count>0){
					for (int i=0; i<visitors.Count; i++){
						result += String.Format(" You can smell {0}'s musk pour out of {1}'s {2}.\n", this.visitors[i].origin, owner.name, this.name);
					}
				}
				return result;
			}
		}

		internal class Nipples : Bodypart
		{
			internal ushort stored;
			internal ushort nProduction;
			internal Nipples(string name) : base(name){}
		}

		internal class Head {
			internal Color haircolor = new Color();
			internal string hairtexture;
			internal string hairstyle;
			internal string eyebrows;
			internal Color eyecolor = new Color();
			internal string pupils;
			internal string nose;		//
			internal Bodypart tongue = new Bodypart("tongue");
			internal ushort teeth;		//e.g. 0 - blunt, 1 - tusks, 2 - sharp
			internal string beard;		//easier to use a string than a list and enum styles
			internal Bodypart jaw = new Bodypart("jaw");
			internal Bodypart lowerLip = new Bodypart("lower lip");
			internal Bodypart upperLip = new Bodypart("upper lip");
		}

		internal class Chest {
			internal Muscle Pecs = new Muscle("pecs");

		}

		internal class Arms{
			internal Muscle Biceps = new Muscle("biceps");
			internal Muscle Triceps = new Muscle("triceps");
			internal Bodypart Armpit = new Bodypart("sweaty pit");
		}

		internal class Legs{
			internal List<Bodypart> legs;
		}

		internal class Crotch
		{
			internal List<Penis> dicks = new List<Penis>();
			internal List<Testicles> balls = new List<Testicles>();
			internal Prostate prostate = new Prostate("throbbing");
			internal Pucker pucker = new Pucker("puffy, swollen black pucker");
			internal Muscle glutes = new Muscle("meaty arse");

			internal Crotch(){
				dicks.Add(new Penis("turgid fucker"));
				balls.Add(new Testicles("musk-dripping cum factories"));
			}
		}

		internal Musk musk = new Musk();

		private float _height;
		internal float height{
			get{
				if (true) { return _height; } //todo: String.Format("{0:0.0}",  code: game.Settings.Metric

				else{
					float d, m;
					d = (_height/0.3048f); //0.3048 metre = 1 foot
					m = (_height%0.3048f);
					m = (m/2.54f);
					return d + m/10;
				}	
			}
			set{}
		} 
		private float _weight;
		internal float weight{
			get{
				if (true) { return _weight; } //game.Settings.Metric
				else{return _weight * 2.2046f;} //1 kg = 2.2046lbs
			}
			set{
			
			}
		}

		internal Head head;
		internal Chest chest;
		internal Arms arms;
		internal Crotch crotch;
		internal Legs legs;

		internal string describePart(Character c, Bodypart b)
		{
			return b.describePart(c);
		}

		internal string describeFull(Character c, Bodypart b)
		{
			//todo: randomly-generated description of this Body
			//todo: Accuracy of measurements depends on your perception - the less it is, the bigger the wobble
			//todo: Pick a few parts to be described.
			return b.describeFull(c);
		}


		internal Body(){
			//todo: Constructor w/ constraints
			this.head = new Head();
			this.chest = new Chest();
			this.arms = new Arms();
			this.crotch = new Crotch();
			this.legs = new Legs();
		}
	}

	/// <summary>
	/// The abstract base class for all NPC and Protagonist stats, shared methods, and other things.
	/// </summary>
	internal abstract class Character{
		internal string name;
		//internal char[30] name
		internal int gender;
		/* 	Not intended to code for other than male. Appearance, maybe?
			0 - male
			1 - female [NOT IMPLEMENTED]
			2 - neutral
		*/
		internal byte age = 21; 			//aging -> growth, hair going grey
		internal string species;
		
		internal List<string> adjectives = new List<string>();
		
		//core stats
		private byte _STR;		//physical power. Strength + indiv. muscle size (assumption: size = density here, lol as if) = strength for that particular bodypart. todo: randomly generate e.g. chicken leg enemies who can easily be leg-locked.
		public byte STRBonus;
		public byte STR{
			get{
				return (byte)(_STR + STRBonus);
			}
			
			set{
				if ((_STR + value)>100){_STR = 100;}
				else{_STR = (byte)(_STR + value);}
			}
		}
		
		private byte _PER;		//five senses. Susceptibility to musk, but also ability to gauge others and their reaction(s)
		public byte PERBonus;
		public byte PER{
			get{
				return (byte)(_STR + STRBonus);
			}
			
			set{
				if ((_STR + value)>100){_STR = 100;}
				else{_STR = (byte)(_STR + value);}
			}
		}
		
		private byte _END;		//how much HP you get, how much stamina you got. 
		public byte ENDBonus;
		public byte END{
			get{
				return (byte)(_STR + STRBonus);
			}
			
			set{
				if ((_STR + value)>100){_STR = 100;}
				else{_STR = (byte)(_STR + value);}
			}
		}
		
		private byte _CHR;		//social skills. high charisma gives you easier affection gain and opens up special dialogue options
		public byte CHABonus;
		public byte CHA{
			get{
				return (byte)(_STR + STRBonus);
			}
			
			set{
				if ((_STR + value)>100){_STR = 100;}
				else{_STR = (byte)(_STR + value);}
			}
		}
		
		private byte _INT;
		public byte INTBonus;
		public byte INT{
			get{
				return (byte)(_STR + STRBonus);
			}
			
			set{
				if ((_STR + value)>100){_STR = 100;}
				else{_STR = (byte)(_STR + value);}
			}
		}
		
		private byte _AGI;
		public byte AGIBonus;
		public byte AGI{
			get{
				return (byte)(_STR + STRBonus);
			}
			
			set{
				if ((_STR + value)>100){_STR = 100;}
				else{_STR = (byte)(_STR+value);}
			}
		}
		
		private byte _LCK;
		public byte LCKBonus;
		public byte LCK{
			get{
				return (byte)(_STR + STRBonus);
			}
			
			set{
				if ((_STR + value)>100){_STR = 100;}
				else{_STR = (byte)(_STR + value);}
			}
		}
		
		private ushort maxHP;
		private ushort HP;
		private ushort maxStamina;
		private ushort Stamina;
		
		private ushort maxLust;
		private ushort Lust;
		
		public Body body = new Body();	//That body is public, hurr hurr hurr. (I'm so sorry).

		internal byte muscle; 	//summation; need some form of object or struct for per-body-part values
		internal byte fat;	 	//summation

		internal byte metabolism;
		
		//personality
		internal byte dominance{get; set;} //todo -100 - total sub. 0 - switch. 100 - total dom
		internal byte openness{get; set;} //todo: affection threshold for talking about ~certain subjects~
		
		//RPG	
		internal Inventory inventory;
		internal ushort money;
	
		//Serialization and exporting. Must be set by whoever implements Character.
		public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
		public Character(SerializationInfo info, StreamingContext context){
			throw new NotImplementedException("Character deserialization not implemented. Todo: Make this");
		}

		internal string describePart(Body.Bodypart b)
		{
			return this.body.describePart(this, b);
		}

		internal string describe(Body.Bodypart b)
		{
			//todo: randomly-generated description of this Body
			//todo: Accuracy of measurements depends on your perception - the less it is, the bigger the wobble
			//todo: Pick a few parts to be described.
			return this.body.describeFull(this, b);
		}
		
		internal Character() //Debug constructor, maketh a Zhu
		{
			this.gender = 0;
			this.name = "Zhu";
			this.species = "Beast";
			this.body.musk.sex = 255;
			this.body.crotch.pucker.protrusion = 10;
		}
	}

	internal class NPC : Character
	{
		//Schedule - her on in location?
		//Inventory
		static int CharID;

		public override void GetObjectData(SerializationInfo info, StreamingContext context){
			throw new NotImplementedException ("NPC Serialization not implemented. TODO: Work out a flag structure, then this and base GetObjectData?");
		}
	}

	internal class Protagonist : Character
	{
		//Different from NPS in that you always have a perfect estimation of own wants, size, stat
		//Also, yanno, inventory and stuff
		Protagonist() : base()
		{					//runs base constructor before these additional events:
			this.inventory = new Inventory();
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException("Protagonist Serialization not implemented. TODO: Separate from game state? also yeah how bout a base in class Character?");
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
	
	/// <summary>
	/// Base for events. Encounters, iten drops, etc. Define series of events as simple text and make them parseable.
	/// </summary>
	public class Event{
		/*
		 * you'll have to make some sort of parser here.
		 * todo: The deserialization function read in all data, adds it into the central repository, assigns ID number(s) to all components
		 * and THEN gives the components their numbers. This'll allow collision-free import and coexistence of multiple addons
		 * Parser functions to:
		 * Write dialogue (character name, facial expression for vn interface)
		 * Read Location 
		 * Read Triggers
		 *	Affection/Str/PER greater than, smaller than
		 *	Flags set, not set
		 * Add items - randomly generated OR specific.
		 *	randItem() - specify type/family. code makes one, add it
		 *	specItem() - give ID (or name).
		 *	newItem()  - specify all components, system makes one and gives it.
		 */
		 int SceneID;
	}
	
	/// <summary>
	/// Base for Food, Equipment, Clothing, maybe Toys/Furniture/Decoration
	/// </summary>
	internal abstract class Item : IComparable{
		public delegate void invokeEffect(Character target);	//bind the actual effect code here
		public int price;										//(Base Price * Potency)* (1 + CHA/100)
		int ItemID;												//ID Number
		//float potency;										//todo: potency system for synthesis OR boil down two items into one with stronger effect
		public string description;				
							
		public string describe(){
			throw new NotImplementedException();
		}
		
		public int CompareTo(object o){
			throw new NotImplementedException("Item comparison not yet implemented. todo: Compare item ID, then compare potency.");
		}								
	}

	internal class Recipe : Item{
		internal Item craft(){
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
	
	enum Slots : byte {Crotch, Feet, Legs, Chest, Arms, Hands, Neck, Head};

	internal class Equipment : Item{
		/*
			Base lust growth of observers in area on tightness of clothing, their preferences, etc
			Apparel, Armor, Equipment - all the same
		*/
		byte equipSlot;		//depending on the slot it binds to, the item will have to watch for different changes e.g. chest items have to watch out for chest and pecs
		byte elasticity;	
		Color color;					
		string name;		
		Musk musk;			//
		bool infectSpecies;	//Will wearing/sniffing this item transform your species?
		bool infectSize;	//Will wearing/sniffing this item make you grow?
		delegate void infectOther(); //binding for other effects from this piece of clothing

		public new string describe(){
			throw new NotImplementedException();
		}

		public string sniff(){
			throw new NotImplementedException();
		}
	}

	internal class Food : Item{
	
	}

	internal class Decoration : Item{}
}