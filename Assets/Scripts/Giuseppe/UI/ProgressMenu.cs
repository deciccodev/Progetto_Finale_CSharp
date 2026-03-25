using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMenu : MonoBehaviour
{
    private int _progressAmount;

    //[SerializeField] GameManager gameManager;
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
        Debug.Log("Start!!!");
        SoloMode();

        
   }

   //Metodo per l'attivazione della modalità SOLO, lasciamo interagibile solo il bottone che corrisponde al progresso del giocatore
   public void SoloMode()
    {
        //Prendo lo stato di avanzamento del gioco dal game manager 
        _progressAmount = GameManager.Instance.GetProgress();
        Debug.Log($"Progresso gM {GameManager.Instance.GetProgress()}");
        Debug.Log($"valore progress:  {_progressAmount}");

        for (var i = 0; i < arrayButton.Length; i++)
        {
            int index = i;
            if(index != _progressAmount)
            {
                arrayButton[index].interactable = false ;
                Debug.Log($"Bottone {arrayButton[index]} disattivato");
            }

        }
        ResetBottone();
    }

    public void ResetBottone()
    {
        arrayButton[_progressAmount].onClick.RemoveAllListeners();
        arrayButton[_progressAmount].interactable = true;
        arrayButton[_progressAmount].onClick.AddListener(StartQuestions);
    }

    public void StartQuestions()
    {
        //Prendo lo stato di avanzamento del gioco dal game manager 
        //_progressAmount = gameManager.GetProgress();

        starterQuiz.SelezionaArgomento(arrayJsonString[_progressAmount]);
        Debug.Log($"Argomento selezionato {arrayJsonString[_progressAmount]}, avvio il questionario.");
    }

}
