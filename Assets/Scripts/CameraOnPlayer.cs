using System.Collections;

using UnityEngine;

public class CameraOnPlayer : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public Vector3 offset = new Vector3(0.0f, 1.0f, -10.0f);
    [Range(0.0f, 1.0f)]public float smoothness = 0.5f;

    private Vector3 _velocity;
    
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, 
        _target.position + offset, 
        ref _velocity,
        smoothness
        );

    }
}
