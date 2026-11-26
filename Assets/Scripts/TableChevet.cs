using System;
using UnityEngine;

public class TableChevet : MonoBehaviour
{
    [SerializeField]
    private Statue statue;

    [SerializeField]
    private Animator animateur;

    private void Start()
    {
        statue.ouvrir += Animation;
    }
    private void Animation(bool ouvert)
    {
        Debug.Log("Evenement : "+ouvert);
        animateur.SetBool("Ouvert",ouvert);
    }

}
