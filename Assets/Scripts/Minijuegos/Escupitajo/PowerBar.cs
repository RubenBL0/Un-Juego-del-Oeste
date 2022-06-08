using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerBar : MonoBehaviour
{
    bool charging = false;
    Direction direction;

    [SerializeField] Image imageBar;
    [SerializeField] float speed = 2f;

    [SerializeField] Spittle escupitajo;


    bool canMoveBar = true;
    // Start is called before the first frame update
    void Start()
    {
        imageBar.fillAmount = 0f;
        direction = Direction.Left;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoveBar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChargeBar();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ReleaseBar();
            }

        }
    }

    void ChargeBar()
    {
        charging = true;
        StartCoroutine(BarCoroutine());
    }

    IEnumerator BarCoroutine()
    {
        while (charging)
        {
            if(direction == Direction.Right)
            {
                imageBar.fillAmount -= Time.deltaTime;
                if (imageBar.fillAmount <= 0.01f)
                {
                    ChangeDirection();
                }
            }
            else
            {
                imageBar.fillAmount += Time.deltaTime;
                if (imageBar.fillAmount == 1f)
                {
                    ChangeDirection();
                }
            }

            yield return null;
        }
        yield return null;
    }

    void ReleaseBar()
    {
        float power = imageBar.fillAmount * 10f;
        canMoveBar = false;
        charging = false;
        print(power);
        Spit(power);
    }

    void ChangeDirection()
    {
        if(direction == Direction.Right)
        {
            direction = Direction.Left;
        }
        else
        {
            direction = Direction.Right;
        }
    }

    void Spit(float amount)
    {
        escupitajo.Spit(amount);
    }

    public void HideBar()
    {
        canMoveBar = true;
        imageBar.fillAmount = 0f;
    }
}

enum Direction
{
    Left, Right
}
