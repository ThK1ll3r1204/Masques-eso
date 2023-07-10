using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    GameManager gManager;

    [SerializeField] GameObject dialoguePanel;
    [SerializeField, TextArea(4, 5)] private string[] dialogos;
    [SerializeField] TMP_Text dialogueText;
    Interaction interaction;

    private float tipeo = 0.05f;

    private bool DialogueStart;
    bool dialoguesInCourse;
    private int lineIndex;

    List<int> charAmount = new List<int>();
    int charCount;

    Coroutine StartShowLine;
    private void Awake()
    {
        interaction = transform.Find("Interaction Sign").GetComponent<Interaction>();
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        for (int i = 0; i < dialogos.Length; i++)
        {
            charCount = 0;
            foreach (char ch in dialogos[i])
            {
                charCount++;
            }
            charAmount.Add(charCount);
        }

        DialogueStart = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (interaction.wantInteract == true)
        {
            if (!DialogueStart)
            {
                StartDialogue();
            }
        }

        if (lineIndex == dialogos.Length - 1 && Input.GetKeyUp(KeyCode.E) && dialogueText.textInfo.characterCount > 5)
        {
            StartCoroutine(StopLine());
        }
        else if (Input.GetKeyUp(KeyCode.E) && dialogueText.textInfo.characterCount > 5)
        {
            lineIndex++;
            StopCoroutine(StartShowLine);
            StartShowLine = StartCoroutine(ShowLine());
        }
    }

    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        DialogueStart = true;
        lineIndex = 0;
        gManager.isPaused = true;
        StartShowLine = StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogos[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(tipeo);
        }
    }


    private IEnumerator StopLine()
    {
        StopCoroutine(StartShowLine);
        gManager.isPaused = false;
        dialogueText.text = string.Empty;
        DialogueStart = false;
        lineIndex = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        dialoguePanel.SetActive(false);
    }
}
