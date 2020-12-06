using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Menu
    [SerializeField] private PauseMenu pm = null;

    //Aspect
    private SpriteRenderer hood;
    private SpriteRenderer body;
    private SpriteRenderer armLeft;
    private SpriteRenderer armRight;
    [SerializeField] private PlayerColorManager pcm = null;

    //Movements
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] Transform groundCheck = null;
    [SerializeField] LayerMask whatIsGround = default;

    private float moveInput;
    private readonly float checkRadius = 0.5f;
    private readonly float speed = 3;
    private readonly float jumpForce = 5;

    private int extraJumps = 0;
    private readonly int extraJumpsValue = 1;

    private bool isFacingRight = true;
    private bool isGrounded = false;

    //Inventory
    public event EventHandler OnInventoryChanged;
    private List<Key> keyList;
    private int dices;
    [SerializeField] private TimeManager time = null;
    [SerializeField] private GameObject genericSound = null;

    // property
    public List<Key> KeyList
    {
        get
        {
            return keyList;
        }
    }

    public int DicesNumber
    {
        get
        {
            return dices;
        }
    }

    private void Awake()
    {
        keyList = new List<Key>();
        dices = PlayerPrefs.GetInt("Dices");
        hood     = GameObject.Find("Head/Hood").GetComponent<SpriteRenderer>();
        body     = GameObject.Find("Body").GetComponent<SpriteRenderer>();
        armLeft  = GameObject.Find("Arm left").GetComponent<SpriteRenderer>();
        armRight = GameObject.Find("Arm right").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb       = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hood.color     = pcm.IsPlayerColor;
        body.color     = pcm.IsPlayerColor;
        armLeft.color  = pcm.IsPlayerColor;
        armRight.color = pcm.IsPlayerColor;
    }

    private void FixedUpdate()
    {
        // Moving
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

        // Facing left or right
        if (isFacingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (isFacingRight == true && moveInput < 0)
        {
            Flip();
        }

        // Jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void Update()
    {
        // Jumps
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            extraJumps = extraJumpsValue;
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            animator.SetTrigger("lower");
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }


        // Change time
        if (ContainsAtLeastOne() && Input.GetKeyUp(KeyCode.T))
        {
            time.ChangeTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if showInventory, then show the stored inventory
        if (other.CompareTag("ShowInventory"))
        {
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        }

        // if key, take the key and remove it from the scene
        if (other.CompareTag("Key"))
        {
            Key key = other.GetComponent<Key>();
            AddKey(key);
            Destroy(other.gameObject);
            Instantiate(genericSound, other.transform.position, Quaternion.identity);
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        }

        // if collectible, take the collectible and remove it from the scene
        if (other.CompareTag("Dices"))
        {
            dices++;
            Destroy(other.gameObject);
            Instantiate(genericSound, other.transform.position, Quaternion.identity);
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        }

        // if door, check if there is an available key to open the door
        if (other.CompareTag("Door"))
        {
            Door door = other.GetComponent<Door>();
            if (ContainsKeyType(door.IsDoorKeyType))
            {
                RemoveOneKey(door.IsDoorKeyType);
                Instantiate(genericSound, other.transform.position, Quaternion.identity);
                door.OpenPassage();
                OnInventoryChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // if portal, check if there is an available key to open the portal
        // and the time is present, otherwise do nothing
        if (other.CompareTag("Portal"))
        {
            if (ContainsKeyType(Key.KeyType.Green) && time.IsPresent)
            {
                Portal portal = other.GetComponent<Portal>();
                RemoveOneKey(Key.KeyType.Green);
                Instantiate(genericSound, other.transform.position, Quaternion.identity);
                portal.OpenPassage();
                OnInventoryChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // if enemy, the player is dead and the pause menu appear
        if (other.CompareTag("Enemy"))
        {
            pm.DeadPlayer();
            Destroy(gameObject);
        }

        // if transmitter, check if it active and change the KeyType from
        // Red to Green from the keyList
        if (other.CompareTag("Transmitter"))
        {
            Transmitter transmitter = other.GetComponent<Transmitter>();
            if (transmitter.IsActive)
            {
                if (ContainsKeyType(Key.KeyType.Red) && isGrounded)
                {
                    animator.SetTrigger("lookBack");
                    ColorListRedToGreen();
                    OnInventoryChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // if level manager, leave the player go into the portal
        if (other.CompareTag("LevelManager"))
        {
            LevelManager lm = other.GetComponent<LevelManager>();
            PlayerPrefs.SetInt("Dices", dices);
            lm.NextLevel();
        }
    }

    // flip the facing of the character
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


    ///// Inventory /////

    // add a kye to the keyList, no matter the KeyType
    public void AddKey (Key key)
    {
        keyList.Add(key);
    }

    // remove one key that has a specific KeyType
    public void RemoveOneKey(Key.KeyType keyType)
    {
        keyList.Remove(FindKeyType(keyType));
    }

    // check if there is at least one key in the keyList
    public bool ContainsAtLeastOne()
    {
        return keyList.Count > 0;
    }

    // check if there is at leat one key that has a specific KeyType
    public bool ContainsKeyType(Key.KeyType keyType)
    {
        foreach (Key k in keyList)
        {
            if (k.IsKeyType == keyType)
            {
                return true;
            }
        }
        return false;

    }

    // find a key that has a specific KeyType
    private Key FindKeyType(Key.KeyType keyType)
    {
        foreach (Key k in keyList)
        {
            if (k.IsKeyType == keyType)
            {
                return k;
            }
        }
        return null;
    }

    // set the KeyType Green to all the keys with the KeyType Red
    private void ColorListRedToGreen()
    {
        foreach (Key key in keyList)
        {
            if (key.IsKeyType == Key.KeyType.Red)
            {
                key.IsKeyType = Key.KeyType.Green;
            }
        }
    }
}
