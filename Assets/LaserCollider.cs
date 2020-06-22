using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour {
    [SerializeField] private LaserObj laser = null;

    private void OnCollisionEnter(Collision collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Wall")
        {
            laser.CollisionAction(collision);
        }
    }
}
