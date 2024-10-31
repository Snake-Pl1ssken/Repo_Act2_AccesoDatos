using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasilioMixer
{
    internal class SoundPool
    {
        static List<Sound> soundListPlaying;
        static List<Sound> soundListFree;
        static List<Sound> soundListToRemove;

        public static void Init()
        {
            int size = Program.GetConfig().soundPoolSize;

            soundListPlaying = new List<Sound>(size);
            soundListFree = new List<Sound>(size);
            soundListToRemove = new List<Sound>(size);

            for(int i = 0; i < size; i ++)
            {
                soundListFree.Add(new Sound());
            }
        }

        public static void Update()
        {
            for (int i = 0; i < soundListPlaying.Count; i++)
            {
                if (soundListPlaying[i].Status == SoundStatus.Stopped)
                {
                    soundListFree.Add(soundListPlaying[i]);
                    soundListToRemove.Add(soundListPlaying[i]);
                }
            }

            for (int i = 0; i < soundListToRemove.Count; i++)
            {
                soundListPlaying.Remove(soundListToRemove[i]);
            }

            soundListToRemove.Clear();
        }



        public static void PlaySound(SoundBuffer buffer, float pitch)
        {
            if(soundListFree.Count > 0)
            {
                Sound s = soundListFree.First();
                s.SoundBuffer = buffer;
                s.Pitch = pitch;
                s.Play();
                soundListPlaying.Add(s);
                soundListFree.Remove(s);
            }
        }


    }
}
