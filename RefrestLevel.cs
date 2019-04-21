using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrestLevel : MonoBehaviour
{
    [SerializeField]
    GameObject Marker, UpperStripedWall;

    Vector3 temp;
    bool Refresh = false;
    int speed = 27;
    int elevationLevel = 143; // The distance the platform has to travel vertically to reach the new level

    public void Initialize()
    {     
        setPosition();
        setUwall();
        Refresh = false;
    }

    public bool refreshSet
    {
        set { Refresh = value; }
    }


    void setPosition()
    {
        
        temp = this.transform.position;   // Store the position of this object in temporary variable
        temp.y = temp.y + elevationLevel;
        Marker.transform.position = temp; // Place the marker(contains platformDetector) where the new level will be initialized
      
    }

    void setUwall()
    {
        GameObject uWall = Instantiate(UpperStripedWall); // Create a new floor near the marker

        //           -_-             //

        temp = Marker.transform.localPosition;
          temp.x = temp.x + 42;
          temp.y = temp.y + 107;
          temp.z = temp.z - 22;

        uWall.transform.position = temp;
     
    }

    void FixedUpdate()
    {

        if (Refresh == true)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, Marker.transform.position, speed * Time.deltaTime);
        }

    }


}
