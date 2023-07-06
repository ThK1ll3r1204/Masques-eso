using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDetect : MonoBehaviour
{
    [field: SerializeField]
    public bool playerOnZone { get; private set; }
    public bool playerDetected;
    public bool playerFocused;

    public Vector3 DirectionToPlayer => player.transform.position - detectorOrigin.position;
    public float distanceToPlayer => Vector3.Distance(detectorOrigin.position, player.transform.position);

    [Header("Circle Detector parameters")]
    [SerializeField]
    public Transform detectorOrigin;
    [Range(8f, 20f)]
    public float radiusIdle;

    [Range(12f, 28f)]
    public float radiusFocus;

    public float detectionDelay = 0.3f;

    public LayerMask detectorLayerMask;
    public LayerMask wallsLayerMask;
    int layerMask = (1 << 3) | (1 << 6);

    [Header("Debug parameters")]
    public Color debugIdleColor = Color.red;
    public Color debugDetectedColor = Color.green;
    public bool showGizmos = true;



    Color playerRayColor = Color.white;
    Color wallXRayColor = Color.white;
    Color wallYRayColor = Color.white;

    private GameObject player;

    public GameObject Player
    {
        get => player;
        private set
        {
            player = value;
            playerOnZone = player != null;
        }
    }

    // Wall Detector
    RaycastHit2D wallOnXr;
    RaycastHit2D wallOnXl;
    RaycastHit2D wallOnYu;
    RaycastHit2D wallOnYd;

    public bool wallXrDetected;
    public bool wallXlDetected;
    public bool wallYuDetected;
    public bool wallYdDetected;

    private void Start()
    {
        StartCoroutine(DetectCoroutine());
    }

    IEnumerator DetectCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetect();
        WallDetect();
        StartCoroutine(DetectCoroutine());
    }

    private void Update()
    {
        if (!playerFocused)
        {
            if (playerOnZone)
            {
                RaycastHit2D canSeePlayer = Physics2D.Raycast(detectorOrigin.position, DirectionToPlayer, radiusIdle, layerMask);
                if (canSeePlayer.collider != null && canSeePlayer.collider.gameObject == Player)
                {
                    playerDetected = true;
                    playerFocused = true;
                }
                else
                    playerDetected = false;
            }
        }
        else
        {
            if (playerOnZone)
            {
                RaycastHit2D canSeePlayer = Physics2D.Raycast(detectorOrigin.position, DirectionToPlayer, radiusFocus, layerMask);
                if (canSeePlayer.collider != null && canSeePlayer.collider.gameObject == Player)
                {
                    playerDetected = true;
                }
                else
                    playerDetected = false;
            }
        }


        ColorRaySemaphore(playerDetected, playerRayColor);
        ColorRaySemaphore(wallXrDetected, wallXRayColor);
        ColorRaySemaphore(wallXlDetected, wallXRayColor);
        ColorRaySemaphore(wallYuDetected, wallYRayColor);
        ColorRaySemaphore(wallYdDetected, wallYRayColor);
    }

    public void PerformDetect()
    {
        if (!playerFocused)
        {
            Collider2D collider = Physics2D.OverlapCircle((Vector2)detectorOrigin.position, radiusIdle, detectorLayerMask);
            if (collider != null)
            {
                Player = collider.gameObject;
            }
            else
            {
                Player = null;
            }
        }
        else
        {
            Collider2D collider = Physics2D.OverlapCircle((Vector2)detectorOrigin.position, radiusFocus, detectorLayerMask);
            if (collider != null)
            {
                Player = collider.gameObject;
            }
            else
            {
                Player = null;
                playerFocused = false;
            }
        }
    }

    public void WallDetect()
    {
        wallOnXr = Physics2D.Raycast(detectorOrigin.position, Vector2.right, 2, wallsLayerMask);
        wallOnXl = Physics2D.Raycast(detectorOrigin.position, Vector2.left, 2, wallsLayerMask);
        wallOnYu = Physics2D.Raycast(detectorOrigin.position, Vector2.up, 2, wallsLayerMask);
        wallOnYd = Physics2D.Raycast(detectorOrigin.position, Vector2.down, 2, wallsLayerMask);

        if (wallOnXr.collider != null)
            wallXrDetected = true;
        else
            wallXrDetected = false;

        if (wallOnXl.collider != null)
            wallXlDetected = true;
        else
            wallXlDetected = false;

        if (wallOnYu.collider != null)
            wallYuDetected = true;
        else
            wallYuDetected = false;

        if (wallOnYd.collider != null)
            wallYdDetected = true;
        else
            wallYdDetected = false;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = debugIdleColor;
            if (playerOnZone)
                Gizmos.color = debugDetectedColor;


            if(!playerFocused)
                Gizmos.DrawSphere((Vector2)detectorOrigin.position, radiusIdle);
            else
                Gizmos.DrawSphere((Vector2)detectorOrigin.position, radiusFocus);

            if (playerOnZone)
                Debug.DrawRay((Vector3)detectorOrigin.position, DirectionToPlayer, playerRayColor);


            // Wall on X right
            Debug.DrawRay((detectorOrigin.position), Vector3.right * 2, wallXRayColor);
            // Wall on X left
            Debug.DrawRay((detectorOrigin.position), Vector3.left * 2, wallXRayColor);
            // Wall on Y up
            Debug.DrawRay((detectorOrigin.position), Vector3.up * 2, wallYRayColor);
            // Wall on Y down
            Debug.DrawRay((detectorOrigin.position), Vector3.down * 2, wallYRayColor);


        }
    }

    private void ColorRaySemaphore(bool ray, Color rayColorVar)
    {
        if (ray)
        {
            rayColorVar = Color.green;
        }
        else
        {
            rayColorVar = Color.red;
        }
    }
}
