using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelLoadDelayS = 1f;
  [SerializeField] AudioClip crashClip;
  [SerializeField] AudioClip finishedClip;

  private AudioSource _audioSource;
  private bool _isTransitioning = false;

  void Start()
  {
    _audioSource = GetComponent<AudioSource>();
  }

  private void OnCollisionEnter(Collision other)
  {
    Debug.Log($"Collision occurred with: {other.collider.name}");
    string tag = other.collider.tag;
    switch (tag)
    {
      case "Friendly":
        Debug.Log("Touched a friendly object.");
        break;
      case "Finish":
        Debug.Log("Finished!");
        if (!_isTransitioning)
        {
          StartFinishSequence();
        }
        break;
      default:
        Debug.Log($"{tag} hit.");
        if (!_isTransitioning)
        {
          StartCrashSequence();
        }
        break;
    }
  }

  private void StartFinishSequence()
  {
    _isTransitioning = true;
    _audioSource.PlayOneShot(finishedClip);
    GetComponent<Movement>().enabled = false;
    Invoke("NextLevel", levelLoadDelayS);
  }

  private void StartCrashSequence()
  {
    _isTransitioning = true;
    _audioSource.PlayOneShot(crashClip);
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelayS);
  }

  private void NextLevel()
  {
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }

  private void ReloadLevel()
  {
    int sceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(sceneIndex);
  }
}
