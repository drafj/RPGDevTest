using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject finishPanel;
    private GameManager manager;
    private bool finish;
    private float giroSpeed;

    private void Start()
    {
        manager = GameManager.instance;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("HomeScene");
        Time.timeScale = 1;
    }

    public void End(string result)
    {
        Color color = new Color();
        if (result == "you win")
            color = Color.green;
        else
            color = Color.red;

        finishPanel.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = result;
        finishPanel.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
        finishPanel.SetActive(true);
        finish = true;
    }

    private void Update()
    {
        if (finish)
        {
            GameManager.instance.cam.m_YAxis.m_InputAxisName = "";
            GameManager.instance.cam.m_XAxis.m_InputAxisName = "";
            GameManager.instance.cam.m_YAxis.Value = 0.6f;
            GameManager.instance.cam.m_XAxis.m_InputAxisValue = -0.1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject inv = manager.inventory;
            if (inv.activeSelf)
            {
                inv.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                manager.cam.m_XAxis.m_InputAxisName = "Mouse X";
                manager.cam.m_YAxis.m_InputAxisName = "Mouse Y";
            }
            else
            {
                inv.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                manager.cam.m_XAxis.m_InputAxisValue = 0;
                manager.cam.m_YAxis.m_InputAxisValue = 0;
                manager.cam.m_XAxis.m_InputAxisName = "";
                manager.cam.m_YAxis.m_InputAxisName = "";
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !finish)
        {
            if (pauseMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
