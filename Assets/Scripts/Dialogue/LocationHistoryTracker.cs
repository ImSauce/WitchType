using System.Collections.Generic;
using UnityEngine;

public class LocationHistoryTracker : MonoBehaviour
{
    private HashSet<LocationSO> locationsVisited = new HashSet<LocationSO>();

    public void RecordLocation(LocationSO locationSO)
    {
        locationsVisited.Add(locationSO);
        Debug.Log("Just Visted " + locationSO.displayName);
    }

    public bool HasVisited(LocationSO locationSO)
    {
        return locationsVisited.Contains(locationSO);
    }
}
