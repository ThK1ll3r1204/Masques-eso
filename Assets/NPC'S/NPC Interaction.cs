using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public class Interaction : MonoBehaviour
    {
        public GameObject interactionSignPrefab;
        Interaction sign;

        private void Awake()
        {
            GameObject interactionSignObject = Instantiate(interactionSignPrefab, transform);
            sign = interactionSignObject.GetComponent<Interaction>();
        }

    }
}
