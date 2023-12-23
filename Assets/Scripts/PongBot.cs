using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PongBot
{
    // x, y, v.x, v.y, p, up-none-down
    private Dictionary<((int, int, int, int, int), int), float> m_QTable =
        new Dictionary<((int, int, int, int, int), int), float>();

    public void UpdateTable(((int, int, int, int, int, int), int) previousState,
        ((int, int, int, int, int, int), int) newState, float reward)
    {
        
    }

    public int ChooseAction((int, int, int, int, int) state, int percentFollowBest)
    {
        int action = 0;
        float val = Random.value;
        if (val <= (float)percentFollowBest / 100f)
        {
            float max = float.MinValue;
            int maxIndex = 0;
            for (int a = -1; a <= 1; a++)
            {
                float qValue = m_QTable[(state, a)];
                if (qValue > max)
                {
                    max = qValue;
                    maxIndex = a;
                }
            }
        }
        Random.Range(-1, 1);

        return action;
    }

    public PongBot()
    {
        for (int x = -4; x < 5; x++)
        {
            for (int y = -4; y < 5; y++)
            {
                for (int vx = -4; vx < 5; vx++)
                {
                    for (int vy = -4; vy < 5; vy++)
                    {
                        for (int p = -4; p < 5; p++)
                        {
                            for (int a = -1; a < 2; a++)
                            {
                                m_QTable[((x, y, vx, vy, p), a)] = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}