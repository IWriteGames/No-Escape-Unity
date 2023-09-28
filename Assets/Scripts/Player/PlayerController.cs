using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private float moveSpeed, gravityModifier, jumpPower, runSpeed;
    private float mouseSensitivityX,  mouseSensitivityY, xRotation;
    private Vector3 moveInput;

    //Player
    public Transform camTrans;
    [SerializeField] CharacterController charCon;

    //Animations Player
    Animator animator;
    readonly int idAnimMoveSpeed = Animator.StringToHash("moveSpeed");
    readonly int idOnGround = Animator.StringToHash("onGround");

    //Jump
    public bool canJump;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask whatIsGround;

    //Weapon
    [SerializeField] private GameObject Glock18;
    [SerializeField] private GameObject MP18;
    private int numberWeaponActive;
    public Weapon weaponActive;
    public bool isAim;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        Instance = this;
        Glock18.SetActive(true);
        MP18.SetActive(false);
        weaponActive = Glock18.GetComponent<Weapon>();
        numberWeaponActive = 1;
    }

    void Update()
    {
        if(!PauseManager.Instance.IsPause && !GameManager.Instance.playerDefeat)
        {
            Movement();
            Look();
            Shoot();
            Reload();
            AimWeapon();
            ChangeWeapon();
            CheckAnimator();
        }
    }

    private void Movement()
    {
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * moveSpeed;
        }

        moveInput.y = yStore;

        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charCon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jumpPower;
        }
        charCon.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {
        mouseSensitivityX = PlayerPrefs.GetFloat(ConstantsGame.MouseHorizontalX);
        mouseSensitivityY = PlayerPrefs.GetFloat(ConstantsGame.MouseVerticalY);

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") * mouseSensitivityX, Input.GetAxisRaw("Mouse Y")) * mouseSensitivityY;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        xRotation -= mouseInput.y;
        xRotation = xRotation = Mathf.Clamp(xRotation, -65f, 65f);
        camTrans.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) 
        {
            if(weaponActive != null)
            {
                weaponActive.ShootWeapon();
            }
        }
    }

    private void AimWeapon()
    {
        weaponActive.AimWeapon();

        if (Input.GetMouseButton(1))
        {
            isAim = true;
        }
        else
        {
            isAim = false;
        }
    }

    private void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            weaponActive.ReloadWeapon();
        }
    }

    private void CheckAnimator()
    {
        animator.SetFloat(idAnimMoveSpeed, moveInput.magnitude);
        animator.SetBool(idOnGround, canJump);
    }

    private void ChangeWeapon()
    {
        if(!weaponActive.reload) 
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(numberWeaponActive == 1)
                {
                    Glock18.SetActive(false);
                    MP18.SetActive(true);
                    weaponActive = MP18.GetComponent<Weapon>();
                    numberWeaponActive = 2;
                } 
                else if(numberWeaponActive == 2)
                {
                    Glock18.SetActive(true);
                    MP18.SetActive(false);
                    weaponActive = Glock18.GetComponent<Weapon>();
                    numberWeaponActive = 1;
                }
                weaponActive.UpdateWeaponInfoUI();
            }
        }

    }

    public void UpdateWeaponInfoUI()
    {
        weaponActive.UpdateWeaponInfoUI();
    }

}
