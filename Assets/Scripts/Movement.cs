using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float rotationThrust = 100f;
  [SerializeField] AudioClip mainEngineClip;

  private Rigidbody _rigidBody;
  private AudioSource _audioSource;

  // Start is called before the first frame update
  void Start()
  {
    _rigidBody = GetComponent<Rigidbody>();
    _audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    ProcessThrust();
    ProcessRotation();
  }

  private void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.W))
    {
      // Add thrust in direction rocket is pointing
      float impulse = mainThrust * Time.deltaTime;
      _rigidBody.AddRelativeForce(impulse * Vector3.up);

      if (!_audioSource.isPlaying)
      {
        _audioSource.PlayOneShot(mainEngineClip);
      }
    }
    else
    {
      _audioSource.Stop();
    }
  }

  private void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      // Rotate counter-clockwise
      ApplyRotation(rotationThrust);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      // Rotate clockwise
      ApplyRotation(-rotationThrust);
    }
  }

  private void ApplyRotation(float rotation)
  {
    // Freeze rotation to avoid conflict with physics system
    _rigidBody.freezeRotation = true;
    transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
    _rigidBody.freezeRotation = false;
  }
}
