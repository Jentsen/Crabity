using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjClick : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Book;
    public GameObject Gear;
    public GameObject Cam;
    [SerializeField] AudioSource HoverSound;
    private ChangeScene ChangeSceneScript;
    public int SceneNumber;
    bool stepOff = true; // keeps audio from being played repetedly

    public bool hoveringP = false;
    public bool hoveringS = false;
    public bool hoveringC = false;

    Vector3 initPos;

    private void Start()
    {
        ChangeSceneScript = Cam.GetComponent<ChangeScene>();
        Cam.transform.position = new Vector3(0.35f, 0.8f, 3.94f);
    }
    void Update()
    {
        PlayAnim();
        if (Input.GetMouseButtonDown(0)) 
        {
            GameObject Name = GetClickedObject(out RaycastHit hit);
            if (Cube == Name)
            {
                SceneNumber = 1;
                ChangeSceneScript.ChangeTheScene = true;
                Cube.GetComponent<AudioSource>().Play();
                Destroy(Book.transform.parent.gameObject);
                Destroy(Gear.transform.parent.gameObject);
                //Gets rid of the NameTags too this way
                
            }
            else if (Book == Name) //Change this to an exit button
            {
                Application.Quit();
            }
            else if (Gear == Name)
            {
                SceneNumber = 2;
                ChangeSceneScript.ChangeTheScene = true;
                Gear.GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            //Debug.Log("Mouse Off");
        }
    }
    GameObject GetClickedObject(out RaycastHit hit)
    {
        //Checks if Obj is clicked on tapped on, and returns it if so
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
        }
        return target;
    }
    private bool isPointerOverUIObject()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }

    void PlayAnim() 
    {
        GameObject Bot = GetClickedObject(out RaycastHit hit);
        if (stepOff) {
            stepOff = false;
            if (Cube == Bot)
                {
                    hoveringP = true;
                    HoverSound.Play();         
                }
            else if (Book == Bot)
                {
                    hoveringC = true;
                    HoverSound.Play();         
                }
            else if (Gear == Bot)
                {
                    hoveringS = true;
                    HoverSound.Play();         
                }
            else 
            {   
                hoveringP = false;
                hoveringS = false;
                hoveringC = false;
                HoverSound.Stop();
                stepOff = true;
            }
        }
        if (Bot == null) {
                hoveringP = false;
                hoveringS = false;
                hoveringC = false;
                HoverSound.Stop();
                stepOff = true;
        }
    }
}
