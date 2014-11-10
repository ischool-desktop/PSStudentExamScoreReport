using Aspose.Words;
using FISCA.Data;
using FISCA.Presentation;
using FISCA.Presentation.Controls;
using JHSchool.Data;
using JHSchool.Evaluation.Mapping;
using K12.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PSStudentSemesterScoreNotification
{
	public partial class MainForm : BaseForm
	{
		//主文件
		private Document _doc;

		//單頁範本
		private Document _template;

		//進入類型判斷( 學生 or 班級 )
		private EnterType _enterType;

		//學年度、學期、試別數字、試別名稱
		private string _schoolYear, _semester, _exam, _examName;

		private QueryHelper queryHelper = new QueryHelper();
		BackgroundWorker BGW = new BackgroundWorker();
		Dictionary<string, string> _subExamDic = new Dictionary<string, string>();

		//樣板設定
		string _temp1 = "低年級(1~2年級)";
		string _temp2 = "中高年級(3~6年級)";

		//等第對照
		private DegreeMapper _degreeMapper;

		internal static void Run(EnterType enterType)
		{
			new MainForm(enterType).ShowDialog();
		}

		private void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				_doc.Sections.RemoveAt(0);
				_doc.MailMerge.RemoveEmptyParagraphs = true;
				_doc.MailMerge.DeleteFields(); //刪除未使用的功能變數
				btn_Print.Enabled = true;
			}
			catch
			{

			}

			try
			{
				SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

				SaveFileDialog1.Filter = "Word (*.doc)|*.doc|所有檔案 (*.*)|*.*";
				SaveFileDialog1.FileName = "評量學習表現通知單" + string.Format("{0:MM_dd_yy_H_mm_ss}", DateTime.Now);

				if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					_doc.Save(SaveFileDialog1.FileName);
					Process.Start(SaveFileDialog1.FileName);
					MotherForm.SetStatusBarMessage("評量學習表現通知單,列印完成!!");
				}
				else
				{
					FISCA.Presentation.Controls.MsgBox.Show("檔案未儲存");
					return;
				}
			}
			catch
			{
				FISCA.Presentation.Controls.MsgBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
				MotherForm.SetStatusBarMessage("檔案儲存錯誤,請檢查檔案是否開啟中!!");
			}
		}

		internal List<JHStudentRecord> GetStudents()
		{
			if (_enterType == EnterType.Student)
				return JHStudent.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource);
			else
			{
				List<JHStudentRecord> list = new List<JHStudentRecord>();
				// 取得班級學生(一般和輟學)                
				foreach (JHClassRecord cla in JHClass.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
				{
					foreach (JHStudentRecord stud in cla.Students)
					{
						if (stud.Status == StudentRecord.StudentStatus.一般 || stud.Status == StudentRecord.StudentStatus.輟學)
							list.Add(stud);
					}
				}
				return list;
			}
		}

		public MainForm(EnterType enterType)
		{
			InitializeComponent();
			_enterType = enterType;
			InitializeSemester();
			InitializeGrade();
			InitializeExam();
			_degreeMapper = new DegreeMapper();
			BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
			BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);

			cboExam.Text = Properties.Settings.Default.LastExam;
		}

		private void InitializeGrade()
		{
			cboGrade.Items.Add(_temp1);
			cboGrade.Items.Add(_temp2);
			cboGrade.SelectedItem = cboGrade.Items[0];
		}

		private void InitializeExam()
		{
			string sql = "select * from exam";
			DataTable dt = queryHelper.Select(sql);


			foreach (DataRow row in dt.Rows)
			{
				string subId = row["id"] + "";
				string subName = row["exam_name"] + "";

				if (!_subExamDic.ContainsKey(subName))
				{
					_subExamDic.Add(subName, subId);
				}
				cboExam.Items.Add(row["exam_name"]);
			}

			//if (cboExam.Items.Count > 0)
				//cboExam.Text = cboExam.Items[0] + "" ;
		}

		private void InitializeSemester()
		{
			try
			{
				int schoolYear = int.Parse(School.DefaultSchoolYear);
				int semester = int.Parse(School.DefaultSemester);
				for (int i = -3; i <= 2; i++)
					cboSchoolYear.Items.Add(schoolYear + i);
				cboSemester.Items.Add(1);
				cboSemester.Items.Add(2);

				cboSchoolYear.Text = schoolYear.ToString();
				cboSemester.Text = semester.ToString();
			}
			catch (Exception ex)
			{
				MsgBox.Show("必須選擇為數字");
			}
		}

		private string ServerTime() //取得Server的時間
		{
			QueryHelper Sql = new QueryHelper();
			DataTable dtable = Sql.Select("select now()"); //取得時間
			DateTime dt = DateTime.Now;
			DateTime.TryParse("" + dtable.Rows[0][0], out dt); //Parse資料
			string ComputerSendTime = dt.ToString("yyyy/MM/dd"); //最後時間

			return ComputerSendTime;
		}

		private void btn_Print_Click(object sender, EventArgs e)
		{
			_schoolYear = cboSchoolYear.Text;
			_semester = cboSemester.Text;
			_examName = cboExam.Text;
			_exam = _subExamDic[cboExam.Text];

			Properties.Settings.Default.LastExam = _examName;
			Properties.Settings.Default.Save();

			if (cboGrade.Text == _temp1)
			{
				_template = new Document(new MemoryStream(Properties.Resources.評量學習表現通知單_國小_));
				BGW.RunWorkerAsync();
			}

			if (cboGrade.Text == _temp2)
			{
				_template = new Document(new MemoryStream(Properties.Resources.評量學習表現通知單_國小高年級_));
				BGW.RunWorkerAsync();
			}
			this.Close();
		}

		class mySubject
		{
			public string subject;
			public decimal score;
			public string domain;
			public decimal credit;
			public bool HasSceTake;
		}

		class StudentData
		{
			public string name;
			public string seat_no;
			public string class_name;
		}

		private void BGW_DoWork(object sender, DoWorkEventArgs e)
		{
			_doc = new Document();
			#region 資料建立
			string DatePrint = ServerTime();
			string schoolName = K12.Data.School.ChineseName;
			//List<JHStudentRecord> students = GetStudents();

			string str = string.Join(",", GetStudents().Select(x => x.ID));

			//科目、成績
			if (string.IsNullOrWhiteSpace(str))
			{
				MsgBox.Show("請確認是否有學生資料");
				return;
			}
			string textSql = "select * from list where name='努力程度對照表'";
			Dictionary<string, string> learnTextDic = new Dictionary<string, string>();
			DataTable dx = queryHelper.Select(textSql);
			foreach (DataRow row in dx.Rows)
			{
				string content = "" + row["content"];
				XmlElement xml = XmlHelper.LoadXml(content);

				foreach (XmlElement root in xml.SelectNodes("/Configurations/Configuration"))
				{
					XmlElement second = XmlHelper.LoadXml(root.InnerText);
					foreach (XmlElement elem in second.SelectNodes("//Effort"))
					{
						string name = elem.GetAttribute("Name");
						string score = elem.GetAttribute("Score");
						learnTextDic.Add(score, name);
					}
				}
			}
			string sql = "select sce_take.id,sc_attend.ref_student_id,student.name,student.seat_no,class.class_name,course.id as course_id,course.subject,course.domain,sce_take.ref_exam_id,xpath_string(sce_take.extension,'//Extension/Score') score,xpath_string(sce_take.extension,'//Extension/AssignmentScore') ascore,credit,xpath_string(exam_template.extension,'//Extension/ScorePercentage') percerntage from sc_attend";
			sql += " join student on sc_attend.ref_student_id=student.id";
			sql += " left join class on student.ref_class_id=class.id ";
			sql += " join course on sc_attend.ref_course_id=course.id";
			sql += " left join sce_take on sc_attend.id=sce_take.ref_sc_attend_id and sce_take.ref_exam_id=" + _exam;
			sql += " left join exam_template on course.ref_exam_template_id=exam_template.id";
			sql += " where student.id in (" + str + ") and course.school_year=" + _schoolYear + " and course.semester=" + _semester;

			DataTable dt = queryHelper.Select(sql);
			Dictionary<string, Dictionary<string, List<mySubject>>> dsds = new Dictionary<string, Dictionary<string, List<mySubject>>>();
			Dictionary<string, StudentData> data = new Dictionary<string, StudentData>();

			foreach (DataRow row in dt.Rows)
			{
				string id = "" + row["ref_student_id"];
				string domain = "" + row["domain"];
				if (!dsds.ContainsKey(id))
					dsds.Add(id, new Dictionary<string, List<mySubject>>());
				if (!dsds[id].ContainsKey(domain))
					dsds[id].Add(domain, new List<mySubject>());
				if (!data.ContainsKey(id))
				{
					data.Add(id, new StudentData()
					{
						name = "" + row["name"],
						seat_no = "" + row["seat_no"],
						class_name = "" + row["class_name"]
					});
				}

				decimal credit, score , ascore,scorePercentage,aScorePercentage;
				bool hasCredit = decimal.TryParse("" + row["credit"], out credit);
				bool hasScore = decimal.TryParse("" + row["score"], out score);
				bool hasAScore = decimal.TryParse("" + row["ascore"], out ascore);
				if (decimal.TryParse("" + row["percerntage"], out scorePercentage))
				{
					scorePercentage = scorePercentage / 100;
				}
				else 
				{
					scorePercentage = 0.5m;
				}
				aScorePercentage = (1 - scorePercentage);
				score = score * scorePercentage + ascore * aScorePercentage;

				if (!string.IsNullOrWhiteSpace(row["ref_exam_id"] + ""))
				{
					if (hasCredit && hasScore)
					{
						mySubject subj = new mySubject();
						subj.domain = row["domain"] + "";
						subj.subject = row["subject"] + "";
						subj.credit = credit;
						subj.score = score;
						subj.HasSceTake = true;
						dsds[id][domain].Add(subj);
					}
				}
				else
				{
					mySubject subj = new mySubject();
					subj.domain = row["domain"] + "";
					subj.subject = row["subject"] + "";
					subj.HasSceTake = false;
					dsds[id][domain].Add(subj);
				}
			}


			foreach (KeyValuePair<string, Dictionary<string, List<mySubject>>> item in dsds)
			{
				Document perPage = _template.Clone();
				Dictionary<string, string> mergeDic = new Dictionary<string, string>();
				mergeDic.Add("列印日期", DatePrint);
				mergeDic.Add("學校名稱", schoolName);
				mergeDic.Add("學年度", _schoolYear);
				mergeDic.Add("學期", _semester);
				mergeDic.Add("試別", _examName);
				mergeDic.Add("姓名", data.ContainsKey(item.Key) ? data[item.Key].name : "");
				mergeDic.Add("座號", data.ContainsKey(item.Key) ? data[item.Key].seat_no : "");
				mergeDic.Add("班級", data.ContainsKey(item.Key) ? data[item.Key].class_name : "");

				foreach (KeyValuePair<string, List<mySubject>> domain in item.Value)
				{
					decimal weithSum = 0, weightCount = 0, weightAvg = 0;

					foreach (mySubject subj in domain.Value)
					{
						if (subj.HasSceTake)
						{
							mergeDic.Add(domain.Key + "_" + subj.subject + "_等第", _degreeMapper.GetDegreeByScore(subj.score));
							mergeDic.Add(domain.Key + "_" + subj.subject + "_文字", GetTextByScore(Math.Round(subj.score, 0, MidpointRounding.AwayFromZero)));
							decimal d = subj.score * subj.credit;
							weithSum += d;
							weightCount += subj.credit;
						}
						else
						{
							if (!mergeDic.ContainsKey(domain.Key + "_" + subj.subject + "_等第"))
								mergeDic.Add(domain.Key + "_" + subj.subject + "_等第", "＊");
							continue;
						}
					}
					//加權平均
					if (weightCount > 0)
					{
						weightAvg = weithSum / weightCount;
						mergeDic.Add(domain.Key + "_平均_等第", _degreeMapper.GetDegreeByScore(weightAvg));
						mergeDic.Add(domain.Key + "_文字", GetTextByScore(Math.Round(weightAvg, 0, MidpointRounding.AwayFromZero)));
					}
					else
					{
						mergeDic.Add(domain.Key + "_平均_等第", "＊");
						mergeDic.Add(domain.Key + "_文字", "");
					}

				}

				if (!mergeDic.ContainsKey("語文_國語_等第"))
					mergeDic.Add("語文_國語_等第", "※");
				if (!mergeDic.ContainsKey("語文_英語_等第"))
					mergeDic.Add("語文_英語_等第", "※");
				if (!mergeDic.ContainsKey("語文_本土語_等第"))
					mergeDic.Add("語文_本土語_等第", "※");
				if (!mergeDic.ContainsKey("數學_平均_等第"))
					mergeDic.Add("數學_平均_等第", "※");
				if (!mergeDic.ContainsKey("社會_平均_等第"))
					mergeDic.Add("社會_平均_等第", "※");
				if (!mergeDic.ContainsKey("藝術與人文_平均_等第"))
					mergeDic.Add("藝術與人文_平均_等第", "※");
				if (!mergeDic.ContainsKey("自然與生活科技_平均_等第"))
					mergeDic.Add("自然與生活科技_平均_等第", "※");
				if (!mergeDic.ContainsKey("健康與體育_平均_等第"))
					mergeDic.Add("健康與體育_平均_等第", "※");
				if (!mergeDic.ContainsKey("綜合活動_平均_等第"))
					mergeDic.Add("綜合活動_平均_等第", "※");

				perPage.MailMerge.CleanupOptions = Aspose.Words.Reporting.MailMergeCleanupOptions.RemoveEmptyParagraphs;
				perPage.MailMerge.Execute(mergeDic.Keys.ToArray<string>(), mergeDic.Values.ToArray<object>());
				perPage.MailMerge.DeleteFields();

				_doc.Sections.Add(_doc.ImportNode(perPage.Sections[0], true));
			}

			#endregion
		}

		private void btn_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public string GetTextByScore(decimal score)
		{
			if (score >= 90)
				return "表現優異";
			if (score >= 80 && score < 90)
				return "表現良好";
			if (score >= 70 && score < 80)
				return "表現尚可";
			if (score >= 60 && score < 70)
				return "需再加油";

			return "有待改進";
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}
	}

	public enum EnterType
	{
		Student, Class
	}
}
