﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum PlayerType
{
	human,
	ai
}

[System.Serializable]
public class Player {
	public PlayerType type = PlayerType.ai;
	public int playerNum;
	public List<CardBartok> hand;
	public SlotDefB handSlotDef;

	public CardBartok addCard(CardBartok eCB)
	{
		if (hand == null) hand = new List<CardBartok> ();
		hand.Add (eCB);
		fanHand ();
		return eCB;
	}
	public CardBartok removeCard(CardBartok cb)
	{
		hand.Remove (cb);
		fanHand ();
		return cb;
	}

	public void fanHand()
	{
		float startRot = handSlotDef.rot;
		if (hand.Count >1)
		{
			startRot += Bartok.S.handFanDegrees * (hand.Count-1)/2;
		}
		Vector3 pos;
		float rot;
		Quaternion rotQ;
		for (int i=0;i<hand.Count;i++)
		{
			rot = startRot - Bartok.S.handFanDegrees * i;
			rotQ = Quaternion.Euler (0, 0, rot);
			pos = Vector3.up * CardBartok.CARD_HEIGHT / 2f;
			pos = rotQ * pos;
			pos += handSlotDef.pos;
			pos.z = -0.5f * i;
			hand[i].transform.localPosition = pos;
			hand[i].transform.rotation = rotQ;
			hand[i].state = CBState.hand;
			hand[i].faceUp = (type == PlayerType.human);
			hand[i].setSortOrder(i*4);
		}
	}
}
	