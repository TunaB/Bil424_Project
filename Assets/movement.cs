using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 7.5f;
     float shootForce = 100;
    float doubleSpeed;
    float oldSpeed;
    public GameObject arrow;
    float hp = 100;
    public int arrowCount = 10;
    public Transform arrowSpawn;
    public GameObject HitObject = null;
    public bool HasHit = false;
    bool aimed = false;
    public float Length = 30;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float minFov = 15f;
    public float maxFov = 90f;
    public float sensitivity = 10f;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    static Vector3 startPos;
    public static bool canMove = true;
    public float velocity = 2;
    public float forceMultiplier = 200;
    public Rigidbody rb;
    public Animator animator;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    int mode;
    float manaCooldown;
    public double stamina;
    public int mana = 100;
    bool staminaRecovery = false;
    bow currentBow;
    staff currentStaff;
    sword currentSword;
    List<GameObject> relicList;
    // Start is called before the first frame update
    void Start()
    {
        relicList = new List<GameObject>();
        currentBow = new bow(1,8,"multi");
        currentStaff = new staff(1, 8, "fire");

        currentSword = new sword(1, 8,  "fire");

        manaCooldown = Time.time;
        doubleSpeed = speed * 2;
        oldSpeed = speed;
        mode = 1;
        stamina = 100;
        characterController = GetComponent<CharacterController>();

        animator = gameObject.GetComponent<Animator>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void checkStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && staminaRecovery == false)
        {
            if (stamina > 0)
            {
                speed = doubleSpeed;
                stamina -= Time.deltaTime * 10;
            }
            else
            {
                staminaRecovery = true;
            }

        }
        else
        {
            speed = oldSpeed;
            if (stamina < 100)
            {
                if (staminaRecovery && stamina > 10)
                {
                    staminaRecovery = false;
                }
                stamina += Time.deltaTime * 10;
            }
        }
    }
    void moveAndCamera()
    {
        float fov = Camera.main.fieldOfView;
        fov += -Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
        if (characterController.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }

    }
    void setMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mode = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mode = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mode = 2;
        }
    }
    GameObject FindHit()
    {
        RaycastHit hitinfo;
        var mask = 8;
        var hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, Length);
        HasHit = false;
        foreach (RaycastHit hit in hits)
        {
            if(hit.transform.gameObject.tag.Equals("equipment")){
                HasHit = true;
                GameObject go= hit.transform.gameObject;
                equipmentValues qv = go.GetComponent<equipmentValues>();
                Debug.Log(qv.type + " " + qv.altType);
                return hit.transform.gameObject;
            }
        }
        return null;
            
    }
    public void enemyKilled()
    {
        arrowCount += 5;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("enemy"))
        {
            hp -= 40;
            Vector3 vector = gameObject.transform.position- collision.gameObject.transform.position ;
            vector.y = 0;
            vector.Normalize();
            vector = vector * 3;
            //Debug.Log(vector.x+" "+ vector.y+" "+ vector.z);
            characterController.Move(vector);
        }
    }
    void equip(GameObject item)
    {
        //receive item
        equipmentValues values = item.GetComponent<equipmentValues>();
        if (values.type.Equals("sword"))
        {
            currentSword = new sword(values.level,values.dmg,values.altType);
        }
        else if (values.type.Equals("bow"))
        {
            currentBow = new bow(values.level, values.dmg, values.altType);
        }
        else if (values.type.Equals("staff"))
        {
            currentStaff = new staff(values.level, values.dmg, values.altType);
        }
        else if(values.type.Equals("relic"))
        {
            relicList.Add(item);
            //add relic effects to player
        }
        Destroy(item);
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            //fail
            //check relicList to permanent relics
            //final screen
        }
        checkStamina();
        moveAndCamera();
        setMode();
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            GameObject item =FindHit();
            if (HasHit)
            {
                equip(item);
                Debug.Log(item.GetComponent<equipmentValues>().level);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowGrenade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            useManaPotion();
        }
        if (Input.GetMouseButtonDown(1))
        {
            switch (mode)
            {
                case 0://sword
                    //shield
                    break;
                case 1://bow
                    bowAim();
                    //aim
                    break;
                case 2://staff
                    Debug.Log("2");
                    break;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            switch (mode)
            {
                case 0://sword
                    
                    break;
                case 1://bow
                    unbowAim();
                    //aim
                    break;
                case 2://staff
                    Debug.Log("2");
                    break;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            switch (mode)
            {
                case 0:
                    //attack
                    break;
                case 1:
                    if (aimed)
                    {
                        bowShoot();
                    }
                    //attack if aimed
                    break;
                case 2:
                    shootStaff();
                    //attack
                    break;
            }
        }
    }
    void useManaPotion()
    {
        if(Time.time> manaCooldown)
        {
            mana += 20;
            if (mana > 100)
                mana = 100;
            manaCooldown = Time.time + 2;
        }
       
    }
    void bowShoot()
    {
        if (arrowCount <= 0)
        {
            return;
        }
        Quaternion rotation = Quaternion.Euler(90, 0, 0);

        rotation *= gameObject.transform.rotation;
        arrow.GetComponent<arrow>().dmg = currentStaff.dmg;
        arrow.GetComponent<arrow>().type = 0;
        arrow.GetComponent<arrow>().altType = currentBow.type;
        RaycastHit hitinfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitinfo,Mathf.Infinity))
        {
            HasHit = true;
            //HitDistance = (hitinfo.point - transform.position).magnitude;
            HitObject = hitinfo.transform.gameObject;
            
            GameObject shot = Instantiate(arrow, arrowSpawn.position, rotation);
            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.velocity = (hitinfo.point - arrowSpawn.position).normalized* shootForce;
            Destroy(shot, 5);
            if (currentBow.type.Equals("multi"))
            {
                shot = Instantiate(arrow, arrowSpawn.position, gameObject.transform.rotation);
                rb = shot.GetComponent<Rigidbody>();
                Vector3 xxx = (hitinfo.point - arrowSpawn.position).normalized;
                xxx = Quaternion.Euler(0, 15, 0) * xxx;
                rb.velocity = xxx * (shootForce );
                Destroy(shot, 5);
                shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
                rb = shot.GetComponent<Rigidbody>();
                xxx = (hitinfo.point - arrowSpawn.position).normalized;
                xxx = Quaternion.Euler(0, -15, 0) * xxx;
                rb.velocity = xxx * (shootForce );
                Destroy(shot, 5);
            }
        }
        else
        {
            GameObject shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward.normalized * shootForce;
            Destroy(shot, 5);
            if (currentBow.type.Equals("multi"))
            {
                shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
                rb = shot.GetComponent<Rigidbody>();
                Vector3 xxx = Camera.main.transform.forward.normalized;
                xxx = Quaternion.Euler(0, 15, 0) * xxx;
                rb.velocity = xxx * (shootForce);
                Destroy(shot, 5);
                shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
                rb = shot.GetComponent<Rigidbody>();
                xxx = Camera.main.transform.forward.normalized;
                xxx = Quaternion.Euler(0, -15, 0) * xxx;
                rb.velocity = xxx * (shootForce);
                Destroy(shot, 5);
            }
        }
        arrowCount--;
        
    }
    void unbowAim()
    {
        float fov = Camera.main.fieldOfView;
        fov = fov *2;

        Camera.main.fieldOfView = fov;
        aimed = false;
    }
    void bowAim()
    {
        float fov = Camera.main.fieldOfView;
        fov = fov/2;
        
        Camera.main.fieldOfView = fov;
        aimed = true;
    }
    void shootStaff()
    {
        if (mana < 10)
        {
            return;
        }
        arrow.GetComponent<arrow>().dmg = currentStaff.dmg;
        arrow.GetComponent<arrow>().type =1;
        arrow.GetComponent<arrow>().altType = currentStaff.type;

        GameObject shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
        Rigidbody rb = shot.GetComponent<Rigidbody>();
        GameObject enemy = FindClosestEnemy();
        if (enemy == null)
        {
            //shoot at aimed
            rb.velocity = Camera.main.transform.forward.normalized * (shootForce / 3);
            if (currentStaff.type.Equals("multi")){
                shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
                rb = shot.GetComponent<Rigidbody>();
                Vector3 xxx = Camera.main.transform.forward.normalized;
                xxx = Quaternion.Euler(0, 15, 0) * xxx;
                rb.velocity = xxx * (shootForce / 3);
                shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
                rb = shot.GetComponent<Rigidbody>();
                xxx = Camera.main.transform.forward.normalized;
                xxx = Quaternion.Euler(0, -15, 0) * xxx;
                rb.velocity = xxx * (shootForce / 3);
            }
        }
        else {
            rb.velocity = (enemy.transform.position - arrowSpawn.position).normalized * (shootForce / 3);
            if (currentStaff.type.Equals("multi"))
            {
                shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
                rb = shot.GetComponent<Rigidbody>();
                Vector3 xxx = (enemy.transform.position - arrowSpawn.position).normalized;
                xxx = Quaternion.Euler(0, 15, 0) * xxx;
                rb.velocity = xxx * (shootForce / 3);
                shot = Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
                rb = shot.GetComponent<Rigidbody>();
                xxx = (enemy.transform.position - arrowSpawn.position).normalized;
                xxx = Quaternion.Euler(0, -15, 0) * xxx;
                rb.velocity = xxx * (shootForce / 3);
            }
        }

        mana -= 10;
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    void ThrowGrenade()
    {
        //get current selected greande
        //get camera forward
        //create bomb object and launch
        //detonate
    }
    void temp()
    {
        /*if (Input.GetKey(KeyCode.W))
       {
           if (Input.GetKey(KeyCode.A))
           {
               transform.position += -transform.right * Time.deltaTime * velocity;
               transform.position += transform.forward * Time.deltaTime * velocity;
               animator.Play("f_left");
           }

           else if (Input.GetKey(KeyCode.D))
           {
               transform.position += transform.right * Time.deltaTime * velocity;
               transform.position += transform.forward * Time.deltaTime * velocity;
               animator.Play("f_right");
           }
           else
           {
               transform.position += transform.forward * Time.deltaTime * velocity;
               animator.Play("forward");
           }

       }
       else if (Input.GetKey(KeyCode.S))
       {
           if (Input.GetKey(KeyCode.A))
           {
               transform.position += -transform.right * Time.deltaTime * velocity;
               transform.position += -transform.forward * Time.deltaTime * velocity;
               animator.Play("b_left");
           }

           else if (Input.GetKey(KeyCode.D))
           {
               transform.position += transform.right * Time.deltaTime * velocity;
               transform.position += -transform.forward * Time.deltaTime * velocity;
               animator.Play("b_right");
           }
           else
           {
               transform.position += -transform.forward * Time.deltaTime * velocity;
               animator.Play("backward");
           }

       }
       else if (Input.GetKey(KeyCode.A))
       {
           transform.position += -transform.right * Time.deltaTime * velocity;
           animator.Play("left");
       }

       else if (Input.GetKey(KeyCode.D))
       {
           transform.position += transform.right * Time.deltaTime * velocity;
           animator.Play("right");
       }
       else if (Input.GetKey(KeyCode.Space) && isGrounded)
       {
           rb.AddForce(jump * jumpForce, ForceMode.Impulse);
           isGrounded = false;
           animator.Play("jump");
       }
       if (!(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
       {
           animator.Play("Idle");
       }*/
    }

    class bow
    {
        public int level;
        public float dmg;

        public string type;
        public bow(int level,float dmg, string type)
        {
            this.level = level;
            
            this.dmg = dmg;
            this.type = type;
        }

    }
    class sword
    {
        public int level;
        public float dmg;

        public string type;
        public sword(int level, float dmg,  string type)
        {
            this.level = level;
            
            this.dmg = dmg;
            this.type = type;
        }

    }
    class staff
    {
        public int level;
        public float dmg;

        public string type;
        public staff(int level, float dmg, string type)
        {
            this.level = level;
            
            this.dmg = dmg;
            this.type = type;
        }

    }
}
