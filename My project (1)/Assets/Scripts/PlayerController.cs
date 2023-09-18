using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;

    private float movementX;
    private float movementY;

    public AudioClip sucessSound;
    private AudioSource audioSource;

    public TextMeshProUGUI timerText;
    public float startingTime = 25f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        time = startingTime;
        SetTimerText();
        
    }

    void SetTimerText(){
        timerText.text = "Time left: " + time.ToString("f0");
        if(time <= 0){
            SceneManager.LoadScene("DeadEnd");
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Points: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
            SceneManager.LoadScene("DeadEnd");
        }
        if (transform.position.y < 0.0f)
        {
            SceneManager.LoadScene("DeadEnd");
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        time = time - Time.deltaTime;
        SetTimerText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;



            SetCountText();
            audioSource.PlayOneShot(sucessSound);
        }
    }
}