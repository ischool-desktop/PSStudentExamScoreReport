namespace PSStudentSemesterScoreNotification
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.btn_Print = new DevComponents.DotNetBar.ButtonX();
			this.btn_Exit = new DevComponents.DotNetBar.ButtonX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.cboGrade = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.cboExam = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.SuspendLayout();
			// 
			// cboSemester
			// 
			this.cboSemester.DisplayMember = "Text";
			this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSemester.FormattingEnabled = true;
			this.cboSemester.ItemHeight = 19;
			this.cboSemester.Location = new System.Drawing.Point(213, 14);
			this.cboSemester.Name = "cboSemester";
			this.cboSemester.Size = new System.Drawing.Size(54, 25);
			this.cboSemester.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cboSemester.TabIndex = 0;
			// 
			// cboSchoolYear
			// 
			this.cboSchoolYear.DisplayMember = "Text";
			this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboSchoolYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSchoolYear.FormattingEnabled = true;
			this.cboSchoolYear.ItemHeight = 19;
			this.cboSchoolYear.Location = new System.Drawing.Point(82, 14);
			this.cboSchoolYear.Name = "cboSchoolYear";
			this.cboSchoolYear.Size = new System.Drawing.Size(83, 25);
			this.cboSchoolYear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cboSchoolYear.TabIndex = 1;
			// 
			// labelX1
			// 
			this.labelX1.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Location = new System.Drawing.Point(17, 14);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(52, 23);
			this.labelX1.TabIndex = 2;
			this.labelX1.Text = "學年度";
			// 
			// labelX2
			// 
			this.labelX2.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Location = new System.Drawing.Point(171, 16);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(36, 23);
			this.labelX2.TabIndex = 3;
			this.labelX2.Text = "學期";
			// 
			// btn_Print
			// 
			this.btn_Print.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btn_Print.BackColor = System.Drawing.Color.Transparent;
			this.btn_Print.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btn_Print.Location = new System.Drawing.Point(132, 112);
			this.btn_Print.Name = "btn_Print";
			this.btn_Print.Size = new System.Drawing.Size(75, 23);
			this.btn_Print.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btn_Print.TabIndex = 4;
			this.btn_Print.Text = "列印";
			this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
			// 
			// btn_Exit
			// 
			this.btn_Exit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btn_Exit.BackColor = System.Drawing.Color.Transparent;
			this.btn_Exit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btn_Exit.Location = new System.Drawing.Point(213, 112);
			this.btn_Exit.Name = "btn_Exit";
			this.btn_Exit.Size = new System.Drawing.Size(75, 23);
			this.btn_Exit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btn_Exit.TabIndex = 5;
			this.btn_Exit.Text = "離開";
			this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
			// 
			// labelX3
			// 
			this.labelX3.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Location = new System.Drawing.Point(17, 47);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(52, 23);
			this.labelX3.TabIndex = 8;
			this.labelX3.Text = "試別";
			// 
			// labelX4
			// 
			this.labelX4.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.Class = "";
			this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX4.Location = new System.Drawing.Point(12, 76);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(67, 23);
			this.labelX4.TabIndex = 9;
			this.labelX4.Text = "年級設定";
			// 
			// cboGrade
			// 
			this.cboGrade.DisplayMember = "Text";
			this.cboGrade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGrade.FormattingEnabled = true;
			this.cboGrade.ItemHeight = 19;
			this.cboGrade.Location = new System.Drawing.Point(82, 76);
			this.cboGrade.Name = "cboGrade";
			this.cboGrade.Size = new System.Drawing.Size(185, 25);
			this.cboGrade.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cboGrade.TabIndex = 10;
			// 
			// cboExam
			// 
			this.cboExam.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PSStudentSemesterScoreNotification.Properties.Settings.Default, "LastExam", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.cboExam.DisplayMember = "Text";
			this.cboExam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboExam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboExam.FormattingEnabled = true;
			this.cboExam.ItemHeight = 19;
			this.cboExam.Location = new System.Drawing.Point(82, 45);
			this.cboExam.Name = "cboExam";
			this.cboExam.Size = new System.Drawing.Size(185, 25);
			this.cboExam.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cboExam.TabIndex = 7;
			this.cboExam.Text = global::PSStudentSemesterScoreNotification.Properties.Settings.Default.LastExam;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(293, 147);
			this.Controls.Add(this.cboGrade);
			this.Controls.Add(this.labelX4);
			this.Controls.Add(this.labelX3);
			this.Controls.Add(this.cboExam);
			this.Controls.Add(this.btn_Exit);
			this.Controls.Add(this.btn_Print);
			this.Controls.Add(this.labelX2);
			this.Controls.Add(this.labelX1);
			this.Controls.Add(this.cboSchoolYear);
			this.Controls.Add(this.cboSemester);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.Text = "個人評量成績單";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.ButtonX btn_Print;
		private DevComponents.DotNetBar.ButtonX btn_Exit;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cboExam;
		private DevComponents.DotNetBar.LabelX labelX3;
		private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cboGrade;
	}
}