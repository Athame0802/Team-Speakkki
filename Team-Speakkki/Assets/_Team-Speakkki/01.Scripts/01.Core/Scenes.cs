using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamSpeakkki.Chaewon
{
    public static class Scenes
    {
        public static int Ingame = SceneManager.GetSceneByName("01.Ingame").buildIndex;
        public static int GameOver = SceneManager.GetSceneByName("02.GameOver").buildIndex;
    }
}
