using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHunters
{
    class GUI
    {
        private int score;
        private int shield;
        private int lives;
        private int wave;
        private int nextwave = 750;

        public void Initialize(int Score, int Shield, int Lives, int Wave)
        {
            score = Score;
            shield = Shield;
            lives = Lives;
            wave = Wave;
        }

        public int SCORE
        {
            get { return score; }
            set { this.score = value; }
        }
        public int SHIELD
        {
            get { return shield; }
            set { this.shield = value; }
        }
        public int LIVES
        {
            get { return lives; }
            set { this.lives = value; }
        }
        public int WAVE
        {
            get { return wave; }
            set { this.wave = value; }
        }
        public int NEXTWAVE
        {
            get { return nextwave; }
            set { this.nextwave = value; }
        }
    }
}
