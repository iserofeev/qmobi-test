using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private float skyBoxRotationSpeed = 0.9f;
    
    private static readonly int Rotation = Shader.PropertyToID("_Rotation");

    public Player player;

    private Camera _mainCamera;
    
    public TextMeshProUGUI pointsText, livesTest, recordText;
    public GameObject newRecordText;

    private AudioManager AudioManager;
    
    public static Plane PlayerPlane;
    public static Vector2 minWorldPoint, maxWorldPoint;

    public Animator canvasAnimator;

    public static bool isGameOver;
    public static GameManager Instance
    {
        get;
        private set;
    }

    private CapsuleCollider playerCollider;
    private int _playerPoints;
    public int _playerLives;

    private bool Invulnerable;

    private Animator playerAnimator;
    private static readonly int Blink = Animator.StringToHash("Blink");

    private int screenWidth, screenHeight;

    private void Awake()
    {
        playerCollider = player.GetComponent<CapsuleCollider>();

        AudioManager = GetComponent<AudioManager>();
        isGameOver = false;
        playerAnimator = player.GetComponent<Animator>();
        _playerLives = 3;
        livesTest.text = $"Lives: <b>{_playerLives}</b>";

        
        Instance = this;
        
        PlayerPlane = new Plane(Vector3.back, player.transform.position);
        _mainCamera = Camera.main;
        CalcWorldConstrains();
    }

    private void CalcWorldConstrains()
    {
        var ray = _mainCamera.ScreenPointToRay(Vector3.zero);

        float enter = 0.0f;

        if (PlayerPlane.Raycast(ray, out enter))
        {
            minWorldPoint = ray.GetPoint(enter);
        }

        ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));

        if (PlayerPlane.Raycast(ray, out enter))
        {
            maxWorldPoint = ray.GetPoint(enter);
        }
    }

    void Start()
    {
        RenderSettings.skybox.SetFloat(Rotation, Random.Range(0, 360));
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat(Rotation, Time.time * skyBoxRotationSpeed);

        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            CalcWorldConstrains();
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }
    }

    public void AddPlayerPoints(int points)
    {
        switch (points)
        {
            case 100:
            {
                AudioManager.PlayAudio("Collect1");
                return;
            }

            case 200:
            {
                AudioManager.PlayAudio("Collect2");
                return;
            }
                
        }
        _playerPoints += points;
        pointsText.text = $"Points: <b>{_playerPoints}</b>";
    }

    public void PlayerDamage()
    {
        AudioManager.PlayAudio("PlayerDamage");
        _playerLives--;
        
        livesTest.text = $"Lives: <b>{_playerLives}</b>";
        if (_playerLives == 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(PlayerInvulnerable());

        }
    }
    

    private void GameOver()
    {
        player.gameObject.SetActive(false);
        var hScore = PlayerPrefs.GetInt("highScore");
        if (_playerPoints > hScore)
        {
            AudioManager.PlayAudio("Win");
            newRecordText.SetActive(true);
            PlayerPrefs.SetInt("highScore", _playerPoints);
        }
        canvasAnimator.SetTrigger("GameOver");
        isGameOver = true;
        recordText.text = "Record: " + PlayerPrefs.GetInt("highScore");
        StopAllCoroutines();
    }

    IEnumerator PlayerInvulnerable()
    {
        Invulnerable = true;
        playerCollider.enabled = false;
        
        playerAnimator.SetBool(Blink, true);
        yield return new WaitForSeconds(3);
        playerAnimator.SetBool(Blink, false);

        playerCollider.enabled = true;
        Invulnerable = false;
    }

    public void Restart()
    {
        AudioManager.PlayAudio("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }


}
