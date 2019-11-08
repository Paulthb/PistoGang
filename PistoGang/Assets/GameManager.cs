using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int sensorOne = 0;
    int sensorTwo = 0;

    private bool gameCanStart;
    public bool roundCanBegin;
    private bool isPlaying;
    private bool weaponChoosed;
    private bool gameHasBegun;
    private bool canCheck;

    private int round;

    // PLAYER 1
    private int weaponOnPlayer1;
    private PlayerController playerOneController;
    private int scorePlayerOne;

    // PLAYER 2
    private int weaponOnPlayer2;
    private PlayerController playerTwoController;
    private int scorePlayerTwo;

    public SoundManager soundMgr;

    // Start is called before the first frame update
    void Start()
    {
        UduinoManager.Instance.OnBoardConnected += OnBoardConnected;
        //UduinoManager.Instance.OnDataReceived += OnDataReceived;

        StartCoroutine("WaitTwoSecondsForCheck");

        round = 1;
        scorePlayerOne = scorePlayerTwo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOneController != null)
        {
            playerOneController.Update();
            //playerOneController.SendCommand();
        }

        if (playerTwoController != null)
        {
            playerTwoController.Update();
            //playerTwoController.SendCommand();
        }

        if(canCheck)
            CheckPlayerReady();

        if (roundCanBegin && round <= 3)
        {
            PlayRound();
            if(weaponChoosed)
                CheckCombinaison();
        }
    }

    /*
    void OnDataReceived(string data, UduinoDevice device)
    {
        if (device.name == "firstUduino") sensorOne = int.Parse(data);
        else if (device.name == "secondUduino") sensorTwo = int.Parse(data);
    }
    */
    void OnBoardConnected(UduinoDevice connectedDevice)
    {
        //You can launch specific functions here
        if (connectedDevice.name == "firstUduino")
        {
            playerOneController = new PlayerController(connectedDevice);
        }
        else if (connectedDevice.name == "secondUduino")
        {
            playerTwoController = new PlayerController(connectedDevice);
        }
    }

    void CheckPlayerReady()
    {
        if (playerOneController != null && playerTwoController != null)
        {
            print("First Device : " + playerOneController.GetUduinoDevice().name);
            print("Second Device : " + playerTwoController.GetUduinoDevice().name);
            print("Input First Device : " + playerOneController.GetButtonDown(3));
            print("Input Second Device : " + playerTwoController.GetButtonDown(3));
            if (!gameHasBegun)
            {
                StartCoroutine("TimeBeforeGameCanStart");
                if (playerOneController.GetButtonDown(3) && playerTwoController.GetButtonDown(3) && gameCanStart)    // si le PLAYER 1 appuie sur les 4 boutons
                {
                    roundCanBegin = true;
                    Debug.Log("Round can begin");
                    gameHasBegun = true;

                    //JOUER ROUND 1
                    //soundMgr.PlayAudioOn1("annonceRound1");
                }
            }
        }
    }

    void CheckCombinaison()
    {
        // si le joueur 1 appuie sur les bons boutons correspondant à l'arme allumée sur le joueur 2
        if ((playerOneController.GetButtonDown(0) && playerOneController.GetButtonDown(1) && weaponOnPlayer2 == 9)
            || (playerOneController.GetButtonDown(1) && playerOneController.GetButtonDown(2) && weaponOnPlayer2 == 10)
            || (playerOneController.GetButtonDown(0) && playerOneController.GetButtonDown(1) && playerOneController.GetButtonDown(2) 
            && playerOneController.GetButtonDown(3) && weaponOnPlayer2 == 11))
        {
            if(weaponOnPlayer2 == 9)
            {
                // JOUER GUN
                //soundMgr.PlayAudioOn1("bruitPan");
                StartCoroutine("WaitTwoSeconds");
                //soundMgr.PlayAudioOn1("victoirePistolet");
                StartCoroutine("TimeBeforeRoundCanStart");
            }
            else if (weaponOnPlayer2 == 10)
            {
                // JOUER SPIDERBLAST
                //soundMgr.PlayAudioOn1("bruitSpider");
                StartCoroutine("WaitTwoSeconds");
                //soundMgr.PlayAudioOn1("victoireSpider");
                StartCoroutine("TimeBeforeRoundCanStart");
            }
            else if (weaponOnPlayer2 == 11)
            {
                // JOUER LASSO
                //soundMgr.PlayAudioOn1("bruitLasso");
                StartCoroutine("WaitTwoSeconds");
                //soundMgr.PlayAudioOn1("victoireIndiana");
                StartCoroutine("TimeBeforeRoundCanStart");
            }
            scorePlayerOne++;
            isPlaying = false;
            roundCanBegin = false;
            NextRound();
        }

        // si le joueur 2 appuie sur les bons boutons correspondant à l'arme allumée sur le joueur 1
        if ((playerTwoController.GetButtonDown(0) && playerTwoController.GetButtonDown(1) && weaponOnPlayer1 == 9)
            || (playerTwoController.GetButtonDown(1) && playerTwoController.GetButtonDown(2) && weaponOnPlayer1 == 10)
            || (playerTwoController.GetButtonDown(0) && playerTwoController.GetButtonDown(1) && playerTwoController.GetButtonDown(2)
            && playerTwoController.GetButtonDown(3) && weaponOnPlayer1 == 11))
        {
            if (weaponOnPlayer1 == 9)
            {
                // JOUER GUN
                //soundMgr.PlayAudioOn2("bruitPan");
                StartCoroutine("WaitTwoSeconds");
                //soundMgr.PlayAudioOn2("victoirePistolet");
                StartCoroutine("TimeBeforeRoundCanStart");
            }
            else if (weaponOnPlayer1 == 10)
            {
                // JOUER SPIDERBLAST
                //soundMgr.PlayAudioOn2("bruitSpider");
                StartCoroutine("WaitTwoSeconds");
                //soundMgr.PlayAudioOn2("victoireSpider");
                StartCoroutine("TimeBeforeRoundCanStart");
            }
            else if (weaponOnPlayer1 == 11)
            {
                // JOUER LASSO
                //soundMgr.PlayAudioOn2("bruitLasso");
                StartCoroutine("WaitTwoSeconds");
                //soundMgr.PlayAudioOn2("victoireIndiana");
                StartCoroutine("TimeBeforeRoundCanStart");
            }
            scorePlayerTwo++;
            isPlaying = false;
            roundCanBegin = false;
            NextRound();
        }
    }

    void PlayRound()
    {
        isPlaying = true;

        if (!weaponChoosed)
        {
            weaponOnPlayer1 = Random.Range(9, 12);
            weaponOnPlayer2 = Random.Range(9, 12);
            Debug.Log("Arme sur PLAYER 1 :  " + weaponOnPlayer1);
            Debug.Log("Arme sur PLAYER 2 :  " + weaponOnPlayer2);

            //JOUER 3,2,1 SHOT
            //soundMgr.PlayAudioOn1("decompteRound");
            StartCoroutine("TimeBeforeWeaponDisplay");
            weaponChoosed = true;
        }

        // tant que le joueur n'a pas trouvé la bonne combinaison ou que le round n'est pas fini
        if (isPlaying)
        {
            // allumer directement les leds en question
            playerOneController.LedOn(weaponOnPlayer1);
            playerTwoController.LedOn(weaponOnPlayer2);
            Debug.Log("Leds allumées");
        }
        else 
        {
            playerOneController.LedOff(weaponOnPlayer1);
            playerTwoController.LedOff(weaponOnPlayer2);
        }
    }

    public void NextRound()
    {
        round++;
        StartCoroutine("TimeBeforeRoundCanStart");
        if (round <= 3)
        {
            if (round == 2)
            {
                //Jouer ROUND 2
                //soundMgr.PlayAudioOn1("annonceRound2");
            }
            if (round == 3)
            {
                //Jouer ROUND 3
                //soundMgr.PlayAudioOn1("annonceRound3");
            }
            Debug.Log("Le round " + round + "commence");
            StartCoroutine("TimeBeforeRoundCanStart");
            roundCanBegin = true;
        }
        else
        {
            if (scorePlayerOne > scorePlayerTwo)
            {
                //Jouer PLAYER 1 WINS
                //soundMgr.PlayAudioOn1("winPlayer1");
            } else if (scorePlayerTwo > scorePlayerOne)
            {
                //Jouer PLAYER 2 WINS
                //soundMgr.PlayAudioOn1("winPlayer2");
            }
        }
    }

    IEnumerator WaitTwoSeconds()
    {
        yield return new WaitForSeconds(2f);
    }

    IEnumerator WaitTwoSecondsForCheck()
    {
        yield return new WaitForSeconds(2f);
        canCheck = true;
    }

    IEnumerator TimeBeforeWeaponDisplay()
    {
        yield return new WaitForSeconds(3f);
    }

    IEnumerator TimeBeforeRoundCanStart()
    {
        yield return new WaitForSeconds(4f);
    }

    IEnumerator TimeBeforeGameCanStart()
    {
        yield return new WaitForSeconds(3f);
        gameCanStart = true;
    }
}
