using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    SpriteRenderer interSignSprite;
    public bool wantInteract;
    public bool couldInteract;

    Color activate;
    void Awake()
    {
        interSignSprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
    }

    private void Update()
    {
        if (couldInteract)
        {
            if (Input.GetKeyUp("e"))
                wantInteract = true;
            else
                wantInteract = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activate = interSignSprite.color;
            activate.a = 1;
            interSignSprite.color = activate;
            couldInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activate = interSignSprite.color;
            activate.a = 0;
            interSignSprite.color = activate;
            couldInteract = false;
        }
    }

}
