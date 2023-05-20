using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Transform _player;
    private float posZ;
    public Vector2 min;
    public Vector2 max;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GetComponent<Camera>();
        _player = GameObject.Find("Player").GetComponent<Transform>();

        posZ = transform.position.z;
    }

    void LateUpdate()
    {
        Vector3 pos = _player.position;
        pos.z = posZ;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }   
}
