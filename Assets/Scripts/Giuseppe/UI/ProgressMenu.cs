using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMenu : MonoBehaviour
{
    //[SerializeField] private string _progressType;
    private int _progressAmount;
    [SerializeField] QuizController starterQuiz;
    public Button buttonVar;
    public Button buttonCicli;
    public Button buttonOop;
    public Button buttonDesignP;
    public Button buttonMethod;

    private Button[] arrayButton;

    // Credo un array di stringhe con i nomi in json per far partire il questionario
    private string[] arrayJsonString = {"DomandeTipiDiDato.json","DomandeCicliCondizioni.json","DomandeMetodi.json","DomandeOOP.json","DomandeDesignPattern.json"};


   void Awake()
   {
        //Inizializzo la lista;
        arrayButton = new Button[] {buttonVar, buttonCicli, buttonMethod, buttonOop, buttonDesignP};
   }

   void Start()
   {
        //Prendo lo stato di avanzamento del gioco dal game manager 
        _progressAmount = GameManager.Instance.GetProgress();

        SoloMode();

        arrayButton[_progressAmount].onClick.AddListener(StartQuestions);
   }

   //Metodo per l'attivazione della modalità SOLO, lasciamo interagibile solo il bottone che corrisponde al progresso del giocatore
   public void SoloMode()
    {

        for (var i = 0; i < arrayButton.Length; i++)
        {
            if(i != _progressAmount)
            {
                arrayButton[i].interactable = false ;
                Debug.Log($"Bottone {arrayButton[i]} disattivato");
            }

        }
        
    }

    public void StartQuestions()
    {
        starterQuiz.SelezionaArgomento(arrayJsonString[_progressAmount]);
        Debug.Log($"Argomento selezionato {arrayJsonString[_progressAmount]}, avvio il questionario.");
    }

}
