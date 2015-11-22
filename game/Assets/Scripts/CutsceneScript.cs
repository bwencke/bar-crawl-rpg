using UnityEngine;
using System.Collections;

public abstract class CutsceneScript : TopLevelController {

	public abstract void LoadResults();
	public abstract IEnumerator Next(System.Action callback);
	public abstract bool HasNext ();
	public override void TriggerMovement (Vector2 movement_vector) {
		// do nothing
	}

	public override void TriggerPrimaryAction (System.Action<string> callback)
	{
		// do nothing
	}
}
