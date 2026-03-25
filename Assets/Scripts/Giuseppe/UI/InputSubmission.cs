using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputSubmission : MonoBehaviour
{
    #region Proprietà di classe

    public TMP_InputField inputAnswer;
    public Button buttonSubmit;
    public TMP_Text questionText;
    public TMP_Text tooltipText;

    private string[] arrAnswers;
    private string[] arrTooltip;
    private int _indexAnswer;
    private bool _isCorrect;
    [SerializeField] private FormQuestion _formQuestion;
    [SerializeField] private QuizController _quizController;
    
    #endregion
   
   void Awake()
   {
        tooltipText.gameObject.SetActive(false);
        //Listener da bottone 
        buttonSubmit.onClick.AddListener(CheckSubmit);
        //Listener da tastiera
        inputAnswer.onSubmit.AddListener(CheckSubmit);
   }

    #region Caricamento dei dati necessari al controllo per la domanda 
    public void LoadQuestions(FormQuestion formQuestion)
    {
        _formQuestion = formQuestion;
        inputAnswer.text = "";
        tooltipText.text = "";
        questionText.text = _formQuestion.question;

        arrAnswers = new string[] {_formQuestion.answerA,_formQuestion.answerB,_formQuestion.answerC,_formQuestion.answerD};
        arrTooltip = new string[] {_formQuestion.tooltipA,_formQuestion.tooltipB,_formQuestion.tooltipC,_formQuestion.tooltipD};

        _indexAnswer = _formQuestion.rightAnswer;

        
    }

    #endregion

     #region Listener per i submit 
    public void CheckSubmit()
    {
        if(_formQuestion != null)
        {
            string textAnswer = inputAnswer.text.Trim().ToLower();
            Debug.Log($"testo della risposta: {textAnswer}");

            if (textAnswer == arrAnswers[_indexAnswer].ToLower())
            {
                Debug.Log("Risposta corretta!");
                tooltipText.gameObject.SetActive(true);
                tooltipText.text = $"Esatto! {arrTooltip[_indexAnswer]}";
                tooltipText.color = Color.green;
                _isCorrect = true;
            }
            else
            {
                Debug.Log($"Risposta sbagliata: {arrAnswers[_indexAnswer]}");
                tooltipText.gameObject.SetActive(true);
                tooltipText.text = $"Peccato era {arrAnswers[_indexAnswer]} \ntip: {arrTooltip[_indexAnswer]}";
                tooltipText.color = Color.red;
                _isCorrect = false;
            }
            NextQuestion();
        }
    }

    public void CheckSubmit(string text)
    {
        if(_formQuestion != null)
        {
            text.Trim().ToLower();
            Debug.Log($"testo della risposta: {text}");

            if (text == arrAnswers[_indexAnswer].ToLower())
                {
                    Debug.Log("Risposta corretta!");
                    tooltipText.text = arrTooltip[_indexAnswer];
                    tooltipText.color = Color.green;
                    _isCorrect = true;
                }
            else
                {
                    Debug.Log($"Risposta sbagliata: {arrAnswers[_indexAnswer]}");
                    tooltipText.gameObject.SetActive(true);
                    tooltipText.text = $"Peccato era {arrAnswers[_indexAnswer]} \ntip: {arrTooltip[_indexAnswer]}";
                    tooltipText.color = Color.red;
                    _isCorrect = false;
                }
            
            NextQuestion();
        }
    }

    public void NextQuestion()
    {
        _quizController.RispostaData(_isCorrect);
    }
#endregion

}
