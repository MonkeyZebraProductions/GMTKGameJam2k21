using UnityEngine;

public static class Utility
{
    //Use these instead of the engine's ones for the gameplay, so that the UI animation can work even when the game is "Paused".
    public static float LocalTimeScale = 1f;

    public static float LocalDeltaTime => Time.deltaTime * LocalTimeScale;
}
