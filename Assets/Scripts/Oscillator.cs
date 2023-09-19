using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
  [SerializeField] Vector3 movementVector;
  [SerializeField] float periodSeconds = 2f;
  [SerializeField] bool useBounce = false;

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
    if (periodSeconds <= Mathf.Epsilon)
    {
      return;
    }

    const float tau = Mathf.PI * 2;
    float cycles = Time.time / periodSeconds;
    float rawSineWave = Mathf.Sin(cycles * tau);

    _movementFactor = (rawSineWave + 1f) / 2f; // So that movementFactor oscillates between 0 and 1
    if (useBounce)
    {
      _movementFactor = Mathf.Pow(_movementFactor, 2f);
    }

    Vector3 offset = movementVector * _movementFactor;
    transform.position = _startingPosition + offset;

  }
}
