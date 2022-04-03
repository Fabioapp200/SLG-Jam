using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMovement : MonoBehaviour
{
    GameObject moveToM;
    float speed = 1f, step = 1f;
    bool moveUp;
    bool moveDown;
    bool moveRight;
    bool moveLeft;
    GameManager gameManager;
    void Start()
    {
        //References the Minotaur's MoveTo Object and removes it as a child
        moveToM = GameObject.FindWithTag("minotaurMoveTo");
        moveToM.transform.parent = null;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {

    }
    public void Move(float pPosX, float pPoxY)
    {
        if (!gameManager.winBadgeUp)
        {
            for (int i = 0; i < 2; i++)
            {
                //Choosing to move left or right
                //If the minotaur is to the left of the player, move right. Do the oposite if to the right
                bool toTheLeft = pPosX > transform.position.x;
                bool toTheRight = pPosX < transform.position.x;

                //Choosing to move up or down
                //If the minotaur is below the player, move up. Do the oposite if above
                bool Below = pPoxY > transform.position.y;
                bool Above = pPoxY < transform.position.y;

                //Checking if the player is more distant in the horizontal or in the vertical axis
                float horDis = Mathf.Abs(pPosX - transform.position.x);
                float verDis = Mathf.Abs(pPoxY - transform.position.y);

                //Defines in which direction to run, prioritize the vertical axis in case of a tie
                moveUp = verDis >= horDis && Below;
                moveDown = verDis >= horDis && Above;
                moveRight = verDis <= horDis && toTheLeft;
                moveLeft = verDis <= horDis && toTheRight;

                //Declaring raycast and barrier's layer mask
                RaycastHit2D hit;
                LayerMask mask = LayerMask.GetMask("Barriers");
                float raySize = 1f, debugDur = 1;

                //Casts a Ray in chosen direction, if it hits a barrier tile. it does nothing
                //If the next tile is free, moves in the chosen direction
                if (moveUp)
                {
                    hit = Physics2D.Raycast(transform.position, Vector2.up, raySize, mask);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "barrier")
                        {
                            Debug.DrawRay(transform.position, Vector3.up * raySize, Color.red, debugDur);
                        }
                    }
                    else
                    {
                        moveToM.transform.position += new Vector3(0f, speed, 0f); //Move up
                        transform.position = Vector3.MoveTowards(transform.position, moveToM.transform.position, step);
                        Debug.DrawRay(transform.position, Vector3.up * raySize, Color.red, debugDur);
                    }
                }
                else if (moveDown)
                {
                    hit = Physics2D.Raycast(transform.position, Vector2.down, raySize, mask);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "barrier")
                        {
                            Debug.DrawRay(transform.position, Vector3.down * raySize, Color.red, debugDur);
                        }
                    }
                    else
                    {
                        moveToM.transform.position += new Vector3(0f, -speed, 0f); //Move Down
                        transform.position = Vector3.MoveTowards(transform.position, moveToM.transform.position, step);
                        Debug.DrawRay(transform.position, Vector3.down * raySize, Color.red, debugDur);
                    }
                }
                else if (moveRight)
                {
                    hit = Physics2D.Raycast(transform.position, Vector2.right, raySize, mask);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "barrier")
                        {
                            Debug.DrawRay(transform.position, Vector3.right * raySize, Color.red, debugDur);
                        }
                    }
                    else
                    {
                        moveToM.transform.position += new Vector3(speed, 0f, 0f); //Move right
                        transform.position = Vector3.MoveTowards(transform.position, moveToM.transform.position, step);
                        Debug.DrawRay(transform.position, Vector3.right * raySize, Color.red, debugDur);
                    }
                }
                else if (moveLeft)
                {
                    hit = Physics2D.Raycast(transform.position, Vector2.left, raySize, mask);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "barrier")
                        {
                            Debug.DrawRay(transform.position, Vector3.left * raySize, Color.red, debugDur);
                        }
                    }
                    else
                    {
                        moveToM.transform.position += new Vector3(-speed, 0f, 0f); //Move left
                        transform.position = Vector3.MoveTowards(transform.position, moveToM.transform.position, step);
                        Debug.DrawRay(transform.position, Vector3.left * raySize, Color.red, debugDur);
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If the minotaur gets to theseus' tile, it destroys him
        if (other.tag == "theseus")
        {
            //References theseus' gameobject and deactives its sprite. Giving the impression that he died;
            GameObject theseus = GameObject.FindGameObjectWithTag("theseus");
            theseus.transform.GetChild(0).gameObject.SetActive(false);

            if (!gameManager.winBadgeUp)
            {
                StartCoroutine(gameManager.Lose());
            }
        }
    }
}
