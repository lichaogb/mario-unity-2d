using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class mario : MonoBehaviour {

    public int speed;

    public float Jumpforce;

    Vector2 bodySize;

    public float Tspeed;

    bool controlable = true;

    float squatTime; // 下蹲计时
    float ohno;//失败后延迟计时

    public AudioClip jump;
    public AudioClip hit;
    bool sound = false;

    Vector2 offsetC;
    Vector2 offsetN;

	// Use this for initialization
	void Start () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        bodySize = GetComponent<BoxCollider2D>().size;
        GetComponent<BoxCollider2D>().offset = offsetN;
        offsetC = new Vector2(offsetN.x,offsetN.y - 0.5f); 

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (stopped())
        {


        }



         Tspeed = GetComponent<Rigidbody2D>().velocity.x;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");

        }


        if (!onGround())
        {
            GetComponent<Animator>().SetBool("running", false);
            GetComponent<Animator>().SetBool("jumping", true);
            

        }
        else {
            GetComponent<Animator>().SetBool("jumping",false);
            
        }



        if (Input.GetKeyDown(KeyCode.UpArrow) && onGround() &&controlable)//跳跃
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Jumpforce));
            AudioSource.PlayClipAtPoint(jump,gameObject.transform.position);


        }
        else if (Input.GetKey(KeyCode.DownArrow) && controlable) // 下蹲
        {

            GetComponent<BoxCollider2D>().size = new Vector2(bodySize.x, bodySize.y * 0.5f);
            GetComponent<BoxCollider2D>().offset = offsetC;
           GetComponent<Animator>().SetBool("squating", true);
            GetComponent<Animator>().SetBool("running", false);
            squatTime = squatTime + Time.deltaTime * 1;
           // if (squatTime >= 1.5)
            //{
              //  rolling();
           // }

        }
        else

        {//取消下蹲
            GetComponent<Animator>().SetBool("squating",false);
            
            if (controlable&&onGround()) {
                GetComponent<BoxCollider2D>().size = new Vector2(bodySize.x, bodySize.y);
                GetComponent<BoxCollider2D>().offset = offsetN;
               GetComponent<Animator>().SetBool("running", true);
            }

            squatTime = 0;
        }

	}


    bool onGround() {//在地上
        Bounds bounds = GetComponent<Collider2D>().bounds;
        float range = bounds.size.y * 0.1f;
        Vector2 v = new Vector2(bounds.center.x, bounds.min.y - range);
        RaycastHit2D hit = Physics2D.Linecast(v, bounds.center);
        return(hit.collider.gameObject != gameObject);

    }

    bool stopped()// 是否被撞停
    {
        if (Tspeed <= 0&&onGround())
        {
            print("你撞晕了");
            GetComponent<Animator>().SetBool("done",true);
            
            controlable = false;
            GetComponent<Animator>().SetBool("running", false);
            hitmusic();
            ohno += Time.deltaTime * 1f;

            if (ohno > 1.5)
            {
                SceneManager.LoadScene("main");
            }
            return true;

        }
        return false;
    }

    

    void rolling()
    {
        controlable = false;
        GetComponent<BoxCollider2D>().size = new Vector2(bodySize.x, bodySize.y * 0.5f);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Finish")
        {
            SceneManager.LoadScene("finish");
        }
    }

    void hitmusic()//单例
    {
        if (sound)
        {

        }
        else {
            sound = true;
            AudioSource.PlayClipAtPoint(hit,gameObject.transform.position);

        }


    }

}
