using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
   public static readonly int  sMaxRoundCount   = 2;
   public static          int  sCurRoundCount   = 1;
   public static          bool sMoveToNextRound = false;
   public static          int  sScore           = 0;
   public static          int  sExtraScoreCount  = 0;

   public TextMeshProUGUI roundText;
   public TextMeshProUGUI specialScore;
   public TextMeshProUGUI displayTotalScoreText;

   private void Start()
   {
      specialScore.gameObject.SetActive(false);
      displayTotalScoreText.gameObject.SetActive(false);
   }

   private void Update()
   {
      roundText.text = "Round : "+ sCurRoundCount.ToString()+ "  Score : " + sScore.ToString();

      if (sExtraScoreCount >= 2)
      {
         StartCoroutine(ExtraScoreTMP());
      }

      if (sCurRoundCount > sMaxRoundCount)
      {
         StartCoroutine(ToEndScene());
      }
   }

   IEnumerator ExtraScoreTMP()
   {
      sScore            += sExtraScoreCount - 1;
      specialScore.text =  "Additional Score! + " + (sExtraScoreCount - 1).ToString();
      sExtraScoreCount   =  0;
      specialScore.gameObject.SetActive(true);
      yield return new WaitForSeconds(1.5f);
      specialScore.gameObject.SetActive(false);
   }

   IEnumerator ToEndScene()
   {
      sCurRoundCount             = 0;
      displayTotalScoreText.text = "Total Score : " + sScore.ToString() + " !!";
      sScore                     = 0;
      displayTotalScoreText.gameObject.SetActive(true);
      yield return new WaitForSeconds(2.0f);
      displayTotalScoreText.gameObject.SetActive(false);
      SceneManager.LoadScene("EndScene");
   }
}
