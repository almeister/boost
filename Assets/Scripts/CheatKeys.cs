using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatKeys : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    ProcessCheatKeys();
  }

  private void ProcessCheatKeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      GetComponent<CollisionHandler>().NextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      GetComponent<CollisionHandler>().DisableCollisions();
    }
  }
}
