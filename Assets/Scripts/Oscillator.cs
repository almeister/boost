using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
  [SerializeField] Vector3 movementVector;
  [SerializeField] float periodSeconds = 2f;

  private Vector3 _startingPosition;
  private float _movementFactor;

  // Start is called before the first frame update
  void Start()
  {
    _startingPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    float cycles = Time.time / periodSeconds;
    const float tau = Mathf.PI * 2;
    float rawSineWave = Mathf.Sin(cycles * tau);

    _movementFactor = (rawSineWave + 1f) / 2f; // So that movementFactor oscillates between 0 and 1
    Vector3 offset = movementVector * _movementFactor;
    transform.position = _startingPosition + offset;
  }
}
