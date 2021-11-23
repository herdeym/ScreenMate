﻿using ScreenMate.Controller;
using ScreenMate.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenMate.View
{
    public partial class Form2 : Form
    {
        private OpenFileDialog ofd;
        public Form2()
        {
            ComponentConfigurator.GetComponentConfigurator().SuspendAllComponents();
            InitializeComponent();
            Configurations configuration = ConfigController.GetConfigController().Configurations;
            this.cpuCheckBox.Checked = configuration.EnabledComponents.Contains("Processor");
            this.ramCheckBox.Checked = configuration.EnabledComponents.Contains("Ram");
            this.disappearCheckBox.Checked = configuration.EnabledComponents.Contains("Disappear");
            this.idleCheckBox.Checked = configuration.EnabledComponents.Contains("Idle");
            this.followCheckBox.Checked = configuration.EnabledComponents.Contains("MouseFollow");
            this.sitCheckBox.Checked = configuration.EnabledComponents.Contains("SitOnWindow");

            this.ramTextBox.Text = configuration.RamThreshold.ToString();
            this.cpuTextBox.Text = configuration.ProcessorThreshold.ToString();
            this.idleTextBox.Text = configuration.IdleThresholdInSeconds.ToString();
            this.uploadFeedbackLabel.Visible = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            List<string> enabledComponents = new List<string>();
            if (this.cpuCheckBox.Checked)
                enabledComponents.Add("Processor");
            if (this.ramCheckBox.Checked)
                enabledComponents.Add("Ram");
            if (this.idleCheckBox.Checked)
                enabledComponents.Add("Idle");
            if (this.disappearCheckBox.Checked)
                enabledComponents.Add("Disappear");
            if (this.followCheckBox.Checked)
                enabledComponents.Add("MouseFollow");
            if (this.sitCheckBox.Checked)
                enabledComponents.Add("SitOnWindow");

            Configurations newConfig = new Configurations()
            {
                EnabledComponents = enabledComponents,
                RamThreshold = Convert.ToInt32(this.ramTextBox.Text),
                ProcessorThreshold = Convert.ToInt32(this.cpuTextBox.Text),
                IdleThresholdInSeconds = Convert.ToInt32(this.idleTextBox.Text),
            };

            ConfigController.GetConfigController().Configurations = newConfig;
            ConfigController.GetConfigController().SaveConfigurations();
            ComponentConfigurator.GetComponentConfigurator().ResumeAllComponents();
            this.Close();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            DialogResult result = ofd.ShowDialog();
            if(result == DialogResult.OK)
            {
                uploadFeedbackLabel.Text = "Successfull upload.";
                Image newTileset = Image.FromFile(ofd.FileName);
            }
            else
            {
                uploadFeedbackLabel.Text = "Upload canceled.";
            }

            uploadFeedbackLabel.Visible = true;

        }
    }
}
