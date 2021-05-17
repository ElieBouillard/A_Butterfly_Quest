using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningScript : MonoBehaviour
{

    private void Update()
    {
        transform.Rotate(0, 2, 0 * Time.deltaTime);
    }
}
