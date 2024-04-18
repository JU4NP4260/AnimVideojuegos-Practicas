using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemylLookingAtCharacter : MonoBehaviour
{
    public GameObject Character;
    // Update is called once per frame
    void Update()
    {
        // Look at the character
        transform.LookAt(Character.transform);
        
    }
}
