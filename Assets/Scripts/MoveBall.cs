using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MoveBall : MonoBehaviour
{

    public float Speed;
    public TextMeshProUGUI ScoreText;
    public int MaxScore;
    public GameObject WinMessage;

    private Rigidbody rb;
    private float MovementX;
    private float MovementY;

    private int ScoreCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ScoreCount = 0;
        SetScoreText();
        WinMessage.SetActive(false);
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
        }
    }

    void SetScoreText()
    {
        ScoreText.text = "Score: " + ScoreCount.ToString();
        if(ScoreCount >= MaxScore)
        {
            WinMessage.SetActive(true);
        }
    }

}
