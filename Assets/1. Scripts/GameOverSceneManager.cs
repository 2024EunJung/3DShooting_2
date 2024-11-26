using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 네임스페이스 추가

public class GameOverSceneManager : MonoBehaviour
{
    public void RestartGame()
    {
        // 현재 활성화된 씬의 이름을 가져와 다시 로드
        string PlayScene_lsh = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(PlayScene_lsh);
    }
}

