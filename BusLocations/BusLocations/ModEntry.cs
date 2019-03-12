using System.Linq;
using BusLocations.Framework;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace BusLocations
{
    public class ModEntry : Mod
    {
        private static BusLoc[] _busLocs;
        private static Response[] _busChoices;

        /*********
       ** Public methods
       *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            //Events
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            
            //Content Packs
            var cPacks = helper.ContentPacks.GetOwned().Count();
            _busChoices = new Response[cPacks + 1];
            _busLocs = new BusLoc[cPacks];
            int index = 0;
            foreach (IContentPack contentPack in helper.ContentPacks.GetOwned())
            {
                Monitor.Log($"Reading content pack: {contentPack.Manifest.Name} {contentPack.Manifest.Version} from {contentPack.DirectoryPath}");
                _busLocs[index] = contentPack.ReadJsonFile<BusLoc>("content.json");
                _busChoices[index] = new Response(index.ToString(), $"{_busLocs[index].Displayname} ({_busLocs[index].TicketPrice}g)");
                index++;
            }
            _busChoices[_busChoices.Length - 1] = new Response("Cancel", "Cancel");
        }

        /*********
       ** Private methods
       *********/

        /// <summary>Raised when a button is pressed.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!(Context.IsWorldReady && (e.Button.IsActionButton() || !Game1.currentLocation.Name.Contains("BusStop")) && Game1.currentLocation.doesTileHaveProperty(7, 11, "Action", "Buildings") == "BusTicket" && (e.Cursor.GrabTile.X == 7.0 && (e.Cursor.GrabTile.Y == 11.0 || e.Cursor.GrabTile.Y == 10.0))))
                return;
            Helper.Input.Suppress(e.Button);
            if(Game1.MasterPlayer.mailReceived.Contains("ccVault"))
                Game1.currentLocation.createQuestionDialogue("Where would you like to go?", _busChoices, DialogueAction);
            else
                Game1.drawObjectDialogue("Out of service");

        }
        /// <summary>Method that gets ran after the QuestionDialogue</summary>
        /// <param name="who">The player</param>
        /// <param name="whichAnswer">The Answer</param>
        private void DialogueAction(Farmer who, string whichAnswer)
        {
            if (whichAnswer == "Cancel")
                return;
            int index = int.Parse(whichAnswer);
            NPC characterFromName = Game1.getCharacterFromName("Pam");
            if (Game1.player.Money >= _busLocs[index].TicketPrice && Game1.currentLocation.characters.Contains(characterFromName) && (characterFromName).getTileLocation().Equals(new Vector2(11f, 10f)))
            {
                Farmer player = Game1.player;
                player.Money -= _busLocs[index].TicketPrice;
                Game1.player.Halt();
                Game1.player.freezePause = 700;
                Game1.warpFarmer(_busLocs[index].Mapname, _busLocs[index].DestinationX, _busLocs[index].DestinationY, _busLocs[index].ArrivalFacing);
            }
            else if (Game1.player.Money < _busLocs[index].TicketPrice)
                Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:BusStop_NotEnoughMoneyForTicket"));
            else
                Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:BusStop_NoDriver"));
        }
    }
}
