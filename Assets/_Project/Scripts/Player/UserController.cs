using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;

    public float jumpSpeed = 5.0f;

    float sprint = 2f;

    Animator animator;

    const float locomotionAnimationSmoothTime = 0.1f;

    public GameObject inventoryTabUI;
    public GameObject loadOutUI;
    public GameObject lootUI;
    public GameObject vendorUI;

    public bool isStandingStill;
    public bool isJumping;
    public bool isFishing;
    public bool isSittingOnChair;
    public bool isAttacking;
    public bool isGoingBackwards;
    public bool isInMenu = false;

    GameObject myUseObject = null;
    Outline outLine = null;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInMenu)
        {
            return;
        }

        Attack();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OutLine();

        Menu();

        if (isInMenu)
        {
            return;
        }

        if (this.animator.GetCurrentAnimatorStateInfo(1).IsName("Armature|Attack"))
        {
            AttackEnded();
        }

        Movement();

        Sprint();

        Rotation();

        Jumping();

        Sitting();

        Fishing();

        Use();

        Talk();
    }

    private void Menu()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            isInMenu = !isInMenu;

            if (isInMenu)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            Camera.main.GetComponent<CameraController>().isInMenu = isInMenu;

            inventoryTabUI.SetActive(isInMenu);

            loadOutUI.SetActive(isInMenu);

            vendorUI.SetActive(false);

            lootUI.SetActive(false);
        }
    }

    private void OutLine()
    {
        myUseObject = SingleRayCast();

        if (myUseObject != null && myUseObject.GetComponent<Outline>() != null)
        {
            outLine = myUseObject.GetComponent<Outline>();
            outLine.OutlineWidth = 2f;
        }
        else if (outLine != null)
        {
            outLine.OutlineWidth = 0f;
        }
    }

    private void Use()
    {
        GameObject useObject = null;
        if (Input.GetButtonDown("Use"))
        {
            useObject = SingleRayCast();
        }

        if(useObject == null)
        {
            return;
        }
        else
        {
            if(useObject.tag == "Chair")
            {
                gameObject.transform.position = useObject.transform.position;
                gameObject.transform.rotation = useObject.transform.rotation;
                Sit();
            }
            if(useObject.tag == "Chest")
            {
                isInMenu = !isInMenu;

                if (isInMenu)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }

                Camera.main.GetComponent<CameraController>().isInMenu = isInMenu;

                inventoryTabUI.SetActive(isInMenu);

                loadOutUI.SetActive(false);

                vendorUI.SetActive(false);

                lootUI.SetActive(isInMenu);

                lootUI.GetComponent<InventoryView>().SynchInventory(useObject.GetComponent<Chest>().chestInventory);
            }
            if(useObject.tag == "Vendor")
            {
                isInMenu = !isInMenu;

                if (isInMenu)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }

                Camera.main.GetComponent<CameraController>().isInMenu = isInMenu;

                inventoryTabUI.SetActive(isInMenu);

                loadOutUI.SetActive(false);

                lootUI.SetActive(false);

                vendorUI.SetActive(isInMenu);

                vendorUI.GetComponent<ScrollRect>().content.GetComponent<VendorInventoryView>().SynchInventory(useObject.GetComponent<VendorInventory>());
            }
        }

        // Do different Use function with IUseable interface on objects with Use() function.
    }

    private void Talk()
    {
        GameObject useObject = null;
        if (Input.GetButtonDown("Talk"))
        {
            useObject = SingleRayCast();
        }
        if (useObject == null)
        {
            return;
        }
        else if(useObject.tag == "Vendor" || useObject.tag == "NPC")
        {
            useObject.GetComponent<NPC>().TriggerDialogue(gameObject.GetComponent<Player>());
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking && !isFishing && !isSittingOnChair)
        {
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }

    public void AttackEnded()
    {
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    private GameObject SingleRayCast()
    {
        GameObject useObject = null;

        Vector3 origin = transform.position + new Vector3(0f,1f,0f);
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction * 2f, Color.red);

        RaycastHit hit;

        if(Physics.Raycast(origin, direction, out hit, 2f))
        {
            useObject = hit.collider.gameObject;
        }

        return useObject;
    }

    private void Movement()
    {
        float translation = (Input.GetAxis("Vertical") * speed * Time.deltaTime) / sprint;

        float speedPercent = 0f;
        speedPercent = Mathf.Abs(translation / 0.2f);

        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        transform.Translate(0, 0, translation);

        if (translation == 0f & !isJumping)
        {
            isStandingStill = true;
        }
        else
        {
            isStandingStill = false;
            animator.SetBool("isStartedFishing", false);
            isFishing = false;

            isSittingOnChair = false;
            animator.SetBool("isSitting", false);
        }
    }

    private void Sprint()
    {
        sprint = 2f;
        if (Input.GetButton("Sprint") && !isGoingBackwards)
        {
            sprint = 1f;
        }
    }

    private void Jumping()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isJumping = true;
            isStandingStill = false;
            animator.SetBool("isStartedFishing", false);
            isFishing = false;
        }
    }

    private void Rotation()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        Rotate(rotation);
    }

    private void Rotate(float rotation)
    {
        transform.Rotate(0f, rotation, 0f);
    }

    private void Sitting()
    {
        if (Input.GetKeyDown(KeyCode.N) && isStandingStill)
        {
            Sit();
        }
    }

    private void Sit()
    {
        isSittingOnChair = true;
        animator.SetBool("isSitting", true);
    }

    private void Fishing()
    {
        if (Input.GetKeyDown(KeyCode.F) && isStandingStill)
        {
            isFishing = true;
            animator.SetBool("isStartedFishing", true);
        }
    }
}