using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class CrazyDriver : MonoBehaviour
{
    Rigidbody sportsCar;

    AudioSource carAudioSource;
    [SerializeField] AudioClip crashSoundEffect;
    [SerializeField] GameObject crashEffect;
    [SerializeField] GameObject[] guns;
    [SerializeField] float forwardSpeed;
    [SerializeField] float backwardSpeed;
    [SerializeField] float rotationSpeed;
    bool isControlEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        crashEffect.SetActive(false);
        sportsCar = GetComponent<Rigidbody>();
        carAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
        fbmovement();
        rotation();
        ProcessFiring();

        }
    }
    private void fbmovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            sportsCar.AddRelativeForce(Vector3.forward * forwardSpeed);
            print("moving");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            sportsCar.AddRelativeForce(Vector3.back * backwardSpeed);
            print("moving");
        }

    }
    private void rotation()
    {
        sportsCar.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))//hit A to move left
        {
            transform.Rotate(Vector3.down * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))//hit D to move right
        {
            transform.Rotate(Vector3.up * rotationSpeed);
        }
        sportsCar.freezeRotation = false;
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
        
            case "Finish":
                print("YOU WIN!!");
              
                SceneManager.LoadScene(1);
                break;


            case "lose":
                print("Wasted");
                crashEffect.SetActive(true);
                isControlEnabled = false;
                carAudioSource.PlayOneShot(crashSoundEffect);
                Invoke("ReloadScene", 5f);
                break;
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }
    void ProcessFiring()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }
    void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;

        }
    }


}
