using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider_NotPool : MonoBehaviour {

    [SerializeField] private LaserObj_NotPool laser = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Wall")
        {
            laser.CollisionAction(collision);
        }
    }
}
