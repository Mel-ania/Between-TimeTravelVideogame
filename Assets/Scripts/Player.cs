using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movements
    //private Animator anim;
    private Rigidbody2D rb;
    [SerializeField]
    Transform groundCheck = null;
    [SerializeField]
    LayerMask whatIsGround = default;

    private float moveInput;
    private readonly float checkRadius = 0.5f;
    private readonly float speed = 3;
    private readonly float jumpForce = 5;

    private int extraJumps = 0;
    private readonly int extraJumpsValue = 1;

    private bool isFacingRight = true;
    private bool isGrounded = false;
        
    //Inventory
    public event EventHandler OnKeysChanged;

    private List<Key> keyList;

    [SerializeField]
    private TimeManager time = null;
    
    // property
    public List<Key> KeyList
    {
        get
        {
            return keyList;
        }
    }

    private void Awake()
    {
        keyList = new List<Key>();
    }

    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Moving
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput == 0)
        {
            //anim.SetBool("isRunning", false);
        }
        else
        {
            //anim.SetBool("isRunning", true);
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
        if (isGrounded == true)
        {
            //anim.SetBool("isJumping", false);
            extraJumps = extraJumpsValue;
        }
        else
        {
            //anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            //anim.SetTrigger("takeOf");
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
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
        // if key, take the key and remove it from the scene
        if (other.CompareTag("Key"))
        {
            Key key = other.GetComponent<Key>();
            AddKey(key);
            Destroy(other.gameObject);
            OnKeysChanged?.Invoke(this, EventArgs.Empty);
        }

        // if door, check if there is an available key to open the door
        // and the time is present, otherwise do nothing
        if (other.CompareTag("Door"))
        {
            if (ContainsKeyType(Key.KeyType.Green) && time.IsPresent)
            {
                Door door = other.GetComponent<Door>();
                RemoveOneKey(Key.KeyType.Green);
                door.OpenDoor();
                OnKeysChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // if transmitter, check if it active and change the KeyType from
        // Red to Green from the keyList
        if (other.CompareTag("Transmitter"))
        {
            Transmitter transmitter = other.GetComponent<Transmitter>();
            if (transmitter.IsActive)
            {
                if (ContainsKeyType(Key.KeyType.Red))
                {
                    ColorListRedToGreen();
                    OnKeysChanged?.Invoke(this, EventArgs.Empty);
                }
            }
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
