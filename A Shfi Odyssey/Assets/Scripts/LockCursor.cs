using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockCursor : MonoBehaviour
{
    public GameObject currentButton;
    
    // Start is called before the first frame update
    void Start()
    {
        // esc
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {

            CatchMouseClicks(currentButton);

        }
    }

    public void CatchMouseClicks(GameObject setSelection)
    {

        EventSystem.current.SetSelectedGameObject(setSelection);

    }
}

