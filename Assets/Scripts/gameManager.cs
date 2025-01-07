using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public bool active = true;

    public playerStats player;
    public Enemy enemy;
    public bool isPlayerTurn = true;
    public List<Card> cardDeck;
    public List<CardReader> cardHands;
    public GameObject cardHand;

    public GameObject playerCanvas;
    public GameObject enemyCanvas;
    public GameObject endCanvas;
    public GameObject winState;
    public GameObject loseState;

    public List<int> tempHand = new List<int>();

    private void Start()
    {
        tempHand.Clear();
        tempHand.Add(Random.Range(0, cardDeck.Count));

        while (tempHand.Count < 5)
        {
            int temp = Random.Range(0, cardDeck.Count);
            if (!tempHand.Contains(temp))
                tempHand.Add(temp);
        }

        Debug.Log(tempHand);

        for (int i = 0; i < cardHands.Count; i++)
        {
            cardHands[i].gameObject.SetActive(true);
            cardHands[i].cardInfo = cardDeck[tempHand[i]];
            cardHands[i].setCardInfo();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (player.health <= 0)
                endGame(false);
            else if (enemy.health <= 0)
                endGame(true);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            enemy.health = enemy.maxHealth;
            player.health = player.maxHealth;
        }
    }

    public void nextTurn(bool isPlayerTurn)
    {
        if (isPlayerTurn)
        {
            player.turnsLeftImage.gameObject.SetActive(false);
            player.endTurnButton.gameObject.SetActive(false);

            for (int i = 0; i < cardHands.Count; i++)
            {
                cardHands[i].gameObject.SetActive(false);
            }
            cardHand.SetActive(false);

            Invoke("enemyTurn", Random.Range(0.2f, 1f));
        }
        else
        {
            player.turnsLeftImage.gameObject.SetActive(true);
            player.endTurnButton.gameObject.SetActive(true);
            cardHand.SetActive(true);
            player.turnsLeft = 3;

            shuffleHand();
        }
    }

    void shuffleHand()
    {
        tempHand.Clear();
        tempHand.Add(Random.Range(0,cardDeck.Count));

        while(tempHand.Count < 5)
        {
            int temp = Random.Range(0, cardDeck.Count);
            if (!tempHand.Contains(temp))
                tempHand.Add(temp);
        }

        Debug.Log(tempHand);

        for (int i = 0; i < cardHands.Count; i++)
        {
            cardHands[i].resetCard();
            cardHands[i].gameObject.SetActive(true);
            cardHands[i].cardInfo = cardDeck[tempHand[i]];
            cardHands[i].setCardInfo();
        }
    }

    void enemyTurn()
    {
        enemy.triggerAttackAnim();
        Invoke("enemyTurnAttack", 1f);
    }

    void enemyTurnAttack()
    {
        enemy.attack();

    }

    public void endGame(bool playerWin)
    {
        active = false;

        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);
        endCanvas.SetActive(true);

        if (playerWin)
            winState.SetActive(true);
        else
            loseState.SetActive(true);
    }
}
