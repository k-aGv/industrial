﻿using kagv.DLL_source;
using System.Collections.Generic;

namespace kagv {

    public partial class MainForm  {

        //function that returns a list that contains the Available for action _AGVs
        private List<GridPos> NotTrappedVehicles(List<GridPos> vehicles, GridPos end) {
            //Vehicles is a list with all the _AGVs that are inserted in the Grid by the user
            short listIndex = 0;
            short trappedIndex = 0;

            //First, we must assume that ALL the _AGVs are trapped and cannot move (trapped means they are prevented from reaching the END block)
            for (int i = 0; i < _trappedStatus.Length; i++)
                _trappedStatus[i] = true;

            do {
                bool removed = false;
                _jumpParam.Reset(vehicles[listIndex], end); //we use the A* setting function and pass the 
                                                            //initial start point of every AGV and the final destination (end block)
                if (AStarFinder.FindPath(_jumpParam, nud_weight.Value).Count == 0) //if the number of JumpPoints that is calculated is 0 (zero)
                {                                                          //it means that there was no path found
                    vehicles.Remove(vehicles[listIndex]); //we removed, from the returning list, the AGV for which there was no path found
                    _AGVs.Remove(_AGVs[listIndex]); //we remove the corresponding AGV from the public list that contains all the _AGVs which will participate in the simulation
                    removed = true;
                } else
                    _trappedStatus[trappedIndex] = false; //since it's not trapped, we switch its state to false 

                if (!removed) {
                    _AGVs[listIndex].ID = listIndex;
                    listIndex++;
                }
                trappedIndex++;
            }
            while (listIndex < vehicles.Count); //the above process will be repeated until all elements of the incoming List are parsed.
            return vehicles; //list with NOT TRAPPED _AGVs' starting points (trapped _AGVs have been removed)


            //the point of this function is to consider every AGV as trap and then find out which _AGVs
            //eventually, are not trapped and keep ONLY those ones.
        }
    }
}
