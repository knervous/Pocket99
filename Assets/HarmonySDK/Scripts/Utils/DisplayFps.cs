using UnityEngine;
using System.Collections;

/*!
 *  @class DisplayFps
 *  FPS monitor ui.
 */
[AddComponentMenu("Harmony/Utils/DisplayFps")]
public class DisplayFps : MonoBehaviour
{

  // Attach this to a GUIText to make a frames/second indicator.
  //
  // It calculates frames/second over each updateInterval,
  // so the display does not keep changing wildly.
  //
  // It is also fairly accurate at very low FPS counts (<10).
  // We do this not by simply counting frames per interval, but
  // by accumulating FPS for each frame. This way we end up with
  // correct overall FPS even if the interval renders something like
  // 5.5 frames.

  public  float updateInterval = 0.5F;

  private float accum   = 0; // FPS accumulated over the interval
  private int   frames  = 0; // Frames drawn over the interval
  private float timeleft; // Left time for current interval

  void Start()
  {
    GUIText guiText = GetComponent<GUIText>();

    if( !guiText )
    {
      Debug.Log("DisplayFps needs a GUIText component!");
      enabled = false;
      return;
    }
    timeleft = updateInterval;

    float fontMult = 1.0f / 120.0f;
    int   fontSize = 30;

    guiText.fontSize = (Screen.dpi != 0) ? (int)(Screen.dpi * fontMult * fontSize) : fontSize;
  }

  void Update()
  {
    timeleft -= Time.deltaTime;
    accum += Time.timeScale/Time.deltaTime;
    ++frames;

    // Interval ended - update GUI text and start new interval
    if( timeleft <= 0.0 )
    {
      GUIText guiText = GetComponent<GUIText>();

      // display two fractional digits (f2 format)
      float fps = accum/frames;
      string format = System.String.Format("{0:F2} FPS",fps);
      guiText.text = format;

      if(fps < 30)
        guiText.material.color = Color.yellow;
      else
        if(fps < 10)
          guiText.material.color = Color.red;
        else
          guiText.material.color = Color.green;

      timeleft = updateInterval;
      accum = 0.0F;
      frames = 0;
    }
  }
}

