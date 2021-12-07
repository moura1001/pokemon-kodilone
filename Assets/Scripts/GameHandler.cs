using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private CameraFollow cameraFollow;
    [SerializeField]
    private Transform playerTransform;

    void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }
}
