using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CinemachineFreeLook cam;
    public UIController uiController;
    public GameObject
        player,
        attackHitbox,
        inventory;
    public HealthBar 
        PlayerHealthBar,
        EnemyHealthBar;
    public List<TextMeshProUGUI>
        potions,
        swords;
    private int gamesPlayed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gamesPlayed = PlayerPrefs.GetInt("games played", 0);
        gamesPlayed++;
        PlayerPrefs.SetInt("games played", gamesPlayed);
    }
}
