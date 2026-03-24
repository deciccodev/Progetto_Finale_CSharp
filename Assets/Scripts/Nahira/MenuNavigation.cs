using UnityEngine;
using System.Collections.Generic;
public class MenuNavigation : MonoBehaviour
{
    [Header("Lista di tutti i pannelli del menu")]
    public List<GameObject> tuttiIPannelli;

    // Funzione principale per cambiare pannello
    public void OpenPanel(GameObject pannelloDaAprire)
    {
        // 1. Spegne tutti i pannelli nella lista
        foreach (GameObject p in tuttiIPannelli)
        {
            if (p != null)
            {
                p.SetActive(false);
            }
        }
        // 2. Accende solo quello che abbiamo passato come parametro
        if (pannelloDaAprire != null)
        {
            pannelloDaAprire.SetActive(true);
        }
    }
    // All'avvio del gioco, mostra solo il Main Menu
    void Start()
    {
        if (tuttiIPannelli.Count > 0)
        {
            OpenPanel(tuttiIPannelli[0]); // Il primo della lista deve essere il MainMenu
        }
    }
}