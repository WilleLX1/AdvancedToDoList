using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using AdvancedToDoList.Controls;
using AdvancedToDoList.Models;
using AdvancedToDoList;

namespace AdvancedToDoList
{

public partial class MainForm : Form
{
    private TimelineControl timeline;
    private ProjectDataStore dataStore = new ProjectDataStore();
    private string dataFile = Path.Combine(Application.StartupPath, "projects.json");

    public MainForm()
    {
        InitializeComponent();
        Debug.WriteLine("[MainForm] Konstruktor startar.");

        InitializeTimeline();
        LoadData();
        RefreshTimelineData();

        Debug.WriteLine("[MainForm] Konstruktor färdig.");
    }

    private void InitializeTimeline()
    {
        Debug.WriteLine("[MainForm] InitializeTimeline: Skapar TimelineControl.");
        timeline = new TimelineControl
        {
            Dock = DockStyle.Fill
        };

        // --- Viktigt: prenumerera innan du sätter Projects! ---
        timeline.ProjectCreated += Timeline_ProjectCreated;
        timeline.ProjectDragged += Timeline_ProjectDragged;
        timeline.ProjectClicked += Timeline_ProjectClicked;
        
        this.Controls.Add(timeline);
        Debug.WriteLine("[MainForm] InitializeTimeline: Events kopplade.");
    }

    private void RefreshTimelineData()
    {
        Debug.WriteLine($"[MainForm] RefreshTimelineData: sätter timeline.Projects med {dataStore.AllProjects.Count} projekt");
        timeline.Projects = dataStore.AllProjects;
    }

    private void Timeline_ProjectCreated(object sender, RectangleCreatedEventArgs e)
    {
        Debug.WriteLine($"[MainForm] Timeline_ProjectCreated: mottagit start={e.Start:yyyy-MM-dd}, end={e.End:yyyy-MM-dd}, row={e.Row}");

        var newProj = new Project
        {
            Id = Guid.NewGuid(),
            Title = "Nytt projekt",
            Description = "",
            StartDate = e.Start,
            EndDate = e.End,
            Row = e.Row
        };

        Debug.WriteLine($"[MainForm] Skapar Project ID={newProj.Id}, Start={newProj.StartDate:yyyy-MM-dd}, End={newProj.EndDate:yyyy-MM-dd}, Row={newProj.Row}");
        dataStore.AllProjects.Add(newProj);
        Debug.WriteLine($"[MainForm] Totalt antal projekt i dataStore = {dataStore.AllProjects.Count}");

        ShowProjectDetailForm(newProj);
        RefreshTimelineData();
        SaveData();
    }

    private void Timeline_ProjectDragged(object sender, RectangleDraggedEventArgs e)
    {
        Debug.WriteLine($"[MainForm] Timeline_ProjectDragged: projekt \"{e.Project.Title}\" har flyttats till {e.Project.StartDate:yyyy-MM-dd}–{e.Project.EndDate:yyyy-MM-dd}");
        SaveData();
    }

    private void Timeline_ProjectClicked(object sender, RectangleClickedEventArgs e)
    {
        Debug.WriteLine($"[MainForm] Timeline_ProjectClicked: Klickade på projekt \"{e.Project.Title}\"");
        ShowProjectDetailForm(e.Project);
    }

    private void ShowProjectDetailForm(Project proj)
    {
        // Öppna ett nytt fönster/dialog med projektets inställningar
        var detailForm = new ProjectDetailForm(proj, dataStore);
        detailForm.ProjectChanged += DetailForm_ProjectChanged;

        detailForm.Show();
    }

    private void DetailForm_TaskChanged(object sender, EventArgs e)
    {
        SaveData();       // Spara ändringar till JSON
        RefreshTimelineData(); // Uppdatera tidslinjen
    }

    private void DetailForm_ProjectChanged(object sender, EventArgs e)
    {
        // Användaren har uppdaterat något i projektet (titel, beskrivning, uppgifter, bilagor etc)
        SaveData();
        RefreshTimelineData();
    }

    private void LoadData()
    {
        Debug.WriteLine($"[MainForm] LoadData: Försöker läsa fil \"{dataFile}\".");
        if (File.Exists(dataFile))
        {
            try
            {
                var json = File.ReadAllText(dataFile);
                dataStore = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectDataStore>(json)
                            ?? new ProjectDataStore();
                Debug.WriteLine($"[MainForm] LoadData: Laddade {dataStore.AllProjects.Count} projekt.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[MainForm] LoadData: FEL vid läsning av JSON: {ex.Message}");
                dataStore = new ProjectDataStore();
            }
        }
        else
        {
            Debug.WriteLine("[MainForm] LoadData: Ingen fil hittades – ny tom dataStore.");
            dataStore = new ProjectDataStore();
        }
    }

    private void SaveData()
    {
        Debug.WriteLine("[MainForm] SaveData: Sparar dataStore.");
        try
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dataStore, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(dataFile, json);
            Debug.WriteLine("[MainForm] SaveData: Sparning lyckades.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MainForm] SaveData: FEL vid sparning: {ex.Message}");
        }
    }
}
}
