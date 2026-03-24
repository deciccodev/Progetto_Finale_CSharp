using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputUsername : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public Button bottoneUsername;

    [SerializeField] private string _nextScene;

    void Start()
    {
        //usernameInput.onSubmit.AddListener(GameManager.Instance.NameToSave);
        bottoneUsername.onClick.AddListener(InserisciNome);
    }

    public void InserisciNome()
    {
        GameManager.Instance.NameToSave(usernameInput.text);
        GameManager.Instance.SaveData();
        Debug.Log("Salvato il nome da input");
        GameManager.Instance.NextScene(_nextScene);
    }

}
