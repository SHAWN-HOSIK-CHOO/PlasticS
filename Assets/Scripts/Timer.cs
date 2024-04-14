using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
   public float time  { get; set; }
   public bool  Reset { get; set; }
   
   public void StartTimer()
   {
      StartCoroutine(Counter());
   }

   public void ResetTimerTo10()
   {
      time = 10.0f;
   }

   IEnumerator Counter()
   {
      while (time > 0.0f)
      {
         time -= Time.deltaTime;
         yield return null;
      }

      time = 0.0f;
   }
}
