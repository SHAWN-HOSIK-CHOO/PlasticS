using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   public void ToGame()
   {
      SceneManager.LoadScene("BasicScene");
   }

   public void ToExit()
   {
      Application.Quit();
      #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
      #endif
   }
}
