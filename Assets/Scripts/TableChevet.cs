using System;
using UnityEngine;

public class TableChevet : MonoBehaviour
{
    [SerializeField]
    private Statue statue;

    [SerializeField]
    private Animator animateur;

    [SerializeField]
    private AudioSource audioTirroir;

    private void Start()
    {
        statue.ouvrir += Animation;
    }
    private void Animation(bool ouvert)
    {

        audioTirroir.Play();

        animateur.SetBool("Ouvert",ouvert);
    }

}
