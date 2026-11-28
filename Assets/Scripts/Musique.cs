using UnityEngine;

public class Musique : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip clipVictoire;

    public void victoire()
    {
        audioSource.Stop();
        audioSource.clip = clipVictoire;
        audioSource.Play();
    }
}
