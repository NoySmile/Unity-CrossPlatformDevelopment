using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunkMovementBehaviour : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var botBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        border = new Rect(leftBorder, rightBorder, topBorder, botBorder);
        StartCoroutine(GameLoop());
        GetComponent<BackPackBehaviour>().onBackPackAddItem.AddListener(SetOwner);
    }


    private static readonly int HORIZONTAL = Animator.StringToHash("Horizontal");
    private static readonly int VERTICAL = Animator.StringToHash("Vertical");
    private static readonly int SPEED = Animator.StringToHash("Speed");
    public float user_speed = 5f;
    public float dist;
    public Rect border;

    private int loop = 0;
    public bool GameOver = false;

    private IEnumerator GameLoop()
    {
        while(!GameOver)
        {
            loop++;
            yield return StartCoroutine(PlayerLoop());

        }
        yield return null;
    }

    void SetOwner(Item item)
    {
        item.SetOwner(GetComponent<CharacterBehaviour>());
    }
    private IEnumerator PlayerLoop()
    {
        dist = (transform.position - Camera.main.transform.position).z;

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        
        user_speed = (PlayerInput.isInputBlock) ? 10f : 4f;
        
        var velocity = new Vector3(h, v, 0) * user_speed;
        //transform.localPosition += velocity * Time.deltaTime;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var botBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        var x = Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
        var y = Mathf.Clamp(transform.position.y, botBorder, topBorder);
        //transform.position = new Vector3(x, transform.position.y, 0);

        Vector3 dir = Vector3.zero;
        if(x <= leftBorder)
            dir = Vector3.left;
        if(x >= rightBorder)
            dir = Vector3.right;
        if (y <= botBorder)
            dir = Vector3.down;
        if(y >= topBorder)
            dir = Vector3.up;
        
        if(!cameramove && dir != Vector3.zero)
        { 
            cameramove = true;
            rb.velocity = Vector3.zero;
            var timer = 0f;
            var total = 1f;
            var startpos = Camera.main.transform.position;
            var offset = (Mathf.Abs(dir.y) > 0) ? roomheight : roomwidth;
            var endpos = Camera.main.transform.position + (dir * offset);

            while(timer <= total)
            {
                Camera.main.transform.position = Vector3.Lerp(startpos, endpos, timer / total);
                yield return timer += Time.deltaTime;
            }

            cameramove = false;
        }

        rb.velocity = velocity;
        anim.SetFloat(HORIZONTAL, h);
        anim.SetFloat(VERTICAL, v);
        anim.SetFloat(SPEED, velocity.magnitude);
        yield return null;
    }

    public float roomwidth = 71f;
    public float roomheight = 40f;
    private bool cameramove = false;
}
