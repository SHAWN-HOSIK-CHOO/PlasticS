using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
   public TextMeshProUGUI credits;

   private void Awake()
   {
      credits.gameObject.SetActive(false);
   }

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

   public void OpenCredits()
   {
      StartCoroutine(ActivateCredits());
   }

   IEnumerator ActivateCredits()
   {
      credits.text = "HS CHOO";
      credits.gameObject.SetActive(true);
      yield return new WaitForSeconds(1.5f);
      credits.gameObject.SetActive(false);
   }
}
