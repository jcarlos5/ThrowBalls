using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    void Update()
    {
        if(gameObject.transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
}
