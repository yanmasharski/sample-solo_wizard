using System;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    
    private Vector3 initialOffset;
    
    private void Start()
    {
        initialOffset = transform.position - targetToFollow.position;
    }
    
    private void LateUpdate()
    {
         transform.position = Vector3.Lerp(transform.position, targetToFollow.position + initialOffset, Time.deltaTime * 10f);
    }
}
