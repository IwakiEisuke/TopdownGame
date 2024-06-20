using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStartManager : MonoBehaviour
{
    public MapManager mapManager;
    public StairsCreator stairsCreator;

    public UnityEvent startEvent;
    // Start is called before the first frame update
    void Start()
    {
        startEvent.Invoke(); 
    }
}
