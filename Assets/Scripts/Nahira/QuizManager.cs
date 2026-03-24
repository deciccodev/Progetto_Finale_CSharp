using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager istanza;
    public string difficoltaScelta;

    void Awake()
    {
        if (istanza == null) {
            istanza = this;
            DontDestroyOnLoad(gameObject); 
        } else {
            Destroy(gameObject);
        }
    }

    public void ImpostaDifficolta(string nomeDifficolta)
    {
        difficoltaScelta = nomeDifficolta;
        Debug.Log("Difficoltà salvata: " + difficoltaScelta);
    }
}