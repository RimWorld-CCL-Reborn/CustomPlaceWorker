using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace CustomPlaceWorker
{
    /// <summary>
    /// Placeworker for soil fertility of 70% or better.
    /// </summary>
    public class PlaceWorker_OnGravelySoilOrBetter : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            //if the fertility at this cell isn't 1 or more, return message.
            if (!(map.fertilityGrid.FertilityAt(loc) >= 0.7f))
            {
                return new AcceptanceReport("MustPlaceOnGravel".Translate());
            }
            return true;
        }
    }

    public class PlaceWorker_OnRegularSoilOrBetter : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {

            if (!(map.fertilityGrid.FertilityAt(loc) >= 1f))
            {
                return "MustPlaceOnRegularSoil".Translate();
            }
            return true;
        }
    }

    public class PlaceWorker_OnGoodSoilOrBetter : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            if (!(map.fertilityGrid.FertilityAt(loc) >= 1.20f))
            {
                return "MustPlaceOnGoodSoil".Translate();
            }
            return true;
        }
    }

    public class PlaceWorker_OnFertileSoilOrBetter : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            if (!(map.fertilityGrid.FertilityAt(loc) >= 1.40f))
            {
                return "MustPlaceOnFertileSoil".Translate();
            }
            return true;
        }
    }

    /// <summary>
    /// Placeworker for directly touching a wall.
    /// </summary>
    public class PlaceWorker_AgainstWalls : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            if (!GenAdj.CellsAdjacentCardinal(loc, rot, checkingDef.Size)
                      .Any(x => x.Impassable(map)
                           && x.GetThingList(map).Any(y => y.def.holdsRoof && !y.def.IsDoor)))
            {
                return "MustPlaceAgainstWall".Translate();
            }
            return true;
        }
    }

    /// <summary>
    /// Placeworker for only under roof. This is the simple version, which does not take size into account.
    /// </summary>
    public class PlaceWorker_UnderRoof : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            if (!map.roofGrid.Roofed(loc))
            {
                return "MustPlaceUnderRoof".Translate();
            }
            return true;
        }
    }

    /// <summary>
    /// Placeworker for only under roof. Takes the entire thing into account.
    /// </summary>
    public class PlaceWorker_EntirelyUnderRoof : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            if (!GenAdj.CellsOccupiedBy(loc, rot, checkingDef.Size).Any(x => x.Roofed(map)))
            {
                return "MustPlaceEntirelyUnderRoof".Translate();
            }
            return true;
        }
    }

    /// <summary>
    /// Placeworker for entirely unroofed. Takes the entire thing into account, unlike the vanilla worker.
    /// </summary>
    public class PlaceWorker_EntirelyUnroofed : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            if (GenAdj.CellsOccupiedBy(loc, rot, checkingDef.Size).Any(x => x.Roofed(map)))
            {
                return "MustPlaceEntirelyUnroofed".Translate();
            }
            return true;
        }
    }

    public class PlaceWorker_UnderOverheadMountain : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            if (map.roofGrid.RoofAt(loc) != RoofDefOf.RoofRockThick)
            {
                return "MustPlaceUnderOverheadMountain".Translate();
            }
            return true;
        }
    }
}