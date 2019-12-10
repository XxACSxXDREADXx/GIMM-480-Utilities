using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    //14. Respawning if the player falls off the world
    public class respawn : MonoBehaviour
    {
        public float threshold; //Variable that is changed in the inspector to set the y variable the player must pass

        void FixedUpdate()
        {
            if (transform.position.y < threshold)
            {
                transform.position = new Vector3(0, 0, 0); //Fuction that if the player places the y variable the player is then placed at the default location
            }
            
        }
    }
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //7 Basic Player Movement
    public class PlayerMovement : MonoBehaviour
    {
        private float speed = 10f; //speed variable
        private float jumpForce = 8f; //jump variable
        private float gravity = 30f; //gravity factor
        private Vector3 moveDir = Vector3.zero; //vector3 that controls what direction player moves

        void Update()
        {
            //Setting up new player controller game object
            CharacterController controller = gameObject.GetComponent<CharacterController>();

            if (controller.isGrounded)
            {
                //takes the input and uses it to move the character
                moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                moveDir = transform.TransformDirection(moveDir);

                moveDir *= speed;

                //Player Will Jump
                if (Input.GetButtonDown("Jump"))
                {
                    moveDir.y = jumpForce;
                }
            }
            moveDir.y += gravity * Time.deltaTime;

        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //15 Sound Manager
    public class SoundManager : MonoBehaviour
    {
        // Audio players components.
        public AudioSource EffectsSource;
        public AudioSource MusicSource;

        // Random pitch adjustment range.
        public float LowPitchRange = .95f;
        public float HighPitchRange = 1.05f;

        // Singleton instance.
        public static SoundManager Instance = null;

        // Initialize the singleton instance.
        private void Awake()
        {
            // If there is not already an instance of SoundManager, set it to this.
            if (Instance == null)
            {
                Instance = this;
            }
            //If an instance already exists, destroy whatever this object is to enforce the singleton.
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);
        }

        // Play a single clip through the sound effects source.
        public void Play(AudioClip clip)
        {
            EffectsSource.clip = clip;
            EffectsSource.Play();
        }

        // Play a single clip through the music source.
        public void PlayMusic(AudioClip clip)
        {
            MusicSource.clip = clip;
            MusicSource.Play();
        }

        // Play a random clip from an array, and randomize the pitch slightly.
        public void RandomSoundEffect(params AudioClip[] clips)
        {
            int randomIndex = Random.Range(0, clips.Length);
            float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

            EffectsSource.pitch = randomPitch;
            EffectsSource.clip = clips[randomIndex];
            EffectsSource.Play();
        }

    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //16 Animation Controller Exapmple

    void Update()
    {
        // Basic set of animations tied to ints
        anim.SetInteger("unitState", 0); // 0 is Idle
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetInteger("unitState", -1); // -1 is WalkBack
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("unitState", 1); // 1 is run forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("unitState", 2); // 2 is jump
                                             //Jump code here. for example
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //10 Destroying a GameObject over a network
    //This is an example that I found and tried to use to destroy a projectile after it was shot

    public class Example : NetworkBehaviour
    {
        void NetworkDestroy(GameObject Object)
        {
            //Get the NetworkIdentity assigned to the object
            NetworkIdentity id = Object.GetComponent<NetworkIdentity>();
            // Check if we successfully got the NetworkIdentity Component from our object, if not we return(essentially do nothing).
            if (id == null) return;
            // First check if the objects NetworkIdentity can be transferred, or if it is server only.
            if (id.localPlayerAuthority == true)
            {
                // Do we already own this NetworkIdentity? If so, don't do anything.
                if (id.hasAuthority == false)
                {
                    // If we do not already have authority over the NetworkIdentity, assign authority.
                    // Keep in mind, using connectionToClient to get this NetworkIdentity is only valid for Network Player Objects.
                    if (id.AssignClientAuthority(connectionToClient) == true)
                    {
                        // If takeover was successful, we can now destroy our GameObject.
                        Network.Destroy(Object);
                    }
                }
                else
                {
                    // Do nothing because we already have ownership of this NetworkIdentity.
                }
            }
            else
            {
                //Server only, so we can't do anything.
            }
        }
    }

}
