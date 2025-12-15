using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    // QUEST MANAGEMENT VARIABLES 
    private bool hasGuitar = false;
    public GameObject guitarItemInScene; // Assign this in the Unity Inspector
    // 

    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer; // Now only used for the NPC interaction point

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on GameObject.");
        }
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;
        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        if (dialogueManager == null) return;

        // NEW INTERACTION LOGIC 

        
        var guitarCollider = Physics2D.OverlapCircle(interactPos, 0.2f, interactablesLayer);

        
        if (guitarItemInScene != null && guitarCollider != null && guitarCollider.gameObject == guitarItemInScene)
        {
            if (!hasGuitar)
            {
                hasGuitar = true;
                guitarItemInScene.SetActive(false); // Make the guitar disappear
                string[] pickupLines = { "You found Junk's... well, Junk!!" };
                dialogueManager.StartDialogue("Junk Guitar", pickupLines);
            }
            return; 
        }

       
        
        var npcCollider = Physics2D.OverlapCircle(interactPos, 0.2f, interactablesLayer);

        if (npcCollider != null)
        {
            string npcName = "Junk Rocker";
            string[] linesToDisplay;

            if (hasGuitar)
            {
               
                linesToDisplay = new string[] { "She's still a bit rough around the edges, but she should do fine little rocker!", };
            }
            else
            {
              
                linesToDisplay = new string[]
                {
                    "Aye little rocker!",
                    "I see you want to get this place up and running again with that show of yours",
                    "Tell you what, I had an old loaner guitar somewhere in here way back then",
                    "If you can find it and bring it to me, I'll perform for ya little show little rocker Whaddya say?",
                };
            }
            dialogueManager.StartDialogue(npcName, linesToDisplay);
        }
    }

  
    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while (Vector3.SqrMagnitude(targetPos - transform.position) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactablesLayer) != null)
        {
            return false;
        }
        return true;
    }
}