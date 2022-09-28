﻿namespace Nibbles.Engine
{
    public interface ISoundGenerator
    {
        void Beep(int frequenceyHz, int durationMs);
        void PlayGameOverSoundAsync();
        void PlayLevelUpSoundAsync();
        void SingleBeepAsync(int frequenceyHz, int durationMs);
    }
}
