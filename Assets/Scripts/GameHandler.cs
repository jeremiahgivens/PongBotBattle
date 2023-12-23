using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public List<GameObject> m_Paddles;
    public GameObject m_Ball;
    public List<BoxCollider2D> m_Barriers;
    public Rigidbody2D m_BallRigidBody;
    public float m_BallSpeed = 5;
    public float m_PaddleStepSize = 0.125f;
    private bool m_GameIsPlaying = false;
    private Rect m_BallBounds = new Rect(0, 0, 17, 8);
    private float m_PaddleHeight = 0.5f;

    private (int, int, int, int, int) m_LeftPreviousState;
    private (int, int, int, int, int) m_LeftCurrentState;
    private (int, int, int, int, int) m_RightPreviousState;
    private (int, int, int, int, int) m_RightCurrentState;
    
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
        m_BallRigidBody = m_Ball.GetComponent<Rigidbody2D>();

        m_BallBounds.xMin = m_Paddles[0].transform.position.x;
        m_BallBounds.xMax = m_Paddles[1].transform.position.x;
        m_BallBounds.yMax = m_Barriers[0].bounds.min.y;
        m_BallBounds.yMin = m_Barriers[1].bounds.max.y;

        m_PaddleHeight = m_Paddles[0].transform.localScale.y;
        
        ResetGame();
    }

    private (int, int, int, int, int) CalculateState(Paddle paddle)
    {
        // x, y, v.x, v.y, p

        int sign = 1;
        Vector2 paddlePos = Vector2.zero;
        if (paddle == Paddle.Right)
        {
            paddlePos = m_Paddles[1].transform.position;
        }
        else
        {
            sign = -1;
            paddlePos = m_Paddles[0].transform.position;
        }

        Vector2 ballPos = m_BallRigidBody.position;
        Vector2 ballVel = m_BallRigidBody.velocity;

        int x = sign * (int)math.round(9*(ballPos.x - m_BallBounds.xMin) / (m_BallBounds.xMax - m_BallBounds.xMin) - 4.5);
        int y = (int)math.round(9*(ballPos.y - m_BallBounds.yMin) / (m_BallBounds.yMax - m_BallBounds.yMin) - 4.5);
        
        int vx = sign * (int)math.round(9*(ballVel.x + m_BallSpeed) / (2*m_BallSpeed) - 4.5);
        int vy = (int)math.round(9*(ballVel.y + m_BallSpeed) / (2*m_BallSpeed) - 4.5);
        
        int p = (int)math.round(9*(paddlePos.y - m_BallBounds.yMin) / (m_BallBounds.yMax - m_BallBounds.yMin) - 4.5);

        return (x, y, vx, vy, p);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(CalculateState(Paddle.Right));
        }
    }

    private void FixedUpdate()
    {
        if (m_GameIsPlaying)
        {
            if (m_BallRigidBody.position.x < m_BallBounds.xMin || m_BallRigidBody.position.x > m_BallBounds.xMax)
            {
                m_GameIsPlaying = false;
                ResetGame();
            }
            else
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
        }
    }

    private void MoveHandle(Paddle paddle, Direction direction)
    {
        Vector2 pos = m_Paddles[(int)paddle].transform.position;
        switch (direction)
        {
            case Direction.Up:
                pos.y = math.min(pos.y + m_PaddleStepSize, m_BallBounds.height / 2f - m_PaddleHeight/2f);
                break;
            case Direction.Down:
                pos.y = math.max(pos.y - m_PaddleStepSize, -m_BallBounds.height / 2f + m_PaddleHeight/2f);
                break;
        }

        m_Paddles[(int)paddle].transform.position = pos;
    }

    public void StartGame()
    {
        ResetGame();
        m_GameIsPlaying = true;
        
        // Choose a random direction to start ball in
        float theta = UnityEngine.Random.Range(-math.PI/3f, math.PI/3f);
        float sign = RandomSign();
        m_BallRigidBody.velocity = sign * m_BallSpeed * new Vector2(math.cos(theta), math.sin(theta));

        m_LeftPreviousState = CalculateState(Paddle.Left);
        m_RightPreviousState = CalculateState(Paddle.Right);
    }
    
    float RandomSign()
    {
        if (UnityEngine.Random.value >= 0.5)
        {
            return -1;
        }
        return 1;
    }

    public void ResetGame()
    {
        // Put paddles back in the vertical center
        foreach (GameObject paddle in m_Paddles)
        {
            paddle.transform.position = new Vector2(paddle.transform.position.x, 0);
        }
        
        // Stop the ball
        m_BallRigidBody.velocity = Vector2.zero;

        // Place ball in the center of play area
        m_Ball.transform.position = 0.5f*(m_Paddles[0].transform.position + m_Paddles[1].transform.position);
    }
}
