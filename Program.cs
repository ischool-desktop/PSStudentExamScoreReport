﻿using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSStudentSemesterScoreNotification
{
	/// <summary>
	/// 個人評量成績單(國小)
	/// </summary>
	public static class Program
    {
		[MainMethod]
		public static void Main()
		{
			Catalog detail = RoleAclSource.Instance["學生"]["報表"];
			detail.Add(new ReportFeature("PSStudentExamScoreReport - {8C5BF7C9-7B87-4DD8-B3C5-841407E14F92}", "國小個人評量成績單"));

            MenuButton mb = FISCA.Presentation.MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["成績相關報表"]["國小個人評量成績單"];
            mb.Enable = false;
            mb.Click += delegate
            {
				new MainForm(EnterType.Student).ShowDialog();
            };

			MenuButton mb2 = FISCA.Presentation.MotherForm.RibbonBarItems["班級", "資料統計"]["報表"]["成績相關報表"]["國小個人評量成績單"];
            mb2.Enable = false;
            mb2.Click += delegate
            {
				new MainForm(EnterType.Class).ShowDialog();
            };

            //要選學生才可以執行
            K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
            {
				mb.Enable = (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0) && Framework.User.Acl["PSStudentExamScoreReport - {8C5BF7C9-7B87-4DD8-B3C5-841407E14F92}"].Executable;
                //mb.Enable = (JHSchool.Student.Instance.SelectedKeys.Count > 0);
            };

            //要選班級才可以執行
            K12.Presentation.NLDPanels.Class.SelectedSourceChanged += delegate
            {
				mb2.Enable = (K12.Presentation.NLDPanels.Class.SelectedSource.Count > 0) && Framework.User.Acl["PSStudentExamScoreReport - {8C5BF7C9-7B87-4DD8-B3C5-841407E14F92}"].Executable;
                //mb.Enable = (JHSchool.Student.Instance.SelectedKeys.Count > 0);
            };
		}
    }
}
