using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    public RoomOpenings openings;
    
    // RoomOpening.Left => true 
    // RoomOpening.Right => false
    public Dictionary<RoomOpenings, bool> connections = new();
}
