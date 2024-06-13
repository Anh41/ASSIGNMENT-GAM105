using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    ASM_MN asm;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        asm = FindObjectOfType<ASM_MN>();
    }

    private void Start()
    {
        scoreText.text = "You Scored:\n" + ScoreKeeper.Instance.GetScore();

        ASM_MN.Instance.YC1(ScoreKeeper.Instance);
        ASM_MN.Instance.YC2();
        ASM_MN.Instance.YC3(ScoreKeeper.Instance.score);
        /*ASM_MN.YC4();
        ASM_MN.YC5();
        ASM_MN.YC6();
        ASM_MN.YC7();*/
    }

    


}