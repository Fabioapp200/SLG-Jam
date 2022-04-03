using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheseusMovement : MonoBehaviour
{
    //Declaring raycast and barrier's layer mask
    RaycastHit2D hit;
    LayerMask mask;
    float raySize = 1f, debugDur = 1;
    GameObject moveToT, minotaur;
    float speed = 1f, step = 1f;
    void Start()
    {
        //References theseus' MoveTo Object and removes it as a child
        moveToT = GameObject.FindWithTag("theseusMoveTo");
        moveToT.transform.parent = null;

        //References the minotaur
        minotaur = GameObject.FindWithTag("minotaur");

        //Reference to the Barriers Layer 
        mask = LayerMask.GetMask("Barriers");
    }

    //Calls the Move method on the minotaurs movement script, sending the player's current position
    void MoveMinotaur()
    {
        minotaur.GetComponent<MinotaurMovement>().Move(transform.position.x, transform.position.y);
    }
    void Update()
    {
        // For each keyboard input, translates the MoveToObject, then it moves the sprite to that direction
        // After that it call the Move method on the minotaur
        // Casts a ray to detect with there are barriers, if there aren't any, the player moves
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, raySize, mask);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "barrier")
                {
                    Debug.DrawRay(transform.position, Vector3.right * raySize, Color.green, debugDur);
                }
            }
            else
            {
                moveToT.transform.position += new Vector3(speed, 0f, 0f);
                transform.position = Vector3.MoveTowards(transform.position, moveToT.transform.position, step);
                MoveMinotaur();
                Debug.DrawRay(transform.position, Vector3.right * raySize, Color.green, debugDur);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left, raySize, mask);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "barrier")
                {
                    Debug.DrawRay(transform.position, Vector3.left * raySize, Color.green, debugDur);
                }
            }
            else
            {
                moveToT.transform.position += new Vector3(-speed, 0f, 0f);
                transform.position = Vector3.MoveTowards(transform.position, moveToT.transform.position, step);
                MoveMinotaur();
                Debug.DrawRay(transform.position, Vector3.left * raySize, Color.green, debugDur);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            hit = Physics2D.Raycast(transform.position, Vector2.up, raySize, mask);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "barrier")
                {
                    Debug.DrawRay(transform.position, Vector3.up * raySize, Color.green, debugDur);
                }
            }
            else
            {
                moveToT.transform.position += new Vector3(0f, speed, 0f);
                transform.position = Vector3.MoveTowards(transform.position, moveToT.transform.position, step);
                MoveMinotaur();
                Debug.DrawRay(transform.position, Vector3.up * raySize, Color.green, debugDur);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, raySize, mask);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "barrier")
                {
                    Debug.DrawRay(transform.position, Vector3.down * raySize, Color.green, debugDur);
                }
            }
            else
            {
                moveToT.transform.position += new Vector3(0f, -speed, 0f);
                transform.position = Vector3.MoveTowards(transform.position, moveToT.transform.position, step);
                MoveMinotaur();
                Debug.DrawRay(transform.position, Vector3.down * raySize, Color.green, debugDur);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveMinotaur();
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "trophy"){
            Debug.Log("Win");
        }
    }
}
