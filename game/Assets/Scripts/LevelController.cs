using UnityEngine;
using System.Collections;

public abstract class LevelController : MonoBehaviour {

	public abstract int GetLevel();
	public abstract void SetDefaultState();
	public abstract void SetState(ConversationController conversationController);

}
