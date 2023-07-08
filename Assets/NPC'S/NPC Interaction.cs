using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField, TextArea(4, 6)] private string[] dialogos;
    [SerializeField] private TMP_Text dialogueText;
    Interaction interaction; 

    private float tipeo = 0.05f;

    private bool DialogueStart;
    private int lineIndex;

   void Awake()
    {
        interaction = transform.Find("wantInteract").GetComponent<Interaction>();
    }
    private void Update()
    {
        if(interaction.wantInteract == true)
        {
            if(!DialogueStart)
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

        foreach(char ch in dialogos[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(tipeo);
        }
    }
}
