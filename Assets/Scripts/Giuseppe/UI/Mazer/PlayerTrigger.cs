using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private int _idAnswer;
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _panelManager;

    public static event Action<int> EventTriggered;


   void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.CompareTag("PlayerInTheMaze"))
        {
            //_player.gameObject.SetActive(false);
            Debug.Log($"Trigger avvenuto sul collider {_idAnswer}");
            
            Submit();
        }
   }

    public void Submit()
    {
        //Qui la logica che manda una notifica al panelManager per dire 
        // che abbiamo triggerato una risposta, mandiamo l'id della risposta al manager 
        
        EventTriggered?.Invoke(_idAnswer);
        Debug.Log($"Submitting id: {_idAnswer}");
    }
}
