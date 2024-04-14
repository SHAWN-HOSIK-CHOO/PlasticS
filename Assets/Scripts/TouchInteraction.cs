using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchInteraction : MonoBehaviour
{
    [SerializeField] private Camera     _camera;
    private                  RaycastHit _hitObj;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Change GetMouseButton and Input.mousePosition when building application
        if (Input.GetMouseButton(0))
        {
            Ray        ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out _hitObj))
            {
                ARInteractiveObject arInteractiveObject = _hitObj.transform.GetComponent<ARInteractiveObject>();

                if (arInteractiveObject)
                {
                    StartCoroutine(SceneSwapAction());
                }
            }
        }
    }

    private bool EncounterAction()
    {
        _hitObj.transform.localScale *= 2.0f;
        return true;
    }
    
    IEnumerator SceneSwapAction()
    {
        //Debug.Log("Waiting ...");
        yield return new WaitUntil(EncounterAction);
        SceneManager.LoadScene("Encounter1");
    }
}
