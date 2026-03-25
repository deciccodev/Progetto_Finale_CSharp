using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Button bottoneConferma;
    [SerializeField] private Toggle[] toggles;

    [SerializeField] private string _nextScene;

    void Start()
    {
        bottoneConferma.onClick.AddListener(CaricaQuiz);
    }

    public void CaricaQuiz()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                GameManager.Instance.SetDifficulty(i);
                GameManager.Instance.SaveData();
                GameManager.Instance.NextScene(_nextScene);
                return;
            }
        }
    }

}
