using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalMove;
    public CharacterController character;
    private bool hop;
    private bool up;
    private bool down;
    private FollowerOfTheRhythm tempo;

    void Start()
    {
        tempo = GetComponent<FollowerOfTheRhythm>();
    }

    void Update()
    {
        hop = false;
        up = false;
        down = false;
        if (Input.GetKeyDown("space") && tempo.canMoveToRhythmPlayer())
            hop = true;
        if (Input.GetKeyDown("w"))
            up = true;
        if (Input.GetKeyDown("s") && tempo.canMoveToRhythmPlayer())
            down = true;
        horizontalMove = Input.GetAxisRaw("Horizontal");    
        character.Move(horizontalMove, hop, up, down);
    }
}
