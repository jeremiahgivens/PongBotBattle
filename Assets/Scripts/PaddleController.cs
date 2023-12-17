using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public List<GameObject> m_Paddles;
    public GameObject m_Ball;
    private Rect m_BallBounds = new Rect(0, 0, 17, 8);
    
    enum Direction
    {
        Up,
        Down
    }

    enum Paddle
    {
        Left,
        Right
    }
    
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
        if (Input.GetKey(KeyCode.W))
        {
            MoveHandle(Paddle.Left, Direction.Up);
        } else if (Input.GetKey(KeyCode.S)) 
        {
            MoveHandle(Paddle.Left, Direction.Down);
        } 
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveHandle(Paddle.Right, Direction.Up);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveHandle(Paddle.Right, Direction.Down);
        }
    }

    private void MoveHandle(Paddle paddle, Direction direction)
    {
        Vector2 pos = m_Paddles[(int)paddle].transform.position;
        switch (direction)
        {
            case Direction.Up:
                pos.y = math.min(pos.y + 0.25f, m_BallBounds.height / 2f);
                break;
            case Direction.Down:
                pos.y = math.max(pos.y - 0.25f, -m_BallBounds.height / 2f);
                break;
        }

        m_Paddles[(int)paddle].transform.position = pos;
    }
}
