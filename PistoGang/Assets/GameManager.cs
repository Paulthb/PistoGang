using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool canGameStart;
    private bool canRoundBegin;
    private bool isPlaying;
    private bool roundFinished;
    private bool weaponChoosed;
    private bool roundIsPlayed;

    private int quickestPlayer;
    private int round;

    // PLAYER 1
    private int player1Btn0, player1Btn1, player1Btn2, player1Btn3;     // l'état 0 ou 1 des boutons de la main, 3 etant celui le plus près du pouce
    private int weaponOnPlayer1;

    // PLAYER 2
    private int player2Btn, player2Btn1, player2Btn2, player2Btn3;     // l'état 0 ou 1 des boutons de la main, 3 etant celui le plus près du pouce
    private int weaponOnPlayer2;

    

    private void Awake()
    {
        player1Btn0 = player1Btn1 = player1Btn2 = player1Btn3 = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // on definit les 3 pin comme etant des sorties pour les 3 leds
        UduinoManager.Instance.pinMode(9, PinMode.Output);
        UduinoManager.Instance.pinMode(10, PinMode.Output);
        UduinoManager.Instance.pinMode(11, PinMode.Output);

        canRoundBegin = false;

        round = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // on lit en continu les 4 pin des boutons du PLAYER 1
        player1Btn0 = UduinoManager.Instance.digitalRead(2);
        player1Btn1 = UduinoManager.Instance.digitalRead(3);
        player1Btn2 = UduinoManager.Instance.digitalRead(4);
        player1Btn3 = UduinoManager.Instance.digitalRead(5);

        CheckPlayerReady();

        if (canRoundBegin && round <= 3 && !roundIsPlayed)
        {
            PlayRound();
        }
        else
        {
            //Dire qui est le winner
        }

        CheckCombinaison();
    }

    void CheckPlayerReady()
    {
        StartCoroutine("TimeBeforeGameCanStart");
        if (player1Btn3 == 0 && canGameStart)    // si le PLAYER 1 appuie sur les 4 boutons
        {
            canRoundBegin = true;
            Debug.Log("Round can begin");
        }
    }

    void CheckCombinaison()
    {
        // si le joueur appuie sur les bons boutons correspondants à l'arme GUN
        if (player1Btn0 == 0 && player1Btn1 == 0 && weaponOnPlayer2 == 9)
        {
            // on check s'il a été le plus rapide
            Debug.Log("Le joueur a réussi le GUN");
            isPlaying = false;
        }

        // si le joueur appuie sur les bons boutons correspondants à l'arme SPIDERBLAST
        if (player1Btn1 == 0 && player1Btn2 == 0 && weaponOnPlayer2 == 10)
        {
            // on check s'il a été le plus rapide
            Debug.Log("Le joueur a réussi le SPIDERBLAST");
            isPlaying = false;
        }

        // si le joueur appuie sur les bons boutons correspondants à l'arme LASSO
        if (player1Btn0 == 0 && player1Btn1 == 0 && player1Btn2 == 0 && player1Btn3 == 0 && weaponOnPlayer2 == 11)
        {
            // on check s'il a été le plus rapide
            Debug.Log("Le joueur a réussi le LASSO");
            isPlaying = false;
        }
    }

    void PlayRound()
    {
        // mettre un temps d'attente
        //StartCoroutine("TimeBeforeWeaponDisplay");

        if (!weaponChoosed)
        {
            weaponOnPlayer2 = Random.Range(9, 12);
            //weaponOnPlayer1 = Random.Range(6, 8);

            Debug.Log("GOOOOO ! l'arme qui devrait etre sélectionnée est :  " + weaponOnPlayer2);

            isPlaying = true;

            weaponChoosed = true;
        }

        // tant que le joueur n'a pas trouvé la bonne combinaison ou que le round n'est pas fini
        if (isPlaying)
        {
            // allumer directement la led en question
            UduinoManager.Instance.digitalWrite(weaponOnPlayer2, State.HIGH);
            Debug.Log("est dans le if is playing");
        }
        
        // le round est terminé
        roundFinished = true;
        round++;

        roundIsPlayed = true;
    }

    IEnumerator TimeBeforeWeaponDisplay()
    {
        // on définit aléatoirement l'arme qui va s'allumer sur le torse du player 1 et 2, qui est en fait un choix entre les 3 pin des led
        if (!weaponChoosed)
        {
            weaponOnPlayer2 = Random.Range(6, 8);
            //weaponOnPlayer1 = Random.Range(6, 8);

            Debug.Log("GOOOOO ! l'arme qui devrait etre sélectionnée est :  " + weaponOnPlayer2);

            //isPlaying = true;

            weaponChoosed = true;
        }

        yield return new WaitForSeconds(3f);
    }

    IEnumerator TimeBeforeGameCanStart()
    {
        yield return new WaitForSeconds(3f);
        canGameStart = true;
    }
}
