using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SpaceHunters
{
 
  class Sounds
  { 

     private SoundEffectInstance laserSoundInstance;
     private SoundEffectInstance explosionSoundInstance;

     
     public void Initialize(SoundEffect laserSound, SoundEffect explosionSound)
     {
   
         laserSoundInstance = laserSound.CreateInstance();
         explosionSoundInstance = explosionSound.CreateInstance();

       

      }

      public SoundEffectInstance LAZER
      {

          get { return laserSoundInstance; }

       }

       public SoundEffectInstance EXPLOSION
       {

           get { return explosionSoundInstance; }
  
       }

   }
}