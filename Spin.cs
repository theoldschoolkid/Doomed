using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A generic spin script(for spinningMonster enemy)
/// </summary>
public class Spin : MonoBehaviour
{
    [SerializeField]
    int spinSpeed = 180;
  
    
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }
}
