using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class character : MonoBehaviour
{
    public float speed = 10f;
    public float maxSpeed = 10f;
    public bool grounded, plataforma, muerte;
    public float jumpPower = 5f;

    public int vidas;
    public Text textovidas ;

    public int objrecog;
    public Text textomonedas ;

    private Animator animate;
    private bool jump;
    private bool doublejump;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        vidas = 3;
        objrecog = 0;
        textovidas.text = vidas.ToString();
        textomonedas.text = objrecog.ToString();

    }

    // Update is called once per frame
    void Update()
    {

        animate.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
        animate.SetFloat("y", Mathf.Abs(rb2d.velocity.y));
        animate.SetBool("grounded", grounded);
        animate.SetBool("Plataforma", plataforma);

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rb2d.AddForce(Vector3.right * speed);
            /* transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
          */
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector3(maxSpeed, rb2d.velocity.y, 0);
            }
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            rb2d.AddForce(Vector3.left * speed);
            /*   transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
             */
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector3(-maxSpeed, rb2d.velocity.y, 0);
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded || plataforma)
            {
                jump = true;
                doublejump = true;
            }

            else if (doublejump)
            {
                jump = true;
                doublejump = false;
            }
        }
        if (grounded || plataforma)
        {

            doublejump = true;
        }

        if (vidas <= 0)
        {
            Application.LoadLevel(1);
        }
        if (objrecog >= 5)
        {
            Application.LoadLevel(2);
        }
    }
    private void FixedUpdate()
    {
        if (jump)
        {
            rb2d.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Suelo_escenario")
        {
            grounded = true;
            animate.SetBool("grounded", true);
        }
        if (collision.transform.tag == "Plataforma")
        {
            grounded = true;
            this.transform.parent = collision.transform;
            animate.SetBool("grounded", true);

        }
        if (collision.transform.tag == "moneda")
        {
            
            objrecog++;
            textomonedas.text = objrecog.ToString();
            
        }
        if (collision.transform.tag == "enemy")
        {
            vidas--;
            textovidas.text = vidas.ToString();
            
            
        }
        if (collision.transform.tag == "muerte")
        {
            vidas--;
            textovidas.text = vidas.ToString();
            transform.position = new Vector3(-4.801f, 1.74f, -1);
        }
       
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Suelo_escenario")
        {
            grounded = false;
            animate.SetBool("grounded", false);
        }
       
        if (collision.transform.tag == "Plataforma")
        {
            grounded = false;
            this.transform.parent = null;
            animate.SetBool("grounded", false);
        }

    }

    
}

