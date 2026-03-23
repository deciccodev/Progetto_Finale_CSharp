using UnityEngine;

public partial class MenuNavigation : MonoBehaviour
{
    [Header("Riferimenti Pannelli")]
    public GameObject mainMenu;
    public GameObject difficultySelect;
    public GameObject topicSelect;
    public GameObject theory;

    // metodo per cambiare pannello
    public void OpenPanel(GameObject panelToOpen)
    {
        // spegniamo tutto per sicurezza
        mainMenu.SetActive(false);
        difficultySelect.SetActive(false);
        topicSelect.SetActive(false);
        theory.SetActive(false);

        // accendiamo quello richiesto
        panelToOpen.SetActive(true);
    }

    // shortcut per tornare indietro o al menu principale
    public void BackToMain() => OpenPanel(mainMenu);
}