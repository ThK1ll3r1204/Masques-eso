using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    SpriteRenderer interSignSprite;
    public bool wantInteract;
    public bool couldInteract;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField, TextArea(4, 6)] private string[] dialogos;
    [SerializeField] private TMP_Text dialogueText;

    private float tipeo = 0.05f;

    private bool DialogueStart;
    private int lineIndex;

    Color activate;
    void Awake()
    {
        interSignSprite = GetComponent<SpriteRenderer>();
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
        if (wantInteract == true)
        {
            if (!DialogueStart)
            {
                StartDialogue();
            }
        }
    }
    private void StartDialogue()
    {
        DialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogos[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(tipeo);
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
