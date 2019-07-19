﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeAndSleepTilRested : State<Miner>
{
    // Singleton design pattern implementation.
    public static GoHomeAndSleepTilRested instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public override void Enter(Miner miner)
    {
        //If the miner isn't be in the Home, the miner should change his location to go to the Home.
        if (miner.m_location != BaseGameEntity.LocationType.Home)
        {
            Debug.Log("\n" + miner.GetNameOfEntity() + ": " + "Walking to the my Home!");
            miner.ChangeLocation(BaseGameEntity.LocationType.Home);
        }
    }

    public override void Execute(Miner miner)
    {
        int tmpFatigue = miner.GetFatigue();

        // Lay on the bed is the best thing to do...
        miner.DecreaseFatigue();

        
        miner.SetFatigue(miner.GetFatigue());
        Debug.Log("\n" + miner.GetNameOfEntity() + ": " + "Take a sleep on my bed.. : " + tmpFatigue + " → "  + miner.GetFatigue() );
    
        // If he takes a enough sleep, go to the mine.
        if (miner.IsFatigueQuenched())
        {
            Debug.Log("피로회복 완료!\n");
            miner.GetFSM().ChangeState(VisitBankAndDepositGold.instance);
        }
        

    }

    public override void Exit(Miner miner)
    {
        Debug.Log("\n" + miner.GetNameOfEntity() + ": " + "I am leaving the home with starring eyes, how sweet sleep it was.");
    }
}
