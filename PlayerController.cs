using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public Animator animator;
    public SpriteRenderer playerSprites;

    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite topSprite;
    public Sprite bottomSprite;

    public ReactorSwitch reactorSwitch1;
    public ReactorSwitch reactorSwitch2;
    public Animator reactorAnimator;

    private Vector2 moveValue;

    private SwitchController sc;
    private Postit post;
    private PostitNote note;
    private ChemicalMixer mixer;
    private WarpButton warp;
    private ReactorSwitch reactor;

    private bool movingAllowed = true;

    private int lastDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (movingAllowed)
        {
            transform.Translate(new Vector3(moveValue.x, moveValue.y, 0) * moveSpeed * Time.deltaTime);

            if(moveValue.x==0 && moveValue.y==0)
            {
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", 0);
                animator.enabled = false;
                playerSprites.sprite = GetSprite();
            }
            else if(moveValue.x > .5f && moveValue.x != 0)
            {
                animator.enabled = true;
                animator.SetFloat("MoveX", 1);
                animator.SetFloat("MoveY", 0);
                lastDirection = 0;
            }
            else if(moveValue.x < .5f && moveValue.x != 0)
            {
                animator.enabled = true;
                animator.SetFloat("MoveX", -1);
                animator.SetFloat("MoveY", 0);
                lastDirection = 1;
            }
            else if(moveValue.y > .5f)
            {
                animator.enabled = true;
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", 1);
                lastDirection = 2;
            }
            else if(moveValue.y < .5f)
            {
                animator.enabled = true;
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", -1);
                lastDirection = 3;
            }
           
        }
    }

    public void SetMoveAllowed(bool allowed)
    {
        movingAllowed = allowed;
    }

    public void OnMove(InputValue value)
    {
        //Debug.Log("Moving");
        moveValue = value.Get<Vector2>();
    }

    public void OnMenu()
    {
        //Debug.Log("ToggleMenu");
        GameManager.Instance.ToggleMenu();
    }

    public void OnInteract()
    {
        //Debug.Log("Interact");

        if(sc != null)
        {
            sc.FlipSwitch();
        }
        else if(post != null)
        {
            post.UpdateCanvas();
        }
        else if(note != null)
        {
            note.UpdateCanvas();
        }
        else if(mixer != null)
        {
            mixer.Mix();
        }
        else if(warp != null)
        {
            if(warp.CheckReactorStatus())
            {
                GameManager.Instance.DisplayMessage("Warp core shutdown in progress");
                GameManager.Instance.ShutDownCore();
            }
            else
            {
                GameManager.Instance.DisplayMessage("Warp core has to much power to shut down safetly. Maybe I need to find a way to cut the power to it.");
            }
        }
        else if(reactor != null)
        {
            reactor.FlipSwitch();

            if (!reactorSwitch1.getSwitchStatus() && !reactorSwitch2.getSwitchStatus())
            {
                reactorAnimator.enabled = false;
            }
        }
    }

    private Sprite GetSprite()
    {
        if (lastDirection == 0)
        {
            return rightSprite;
        }
        else if (lastDirection == 1)
        {
            return leftSprite;
        }
        else if (lastDirection == 2)
        {
            return topSprite;
        }
        else if (lastDirection == 3)
        {
            return bottomSprite;
        }
        else return leftSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Switch")
        {
            sc = collision.gameObject.GetComponent<SwitchController>();
            GameManager.Instance.DisplayMessage("What happens if I pull this level?");
        }
        else if(collision.tag == "Postit")
        {
            post = collision.gameObject.GetComponent<Postit>();
            GameManager.Instance.DisplayMessage("I wonder if this note could be useful?");
        }
        else if(collision.tag == "Note")
        {
            note = collision.gameObject.GetComponent<PostitNote>();
        }
        else if (collision.tag == "Mixer")
        {
            mixer = collision.gameObject.GetComponent<ChemicalMixer>();
        }
        else if (collision.tag == "Warp")
        {
            warp = collision.gameObject.GetComponent<WarpButton>();
            GameManager.Instance.DisplayMessage("I wonder if presseing this button will stop the warp core?");
        }
        else if (collision.tag == "Reactor")
        {
            reactor = collision.gameObject.GetComponent<ReactorSwitch>();
            GameManager.Instance.DisplayMessage("I think this controls the reactor");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Switch")
        {
            GameManager.Instance.HideMessage();
            sc = null;
        }
        else if(collision.tag == "Postit")
        {
            GameManager.Instance.HideMessage();
            post.CloseCanvas();
            post = null;
        }
        else if (collision.tag == "Note")
        {
            note.CloseCanvas();
            note = null;
        }
        else if(collision.tag == "Mixer")
        {
            GameManager.Instance.HideMessage();
            mixer = null;
        }
        else if(collision.tag == "Warp")
        {
            GameManager.Instance.HideMessage();
            warp = null;
        }
        else if(collision.tag == "Reactor")
        {
            GameManager.Instance.HideMessage();
            reactor = null;
        }
    }
}
