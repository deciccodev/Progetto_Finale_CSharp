using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MazeQuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text tooltipText;

    private string[] arrAnswers;
    private string[] arrTooltip;
    private int _indexAnswer;
    private int _playerGuess;
    private bool _isCorrect;
    public TMP_Text optionA;
    public TMP_Text optionB;
    public TMP_Text optionC;
    public TMP_Text optionD;


    [SerializeField] private FormQuestion _formQuestion;
    [SerializeField] private QuizController _quizController;
    [SerializeField] private GameObject _maze;
    [SerializeField] private GameObject _player;

    void Awake()
    {
        tooltipText.gameObject.SetActive(false);
        _playerGuess = -1;
    }

    private void OnEnable()
    {
        _playerGuess = -1;
        PlayerTrigger.EventTriggered += GetEventTrig;
    }

    private void OnDisable()
    {
        PlayerTrigger.EventTriggered -= GetEventTrig;
    }



    public void ActiveMaze()
    {
        //Attivo labirinto e player torna su SetActive(true);
        _maze.SetActive(true);
        _player.gameObject.SetActive(true);
    }

    public void LoadQuestion(FormQuestion formQuestion)
    {
        //Carico tutte le info necessarie per la domanda

        _formQuestion = formQuestion;
        
        tooltipText.text = "";

        questionText.text = _formQuestion.question;
        arrAnswers = new string[] {_formQuestion.answerA,_formQuestion.answerB,_formQuestion.answerC,_formQuestion.answerD};
        arrTooltip = new string[] {_formQuestion.tooltipA,_formQuestion.tooltipB,_formQuestion.tooltipC,_formQuestion.tooltipD};
        
        optionA.gameObject.SetActive(true);
        optionB.gameObject.SetActive(true);
        optionC.gameObject.SetActive(true);
        optionD.gameObject.SetActive(true);
        
        optionA.text = $"A. {arrAnswers[0]}";
        optionB.text = $"B. {arrAnswers[1]}";
        optionC.text = $"C. {arrAnswers[2]}";
        optionD.text = $"D. {arrAnswers[3]}";

        _indexAnswer = _formQuestion.rightAnswer;
        tooltipText.gameObject.SetActive(false);

        ActiveMaze();
    }

    public void CheckSubmit()
    {
        if(_playerGuess >= 0)
        {
            //Che dall'indice ricevuto dal listener, se è quello corretto 
            // si procede col resto
            Debug.Log($" risposta: {arrAnswers[_playerGuess]}");

            if (_playerGuess == _indexAnswer)
                {
                    Debug.Log("Risposta corretta!");
                    optionA.gameObject.SetActive(false);
                    optionB.gameObject.SetActive(false);
                    optionC.gameObject.SetActive(false);
                    optionD.gameObject.SetActive(false);

                    tooltipText.gameObject.SetActive(true);
                    tooltipText.text = $"Esatto! {arrTooltip[_indexAnswer]}";
                    tooltipText.color = Color.green;
                    _isCorrect = true;
                }

            else
                {
                    Debug.Log($"Risposta sbagliata: {arrAnswers[_indexAnswer]}");
                    optionA.gameObject.SetActive(false);
                    optionB.gameObject.SetActive(false);
                    optionC.gameObject.SetActive(false);
                    optionD.gameObject.SetActive(false);

                    tooltipText.gameObject.SetActive(true);
                    tooltipText.text = $"Peccato era {arrAnswers[_indexAnswer]} \ntip: {arrTooltip[_indexAnswer]}";
                    tooltipText.color = Color.red;
                    _isCorrect = false;
                }

            _player.gameObject.SetActive(false);
            

            NextQuestion();
            

        }
    }

    public void NextQuestion()
    {
        _quizController.RispostaData(_isCorrect);

    }

    private void GetEventTrig(int id)
    {
        _playerGuess = id;

        CheckSubmit();
    }

}
