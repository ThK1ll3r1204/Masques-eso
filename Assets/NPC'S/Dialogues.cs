using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public List<List<string>> dialogosLists = new List<List<string>>();
    void Start()
    {
        foreach (List<string> dialogos in dialogosLists)
        {
            foreach (string line in dialogos)
            {
                Debug.Log("Linea : " + line + "\n Dialogo: " + dialogos);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
