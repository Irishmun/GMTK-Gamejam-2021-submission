using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorScript : MonoBehaviour
{
    [SerializeField] private Vector2 DoorOpenOffset;


    private Vector2 DoorStart;

    private void Awake()
    {
        DoorStart = transform.position;
    }

    public void OpenDoor()
    {
        transform.position = DoorStart + DoorOpenOffset;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CloseDoor()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        transform.position = DoorStart;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + DoorOpenOffset, Vector2.one);
    }
}

