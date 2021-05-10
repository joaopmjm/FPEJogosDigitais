using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class EndGame : MonoBehaviour
{
    public TMP_Text message;
    private void Start()
    {
        if(GameData.Won)
        {
            message.text = "You made it! you survived";
        }
        else
        {
            message.text = "You've died";
        }
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
