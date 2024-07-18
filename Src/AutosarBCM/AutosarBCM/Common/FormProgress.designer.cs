using System.ComponentModel;
using System.Windows.Forms;

namespace AutosarBCM.Common
{
	partial class FormProgress
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelOperationDescription = new System.Windows.Forms.Label();
            this.labelTaskDescription = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(83, 79);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelOperationDescription
            // 
            this.labelOperationDescription.AutoEllipsis = true;
            this.labelOperationDescription.AutoSize = true;
            this.labelOperationDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelOperationDescription.Location = new System.Drawing.Point(13, 58);
            this.labelOperationDescription.Name = "labelOperationDescription";
            this.labelOperationDescription.Size = new System.Drawing.Size(10, 13);
            this.labelOperationDescription.TabIndex = 4;
            this.labelOperationDescription.Text = "-";
            // 
            // labelTaskDescription
            // 
            this.labelTaskDescription.AutoSize = true;
            this.labelTaskDescription.Location = new System.Drawing.Point(13, 9);
            this.labelTaskDescription.Name = "labelTaskDescription";
            this.labelTaskDescription.Size = new System.Drawing.Size(91, 13);
            this.labelTaskDescription.TabIndex = 5;
            this.labelTaskDescription.Text = "Completing task...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 28);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(218, 25);
            this.progressBar.TabIndex = 3;
            // 
            // msgPanel
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.labelTaskDescription);
            this.panel1.Controls.Add(this.labelOperationDescription);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 115);
            this.panel1.TabIndex = 7;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // FormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 115);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private Button buttonCancel;
		private Label labelOperationDescription;
		private Label labelTaskDescription;
		private ProgressBar progressBar;
		private Panel panel1;
        private BackgroundWorker backgroundWorker;
	}
}