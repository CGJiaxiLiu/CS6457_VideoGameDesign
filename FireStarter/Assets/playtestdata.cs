using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;

public class playtestdata : MonoBehaviour
{
    // Start is called before the first frame update
    static int[] death_count = { 0, 0, 0, 0 };
    static float[] level_time = {0.0f,0.0f,0.0f,0.0f};
    static private float time_start = 0.0f;
    static private int current_scene_id = 0;

        
    void Start()
    {
        int scene_id = int.Parse(SceneManager.GetActiveScene().name.Split()[1]);
        //Debug.Log(current_scene_id);
        if (current_scene_id != scene_id)
        {
            record_pass_time();
            current_scene_id = scene_id;
            reset_timer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time);
        //Debug.Log(current_scene_id);

    }

    public void reset_timer() {

        time_start = Time.time;
    }

    public void record_death() {
        death_count[current_scene_id]++;
    }

    public void record_pass_time() {
        level_time[current_scene_id] = Time.time - time_start;
    }

    public void report_results() {
        for (int i = 0; i < 3; ++i) {
            Debug.LogWarning(i.ToString()+"  num of death: "+ death_count[i].ToString()+"  time: "+level_time[i].ToString());
            GameObject.Find("Victory").GetComponentInChildren<Text>().text += "\n Level"+i.ToString() + "  num of death: " + death_count[i].ToString() + "  time: " + level_time[i].ToString();
        }

        var currentPath = Directory.GetCurrentDirectory();
        var complete = Path.Combine(currentPath, "play_test_data" + System.DateTime.Now.Hour + "@" + System.DateTime.Now.Minute + "@" + System.DateTime.Now.Second + ".txt");

        using (FileStream fs = File.Create(complete))
        {
            for (int i = 0; i < 3; ++i)
            {
                byte[] info = new UTF8Encoding(true).GetBytes("Level " + i + " Play Time: " + level_time[i] + " Death: " + death_count[i] + "\n");
                fs.Write(info, 0, info.Length);
            }
        }
        
    }
}
