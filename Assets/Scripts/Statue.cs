using System;
using UnityEngine;

public class Statue : MonoBehaviour, IInteragir
{
    [SerializeField]
    private Animator animateur;

    private bool ouvert;

    public event Action<bool> ouvrir;

    private void Start()
    {
        ouvert = false;
    }
    public void Interagir()
    {
        ouvert = !ouvert;
        animateur.SetBool("Ouvert", ouvert);
        ouvrir?.Invoke(ouvert);
    }
}
