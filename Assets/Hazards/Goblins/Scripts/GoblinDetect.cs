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
    [Range(1f, 10f)]
    public float radiusIdle;

    [Range(5f, 15f)]
    public float radiusFocus;

    public float detectionDelay = 0.3f;

    public LayerMask detectorLayerMask;
    int layerMask = (1 << 3) | (1 << 6);

    [Header("Debug parameters")]
    public Color debugIdleColor = Color.red;
    public Color debugDetectedColor = Color.green;
    public bool showGizmos = true;

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

    private void Start()
    {
        StartCoroutine(DetectCoroutine());
    }

    IEnumerator DetectCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetect();
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

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Color rayColor;
            Gizmos.color = debugIdleColor;
            if (playerOnZone)
                Gizmos.color = debugDetectedColor;
            if (playerDetected)
                rayColor = Color.green;
            else
                rayColor = Color.red;

            if(!playerFocused)
                Gizmos.DrawSphere((Vector2)detectorOrigin.position, radiusIdle);
            else
                Gizmos.DrawSphere((Vector2)detectorOrigin.position, radiusFocus);

            if (playerOnZone)
                Debug.DrawRay((Vector3)detectorOrigin.position, DirectionToPlayer, rayColor);
        }
    }
}
