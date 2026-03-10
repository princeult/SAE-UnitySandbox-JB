using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add some breakpoints into the Start method and see what these values look like.
/// </summary>
public class InspectingDataStructures : MonoBehaviour
{
    private List<int> _inspectThisList;
    private InspectMe _inspectThisStruct;

    private void Start()
    {
        Debug.Log("Add a breakpoint, and do some inspection of the variables...");
        Debug.Log("What do you see? What do the List and Struct look like?");

        // [CAUTION] Getting stuff from null variables can be dangerous...
        // GetSomethingFromAList();
        // GetSomethingFromAStruct();

        Debug.Log("Before we access the List and Struct, we should initialise them! Don't access a null reference!");

        // Initialise them.
        _inspectThisStruct = new InspectMe 
        { 
            Name = "InspectMe", 
            Description = "A struct for us to inspect the values in." 
        };

        _inspectThisList = new List<int> { 0, 10, 20, };

        Debug.Log("We have initialised, so this should work...");

        GetSomethingFromAStruct();
        GetSomethingFromAList();
    }

    private void GetSomethingFromAStruct()
    {
        string desc = _inspectThisStruct.Description;

        Debug.Log($"<color=blue>Got Description from my struct: [{desc}]</color>");
    }

    private void GetSomethingFromAList()
    {
        int listItem = _inspectThisList[0];

        Debug.Log($"<color=yellow>Got a list item from my list: [{listItem}]</color>");
    }

    private struct InspectMe
    {
        public string Name;
        public string Description;
    }
}
