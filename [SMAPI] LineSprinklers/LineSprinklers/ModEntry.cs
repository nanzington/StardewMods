using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using SObject = StardewValley.Object;

namespace LineSprinklers
{
    /// <summary>The mod entry class.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the game begins a new day (including when the player loads a save).</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            foreach (GameLocation location in this.GetLocations())
            {
                foreach (SObject obj in location.Objects.Values)
                {
                    // get sprinkler info
                    Vector2 tile = obj.TileLocation;

                    switch (obj.name)
                    {
                        case "Line Sprinkler (D)":
                            for (int i = 0; i < 4; ++i)
                            {
                                tile.Y++;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Line Sprinkler (U)":
                            for (int i = 0; i < 4; ++i)
                            {
                                tile.Y--;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Line Sprinkler (L)":
                            for (int i = 0; i < 4; ++i)
                            {
                                tile.X--;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Line Sprinkler (R)":
                            for (int i = 0; i < 4; ++i)
                            {
                                tile.X++;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Quality Line Sprinkler (D)":
                            for (int i = 0; i < 8; ++i)
                            {
                                tile.Y++;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Quality Line Sprinkler (U)":
                            for (int i = 0; i < 8; ++i)
                            {
                                tile.Y--;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Quality Line Sprinkler (L)":
                            for (int i = 0; i < 8; ++i)
                            {
                                tile.X--;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Quality Line Sprinkler (R)":
                            for (int i = 0; i < 8; ++i)
                            {
                                tile.X++;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Iridium Line Sprinkler (D)":
                            for (int i = 0; i < 24; ++i)
                            {
                                tile.Y++;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Iridium Line Sprinkler (U)":
                            for (int i = 0; i < 24; ++i)
                            {
                                tile.Y--;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Iridium Line Sprinkler (L)":
                            for (int i = 0; i < 24; ++i)
                            {
                                tile.X--;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;

                        case "Iridium Line Sprinkler (R)":
                            for (int i = 0; i < 24; ++i)
                            {
                                tile.X++;
                                if (location.terrainFeatures.TryGetValue(tile, out TerrainFeature terrainFeature) && terrainFeature is HoeDirt hoeDirt)
                                    hoeDirt.state.Value = 1;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>Get all in-game locations.</summary>
        private IEnumerable<GameLocation> GetLocations()
        {
            foreach (GameLocation location in Game1.locations)
            {
                yield return location;
                if (location is BuildableGameLocation buildableLocation)
                {
                    foreach (Building building in buildableLocation.buildings)
                    {
                        if (building.indoors.Value != null)
                            yield return building.indoors.Value;
                    }
                }
            }
        }
    }
}
