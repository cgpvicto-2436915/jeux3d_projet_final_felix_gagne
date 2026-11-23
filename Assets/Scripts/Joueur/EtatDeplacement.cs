using UnityEngine;

public class EtatDeplacement : EtatJoueur
{
    /// <summary>
    /// Commence le deplacement du joueur
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    public override void OnEnter(Joueur joueur)
    {
        joueur.PouvoirBouger();
    }

    /// <summary>
    /// Tourne le joueur
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    /// <returns></returns>
    public override EtatJoueur OnExecuter(Joueur joueur)
    {
        if (joueur.ModeUi)
        {
            return new EtatUi();
        }
        else if (joueur.ModeCamera)
        {
            return new EtatCamera();
        }

        joueur.Tourner();
        return this;
    }

    /// <summary>
    /// arretre le deplacement du joueur
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    public override void OnSortir(Joueur joueur)
    {
        joueur.ArreterPouvoirBouger();
    }
}
