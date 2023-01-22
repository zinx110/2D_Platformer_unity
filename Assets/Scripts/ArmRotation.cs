using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotationOffset = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;         // Subtracting player position from mouse position.
        distance.Normalize();               // Normalizing the vector. meaning the sum of the vector dimensions is one.

        float rotationZ = Mathf.Atan2(distance.y, distance.x)*Mathf.Rad2Deg ;       // find angle in degrees.

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);




    }
}
