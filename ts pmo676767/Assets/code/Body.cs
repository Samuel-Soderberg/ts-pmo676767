using UnityEngine;

public class Body : MonoBehaviour 
{ 
    public bool photographed = false;
    void Awake() 
    { 
        BodyRegistry.allBodies.Add(this); 
    }
}