using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelLoadDelayS = 1f;
  [SerializeField] AudioClip crashClip;
  [SerializeField] AudioClip finishedClip;

  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;

  private AudioSource _audioSource;
  private bool _isTransitioning = false;
  private bool _collisionsDisabled = false;

  void Start()
  {
    _audioSource = GetComponent<AudioSource>();
  }

  public void NextLevel()
  {
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }

  public void ReloadLevel()
  {
    int sceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(sceneIndex);
  }

  public void DisableCollisions()
  {
    _collisionsDisabled = !_collisionsDisabled;
    Debug.Log("Collisions disabled.");
  }

  private void OnCollisionEnter(Collision other)
  {
    // Debug.Log($"Collision occurred with: {other.collider.name}");
    if (_isTransitioning || _collisionsDisabled)
    {
      return;
    }

    string tag = other.collider.tag;
    switch (tag)
    {
      case "Friendly":
        // Debug.Log("Touched a friendly object.");
        break;
      case "Finish":
        // Debug.Log("Finished!");
        StartFinishSequence();
        break;
      default:
        // Debug.Log($"{tag} hit.");
        StartCrashSequence();
        break;
    }
  }

  private void StartFinishSequence()
  {
    _isTransitioning = true;
    _audioSource.Stop();
    _audioSource.PlayOneShot(finishedClip);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("NextLevel", levelLoadDelayS);
  }

  private void StartCrashSequence()
  {
    _isTransitioning = true;
    _audioSource.Stop();
    _audioSource.PlayOneShot(crashClip);
    crashParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelayS);
  }
}
