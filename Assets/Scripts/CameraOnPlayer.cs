using System.Collections;

using UnityEngine;

public class CameraOnPlayer : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public Vector3 offset = new Vector3(0.0f, 10.0f, -10.0f);
    [Range(1.0f, 0.0f)]public float smoothness = 0.5f;

    private Vector3 _velocity;

    private float fixedY; 

    void Start()
    {
        // Salviamo la Y iniziale della camera
        fixedY = transform.position.y;
    }

    void Update()
    {
        Vector3 desiredPos = _target.position + offset;

        // Mantieni la Y fissa
        desiredPos.y = fixedY;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref _velocity,
            smoothness
        );
    }
}
