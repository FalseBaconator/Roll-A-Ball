using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MoveBall : MonoBehaviour
{

    public float Speed;
    public TextMeshProUGUI HPText;
    public int StartHP;
    public TextMeshProUGUI ScoreText;
    public int MaxScore;
    public GameObject FinishMessage;
    public TextMeshProUGUI FinishText;
    public GameObject WinMessage;
    public GameObject LoseMessage;

    private Rigidbody rb;
    private float MovementX;
    private float MovementY;

    private int ScoreCount;
    private int HPCount;

    private bool Won;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        HPCount = StartHP;
        ScoreCount = 0;
        SetHPText();
        SetScoreText();
        FinishMessage.SetActive(false);
        WinMessage.SetActive(false);
        LoseMessage.SetActive(false);
        Won = false;
    }

    void OnMove(InputValue MovementValue)
    {
        Vector2 MovementVector = MovementValue.Get<Vector2>();
        MovementX = MovementVector.x;
        MovementY = MovementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 Move = new Vector3(MovementX, 0, MovementY);

        rb.AddForce(Move * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            ScoreCount++;
            SetScoreText();
        }else if (other.gameObject.CompareTag("Obstacles") && Won==false)
        {
            HPCount = HPCount - 1;
            SetHPText();
        }else if (other.gameObject.CompareTag("InstaDeath") && Won == false)
        {
            HPCount = 0;
            SetHPText();
        }else if (other.gameObject.CompareTag("End Game"))
        {
            FinishText.text = "You Finished With A Score of " + ScoreCount.ToString() + "/20";
            FinishMessage.SetActive(true);
            Won = true;
            if(ScoreCount >= MaxScore)
            {
                WinMessage.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
    }

    void SetScoreText()
    {
        ScoreText.text = "Score: " + ScoreCount.ToString();
    }

    void SetHPText()
    {
        HPText.text = "Health: " + HPCount.ToString();
        if(HPCount <= 0)
        {
            gameObject.SetActive(false);
            LoseMessage.SetActive(true);
        }
    }
}
