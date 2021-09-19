using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    GameObject pauseMenu;

    void Start()
    {
        pauseMenu = transform.Find("Pause Menu").gameObject;
        CursorLock();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                CursorLock();
            }   
            else
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                CursorUnlock();
            }
        }
    }

    public void CursorLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CursorUnlock()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
