using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    enum Look
    {
        left,  //0
        right, //1
        up,    //2
        down,  //3
        none   //4
    }

    public Rigidbody2D rb;
    public Animator anim;
    public GameObject alert;
    public GameObject flashlight;
    public float speed;
    public float walkDist;
    public float lookDist;
    public float lookRadius;

    private Vector2 rayDirection;
    private Vector3 playerPos;
    private float init, posA, posB;
    private float chaseSpeed;
    private int lookDir = (int)Look.left;
    private bool seePlayerEh = false;
    private bool chasePlayerEh = false;
    private bool goingAEh = true;
    private bool waitingEh = false;
    private bool moveToPlayerEh = false;
    private bool goToEndScreenEh = false;
    private bool runningCrEh;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        init = gameObject.transform.position.x;
        posA = init - walkDist;
        posB = init + walkDist;
        chaseSpeed = speed * 1.05f;
    }

    private void FixedUpdate() {
        moveCharacter();
        animations();
        //if(Input.GetMouseButtonDown(0))
        //{
        //    GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        //}
    }

    void moveCharacter() {
        {
        rayDirection = Vector2.zero;
        if (lookDir == (int)Look.left) rayDirection = Vector2.left;
        if (lookDir == (int)Look.right) rayDirection = Vector2.right;
        if (lookDir == (int)Look.down) rayDirection = Vector2.down;
        if (lookDir == (int)Look.up) rayDirection = Vector2.up;


        if (!seePlayerEh) {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, lookRadius, rayDirection, lookDist);
            chasePlayerEh = false;

            foreach (RaycastHit2D thingHit in hit) {
                if (thingHit.collider.tag == "Player") {
                    //player got hit or seen
                    playerPos = thingHit.transform.position;
                        if (!runningCrEh) StartCoroutine(shakeCamPls());
                    chasePlayerEh = true;
                } 
                //if (thingHit.collider.tag == "Environment") {
                //    if(Mathf.Abs(thingHit.transform.position.x - transform.position.x) < 4) {
                //        rb.velocity = Vector2.zero;
                //        if (true)
                //        {
                //                justGoBack();
                //            //if (lookDir == (int)Look.left) StartCoroutine(turnUp());
                //            //else StartCoroutine(turnDown());
                //            //waitingEh = true;
                //        }
                //        //chasePlayerEh = false;
                //        break;
                //    }
                //}
            }
                //if (!chasePlayerEh)
                //{
                //    GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                //}

                GameObject l = Instantiate(flashlight, transform.position, Quaternion.identity);
                l.transform.parent = transform;

                l.transform.position = new Vector3((rayDirection * lookDist / 2).x, 1f, 1f);
                l.transform.localScale = new Vector3(lookDist, 1f, 1f);
            Debug.DrawRay(transform.position, rayDirection * lookDist);
        }

        if (seePlayerEh) {
            Vector2 targetPlayer = playerPos - transform.position;
            targetPlayer.Normalize();
            RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, lookRadius, targetPlayer, lookDist);
            bool findPlayerEh = false;
            foreach (RaycastHit2D thing in hit)
            {
                if (thing.collider.tag == "Player") {
                    playerPos = thing.transform.position;
                    findPlayerEh = true;
                }
            }
            if (!findPlayerEh) chasePlayerEh = false;
            Debug.DrawRay(transform.position, targetPlayer * lookDist);
        }

            if (chasePlayerEh) seePlayerEh = true;
            else
            {
                seePlayerEh = false;
            }
        rb.velocity = Vector2.zero;
            if (seePlayerEh)
            {
                if (!moveToPlayerEh) StartCoroutine(moveToPlayerCR());
                else moveToPlayer();
            }
            else if (goingAEh)
            {
                //GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                moveToPointA();
            }
            else
            {
                //GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                moveToPointB();
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().shakeCamEh = seePlayerEh;
        }
    }

    void moveToPlayer() {        //chase player fn
        lookDir = (int)Look.none;
        Vector2 target = playerPos - transform.position;
        target.Normalize();

        rb.velocity = target * chaseSpeed;
        recalculatePos();
    }

    void recalculatePos() {
        init = gameObject.transform.position.x;
        posA = init - walkDist;
        posB = init + walkDist;
    }

    void moveToPointA() {
        //CameraShake.StopShake();
        moveToPlayerEh = false;
        if (transform.position.x <= posA + 0.2f && transform.position.x >= posA - 0.2f) {
            rb.velocity = Vector2.zero;
            //reached position
            if (!waitingEh) {
                StartCoroutine(turnUp());
                waitingEh = true;
            }
        }
        else {
            lookDir = (int)Look.left;
            Vector2 target = new Vector2(posA - init, 0);
            target.Normalize();
            rb.velocity = target * speed;
        }
    }

    void moveToPointB() {
        //CameraShake.StopShake();
        moveToPlayerEh = false;
        if (transform.position.x <= posB + 0.2f && transform.position.x >= posB - 0.2f){
            rb.velocity = Vector2.zero;
            //reached position
            if (!waitingEh)
            {
                StartCoroutine(turnDown());
                waitingEh = true;
            }
        }
        else {
            lookDir = (int)Look.right;
            Vector2 target = new Vector2(posB - init, 0);
            target.Normalize();
            rb.velocity = target * speed;
        }
    }

    void animations() {
        if (!seePlayerEh) {
            if (lookDir == (int)Look.left) {
                anim.SetBool("lookLeft", true);
                anim.SetBool("lookUp", false);
                anim.SetBool("lookDown", false);
            }
            if (lookDir == (int)Look.right) {
                anim.SetBool("lookLeft", false);
                anim.SetBool("lookUp", false);
                anim.SetBool("lookDown", false);
            }
            if (lookDir == (int)Look.down) {
                anim.SetBool("lookLeft", true);
                anim.SetBool("lookUp", false);
                anim.SetBool("lookDown", true);
            }
            if (lookDir == (int)Look.up) {
                anim.SetBool("lookLeft", true);
                anim.SetBool("lookUp", true);
                anim.SetBool("lookDown", false);
            }
        }
        else {
            Vector2 target = playerPos - transform.position;
            target.Normalize();
            if(Mathf.Abs(target.x) > Mathf.Abs(target.y) && target.x > 0) {
                //right
                anim.SetBool("lookLeft", false);
                anim.SetBool("lookUp", false);
                anim.SetBool("lookDown", false);
            }
            if (Mathf.Abs(target.x) > Mathf.Abs(target.y) && target.x < 0) {
                //left
                anim.SetBool("lookLeft", true);
                anim.SetBool("lookUp", false);
                anim.SetBool("lookDown", false);
            }
            if (Mathf.Abs(target.x) < Mathf.Abs(target.y) && target.y > 0) {
                //up
                anim.SetBool("lookLeft", true);
                anim.SetBool("lookUp", true);
                anim.SetBool("lookDown", false);
            }
            if (Mathf.Abs(target.x) < Mathf.Abs(target.y) && target.y < 0) {
                //down
                anim.SetBool("lookLeft", true);
                anim.SetBool("lookUp", false);
                anim.SetBool("lookDown", true);
            }
        }
    }

    void justGoBack()
    {
        goingAEh = !goingAEh;
        waitingEh = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Environment")
        {
            seePlayerEh = false;
            chasePlayerEh = false;
            //recalculatePos();
        }
        if(collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            CameraShake.StopShake();
            //Play animation here

            //Go to end game screen.
            StartCoroutine(endGame());
        }
    }

    IEnumerator endGame()
    {
        yield return new WaitForSecondsRealtime(1.2f);
        //GameObject.Find("info").GetComponent<DontDestroyThis>().victory = true;       //uncomment
        GameObject.Find("LevelChanger").GetComponent<LeverChangerScript>().fadeToEnd();
    }

    IEnumerator goBack() {
        yield return new WaitForSecondsRealtime(0.5f);
        goingAEh = !goingAEh;
        waitingEh = false;
    }

    IEnumerator turnUp() {
        yield return new WaitForSecondsRealtime(0.5f);
        lookDir = (int)Look.up;
        lookDist /= 2;
        yield return new WaitForSecondsRealtime(0.5f);
        lookDir = (int)Look.right;
        lookDist *= 2;
        StartCoroutine(goBack());
    }

    IEnumerator turnDown() {
        yield return new WaitForSecondsRealtime(0.5f);
        lookDir = (int)Look.down;
        lookDist /= 2;
        yield return new WaitForSecondsRealtime(0.5f);
        lookDir = (int)Look.left;
        lookDist *= 2;
        StartCoroutine(goBack());
    }

    IEnumerator moveToPlayerCR()
    {
        Vector3 alertLocation = new Vector3(transform.position.x + 0.8f, transform.position.y + 0.8f, -3f);
        Instantiate(alert, alertLocation, Quaternion.identity);
        SoundManagerScript.PlaySound("spotted");
        //CameraShake.StartShake();
        yield return new WaitForSecondsRealtime(0.3f);
        moveToPlayerEh = true;
    }

    IEnumerator shakeCamPls()
    {
        runningCrEh = true;
        while (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().shakeCamEh)
        {
            GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        }
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        yield return null;
        runningCrEh = false;
    }
}
