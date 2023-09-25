using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWaypoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(.5f, .5f, .5f));
    }
}
