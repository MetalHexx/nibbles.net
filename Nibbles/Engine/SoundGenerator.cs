using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles.Engine
{
    public class SoundGenerator : ISoundGenerator
    {
        public void PlayLevelUpSoundAsync() =>
            Task.Run(() =>
            {
                Beep(200, 25);
                Beep(300, 25);
                Beep(400, 25);
                Beep(500, 25);
            });

        public void PlayGameOverSoundAsync() =>
            Task.Run(() =>
            {
                Beep(500, 200);
                Beep(400, 200);
                Beep(100, 600);
            });

        public void SingleBeepAsync(int frequenceyHz, int durationMs) =>
            Task.Run(() => Beep(frequenceyHz, durationMs));

        //TODO: support other platforms
        public void Beep(int frequenceyHz, int durationMs) => Console.Beep(frequenceyHz, durationMs);

        public void Pew() =>
            Task.Run(() =>
            {
                Beep(900, 50);
                Beep(700, 20);
                Beep(600, 20);
            });
    }
}
