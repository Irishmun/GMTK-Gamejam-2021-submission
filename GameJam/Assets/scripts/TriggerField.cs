using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerField : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] Sprite ActivatedSprite, RegularSprite;
    private SpriteRenderer ChildRender;

    public UnityEvent OnButtonActive, OnButtonInactive, OnSoundActive;
    private bool activated;

    private int ObjectsOnTrigger;
    private void Awake()
    {
        ChildRender = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag(playerTag))
        {
            //open door
            ObjectsOnTrigger++;
            if (!activated)
            {
                ChildRender.sprite = ActivatedSprite;
                OnButtonActive.Invoke();
                OnSoundActive.Invoke();
                activated = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
        if (collision.CompareTag(playerTag))
        {
            //open door
            if (ChildRender.sprite != ActivatedSprite)
            {
                ChildRender.sprite = ActivatedSprite;
            }
            OnButtonActive.Invoke();
            activated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            ObjectsOnTrigger--;
            if (activated && ObjectsOnTrigger < 1)
            {
                //close door
                ChildRender.sprite = RegularSprite;
                OnButtonInactive.Invoke();
                activated = false;
            }
        }
    }
}
