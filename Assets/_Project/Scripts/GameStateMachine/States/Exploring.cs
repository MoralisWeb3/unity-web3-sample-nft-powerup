using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using Web3_Elden_Ring;

public class Exploring : State
{
    public PlayerInputController playerInputController;
    
    private void OnEnable()
    {
        playerInputController.EnableInput(true);    
    }
}
