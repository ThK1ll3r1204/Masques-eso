using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeversOpenDoors : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Animator leverAnim;
    [SerializeField] Animator doorAnim;
    [SerializeField] float leverDistance;
    
    [SerializeField] Collider2D doorCool;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        leverAnim= GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= leverDistance && Input.GetKeyDown(KeyCode.E))
        {
            leverAnim.SetTrigger("LeverActive");
            doorAnim.SetTrigger("OpenDoor");
            doorCool.isTrigger = true;
            Debug.Log("Door Unlocked");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, leverDistance);
    }


}
