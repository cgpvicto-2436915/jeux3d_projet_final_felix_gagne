using UnityEngine;

public abstract class EtatJoueur
{
    /// <summary>
    /// methode effectue au debut de l'etat
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    public virtual void OnEnter(Joueur joueur)
    {

    }

    /// <summary>
    /// Methode effectue a chaque frame
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    /// <returns>Prochaine etat</returns>
    public virtual EtatJoueur OnExecuter(Joueur joueur)
    {

        return this;
    }

    /// <summary>
    /// Methode effectue a la fin de l'etat
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    public virtual void OnSortir(Joueur joueur)
    {

    }
}
