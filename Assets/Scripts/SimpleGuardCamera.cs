using UnityEngine;

public class SimpleGuardCamera : MonoBehaviour
{
	[SerializeField] private float degreesOfMotion;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float delayBetweenRotations;

	private float realDegrees;
	private float currentRotation;
	private float timer;

	private float direction;

	private Vector3 rotCache = Vector3.zero;

	private void Awake()
	{
		realDegrees = degreesOfMotion / 2;
		direction = 1f;
		rotCache.z = rotationSpeed;
	}

	private void Update()
	{
		timer -= Time.deltaTime;
		if(timer >= 0f)
		{
			return;
		}

		if(currentRotation >= realDegrees)
		{
			direction *= -1f;
			timer = delayBetweenRotations;
			currentRotation = -realDegrees;
			return;
		}

		currentRotation += rotCache.z * Time.deltaTime;
		transform.Rotate(rotCache * direction * Time.deltaTime);

	}

}
