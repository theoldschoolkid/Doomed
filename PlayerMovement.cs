using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Movement   --------------------DISABLED-----------------------
/// </summary>

public class PlayerMovement : MonoBehaviour {

  
    private Rigidbody myBody;

    public float walk_Speed = 2f;
    public float z_Speed = 1.5f;   
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Awake() {
        myBody = GetComponent<Rigidbody>();      
    }

    // Update is called once per frame
    void Update() {
           
    }

    void FixedUpdate() {   
        
            myBody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (walk_Speed),  myBody.velocity.y, Input.GetAxisRaw("Vertical") * (z_Speed));       
    }

    void DetectMovement( Vector3 direction) {

      //  myBody.velocity = new Vector3(
          //  direction * (-walk_Speed), myBody.velocity.y, Input.GetAxisRaw("Vertical") * (-z_Speed));

    } 


} // class






































