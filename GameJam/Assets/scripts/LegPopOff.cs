using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LegPopOff : MonoBehaviour
{
    [SerializeField] private GameObject LegPrefab;
    [SerializeField] private Vector2 SpawnOffset;
    [SerializeField] private Image LeftLegImage, RightLegImage;
    private int DetachedLegs;
    private int MaxLegs = 2;
    private bool LeftLegDetached, RightLegDetached;
    private bool isLeg;
    private GameObject CurrentLeg;

    private void Update()
    {
        if (DetachedLegs == 2)
        {
            //become ball
            gameObject.GetComponent<MovementScript>().IsBall = true;
            gameObject.GetComponent<Animator>().SetBool("IsBall", true);
            gameObject.GetComponent<MovementScript>().IsHopping = false;
        }
        else if (DetachedLegs == 1)
        {
            //become hop
            gameObject.GetComponent<MovementScript>().IsBall = false;
            gameObject.GetComponent<Animator>().SetBool("IsBall", false);
            gameObject.GetComponent<Animator>().SetBool("Hopping", true);
            gameObject.GetComponent<MovementScript>().IsHopping = true;
        }
        else if (DetachedLegs <= 0)
        {
            //become regular
            gameObject.GetComponent<MovementScript>().IsBall = false;
            gameObject.GetComponent<Animator>().SetBool("IsBall", false);
            gameObject.GetComponent<Animator>().SetBool("Hopping", false);
            gameObject.GetComponent<MovementScript>().IsHopping = false;
        }
    }
    public void TryAttachLeg(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (DetachedLegs > 0)
            {
                Debug.Log("Finding leg...");
                if (isLeg && CurrentLeg)
                {
                    if (CurrentLeg.GetComponentInParent<LegScript>().legType == LegScript.LegType.LeftLeg)
                    {
                        LeftLegImage.gameObject.SetActive(false);
                        LeftLegDetached = false;
                    }
                    else
                    {
                        RightLegImage.gameObject.SetActive(false);
                        RightLegDetached = false;
                    }
                    Destroy(CurrentLeg.transform.parent.gameObject);
                    DetachedLegs--;
                }
            }
            else
            {
                Debug.Log("All legs attached.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<LegScript>())
        {
            isLeg = true;
            CurrentLeg = collision.gameObject;
        }
        else
        {
            isLeg = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<LegScript>())
        {
            isLeg = true;
            if (CurrentLeg != collision.gameObject)
            {
                CurrentLeg = collision.gameObject;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<LegScript>())
        {
            isLeg = false;
            CurrentLeg = null;
        }
    }

    public void DetachLeftLeg()
    {
        if (!LeftLegDetached)
        {
            DetachLeg(LegScript.LegType.LeftLeg);
            LeftLegDetached = true;
        }
    }
    public void DetachRightLeg()
    {
        if (!RightLegDetached)
        {
            DetachLeg(LegScript.LegType.RightLeg);
            RightLegDetached = true;
        }
    }


    private void DetachLeg(LegScript.LegType legType)
    {
        if (DetachedLegs >= MaxLegs)
        {
            Debug.Log("All legs are detached.");
        }
        else
        {
            LegScript spawn = Instantiate(LegPrefab, (Vector2)transform.position + SpawnOffset, Quaternion.identity).GetComponent<LegScript>();
            spawn.legType = legType;
            DetachedLegs++;
        }
    }
}
