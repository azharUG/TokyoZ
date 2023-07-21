using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool swing = false;
    private bool hasKey = false;
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    void Start()
    {
        Debug.Log("a");
        respawnPoint = transform.position;
        //Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("a");
        /*if(currentScene.name == "TrainStation" && hasKey)
        {
        you are trying to get the location of the player to save between scenes and the has key variable. you need to fix why start isnt running and you need to find a way to save data between scenes
        Check favorites bar on google
            Debug.Log("true");
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetMouseButtonDown(0))
        {
            swing = true;
            animator.SetBool("IsSwinging", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        swing = false;
        animator.SetBool("IsSwinging", false);
        animator.SetBool("IsJumping", false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "fallDetector")
        {
            transform.position = respawnPoint;
        }
        else if(col.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }

        if(col.tag == "key")
        {
            col.gameObject.SetActive(false);
            hasKey = true;
        }

        if(col.gameObject.name == "Ladders")
        {
            Debug.Log("hi");
        }
        if(col.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(col.tag == "Exit")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }
}
