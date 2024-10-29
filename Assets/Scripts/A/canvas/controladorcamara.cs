using UnityEngine;

public class controladorcamara : MonoBehaviour
{
    public Modelplayer playerModel;
    public Vistacamara cameraView;

    private GameObject playerObject;

    private void Start()
    {
        playerModel = new Modelplayer(new Vector3(0, 0, 0));
        playerObject = GameObject.Find("Player");
    }

    private void Update()
    {
        playerModel.Position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime;

        cameraView.UpdateCameraPosition(playerModel.Position);
    }
}
