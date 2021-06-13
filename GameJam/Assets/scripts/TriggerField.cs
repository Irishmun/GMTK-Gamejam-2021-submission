using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerField : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] Sprite ActivatedSprite, RegularSprite;
    private SpriteRenderer ChildRender;

    public UnityEvent OnButtonActive, OnButtonInactive;
    private bool activated;
    private void Awake()
    {
        ChildRender = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            //open door
            if (!activated)
            {
                ChildRender.sprite = ActivatedSprite;
                OnButtonActive.Invoke();
                activated = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
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
            if (activated)
            {
                //close door
                ChildRender.sprite = RegularSprite;
                OnButtonInactive.Invoke();
                activated = false;
            }
        }
    }
}
