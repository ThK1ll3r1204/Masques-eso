using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    GameManager gManager;

    [SerializeField] GameObject dialoguePanel;
    [SerializeField] [TextArea(4, 5)] string[] dialogos;
    [SerializeField] TMP_Text dialogueText;
    Interaction interaction;

    private float tipeo = 0.05f;

    NPCs thisNPC;

    List<NPCs> DeactivateList = new List<NPCs>();
    GameObject deActivateObj;

    private bool DialogueStart;
    private int lineIndex;

    List<int> charAmount = new List<int>();
    int charCount;

    bool coroutineRunning;

    Coroutine StartShowLine;
    private void Awake()
    {
        interaction = transform.Find("Interaction Sign").GetComponent<Interaction>();
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dialoguePanel = GameObject.Find("DialoguePanel");
        dialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        NPCs npc = new NPCs(dialogos, this.gameObject.name);
        gManager.NPCList.Add(npc);

        thisNPC = gManager.NPCList.Find(npc => npc.name == this.gameObject.name);


        for (int i = 0; i < dialogos.Length; i++)
        {
            charCount = 0;
            foreach (char ch in thisNPC.lines[i])
            {
                charCount++;
            }
            charAmount.Add(charCount);
            //Debug.Log(dialogos[i] + quienSoy);
        }

        DialogueStart = false;
        dialoguePanel.SetActive(false);
        //StartCoroutine(DebugNPCs());
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

        if (lineIndex == dialogos.Length - 1 && Input.GetKeyUp(KeyCode.E) && dialogueText.textInfo.characterCount > 1)
        {
            gManager.isPaused = false;
            //if (coroutineRunning)
                StartCoroutine(StopLine());
        }
        else if (Input.GetKeyUp(KeyCode.E) && dialogueText.textInfo.characterCount > 1)
        {
            lineIndex++;

            if (coroutineRunning)
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
        foreach(NPCs npc in gManager.NPCList)
        {
            if(npc.name != this.gameObject.name)
            {
                deActivateObj = GameObject.Find(npc.name);
                deActivateObj.GetComponent<NPCInteraction>().enabled = false ;
            }
        }

        coroutineRunning = true;
        Debug.Log(thisNPC.name + "  " + thisNPC.lines[lineIndex] + "  " + this.gameObject.name);
        dialogueText.text = string.Empty;
        foreach (char ch in thisNPC.lines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(tipeo);
        }
        yield return new WaitForEndOfFrame();
        coroutineRunning = false;
    }


    private IEnumerator StopLine()
    {
        dialogueText.text = string.Empty;
        DialogueStart = false;
        lineIndex = 0;
        StopCoroutine(StartShowLine);
        yield return new WaitForSecondsRealtime(0.1f);
        dialoguePanel.SetActive(false);
        foreach (NPCs npc in gManager.NPCList)
        {
            if (npc.name != this.gameObject.name)
            {
                deActivateObj = GameObject.Find(npc.name);
                deActivateObj.GetComponent<NPCInteraction>().enabled = true;
            }
        }
    }


    private IEnumerator DebugNPCs()
    {
        Debug.Log(thisNPC.name);
        Debug.Log("Im: " + this.gameObject.name);
        foreach (string line in thisNPC.lines)
        {
            Debug.Log("\n" + line);
        }
        Debug.Log("//////////////////////");
        yield return new WaitForSeconds(5);
        StartCoroutine(DebugNPCs());
    }
}

public class NPCs
{
    public string[] lines;
    public string name;
    public NPCs(string[] dialogos, string name)
    {
        this.name = name;
        this.lines = dialogos;
    }
}

