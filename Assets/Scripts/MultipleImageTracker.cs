using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultipleImageTracker : MonoBehaviour
{
   public  ARTrackedImageManager trackedImageManager;
   public  PlasticGenerator      plasticGenerator;
   private bool                  _moveOn;
   public  string                firstTrackedTag;
   private bool                  isFirstTracked;
   
   private void Start()
   {
      plasticGenerator.GeneratePlastics();
      _moveOn        = true;
      isFirstTracked = true;
   }

   private void Update()
   {
 
   }

   public void EnableMoveOn()
   {
      _moveOn        = true;
      GameManager.sCurRoundCount++;
      isFirstTracked  = true;
      firstTrackedTag = "";
   }

   private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
   {
      if (_moveOn == false)
      {
         return;
      }
      
      foreach (ARTrackedImage trackedImage in eventArgs.added)
      {
         UpdateSpawnObject(trackedImage);
      }

      foreach (ARTrackedImage trackedImage in eventArgs.updated)
      {
         UpdateSpawnObject(trackedImage);
      }

      foreach (ARTrackedImage trackedImage in eventArgs.removed)
      {
         
      }
      
   }

   private void UpdateSpawnObject(ARTrackedImage trackedImage)
   {
      //Debug.Log("UpdateSpawnObject called");
      string referenceImageName = trackedImage.referenceImage.name;
      
      StartCoroutine(UpdateCoroutine(referenceImageName));
   }

   IEnumerator UpdateCoroutine(string nname)
   {
      if (isFirstTracked)
      {
         firstTrackedTag = nname;
         isFirstTracked  = false;
      }
      
      plasticGenerator.RemoveObjectsWithTag(nname, firstTrackedTag);
      yield return new WaitForSeconds(2.0f);
      plasticGenerator.GeneratePlastics();
      _moveOn        = false;
   }

   private void OnEnable()
   {
      trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
   }

   private void OnDisable()
   {
      trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
   }
}
