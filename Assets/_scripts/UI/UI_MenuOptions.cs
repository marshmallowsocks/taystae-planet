using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuOptions : GameCore {

	public void OnClickPlayButton()
    {
        SceneManager.LoadScene(GameStrings.SCENE_LEVEL_ONE);
    }
}
